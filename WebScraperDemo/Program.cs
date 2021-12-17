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
            
            string strSeperator = ",";
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strPath1 = System.IO.Path.GetDirectoryName(strExeFilePath);
            string strPath2 = System.IO.Path.GetDirectoryName(strPath1);
            string strPath3 = System.IO.Path.GetDirectoryName(strPath2);
            string strPath4 = System.IO.Path.GetDirectoryName(strPath3);
            string strPathCsv = System.IO.Path.Combine(strPath4, "Youtube.csv");
            
            StringBuilder sbOutput = new StringBuilder();


            Console.WriteLine("Geef zoekterm: ");
            String zoekTerm = Console.ReadLine();
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.youtube.com/");
            var cookies = driver.FindElement(By.XPath("/html/body/ytd-app/ytd-consent-bump-v2-lightbox/tp-yt-paper-dialog/div[4]/div[2]/div[5]/div[2]/ytd-button-renderer[2]/a/tp-yt-paper-button"));
            cookies.Click();
            Thread.Sleep(500);
            var element = driver.FindElement(By.XPath("/html/body/ytd-app/div/div/ytd-masthead/div[3]/div[2]/ytd-searchbox/form/div[1]/div[1]/input"));

            Thread.Sleep(500);

            element.SendKeys(zoekTerm);
            element.Submit();

            Thread.Sleep(500);

            var datum = driver.FindElement(By.XPath("/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/div/ytd-toggle-button-renderer/a/tp-yt-paper-button"));
            datum.Click();

            Thread.Sleep(500);

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
                File.WriteAllText(strPathCsv, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, title.Text));
                File.WriteAllText(strPathCsv, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, uploader.Text));
                File.WriteAllText(strPathCsv, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, weergaven.Text));
                File.WriteAllText(strPathCsv, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, "\n"));
                File.WriteAllText(strPathCsv, sbOutput.ToString());

            }

            Console.WriteLine("Scraping Data from youtube.com Done");

        }

        static void Indeed()
        {
            string strSeperator = ",";
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strPath1 = System.IO.Path.GetDirectoryName(strExeFilePath);
            string strPath2 = System.IO.Path.GetDirectoryName(strPath1);
            string strPath3 = System.IO.Path.GetDirectoryName(strPath2);
            string strPath4 = System.IO.Path.GetDirectoryName(strPath3);
            string strPathCsv = System.IO.Path.Combine(strPath4, "Indeed.csv");
            StringBuilder sbOutput = new StringBuilder();

            Console.WriteLine("Geef zoekterm: ");
            String zoekTerm = Console.ReadLine();
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://be.indeed.com/");
            var element = driver.FindElement(By.Id("text-input-what"));
            element.SendKeys(zoekTerm);
            element.Submit();

            
            Thread.Sleep(500);
            
            var advanced = driver.FindElement(By.XPath("/html/body/table[1]/tbody/tr/td/div/div[1]/form/a"));
            advanced.Click();

            Thread.Sleep(500);

            var cookies = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            cookies.Click();

            var date = driver.FindElement(By.XPath("/html/body/div[2]/form/fieldset[2]/div[2]/div[2]/select"));
            date.Click();

            var knop_3dagen = driver.FindElement(By.XPath("/html/body/div[2]/form/fieldset[2]/div[2]/div[2]/select/option[4]"));
            knop_3dagen.Click();

            var aantal = driver.FindElement(By.XPath("/html/body/div[2]/form/fieldset[2]/div[3]/div/div[2]/select"));
            aantal.Click();

            var toon_50 = driver.FindElement(By.XPath("/html/body/div[2]/form/fieldset[2]/div[3]/div/div[2]/select/option[4]"));
            toon_50.Click();

            var vacatures_zoeken = driver.FindElement(By.XPath("/html/body/div[2]/form/button"));
            vacatures_zoeken.Click();

            Thread.Sleep(500);

            var sluit_popup = driver.FindElement(By.ClassName("popover-x-button-close"));
            sluit_popup.Click();

            var ads = driver.FindElements(By.ClassName("job_seen_beacon"));
            int i = 0;
            foreach (IWebElement ad in ads)
            {
                var title = ad.FindElement(By.ClassName("jobTitle"));
                var locatie = ad.FindElement(By.ClassName("companyLocation"));
                var bedrijf = ad.FindElement(By.ClassName("companyName"));
                Console.WriteLine("****** Vacature " + (i + 1) + " ******");
                Console.WriteLine("Titel: " + title.Text);
                Console.WriteLine("Bedrijf: " + bedrijf.Text);
                Console.WriteLine("Locatie: " + locatie.Text);
                Console.WriteLine("************************");
                Console.WriteLine("\n");
                i++; 

                sbOutput.AppendLine(string.Join(strSeperator, "****** Vacature " + i + " ******"));
                File.WriteAllText(strPathCsv, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, title.Text));
                File.WriteAllText(strPathCsv, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, bedrijf.Text));
                File.WriteAllText(strPathCsv, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, locatie.Text));
                File.WriteAllText(strPathCsv, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, "\n"));
                File.WriteAllText(strPathCsv, sbOutput.ToString());
                i++;
            }
            Console.WriteLine("Scraping Data from indeed.com Done");
        }

        static void Coolblue()
        {
            string strSeperator = ",";
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strPath1 = System.IO.Path.GetDirectoryName(strExeFilePath);
            string strPath2 = System.IO.Path.GetDirectoryName(strPath1);
            string strPath3 = System.IO.Path.GetDirectoryName(strPath2);
            string strPath4 = System.IO.Path.GetDirectoryName(strPath3);
            string strPathCsv = System.IO.Path.Combine(strPath4, "Coolblue.csv");
            StringBuilder sbOutput = new StringBuilder();

            Console.WriteLine("Geef zoekterm: ");
            String zoekTerm = Console.ReadLine();
            Console.WriteLine("Hoeveel producten wil je te zien krijgen: ");
            String hoeveelheid = Console.ReadLine();
            int x = 0;
            Int32.TryParse(hoeveelheid, out x);
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

            for (int i = 0; i < x; i++)
            {
                var naam = product[i].FindElement(By.ClassName("h3"));
                var prijs = product[i].FindElement(By.ClassName("js-sales-price"));
                var levertijd = product[i].FindElement(By.ClassName("icon-with-text__text"));
                Console.WriteLine("****** Product " + (i + 1) + " ******");
                Console.WriteLine("Naam: " + naam.Text);
                Console.WriteLine("Prijs (in EUR): " + prijs.Text);
                Console.WriteLine("Levertijd: " + levertijd.Text);
                Console.WriteLine("************************");
                Console.WriteLine("\n");
                sbOutput.AppendLine(string.Join(strSeperator, "****** Artikel " + (i) + " ******"));
                File.WriteAllText(strPathCsv, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, naam.Text));
                File.WriteAllText(strPathCsv, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, prijs.Text));
                File.WriteAllText(strPathCsv, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, levertijd.Text));
                File.WriteAllText(strPathCsv, sbOutput.ToString());

                sbOutput.AppendLine(string.Join(strSeperator, "\n"));
                File.WriteAllText(strPathCsv, sbOutput.ToString());
            }

            Console.WriteLine("Scraping Data from Coolblue Done");
        }
    }
}
