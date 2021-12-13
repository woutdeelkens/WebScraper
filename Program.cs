using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using System.IO;
using System.Text;

namespace WebScraperDemo
{  
    public class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("A -> Youtube");
            Console.WriteLine("B -> Indeed");
            Console.WriteLine("C -> Coolblue");
            Console.WriteLine("Keuze: ");
            String keuze = Console.ReadLine();
            
            
            

            if (keuze == "A" || keuze == "a")
            {
                Youtube();
            }

            else if (keuze == "B" || keuze == "b")
            {
                Indeed();
            }

            else if (keuze == "C" || keuze == "c")
            {
                Coolblue();
            }

            
        }

        

        static void Youtube()
        {
            String strFilePath = @"E:\School\WebScraperDemo\WebScraperDemo\WebScraperDemo\Data.csv";
            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();


            Console.WriteLine("Geef zoekterm: ");
            String zoekTerm = Console.ReadLine();
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.youtube.com/");
            var cookies = driver.FindElement(By.XPath("/html/body/ytd-app/ytd-consent-bump-v2-lightbox/tp-yt-paper-dialog/div[4]/div[2]/div[5]/div[2]/ytd-button-renderer[2]/a/tp-yt-paper-button"));
            cookies.Click();
            var element = driver.FindElement(By.XPath("/html/body/ytd-app/div/div/ytd-masthead/div[3]/div[2]/ytd-searchbox/form/div[1]/div[1]/input"));

            Thread.Sleep(500);

            element.SendKeys(zoekTerm);
            element.Submit();

            var datum = driver.FindElement(By.XPath("/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/div/ytd-toggle-button-renderer/a/tp-yt-paper-button"));
            datum.Click();

            var sorteren_op_datum = driver.FindElement(By.XPath("/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/iron-collapse/div/ytd-search-filter-group-renderer[5]/ytd-search-filter-renderer[2]/a/div/yt-formatted-string"));
            sorteren_op_datum.Click();

            Thread.Sleep(500);

            var video = driver.FindElements(By.Id("dismissible"));

            for (int i = 0; i < 5; i++)
            {
                var title = video[i].FindElement(By.Id("video-title"));
                var uploader = video[i].FindElement(By.Id("channel-info"));
                var weergaven = video[i].FindElement(By.Id("metadata-line"));
                Console.WriteLine("****** Video " + (i + 1) + " ******");
                Console.WriteLine("Titel: " + title.Text);
                Console.WriteLine("Uploader: " + uploader.Text);
                Console.WriteLine("Weergaven: " + weergaven.Text);
                Console.WriteLine("*********************");
                Console.WriteLine("\n");

                sbOutput.AppendLine(string.Join(strSeperator, "****** Video " + (i + 1) + " ******"));
                // Create and write the csv file
                File.WriteAllText(strFilePath, sbOutput.ToString());
                // To append more lines to the csv file
                // File.AppendAllText(strFilePath, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, title.Text));
                // Create and write the csv file
                File.WriteAllText(strFilePath, sbOutput.ToString());
                // To append more lines to the csv file
                // File.AppendAllText(strFilePath, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, uploader.Text));
                // Create and write the csv file
                File.WriteAllText(strFilePath, sbOutput.ToString());
                // To append more lines to the csv file
                // File.AppendAllText(strFilePath, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, weergaven.Text));
                // Create and write the csv file
                File.WriteAllText(strFilePath, sbOutput.ToString());
                // To append more lines to the csv file
                // File.AppendAllText(strFilePath, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, "\n"));
                // Create and write the csv file
                File.WriteAllText(strFilePath, sbOutput.ToString());
                // To append more lines to the csv file
                // File.AppendAllText(strFilePath, sbOutput.ToString());

            }



            Console.WriteLine("Scraping Data from youtube.com Done");


        }

        static void Indeed()
        {
            Console.WriteLine("Geef zoekterm: ");
            String zoekTerm = Console.ReadLine();
            Console.WriteLine("Hoeveel vacatures wil je te zien krijgen: ");
            String hoeveelheid = Console.ReadLine();
            int x = 0;
            Int32.TryParse(hoeveelheid, out x);
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://be.indeed.com/");
            var element = driver.FindElement(By.Id("text-input-what"));
            element.SendKeys(zoekTerm);
            element.Submit();

            var knop_datum = driver.FindElement(By.Id("filter-dateposted"));
            knop_datum.Click();
            var knop_3dagen = driver.FindElement(By.XPath("/html/body/table[1]/tbody/tr/td/div/div[2]/div/div[1]/ul/li[2]"));
            knop_3dagen.Click();
            Thread.Sleep(500);
            var sluit_popup = driver.FindElement(By.ClassName("popover-x-button-close"));
            sluit_popup.Click();
            var cookies = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            cookies.Click();
            var ads = driver.FindElements(By.ClassName("tapItem"));

            for (int i = 0; i < x; i++)
            {
                var title = ads[i].FindElement(By.ClassName("jobTitle"));
                var locatie = ads[i].FindElement(By.ClassName("companyLocation"));
                var bedrijf = ads[i].FindElement(By.ClassName("companyName"));
                Console.WriteLine("****** Vacature " + (i + 1) + " ******");
                Console.WriteLine("Titel: " + title.Text);
                Console.WriteLine("Bedrijf: " + bedrijf.Text);
                Console.WriteLine("Locatie: " + locatie.Text);
                Console.WriteLine("************************");
                Console.WriteLine("\n");
            }
            Console.WriteLine("Scraping Data from indeed.com Done");
        }

        static void Coolblue()
        {
            Console.WriteLine("Geef zoekterm: ");
            String zoekTerm = Console.ReadLine();
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.coolblue.be/nl");
            Thread.Sleep(500);
            var button = driver.FindElement(By.XPath("/html/body/div[7]/div/div[1]/div[2]/div/div[1]/div/div[1]/form/div/div/button"));
            button.Click();
            Thread.Sleep(500);
            var element = driver.FindElement(By.XPath("/html/body/header/div/div[3]/div[1]/form/div/div[1]/input"));
            element.SendKeys(zoekTerm);
            element.Submit();

            var product = driver.FindElements(By.ClassName("product-card"));

            for (int i = 0; i < 5; i++)
            {
                var naam = product[i].FindElement(By.ClassName("h3"));
                //var prijs = product[i].FindElement(By.ClassName("mr--2"));
                var prijs = product[i].FindElement(By.ClassName("js-sales-price"));
                var levertijd = product[i].FindElement(By.ClassName("icon-with-text__text"));
                Console.WriteLine("****** Product " + (i + 1) + " ******");
                Console.WriteLine("Naam: " + naam.Text);
                Console.WriteLine("Prijs (in EUR): " + prijs.Text);
                Console.WriteLine("Levertijd: " + levertijd.Text);
                Console.WriteLine("************************");
                Console.WriteLine("\n");
            }


            Console.WriteLine("Scraping Data from Coolblue Done");
        }
    }
}