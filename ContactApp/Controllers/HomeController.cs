using System;
using System.Data;
using System.Linq;
using ContactApp.Models;
using Dapper.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactApp.Controllers;

public class HomeController(ILogger<HomeController> logger, ISqlDatabase repository) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    /// <summary>
    ///     Method GetPagedList wraps the below sql with "OFFSET @__PageSkip rows fetch next @__PageSize rows only"
    ///     and performs a count query to return TotalCount of matching records
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public IActionResult Index(int page = 1, int pageSize = 25, string search = null)
    {
        var contacts = repository.GetPageList<ContactSummary>(
            page,
            pageSize,
            @"
                    SELECT
                        [C].[Id],
                        [C].[FullName],
                        [C].[Email],
                        [P].[Number] AS [Phone],
                        [C].[Title] + ' ' + [C].[Company] AS [TitleCompany]
                    FROM
                        [dbo].[Contact] [C]
                        OUTER APPLY (
		                    SELECT TOP 1
			                    [Number]
		                    FROM
			                    [dbo].[Phone] [AP]
		                    WHERE
			                    [AP].[ContactId] = [C].[Id]
		                    ORDER BY
			                    [AP].[Ordinal]
	                    ) [P]
                    WHERE
                        (
                        @search IS NULL
                        OR (
                            [C].[FullName] LIKE '%' + @search + '%'
                            OR [C].[Email] LIKE '%' + @search + '%'
                        )
                    )
                    ORDER BY
                        [FullName]
                    ", new { search });


        var model = new ContactIndexPageModel { Search = search, Contacts = contacts };

        return View(model);
    }

    /// <summary>
    ///     Two queries are performed, one using Get and the Primary Key id value
    ///     Second GetList query returns the contact phone numbers with an order by
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IActionResult Edit(Guid? id = null)
    {
        var contact = id != null ? repository.Get<Contact>(id) : new Contact { Id = Guid.NewGuid() };

        if (contact != null)
            contact.Phones =
                repository.GetList<Phone>("WHERE [ContactId] = @id ORDER BY [Ordinal]", new { id });

        return View(contact);
    }

    /// <summary>
    ///     Upsert methods do exist on primary key value and Insert if not found and Update if found
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Contact model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            // Always wrap multiple update/insert/delete with a transaction especially when doing Async calls
            // as this keeps the connection open and rolls back the transaction
            using var transaction = repository.GetTransaction(IsolationLevel.ReadCommitted);
            repository.Upsert(
                model,
                create => create.CreatedOn = DateTime.UtcNow,
                update => update.UpdatedOn = DateTime.UtcNow
            );

            var phoneList = model.Phones?.ToList() ?? [];

            phoneList.ForEach(phone =>
            {
                phone.ContactId = model.Id;
                phone.Ordinal = phoneList.IndexOf(phone) + 1;
            });

            repository.UpsertList(
                phoneList,
                create => create.CreatedOn = DateTime.UtcNow,
                update => update.UpdatedOn = DateTime.UtcNow
            );


            repository.Execute(
                @"DELETE FROM [dbo].[Phone] 
                            WHERE [ContactId] = @contactId 
                            AND [Id] NOT IN @ids",
                new { contactId = model.Id, ids = phoneList.Select(number => number.Id) });

            transaction.Complete();
        }
        catch (Exception ex)
        {
            // Contact table has a Timestamp column, a good error to see is to edit and save the same contact in two browsers
            // showing the optimistic concurrency support provided by the Timestamp Attribute
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }

        TempData["SuccessMessage"] = $"Contact {model.FullName} saved successfully";

        return RedirectToAction("Index");
    }

    public IActionResult AddNumber() => PartialView("EditorTemplates/Phone", new Phone { Id = Guid.NewGuid() });

    /// <summary>
    ///     Two Delete Method examples, first delete all phone numbers assigned to the contact
    ///     and secondly delete using the Primary Key field
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IActionResult Delete(Guid id)
    {
        using (var transaction = repository.GetTransaction(IsolationLevel.ReadCommitted))
        {
            repository.Delete<Phone>("WHERE [ContactId] = @id", new { id });

            repository.Delete<Contact>(id);

            transaction.Complete();
        }

        TempData["SuccessMessage"] = "Contact Deleted Successfully";

        return RedirectToAction("Index");
    }
}
