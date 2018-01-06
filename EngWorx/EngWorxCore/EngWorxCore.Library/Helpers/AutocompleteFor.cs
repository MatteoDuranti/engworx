using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static class AutoCompleteExtensions
    {
        public static MvcHtmlString AutoCompleteFor<T, Tvalue, Tdisplay>(this HtmlHelper<T> helper,
                                                            Expression<Func<T, Tvalue>> exprValueField,
                                                            Expression<Func<T, Tdisplay>> exprDisplayField,
                                                            string action)
        {

            return BuildComponent(helper, exprValueField, exprDisplayField, action, typeof(T).Name, 1);
        }

        public static MvcHtmlString AutoCompleteFor<T, Tvalue, Tdisplay>(this HtmlHelper<T> helper,
                                                            Expression<Func<T, Tvalue>> exprValueField,
                                                            Expression<Func<T, Tdisplay>> exprDisplayField,
                                                            string action,
                                                            int minimumLength)
        {
            return BuildComponent(helper, exprValueField, exprDisplayField, action, typeof(T).Name, minimumLength);
        }

        private static string HeaderJscript(string tName , string displayField, string controller, string action, string valueField)
        {
            // bypass tname
            tName = string.Empty;
            StringBuilder strJscript = new StringBuilder();
            strJscript.Append("<script type=\"text/javascript\">");
            strJscript.AppendFormat("$('#{0}{1}').autocomplete({2}", tName, displayField.Replace(".","_"), "{");
            
            strJscript.AppendFormat("source:function(request,response)\n{0}", "{");
            strJscript.Append("$.ajax({");
            strJscript.AppendFormat("url:'/{0}/{1}'", controller, action);
            strJscript.Append(",dataType:'json'");
            strJscript.Append(",type:'POST'");      // Aggiunto Method Post
            //strJscript.AppendFormat(",data:{0}{1}:request.term{2}", "{", displayField.Replace(".", "_"), "}");
            strJscript.AppendFormat(",data:{0}query:request.term{1}", "{", "}");
            strJscript.Append(",success:function(data){");
            strJscript.AppendFormat(" if(data.length==0) {2}data.push({2}{0}:null,{1}:'Not Found.'{3});$('#{4}{5}').val(null);{3};", valueField.Replace(".", "_"), displayField.Replace(".", "_"), "{", "}", tName, valueField.Replace(".","_"));
            strJscript.Append("response ($.map(data,function(item){");
            strJscript.Append(" return {");

            return strJscript.ToString();
        }

        private static string BodyJscript(string tName, string displayField, string valueField, int minimumLength)
        {
            // bypass tName
            tName = string.Empty;
            StringBuilder strJscript = new StringBuilder();

            //strJscript.AppendFormat("label:item.{0} ", displayField.Replace(".", "_"));
            //strJscript.AppendFormat(",value:item.{0}", displayField.Replace(".", "_"));
            //strJscript.AppendFormat(",id:item.{0}", valueField.Replace(".", "_"));
            strJscript.Append("label:item.VALUE");
            strJscript.Append(",value:item.VALUE");
            strJscript.Append(",id:item.ID");
            strJscript.Append("};}));}});}");
            strJscript.AppendFormat(",minLength: {0}", minimumLength);
            strJscript.AppendFormat(",select: function(event, ui) {0}", "{");
            strJscript.Append("if(ui.item.id==null) return false;");
            strJscript.AppendFormat("$('#{0}{1}').val( ui.item.id);", tName, valueField.Replace(".","_"));
            strJscript.Append("}});");
            strJscript.Append("</script>");

            return strJscript.ToString();
        }

        private static string BodyJscript(string tName, IDictionary<string, string> displayFields, string valueField, int minimumLength)
        {
            StringBuilder strJscript = new StringBuilder();
            StringBuilder displayValues = new StringBuilder();

            int i = 0;
            foreach (var displayField in displayFields)
            {
                if (i == 0)
                    displayValues.AppendFormat("item.{1} + \" - \" + ", displayField.Key, displayField.Value);
                else if (i + 1 == displayFields.Count)
                    displayValues.AppendFormat("\"{0}:\" + item.{1}", displayField.Key, displayField.Value);
                else
                    displayValues.AppendFormat("\"{0}:\" + item.{1} + \" - \" + ", displayField.Key, displayField.Value);
                i++;
            }
            strJscript.AppendFormat("label:function(){1} if(item.{3}=='Not Found.') {1}return 'Not Found';{2} else {1} return {0}{2}{2}", displayValues.ToString(), "{", "}", displayFields.FirstOrDefault().Key);
            strJscript.AppendFormat(",value:item.{0}", displayFields.FirstOrDefault().Value);
            strJscript.AppendFormat(",id:item.{0}", valueField);
            strJscript.Append("};}));}});}");
            strJscript.AppendFormat(",minLength: {0}", minimumLength);
            strJscript.AppendFormat(",select: function(event, ui) {0}", "{");
            strJscript.Append("if(ui.item.id==null) return false;");
            strJscript.AppendFormat("$('#{0}{1}').val( ui.item.id);", tName, valueField);
            strJscript.Append("}});");
            strJscript.Append("</script>");

            return strJscript.ToString();
        }

        private static MvcHtmlString BuildComponent<T, Tvalue, Tdisplay>(this HtmlHelper<T> helper,
                                                            Expression<Func<T, Tvalue>> exprValueField,
                                                            Expression<Func<T, Tdisplay>> exprDisplayField,
                                                            string action,
                                                            string controller,
                                                            int minimumLength)
        {
            var valueField = ExpressionHelper.GetExpressionText(exprValueField);
            var displayField = ExpressionHelper.GetExpressionText(exprDisplayField);
            string stringText = null;
            IDictionary<string, string> displayValues = new Dictionary<string, string>();

            if (valueField == "")
            {
                throw new InvalidCastException("Invalid expression for value field. AutoCompleteFor Extensions not supported anonymous type in value field.");
            }
            if (displayField == "")
            {
                var fields = exprDisplayField.ToString().Split('(')[1].Split(',');

                foreach (var field in fields)
                {
                    var label = field.Split('=')[0].Replace(" ", "");
                    var val = field.Split('=')[1].Replace(" ", "").Replace(")", "");
                    displayValues.Add(label.Replace("_", " "), val.Substring(val.IndexOf(".") + 1, val.Length - (val.IndexOf(".") + 1)));
                }
                displayField = displayValues.FirstOrDefault().Key;
                stringText = helper.Editor(displayValues.FirstOrDefault().Key).ToHtmlString();
            }
            else
            {
                if (displayField == valueField)
                {
                    stringText = helper.Editor(valueField + "_value").ToHtmlString();
                    displayField = valueField + "_value";
                }
                else
                {
                    stringText = helper.EditorFor(exprDisplayField).ToHtmlString();
                }
            }
            var stringHidden = helper.HiddenFor(exprValueField).ToString();

            var tName = typeof(T).Name;

            // Commento il replace del name
            //stringHidden = stringHidden.Replace(valueField, string.Concat(tName, valueField));
            //stringText = stringText.Replace(displayField, string.Concat(tName, displayField));

            StringBuilder strJscript = new StringBuilder();
            strJscript.Append(HeaderJscript(tName, displayField, controller, action, valueField).ToString());
            if (displayValues.Count > 0)
            {
                strJscript.Append(BodyJscript(tName, displayValues, valueField, minimumLength).ToString());
            }
            else
            {
                strJscript.Append(BodyJscript(tName, displayField, valueField, minimumLength)).ToString();
            }
            return new MvcHtmlString(String.Concat(stringText, stringHidden, strJscript.ToString()));
        }

        public static MvcHtmlString AutoCompleteFor<T, Tvalue, Tdisplay>(this HtmlHelper<T> helper,
                                                            Expression<Func<T, Tvalue>> exprValueField,
                                                            Expression<Func<T, Tdisplay>> exprDisplayField,
                                                            string action,
                                                            string controller)
        {
            return BuildComponent(helper, exprValueField, exprDisplayField, action, controller, 1);
        }

        public static MvcHtmlString AutoCompleteFor<T, Tvalue, Tdisplay>(this HtmlHelper<T> helper,
                                                            Expression<Func<T, Tvalue>> exprValueField,
                                                            Expression<Func<T, Tdisplay>> exprDisplayField,
                                                            string action,
                                                            string controller,
                                                            int minimumLength)
        {
            return BuildComponent(helper, exprValueField, exprDisplayField, action, controller, minimumLength);
        }

        public static MvcHtmlString AutoCompleteFor<T, Tvalue, Tdisplay>(this HtmlHelper<T> helper,
                                                            Expression<Func<T, Tvalue>> exprValueField,
                                                            string action,
                                                            string controller,
                                                            int minimumLength)
        {
            return BuildComponent(helper, exprValueField, exprValueField, action, controller, minimumLength);
        }
    }
}
