using SiteLoaderPack;
using System;

namespace SiteLoaderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter start URL:");
            string startURL = Console.ReadLine();

            Console.WriteLine("Enter destination folder:");
            string folder = Console.ReadLine();

            int level;
            Console.WriteLine("Enter level:");
            int.TryParse(Console.ReadLine(), out level);

            SiteLoader siteLoader = new SiteLoader(folder);
            Console.WriteLine("Downloading pages...");
            siteLoader.DownloadPageAsync(startURL, 1).Wait();
            Console.WriteLine("Press any key...");
            Console.ReadLine();
        }
    }
}