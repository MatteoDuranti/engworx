using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Text;
using EngWorxCore.Library.Session;

namespace System.Web.Mvc.Html
{
    public static class BreadCrumbExtension
    {
        public static MvcHtmlString BreadCrumbNav(this HtmlHelper html,int TruncateStringFrom=10)
        {
            List<BreadCrumb> nav = SessionManagerLib.getBreadCrumb();
            StringBuilder Nav = new StringBuilder();
            int index=0;
            if (nav != null)
            {
                foreach (BreadCrumb breadcrumb in nav)
                {
                    index++;
                    if (index != nav.Count)
                    {
                        // Aggiungi anchor
                        if (breadcrumb.controller != null && breadcrumb.action != null)
                        {
                            Nav.Append(Html.LinkExtensions.ActionLink(html, Truncate(html, breadcrumb.description, TruncateStringFrom), "BreadCrumbNavigation", breadcrumb.controller, new { @pos = index }, new { @title = breadcrumb.description }).ToString());
                            Nav.Append(" >> ");
                        }
                        else
                        {
                            Nav.Append(Truncate(html, breadcrumb.description, TruncateStringFrom).ToString());
                            Nav.Append(" >> ");
                        }
                    }
                    else
                    {
                        Nav.AppendFormat("{0}", breadcrumb.description);
                    }

                }
            }
            return new MvcHtmlString(Nav.ToString());
        }
        public static MvcHtmlString BackButton(this HtmlHelper html,string ButtonText)
        {
            List<BreadCrumb> nav = SessionManagerLib.getBreadCrumb();
            int pos=nav.Count;
            string controller= nav[pos-1].controller;
            return html.ButtonLink("btnBack", ButtonText, "BreadCrumbNavigation", controller, new { @pos = pos -1});

        }
        /// <summary>
        /// This is a simple HTML Helper which truncates a string to a given length
        /// </summary>
        /// <param name="helper">HTML Helper being extended</param>
        /// <param name="input">Input string to truncate</param>
        /// <param name="length">Max length of the string</param>
        /// <returns></returns>
        private static string Truncate(this HtmlHelper helper, string input, int length)
        {
            if (input.Length <= length)
            {
                return input;
            }
            else
            {
                return input.Substring(0, length) + "...";
            }
        }
    }
}