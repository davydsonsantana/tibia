using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibia.console.Crowler.Adapters.Interface;
using tibia.console.Tibia.Comunity;

namespace tibia.console.Crowler.Adapters {
    internal class WorldAdapter : IWorldAdapter {

        private ChromeDriver webDriver;
        private string worldListURI = "https://www.tibia.com/community/?subtopic=worlds";
        private string xpathTableTRWorlds = "//*[@id=\"worlds\"]/div[5]/div/div/div/table/tbody/tr/td/div[2]/table/tbody/tr[3]/td/div/table/tbody/tr";

        public WorldAdapter() {
            webDriver = new ChromeDriver();
        }

        public List<World> List() {
            webDriver.Navigate().GoToUrl(worldListURI);
            var trWorlds = webDriver.FindElements(By.XPath(xpathTableTRWorlds)).ToList();

            var worlds = new List<World>();

            for (int i = 1; i < trWorlds.Count; i++) {
                var tdCollection = trWorlds[i].FindElements(By.TagName("td"));
                var html = tdCollection[0].GetAttribute("innerHTML");

                var worldName = tdCollection[0].FindElement(By.TagName("a")).Text;
                var location = tdCollection[2].Text;
                var pvpType = GetPvpType(tdCollection[3].Text);
                var battleEye = GetBattleEye(tdCollection[4]);
                var additionalInfo = tdCollection[5].Text;

                var world = new World(worldName, );
            }

            return null;
        }

        private EPvpType GetPvpType(string pvpType) {
            return pvpType switch {
                "Open PvP" => EPvpType.OpenPVP,
                "Optional PvP" => EPvpType.OptionalPVP,
                "Retro Open PvP" => EPvpType.RetroOpenPVP,
                "Retro Hardcore PvP" => EPvpType.RetroHardcorePVP,
                "Hardcore PvP" => EPvpType.HardcorePVP,
                _ => throw new Exception($"PVP Type: {pvpType} not expected")
            };
        }

        private EBattleEye GetBattleEye(IWebElement tdBattleEye) {
            var battleEyeImg = tdBattleEye.FindElement(By.TagName("img")).GetAttribute("src");
            if (battleEyeImg.Contains("icon_battleyeinitial.gif")) {
                return EBattleEye.ProtectedInicial;
            } else if (battleEyeImg.Contains("icon_battleyeinitial.gif")) {
                return EBattleEye.Protected;
            } else {
                return EBattleEye.Unprotected;
            }
        }
    }