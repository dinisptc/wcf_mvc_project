using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTIC_IT.Libraries;
using System.Web.UI;

namespace MvcTIC_IT.Helpers
{
    public static partial class HtmlHelpers
    {
        /// <summary>
        /// Shows a pager control - Creates a list of links that jump to each page
        /// </summary>
        /// <param name="page">The ViewPage instance this method executes on.</param>
        /// <param name="pagedList">A PagedList instance containing the data for the paged control</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="actionName">Name of the action on the controller.</param>
        public static void ShowPagerControl(this ViewPage page, IPagedList pagedList, string controllerName, string actionName)
        {
            HtmlTextWriter writer = new HtmlTextWriter(page.Response.Output);
            if (writer != null)
            {
                for (int pageNum = 1; pageNum <= pagedList.TotalPages; pageNum++)
                {
                    if (pageNum != pagedList.CurrentPage)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Href, "/" + controllerName + "/" + actionName + "/" + pageNum);
                        writer.AddAttribute(HtmlTextWriterAttribute.Alt, "Page " + pageNum);
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                    }

                    writer.AddAttribute(HtmlTextWriterAttribute.Class,
                                        pageNum == pagedList.CurrentPage ?
                                                            "pageLinkCurrent" :
                                                            "pageLink");

                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(pageNum);
                    writer.RenderEndTag();

                    if (pageNum != pagedList.CurrentPage)
                    {
                        writer.RenderEndTag();
                    }
                    writer.Write("&nbsp;");
                }

                writer.Write("(");
                writer.Write(pagedList.TotalItems);
                writer.Write(" items in all)");
            }
        }
    }
}