using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Text;

namespace System.Web.Mvc.Html
{
    public static class ImageExtensions
    {
        public class ImageLinkClass
        {
            private string _action;
            private string _controller;
            private object _idObject;
            private string _imageURL;
            private string _alternateText;
            private bool _hasPermission;
            private string _linkHtmlAttributes;
            private string _imageHtmlAttributes;
            private string _confirmMessage = null;

            private object _routeValue;
            public ImageLinkClass(string action, string controller, object idObject, string imageURL, string alternateText, bool hasPermission = true, string confirmMessage = null, object routeValue = null)
            {
                _action = action;
                _controller = controller;
                _idObject = idObject;
                _imageURL = imageURL;
                _alternateText = alternateText;
                _hasPermission = hasPermission;
                _confirmMessage = confirmMessage;
                _routeValue = routeValue;
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

            public object IdObject
            {
                get { return _idObject; }
                set { _idObject = value; }
            }

            public string ImageURL
            {
                get { return _imageURL; }
                set { _imageURL = value; }
            }

            public string AlternateText
            {
                get { return _alternateText; }
                set { _alternateText = value; }
            }

            public bool HasPermission
            {
                get { return _hasPermission; }
                set { _hasPermission = value; }
            }

            public string LinkHtmlAttributes
            {
                get { return _linkHtmlAttributes; }
                set { _linkHtmlAttributes = value; }
            }

            public string ImageHtmlAttributes
            {
                get { return _imageHtmlAttributes; }
                set { _imageHtmlAttributes = value; }
            }

            public string ConfirmMessage
            {
                get { return _confirmMessage; }
                set { _confirmMessage = value; }
            }

            public object RouteValue
            {
                get { return _routeValue; }
                set { _routeValue = value; }
            }
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, string action, string controller, object routeValues, string imageURL, string alternateText, Boolean modalPopup, string jsFunction)
        {
            if (imageURL == null)
            {
                return null;
            }
            return ImageLink(html, action, controller, routeValues, imageURL, alternateText, null, null, null, modalPopup, jsFunction);
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, string action, string controller, object routeValues, string imageURL, string alternateText,Boolean modalPopup)
        {
            if (imageURL == null)
            {
                return null;
            }
            return ImageLink(html, action, controller, routeValues, imageURL, alternateText, null, null, null, modalPopup,"loadPopupModal");
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, string action, string controller, string imageURL, string alternateText)
        {
            if (imageURL == null)
            {
                return null;
            }
            return ImageLink(html, action, controller, null, imageURL, alternateText, null, null, null,false,"");
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, string action, string controller, object routeValues, string imageURL, string alternateText)
        {
            if (imageURL == null)
            {
                return null;
            }
            return ImageLink(html, action, controller, routeValues, imageURL, alternateText, null, null, null,false,"");
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, string action, string controller, object routeValues, string imageURL, string alternateText, object linkHtmlAttributes, object imageHtmlAttributes, string ConfirmMessage,Boolean modalpopup,String jsFunctionPopup="loadPopupModal")
        {
            // Create an instance of UrlHelper
            UrlHelper url = new UrlHelper(html.ViewContext.RequestContext);
            // Create image tag builder
            TagBuilder imageBuilder = new TagBuilder("img");
            // Add image attributes
            if (imageURL == null)
            {
                return null;
            }
            imageBuilder.MergeAttribute("src", imageURL);
            imageBuilder.MergeAttribute("alt", alternateText);
            imageBuilder.MergeAttribute("title", alternateText);
            imageBuilder.MergeAttribute("onclick", Convert.ToString((ConfirmMessage == null ? "return true;" : "return confirm('" + ConfirmMessage + "');")));
            imageBuilder.MergeAttributes(new RouteValueDictionary(imageHtmlAttributes));

            //TagBuilder scriptBuilder = new TagBuilder("script");
            //if (modalpopup)
            //{
            //    // Check include PopupDetail 
            //    scriptBuilder.MergeAttribute("language", "javascript");
            //    scriptBuilder.MergeAttribute("type", "text/javascript");
            //    StringBuilder scriptBuilderHTML = new StringBuilder();
            //    string car = ((char)34).ToString();
            //    scriptBuilderHTML.AppendFormat("if (typeof(popupDetailVer)=='undefined') alert(\"A PopupModal script reference is required in order to enable Ajax support in the ImageLink helper.\");");
            //    scriptBuilder.InnerHtml = scriptBuilderHTML.ToString();
            //}
            
            // Create link tag builder
            TagBuilder linkBuilder = new TagBuilder("a");
            // Add attributes
            if (modalpopup)
            {
                linkBuilder.MergeAttribute("href", "#");
                linkBuilder.MergeAttribute("onclick", jsFunctionPopup + "('" + url.Action(action, controller, routeValues) + "')");
            }
            else
                linkBuilder.MergeAttribute("href", url.Action(action, controller, routeValues));

            linkBuilder.InnerHtml = imageBuilder.ToString(TagRenderMode.SelfClosing);
            linkBuilder.MergeAttributes(new RouteValueDictionary(linkHtmlAttributes));
            
            // Render tag
            MvcHtmlString result = default(MvcHtmlString);
            result = new MvcHtmlString(linkBuilder.ToString(TagRenderMode.Normal));
            return result;
        }
    }
}
