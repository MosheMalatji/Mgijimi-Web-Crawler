using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mgijimi
{
    class Program
    {
        [DebuggerDisplay("{Model},{Price}")]

        public class Car
        {
            public string Model { get; set; }
            public string Price { get; set; }
            public string Link { get; set; }
            public string ImageURL { get; set; }
        }
        static void Main(string[] args)
        {
            InitiateCrawler();

            Console.ReadLine();
        }

        private static async Task InitiateCrawler()
        {
            //Page URL
            var url = "https://www.automobile.tn/neuf/bmw.3/";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmldoc = new HtmlDocument();
            htmldoc.LoadHtml(html);

            //A list to store model
            var cars = new List<Car>();

            var divs = htmldoc.DocumentNode.Descendants("div")
                 .Where(node => node.GetAttributeValue("class", "")
                 .Equals("vehicle-list__center-block")).ToList();

            foreach (var div in divs)
            {
                var car = new Car
                {
                    Model = div.Descendants("h2")?.FirstOrDefault().InnerText,
                    Price = div.Descendants("div")?.FirstOrDefault().InnerText,
                    ImageURL = div.Descendants("img")?.FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value,
                    Link = div.Descendants("a")?.FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value
                };

                cars.Add(car);
            }
          //Insert Breakpoint Here  
         }
    }
  
}
