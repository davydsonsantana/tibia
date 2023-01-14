using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Tibia.Domain.Adapters;
using Tibia.Domain.Comunity;

namespace Tibia.Infrastructure.Adapters {
    public class WorldAdapter : IWorldAdapter
    {

        private ChromeDriver webDriver;
        private string worldListURI = "https://www.tibia.com/community/?subtopic=worlds";
        private string xpathTableTRWorlds = "//*[@id=\"worlds\"]/div[5]/div/div/div/table/tbody/tr/td/div[2]/table/tbody/tr[3]/td/div/table/tbody/tr";

        public WorldAdapter()
        {
            webDriver = new ChromeDriver();
        }

        public async Task<List<World>> List()
        {
            var worldList = new List<World>();

            webDriver.Navigate().GoToUrl(worldListURI);
            var trWorlds = webDriver.FindElements(By.XPath(xpathTableTRWorlds)).ToList();
            trWorlds.RemoveAt(0); // Remove table header   

            for (int i = 1; i < trWorlds.Count; i++)
            {
                var tdCollection = trWorlds[i].FindElements(By.TagName("td"));
                var worldName = tdCollection[0].FindElement(By.TagName("a")).Text;
                var location = GetLocation(tdCollection[2].Text);
                var pvpType = GetPvpType(tdCollection[3].Text);
                var battleEye = GetBattleEye(tdCollection[4]);
                var additionalInfo = tdCollection[5].Text;

                var world = new World(worldName, location, pvpType, battleEye, additionalInfo);
                worldList.Add(world);
            }

            return await Task.FromResult(worldList);
        }

        private ELocation GetLocation(string location)
        {
            return location switch
            {
                "Europe" => ELocation.Europe,
                "South America" => ELocation.SouthAmerica,
                "North America" => ELocation.NorthAmerica,
                _ => throw new Exception($"Location: {location} is not expected")
            };
        }

        private EPvpType GetPvpType(string pvpType)
        {
            return pvpType switch
            {
                "Open PvP" => EPvpType.OpenPVP,
                "Optional PvP" => EPvpType.OptionalPVP,
                "Retro Open PvP" => EPvpType.RetroOpenPVP,
                "Retro Hardcore PvP" => EPvpType.RetroHardcorePVP,
                "Hardcore PvP" => EPvpType.HardcorePVP,
                _ => throw new Exception($"PVP Type: {pvpType} is not expected")
            };
        }

        private EBattleEye GetBattleEye(IWebElement tdBattleEye)
        {
            try
            {
                var battleEyeImg = tdBattleEye.FindElement(By.TagName("img")).GetAttribute("src");

                if (battleEyeImg.Contains("icon_battleyeinitial.gif"))
                {
                    return EBattleEye.ProtectedInicial;
                }
                else if (battleEyeImg.Contains("icon_battleyeinitial.gif"))
                {
                    return EBattleEye.Protected;
                }
                else
                {
                    return EBattleEye.Unprotected;
                }

            }
            catch (NoSuchElementException)
            {
                return EBattleEye.Unprotected;
            }
        }
    }
}