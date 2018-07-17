using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrbaneMreze
{
    public static class Auxiliaries
    {
        public static string[] ValidImageTypes = new string[] { "image/gif", "image/jpeg", "image/pjpeg", "image/png" };

        /// <summary>
        /// Given a potential fileName, replaces invalid characters with spaces.
        /// </summary>
        public static string RemoveInvalidFileCharacters(this string fileName)
        {
            string newFileName = fileName;
            foreach (char ch in Path.GetInvalidFileNameChars())
                newFileName.Replace(ch, ' ');

            newFileName = newFileName.Replace(' ', '_');

            return newFileName;
        }

        public static string Action(this UrlHelper helper, string actionName, string controllerName, RouteValueDictionary routeValues, string protocol, string hostName, bool defaultPort)
        {
            if (!defaultPort)
            {
                return helper.Action(actionName, controllerName, routeValues, protocol, hostName);
            }

            string port = "80";
            if (protocol.Equals("https", StringComparison.OrdinalIgnoreCase))
            {
                port = "443";
            }

            Uri requestUrl = helper.RequestContext.HttpContext.Request.Url;
            string defaultPortRequestUrl = Regex.Replace(requestUrl.ToString(), @"(?<=:)\d+?(?=/)", port);
            Uri url = new Uri(new Uri(defaultPortRequestUrl, UriKind.Absolute), requestUrl.PathAndQuery);

            var requestContext = GetRequestContext(url);
            var urlHelper = new UrlHelper(requestContext, helper.RouteCollection);

            var values = new RouteValueDictionary(routeValues);
            values.Add("controller", controllerName);
            values.Add("action", actionName);

            return urlHelper.RouteUrl(null, values, protocol, hostName);
        }

        public static string Action(this UrlHelper helper, string actionName, string controllerName, RouteValueDictionary routeValues, string protocol, bool defaultPort)
        {
            return Action(helper, actionName, controllerName, routeValues, protocol, null, defaultPort);
        }

        private static RequestContext GetRequestContext(Uri uri)
        {
            // Create a TextWriter with null stream as a backing stream 
            // which doesn't consume resources
            using (var writer = new StreamWriter(Stream.Null))
            {
                var request = new HttpRequest(
                    filename: string.Empty,
                    url: uri.ToString(),
                    queryString: string.IsNullOrEmpty(uri.Query) ? string.Empty : uri.Query.Substring(1));
                var response = new HttpResponse(writer);
                var httpContext = new HttpContext(request, response);
                var httpContextBase = new HttpContextWrapper(httpContext);
                return new RequestContext(httpContextBase, new RouteData());
            }
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        
        /* 
         * Returns the Curent user Id as a Guid. 
         */
        public static Guid GetUserId(IPrincipal user)
        {
            return Guid.Parse(user.Identity.GetUserId());
        }

        public enum UserRoles
        {
            Administrator,
            SuperAdmin
        }
    }

    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;

        public MemoryPostedFile(byte[] fileBytes, string fileName = null)
        {
            this.fileBytes = fileBytes;
            this.FileName = fileName;
        }

        public override int ContentLength => fileBytes.Length;

        public override string FileName { get; }
    }
}