using System;
using Dapper.Database;

namespace ContactApp.Models;

public class ContactIndexPageModel
{
    public IPagedEnumerable<ContactSummary> Contacts { get; set; }
    public string Search { get; set; }
}

public class ContactSummary
{
    public virtual Guid Id { get; set; }

    public virtual string FullName { get; set; }
    public virtual string TitleCompany { get; set; }
    public virtual string Email { get; set; }

    public virtual string Phone { get; set; }
}
