using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    public static class TextBoxForExtensions
    {
        public enum TextCase { none, toUpper, toLower };

        public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                Expression<Func<TModel, TProperty>> expression,
                                                object htmlAttributes,
                                                TextCase  textcase)
        {
            var values = new RouteValueDictionary(htmlAttributes);
            switch (textcase)
            {
                case TextCase.toLower:
                    values.Add("case", "lower");
                    break;
                case TextCase.toUpper:
                    values.Add("case", "upper");
                    break;
                default:
                    break;
            }
            return htmlHelper.TextBoxFor<TModel, TProperty>(expression, values);
        }
    }
}
