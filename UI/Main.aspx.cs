using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoogleNewsApp.DAL;
using System.Xml.Linq;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;


namespace GoogleNewsApp.UI
{
    public partial class Main : System.Web.UI.Page
    {
        
        //Load all topics from the cache
        public void LoadTopics()
        {
            var cachedRssFeed = HttpRuntime.Cache.Get("RssFeed");
            // Bind the data to Repeater control
            if (cachedRssFeed != null && cachedRssFeed is XDocument)
            {
                var rssFeed = cachedRssFeed as XDocument;
                var newsItems = rssFeed.Descendants("item")
                                             .Select(item => new
                                             {
                                                 Title = item.Element("title")?.Value,
                                                 Summary = item.Element("description")?.Value,
                                                 Link = item.Element("link")?.Value
                                             });

                // Bind data to Repeater control
                rptNews.DataSource = newsItems;
                rptNews.DataBind();
            }
        }
        //Return the neccasary data required to the body post from the HttpCache
        public List<PostData> LoadBodyTopic(string title)
        {
            var cachedRssFeed = HttpRuntime.Cache.Get("RssFeed");
            List<PostData> newsItems = new List<PostData>();
            // Bind the data to Repeater control
            if (cachedRssFeed != null && cachedRssFeed is XDocument)
            {
                var rssFeed = cachedRssFeed as XDocument;
                newsItems = rssFeed.Descendants("item")
                    .Where(item => (string)item.Element("title") == title)
                                             .Select(item => new PostData
                                             {
                                                 Title = (string)item.Element("title"),
                                                 Summary = (string)item.Element("description"),
                                                 Link = (string)item.Element("link"),
                                                 Source = (string)item.Element("source")?.Attribute("url").Value
                                             }).ToList();

            }
            return newsItems;
        }


        protected async void Page_Load(object sender, EventArgs e)
        {
            await RssFetcher.CacheRssFeedAsync();
            LoadTopics();
        }


        //Return to the Custom.js file the neccasary data of the body topic
        [WebMethod]
        public static string ServerSideMethod(string parameter)
        {
            Main mainInstance = new Main();
            List<PostData> newsItems = mainInstance.LoadBodyTopic(parameter);
            string json = JsonConvert.SerializeObject(newsItems); // Serialize to JSON
            return json;
        }
    }
}
