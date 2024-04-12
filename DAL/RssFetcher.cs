using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Net.Http;
using System.Xml.Linq;

namespace GoogleNewsApp.DAL
{
    public class RssFetcher
    {
        private const string RssFeedUrl = "http://news.google.com/news?pz=1&cf=all&ned=en_il&hl=en&output=rss";

        //Fetch Rss Feed Google News by httpClient request.
        public static async Task<string> FetchRssFeedAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(RssFeedUrl);
                return response;
            }
        }

        //Insert the data to the HttpCache
        public static async Task CacheRssFeedAsync()
        {
            Task.Run(async () =>
            {
                var rssFeed = await RssFetcher.FetchRssFeedAsync();
                XDocument rssFeedXDocument = XDocument.Parse(rssFeed);
                HttpRuntime.Cache.Insert("RssFeed", rssFeedXDocument, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero);
            }).Wait();
        }
    }
}