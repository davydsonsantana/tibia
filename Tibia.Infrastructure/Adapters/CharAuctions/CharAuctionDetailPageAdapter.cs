using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Tibia.Domain.CharAuctions;
using Tibia.Infrastructure.Adapters.CharAuctions.Contracts;

namespace Tibia.Infrastructure.Adapters.CharAuctions {
    public class CharAuctionDetailPageAdapter : ICharAuctionDetailPageAdapter {

        private readonly ILogger _logger;
        private ChromeDriver webDriver;

        public CharAuctionDetailPageAdapter(ILogger<CharAuctionSearchPageAdapter> logger, ChromeDriver chromeDriver) {
            _logger = logger;
            webDriver = chromeDriver;
        }

        public CharAuction LoadChar(CharAuction character) {

            webDriver.Navigate().GoToUrl(character.DetailPageLink);

            character.SetHitpoints(GetHitPoints());
            character.SetMana(GetMana());
            character.SetCapacity(GetCapacity());
            character.SetSpeed(GetSpeed());
            character.SetBlessings(GetBlessing());
            character.SetMounts(GetMounts());
            character.SetOutfits(GetOutfits());
            character.SetTitles(GetTitles());
            character.SetAxeFighting(GetAxeFighting());
            character.SetClubFighting(GetClubFighting());
            character.SetDistanceFighting(GetDistanceFighting());
            character.SetFishing(GetFishing());
            character.SetFistFighting(GetFistFighting());
            character.SetMagicLevel(GetMagicLevel());
            character.SetShielding(GetShielding());
            character.SetSwordFighting(GetSwordFighting());
            character.UpdateCurrentBid(GetCurrentBid());
            character.SetLastUpdateDate();
            character.SetAsDetailed();

            if (IsBidFinished()) {
                character.SetAsFinished();
            } else {
                character.SetAsOpen();
            }

            return character;
        }

        private int GetHitPoints() {
            var hitpoints = webDriver.FindElement(By.XPath("//span[contains(text(),'Hit Points:')]/../div"))
                                .Text.Replace(",", "")
                                .Trim();
            return Convert.ToInt32(hitpoints);
        }

        private int GetMana() {
            var value = webDriver.FindElement(By.XPath("//span[contains(text(),'Mana:')]/../div"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetCapacity() {
            var value = webDriver.FindElement(By.XPath("//span[contains(text(),'Capacity:')]/../div"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetSpeed() {
            var value = webDriver.FindElement(By.XPath("//span[contains(text(),'Speed:')]/../div"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }
        private int GetBlessing() {
            var value = webDriver.FindElement(By.XPath("//span[contains(text(),'Blessings:')]/../div"))
                            .Text.Split('/').First()
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetMounts() {
            var value = webDriver.FindElement(By.XPath("//span[contains(text(),'Mounts:')]/../div"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetOutfits() {
            var value = webDriver.FindElement(By.XPath("//span[contains(text(),'Outfits:')]/../div"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetTitles() {
            var value = webDriver.FindElement(By.XPath("//span[contains(text(),'Titles:')]/../div"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetAxeFighting() {
            var value = webDriver.FindElement(By.XPath("//b[contains(text(),'Axe Fighting')]/../../td[@class='LevelColumn']"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetClubFighting() {
            var value = webDriver.FindElement(By.XPath("//b[contains(text(),'Club Fighting')]/../../td[@class='LevelColumn']"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetDistanceFighting() {
            var value = webDriver.FindElement(By.XPath("//b[contains(text(),'Distance Fighting')]/../../td[@class='LevelColumn']"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetFishing() {
            var value = webDriver.FindElement(By.XPath("//b[contains(text(),'Fishing')]/../../td[@class='LevelColumn']"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetFistFighting() {
            var value = webDriver.FindElement(By.XPath("//b[contains(text(),'Fist Fighting')]/../../td[@class='LevelColumn']"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetMagicLevel() {
            var value = webDriver.FindElement(By.XPath("//b[contains(text(),'Magic Level')]/../../td[@class='LevelColumn']"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetShielding() {
            var value = webDriver.FindElement(By.XPath("//b[contains(text(),'Shielding')]/../../td[@class='LevelColumn']"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetSwordFighting() {
            var value = webDriver.FindElement(By.XPath("//b[contains(text(),'Sword Fighting')]/../../td[@class='LevelColumn']"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private int GetCurrentBid() {
            var value = webDriver.FindElement(By.XPath("//div[@class='ShortAuctionDataBidRow']//b"))
                            .Text.Replace(",", "")
                            .Trim();
            return Convert.ToInt32(value);
        }

        private bool IsBidFinished() {
            var value = webDriver.FindElement(By.XPath("//div[@class='ShortAuctionDataBidRow']/div"))
                            .Text
                            .ToLower()
                            .Contains("winning bid");
            return value;
        }
    }
}