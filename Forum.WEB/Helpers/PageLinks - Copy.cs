//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web;
//using System.Web.Mvc;

//namespace Forum.WEB.Helpers
//{
//    public static class PagingHelpers
//    {
//        public static MvcHtmlString PageLinks(this HtmlHelper html,
//        PageInfo pageInfo, Func<int, string> pageUrl)
//        {
//            StringBuilder result = new StringBuilder();
//            if (pageInfo.TotalPages > 5)
//            {
//                int page = 0;
//                if(pageInfo.PageNumber == 1)
//                {
//                    page = pageInfo.TotalPages;
//                }
//                else
//                {
//                    page = pageInfo.PageNumber - 1;
//                }
//                string tag = Shevron(pageUrl(page), "glyphicon glyphicon-chevron-left");
//                result.Append(tag);
//            }
//            if (pageInfo.PageSize < pageInfo.TotalItems)
//            {
//                for (int i = 1; i <= pageInfo.TotalPages; i++)
//                {
//                    if (i == 1 || i == pageInfo.TotalPages || i < pageInfo.PageNumber + 3 && i > pageInfo.PageNumber - 3)
//                    {
//                        TagBuilder tag = new TagBuilder("a");
//                        if (i != pageInfo.PageNumber)
//                        {
//                            tag.MergeAttribute("href", pageUrl(i));
//                        }
//                        tag.InnerHtml = i.ToString();
//                        // если текущая страница, то выделяем ее,
//                        // например, добавляя класс
//                        if (i == pageInfo.PageNumber)
//                        {
//                            tag.AddCssClass("selected");
//                            tag.AddCssClass("btn-primary");
//                        }
//                        tag.AddCssClass("btn btn-default");
//                        result.Append(tag.ToString());
//                    }
//                    else
//                    {

//                    }
//                }
//            }
//            if (pageInfo.TotalPages > 5)
//            {
//                int page = 0;
//                if (pageInfo.PageNumber == pageInfo.TotalPages)
//                {
//                    page = 1;
//                }
//                else
//                {
//                    page = pageInfo.PageNumber + 1;
//                }
//                string tag = Shevron(pageUrl(page), "glyphicon glyphicon-chevron-right");
//                result.Append(tag);
//            }
//            return MvcHtmlString.Create(result.ToString());
//        }

//        public static string Shevron(string url, string cssClass)
//        {
//            TagBuilder a = new TagBuilder("a");
//            a.MergeAttribute("href", url);
//            a.AddCssClass("btn btn-default");
//            TagBuilder i = new TagBuilder("i");
//            i.AddCssClass(cssClass);
//            a.InnerHtml = i.ToString();
//            return a.ToString();
//        }
//    }
//}