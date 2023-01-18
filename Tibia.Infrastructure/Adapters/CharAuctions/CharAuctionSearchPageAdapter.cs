using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tibia.Domain.CharAuctions;
using Tibia.Domain.Comunity;
using Tibia.Infrastructure.Adapters.CharAuctions.Contracts;

namespace Tibia.Infrastructure.Adapters.CharAuctions
{
    public class CharAuctionSearchPageAdapter : ICharAuctionSearchPageAdapter {

        private readonly ILogger _logger;
        private ChromeDriver webDriver;
        private string xpathTableTRAuctions = "//*[@id=\"currentcharactertrades\"]/div[5]/div/div/div[3]/table/tbody/tr/td/div[2]/table/tbody/tr";

        public CharAuctionSearchPageAdapter(ILogger<CharAuctionSearchPageAdapter> logger, ChromeDriver chromeDriver) {
            _logger = logger;
            webDriver = chromeDriver;
        }

        public (CharAuctionSearchPaginationStatus, List<CharAuction>) ListCurrentPage(CharAuctionFilter filter) {
            var auctionList = new List<CharAuction>();

            webDriver.Navigate().GoToUrl(filter.BuildURI());

            var trAuctions = webDriver.FindElements(By.XPath(xpathTableTRAuctions)).ToList();

            var totalResult = GetTotalResult(trAuctions[0]);

            trAuctions.RemoveAt(0); // Remove table header - pagination   
            trAuctions.RemoveAt(trAuctions.Count - 1); // Remove table footer - pagination   

            var paginationStatus = new CharAuctionSearchPaginationStatus(filter.CurrentPage, totalResult, trAuctions.Count);

            for (int i = 0; i < trAuctions.Count; i++) {
                try {
                    var name = GetName(trAuctions[i]);
                    var charAuctionDetailPageLink = GetCharAuctionDetailPageLink(trAuctions[i]);
                    var level = GetLevel(trAuctions[i]);
                    var vocation = GetVocation(trAuctions[i]);
                    var gender = GetGender(trAuctions[i]);
                    var world = GetWorld(trAuctions[i]);
                    var auctionStart = GetAuctionStart(trAuctions[i]);
                    var auctionEnd = GetAuctionEnd(trAuctions[i]);
                    var currentBid = GetCurrentBid(trAuctions[i]);                    

                    var auction = new CharAuction(name, charAuctionDetailPageLink, level, Enum.Parse<EVocation>(vocation.ToString()), Enum.Parse<EGender>(gender.ToString()), Enum.Parse<EWorld>(world.ToString()), auctionStart, auctionEnd, currentBid);
                    auctionList.Add(auction);

                } catch (Exception ex) {
                    _logger.LogError(ex.Message);
                }
            }

            return (paginationStatus, auctionList);
        }

        private int GetTotalResult(IWebElement element) {
            var totalString = element.FindElement(By.XPath(".//div[contains(@style,'float: right')]/b")).Text;
            var totalStripped = Regex.Replace(totalString, "[^0-9]", "");
            return Convert.ToInt32(totalStripped);
        }

        private string GetName(IWebElement element) {
            try {
                return element.FindElement(By.XPath(".//div[@class='AuctionCharacterName']/a")).Text;
            } catch {
                _logger.LogError("Can't locate 'Character Name' field at HTML");
                throw;
            }
        }

        private string GetCharAuctionDetailPageLink(IWebElement element) {
            try {
                return element.FindElement(By.XPath(".//div[@class='AuctionCharacterName']/a")).GetAttribute("href");
            } catch {
                _logger.LogError("Can't locate 'Character Auction Detail Page Link' on Name field at HTML");
                throw;
            }
        }

        private int GetLevel(IWebElement element) {
            try {
                var auctionHeader = element.FindElement(By.XPath(".//div[@class='AuctionHeader']")).Text;
                return Convert.ToInt32(new Regex(@"(?<=Level: )\d*").Match(auctionHeader).Value);
            } catch {
                _logger.LogError("Can't locate 'Character Level' on AuctionHeader field at HTML");
                throw;
            }
        }

        private EAuctionVocation GetVocation(IWebElement element) {
            try {
                var auctionHeader = element.FindElement(By.XPath(".//div[@class='AuctionHeader']")).Text;
                var vocationAsString = new Regex(@"(?<=Vocation: ).*?(?= [|])").Match(auctionHeader).Value;
                return ParseVocation(vocationAsString);
            } catch {
                _logger.LogError("Can't locate 'Character Level' on AuctionHeader field at HTML");
                throw;
            }
        }

        private EAuctionVocation ParseVocation(string vocation) {
            if (vocation.ToLower().Contains("sorcerer")) {
                return EAuctionVocation.Sorcerer;
            } else if (vocation.ToLower().Contains("druid")) {
                return EAuctionVocation.Sorcerer;
            } else if (vocation.ToLower().Contains("paladin")) {
                return EAuctionVocation.Paladin;
            } else if (vocation.ToLower().Contains("knight")) {
                return EAuctionVocation.Knight;
            } else if (vocation.ToLower().Contains("none")) {
                return EAuctionVocation.None;
            } else {
                throw new Exception($"Can't parse vocation {vocation}");
            }
        }

        private string GetGender(IWebElement element) {
            try {
                var auctionHeader = element.FindElement(By.XPath(".//div[@class='AuctionHeader']")).Text;
                return new Regex(@"(?<= | )(Male|Female)(?= [|] )", RegexOptions.IgnoreCase).Match(auctionHeader).Value;
            } catch {
                _logger.LogError("Can't locate 'Character Gender' on AuctionHeader field at HTML");
                throw;
            }
        }

        private EAuctionWorld GetWorld(IWebElement element) {
            try {
                var auctionHeader = element.FindElement(By.XPath(".//div[@class='AuctionHeader']")).Text;
                var worldAsString = new Regex(@"(?<=World: ).*").Match(auctionHeader).Value;
                return Enum.Parse<EAuctionWorld>(worldAsString);
            } catch {
                _logger.LogError("Can't locate 'Character Level' on AuctionHeader field at HTML");
                throw;
            }
        }

        private DateTime GetAuctionStart(IWebElement element) {
            string stringAuctionStart = "";
            try {
                stringAuctionStart = element.FindElement(By.XPath(".//div[@class='AuctionBodyBlock ShortAuctionData']/div[3]")).Text.Replace(" CET", "");
                var parsedAuctionStart = DateTime.ParseExact(stringAuctionStart, "MMM dd yyyy, HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                return parsedAuctionStart.AddHours(-1);
            } catch {
                _logger.LogError($"Problem to parte Auction Start Date: {stringAuctionStart}");
                throw;
            }
        }

        private DateTime GetAuctionEnd(IWebElement element) {
            try {
                var epochAuctionEndCET = Convert.ToInt32(element.FindElement(By.XPath(".//div[@class='AuctionTimer']")).GetAttribute("data-timestamp"));
                var varauctionEndUtc = DateTimeOffset.FromUnixTimeSeconds(epochAuctionEndCET);
                return varauctionEndUtc.AddHours(-1).DateTime;
            } catch {
                _logger.LogError("Can't locate 'Character Level' on AuctionHeader field at HTML");
                throw;
            }
        }

        private int GetCurrentBid(IWebElement element) {
            try {
                var currentBid = element.FindElement(By.XPath(".//div[@class='ShortAuctionDataBidRow']//b")).Text;
                return Convert.ToInt32(currentBid.Replace(",", ""));
            } catch {
                _logger.LogError("Can't locate 'Character Level' on AuctionHeader field at HTML");
                throw;
            }
        }
    }
}