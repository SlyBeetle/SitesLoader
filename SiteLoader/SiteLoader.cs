using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteLoader
{
    public class SiteLoader
    {
        public static IEnumerable<string> GetAnchors(string site)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = hw.Load(site);
            List<string> anchors = new List<string>();
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                // Get the value of the HREF attribute
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                anchors.Add(hrefValue);
            }
            return anchors;
        }
    }
}