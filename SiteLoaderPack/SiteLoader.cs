using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SiteLoaderPack
{
    public class SiteLoader
    {
        private string _destinationFolder;
        private HtmlWeb _htmlWeb = new HtmlWeb();

        public SiteLoader(string destinationFolder)
        {
            _destinationFolder = destinationFolder;
        }

        public async Task DownloadPageAsync(string stringUri, int level = 0)
        {
            HtmlDocument doc = new HtmlDocument();

            Uri uri;
            try
            {
                uri = new Uri(stringUri);
            }
            catch
            {
                return;
            }
            if (uri.Scheme == "tel")
                return;

            doc = await _htmlWeb.LoadFromWebAsync(uri.ToString());

            string name = GetNameFromTitle(doc);
            File.WriteAllText(name, doc.Text);

            if (level-- <= 0)
                return;

            IEnumerable<string> anchors = GetAnchors(doc, uri);

            List<Task> tasksList = new List<Task>();
            foreach (var a in anchors)
            {
                Task task = new Task(async () => await DownloadPageAsync(a, level));
                tasksList.Add(task);
                task.Start();
            }
            Task[] tasks = tasksList.ToArray();
            Task.WaitAll(tasks);
        }

        public IEnumerable<string> GetAnchors(HtmlDocument doc, Uri uri)
        {
            HashSet<string> anchors = new HashSet<string>();
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                // Get the value of the HREF attribute
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                if (!hrefValue.StartsWith("mailto"))
                    anchors.Add(new Uri(new Uri(uri.AbsoluteUri), hrefValue).ToString());
            }
            return anchors;
        }

        private string GetNameFromTitle(HtmlDocument doc)
        {
            var title = doc.DocumentNode.Descendants("title").FirstOrDefault();
            string name = title?.InnerHtml;
            string forbiddenChars = "/?*:\"<>|" + @"\";
            foreach (char forbiddenChar in forbiddenChars)
                name = name.Replace(forbiddenChar, ' ');
            return _destinationFolder + "\\" + name + ".html";
        }
    }
}