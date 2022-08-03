using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc.Rendering;

public static class ScopeHelpers
{
    public static ModelScopeContext ModelScope(this IHtmlHelper html, string modelName) =>
        new ModelScopeContext(html.ViewData.TemplateInfo, modelName);

    public static ModelItemScopeContext ModelItemScope(this IHtmlHelper html, string modelName, string specificId)
    {
        var modelItemIndex = specificId;

        return new ModelItemScopeContext(html.ViewData.TemplateInfo, modelName, modelItemIndex);
    }

    public static HtmlString ModelItemScopeIndex(this IHtmlHelper html, ModelItemScopeContext scopeContext) =>
        new HtmlString(
            $@"<input type='hidden' name='{scopeContext.ModelName}.index' autocomplete='off' value='{scopeContext.ModelItemIndex}' />"
        );


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

    public class ModelItemScopeContext : ModelScopeContext
    {
        public ModelItemScopeContext(TemplateInfo templateInfo, string collectionName, string collectionId)
            : base(templateInfo, $"{collectionName}[{collectionId}]")
        {
            ModelName = collectionName;
            ModelItemIndex = collectionId;
        }

        public string ModelItemIndex { get; }
        public string ModelName { get; }

        public string ModelItemKey => $"{ModelName}[{ModelItemIndex}]";
    }
}
