﻿// See https://aka.ms/new-console-template for more information
using MongoDB.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using tibia.console.Crowler.Adapters;
using tibia.console.Crowler.Adapters.Interface;
using tibia.console.Tibia;
using tibia.console.Tibia.Comunity;

IWorldAdapter worldAdapter = new WorldAdapter();
var worldList = worldAdapter.List();

var dbClient = new MongoClient("mongodb://root:senha123@localhost:27017/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false");
world


Console.ReadKey();

var driver = new ChromeDriver();
driver.Navigate().GoToUrl("https://www.tibia.com/charactertrade/?subtopic=currentcharactertrades");

var worlds = new List<World>();
SelectElement selectFilterWorld = new SelectElement(driver.FindElement(By.Name("filter_world")));
for (int i = 1; i < selectFilterWorld.Options.Count; i++) {
   // worlds.Add(new World(selectFilterWorld.Options[i].Text));
}

selectFilterWorld.SelectByValue("Dibra");

var applyBtn = driver.FindElement(By.XPath("//*[@id=\"CharacterAuctionSearchBlock\"]/div[1]/div/div/input"));
applyBtn.Click();



Console.ReadKey();