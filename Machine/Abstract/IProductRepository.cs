using System.Linq;
using Machine.Models;

namespace Machine.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Drinks> Drinks { get; }
        IQueryable<Coins> Coins { get; }
        void SaveProduct(Drinks drink);
        void SaveCoin(Coins coin);
        Drinks DeleteDrink(int productID);
    }
}