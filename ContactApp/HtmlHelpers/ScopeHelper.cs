using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc.Rendering;

public static class ScopeHelpers
{
    extension(IHtmlHelper html)
    {
        public ModelScopeContext ModelScope(string modelName) =>
            new(html.ViewData.TemplateInfo, modelName);

        public ModelItemScopeContext ModelItemScope(string modelName, string specificId) =>
            new(html.ViewData.TemplateInfo, modelName, specificId);

        public HtmlString ModelItemScopeIndex(ModelItemScopeContext scopeContext) =>
            new(
                $@"<input type='hidden' name='{scopeContext.ModelName}.index' autocomplete='off' value='{scopeContext.ModelItemIndex}' />"
            );
    }


    public class ModelScopeContext : IDisposable
    {
        private readonly string _previousPrefix;
        private readonly TemplateInfo _templateInfo;

        public ModelScopeContext(TemplateInfo templateInfo, string htmlFieldPrefix)
        {
            _templateInfo = templateInfo;
            _previousPrefix = templateInfo.HtmlFieldPrefix;

            templateInfo.HtmlFieldPrefix = htmlFieldPrefix;
            ModelKey = htmlFieldPrefix;
        }

        public string ModelKey { get; set; }

        public void Dispose() => _templateInfo.HtmlFieldPrefix = _previousPrefix;
    }

    public class ModelItemScopeContext(TemplateInfo templateInfo, string collectionName, string collectionId)
        : ModelScopeContext(templateInfo, $"{collectionName}[{collectionId}]")
    {
        public string ModelItemIndex { get; } = collectionId;
        public string ModelName { get; } = collectionName;

        public string ModelItemKey => $"{ModelName}[{ModelItemIndex}]";
    }
}
