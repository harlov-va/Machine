using Machine.Abstract;
using Machine.Models;
using Machine.Concrete;
using System.Linq;

namespace Machine.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Drinks> Drinks
        {
            get { return context.Drinks; }
        }
        public IQueryable<Coins> Coins
        {
            get { return context.Coins; }
        }
        public void SaveProduct(Drinks drink)
        {
            if (drink.ProductID == 0)
            {
                context.Drinks.Add(drink);
            }
            else
            {
                Drinks dbEntry = context.Drinks.Find(drink.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = drink.Name;
                    dbEntry.Description = drink.Description;
                    dbEntry.Price = drink.Price;
                    dbEntry.iCount = drink.iCount;
                    dbEntry.BThereIsDrink = drink.BThereIsDrink;
                }
            }
            context.SaveChanges();
        }
        public void SaveCoin(Coins coin)
        {
            if (coin.CoinID == 0)
            {
                context.Coins.Add(coin);
            }
            else
            {
                Coins dbEntry = context.Coins.Find(coin.CoinID);
                if (dbEntry != null)
                {
                    dbEntry.SNameCoin = coin.SNameCoin;
                    dbEntry.iCountCoin = coin.iCountCoin;
                    
                    dbEntry.BDontCoin = coin.BDontCoin;
                    dbEntry.SNameNumberCoin = coin.SNameNumberCoin;
                }
            }
            context.SaveChanges();
        }
        public Drinks DeleteDrink(int productID)
        {
            Drinks dbEntry = context.Drinks.Find(productID);
            if (dbEntry != null)
            {
                context.Drinks.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
        }
}