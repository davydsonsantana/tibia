using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.Domain.Comunity;
using Tibia.MongoDB;

namespace Tibia.Domain.CharAuctions {
    public class CharAuction : Entity {

        public EBidStatus Status { get; private set; }

        public bool ItsDetailed { get; private set; }

        public string Name { get; private set; }

        public string DetailPageLink { get; private set; }

        public int Level { get; private set; }

        public EVocation Vocation { get; private set; }

        public EGender Gender { get; private set; }

        public EWorld World { get; private set; }

        public DateTime AuctionStart { get; private set; }

        public DateTime AuctionEnd { get; private set; }

        public int CurrentBid { get; private set; }

        public int HitPoints { get; private set; }

        public int Mana { get; private set; }

        public int Capacity { get; private set; }

        public int Speed { get; private set; }

        public int Blessings { get; private set; }

        public int Mounts { get; private set; }

        public int Outfits { get; private set; }

        public int Titles { get; private set; }

        public int AxeFighting { get; private set; }

        public int ClubFighting { get; private set; }

        public int DistanceFighting { get; private set; }

        public int Fishing { get; private set; }

        public int FistFighting { get; private set; }
        
        public int MagicLevel { get; private set; }

        public int Shielding { get; private set; }

        public int SwordFighting { get; private set; }

        public DateTime LastUpdateDate { get; private set; }

        public CharAuction(string name, string detailPageLink, int level, EVocation vocation, EGender gender, EWorld world, DateTime auctionStart, DateTime auctionEnd, int currentBid) {
            Name = name;
            DetailPageLink = detailPageLink;
            Level = level;
            Vocation = vocation;
            Gender = gender;
            World = world;
            AuctionStart = auctionStart;
            AuctionEnd = auctionEnd;
            CurrentBid = currentBid;
        }

        public void SetAsDetailed() {
            ItsDetailed = true;
        }

        public void SetHitpoints(int hitPoints) {
            HitPoints = hitPoints;
        }

        public void SetMana(int mana) {
            Mana = mana;
        }

        public void SetCapacity(int capacity) {
            Capacity = capacity;
        }

        public void SetSpeed(int speed) {
            Speed = speed;
        }

        public void SetBlessings(int blessings) {
            Blessings = blessings;
        }

        public void SetMounts(int mounts) {
            Mounts = mounts;
        }

        public void SetOutfits(int outfits) {
            Outfits = outfits;
        }

        public void SetTitles(int titles) {
            Titles = titles;
        }

        public void SetAxeFighting(int axeFighting) {
            AxeFighting = axeFighting;
        }

        public void SetClubFighting(int clubFighting) {
            ClubFighting = clubFighting;
        }

        public void SetDistanceFighting(int distanceFighting) {
            DistanceFighting = distanceFighting;
        }

        public void SetFishing(int fishing) {
            Fishing = fishing;
        }

        public void SetFistFighting(int fistFighting) {
            FistFighting = fistFighting;
        }

        public void SetMagicLevel(int magicLevel) {
            MagicLevel = magicLevel;
        }

        public void SetShielding(int shielding) {
            Shielding = shielding;
        }

        public void SetSwordFighting(int swordFighting) {
            SwordFighting = swordFighting;
        }

        public void SetLastUpdateDate() {
            LastUpdateDate = DateTime.UtcNow;
        }

        public void UpdateCurrentBid(int currentBid) {
            CurrentBid = currentBid;
        }

        public void SetAsOpen() {
            Status = EBidStatus.Open;
        }

        public void SetAsFinished() {
            Status = EBidStatus.Finished;
        }
    }
}