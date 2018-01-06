using System.Runtime.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Web.Mvc.Html
{
    public static class ButtonExtensions
    {
        public class ButtonLinkClass
        {
            private string _buttonName;
            private string _buttonText;
            private string _action;
            private string _controller;
            private object _routeValues;
            private string _onClickFunctions;
            private string _confirmMessage = null;
            private object _idObject;
            private bool _hasPermission;

            public ButtonLinkClass(string buttonName, 
                string buttonText, 
                string action, 
                string controller, 
                string onClickFunctions, 
                object idObject,
                bool hasPermission = true,
                object routeValue = null,
                string confirmMessage = null)
            {
                _buttonName = buttonName;
                _buttonText = buttonText;
                _action = action;
                _controller = controller;
                _onClickFunctions = onClickFunctions;
                _idObject = idObject;
                _hasPermission = hasPermission;
                _routeValues = routeValue;
                _confirmMessage = confirmMessage;
            }

            public string ButtonName
            {
                get { return _buttonName; }
                set { _buttonName = value; }
            }

            public string ButtonText
            {
                get { return _buttonText; }
                set { _buttonText = value; }
            }

            public string Action
            {
                get { return _action; }
                set { _action = value; }
            }

            public string Controller
            {
                get { return _controller; }
                set { _controller = value; }
            }

            public string OnClickFunctions
            {
                get { return _onClickFunctions; }
                set { _onClickFunctions = value; }
            }

            public object IdObject
            {
                get { return _idObject; }
                set { _idObject = value; }
            }

            public bool HasPermission
            {
                get { return _hasPermission; }
                set { _hasPermission = value; }
            }

            public string ConfirmMessage
            {
                get { return _confirmMessage; }
                set { _confirmMessage = value; }
            }

            public object RouteValue
            {
                get { return _routeValues; }
                set { _routeValues = value; }
            }
        }

        public static MvcHtmlString ButtonLink(this HtmlHelper htmlHelper, string buttonName, string buttonText, string action, string controller, object routeValues = null, string onClickFunctions = null, string ConfirmMessage = null)
        {
            string link = htmlHelper.ActionLink("-", action, routeValues).ToString();
            link = new Regex("<a.*?href=[\"']([^\"']*)").Match(link).Groups[1].ToString();

            if (onClickFunctions == null) onClickFunctions = "";

            //link = String.Format("<input name=\"{0}\" id=\"{0}\" type=\"button\" onClick=\"{3};location.href='{2}'\" value=\"{1}\" />", buttonName, buttonText, link, onClickFunctions);

            //MvcHtmlString result = new MvcHtmlString(link);

            // Create an instance of UrlHelper
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            // Create image tag builder
            TagBuilder buttonBuilder = new TagBuilder("input");

            buttonBuilder.MergeAttribute("name", buttonName);
            buttonBuilder.MergeAttribute("id", buttonName);
            buttonBuilder.MergeAttribute("type", "button");
            buttonBuilder.MergeAttribute("value", buttonText);
            buttonBuilder.MergeAttribute("onclick", onClickFunctions +  ";location.href='" + link + "'");//Convert.ToString((ConfirmMessage == null ? "return true;" : "return confirm('" + ConfirmMessage + "');")) + link);

            // Render tag
            MvcHtmlString result = default(MvcHtmlString);
            result = new MvcHtmlString(buttonBuilder.ToString(TagRenderMode.Normal));
            return result;
        }
    }
}