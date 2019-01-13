using Machine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Machine.Concrete;


namespace Machine.Controllers
{
    public class HomeController : Controller
    {
        
        private EFProductRepository repository = new EFProductRepository();
        //public HomeController(EFProductRepository productRepository)
        //{
        //    this.repository = productRepository;
        //}
        //private void PlusCoin(string ValueCoin)
        //{
        //    HttpContext.Application.Lock();
        //    int count = 0;

        //    if (HttpContext.Application[ValueCoin] != null)
        //        count = (int)HttpContext.Application[ValueCoin];

        //    count++;
        //    HttpContext.Application[ValueCoin] = count;

    
        //    HttpContext.Application.UnLock();
        //}
        //private void DefaultGlobalVariable()
        //{
        //    if ((int)(HttpContext.Application["1 руб."]) > 0) SaveCoinInBase("One", "1 руб.", 0);
        //    if ((int)(HttpContext.Application["2 руб."]) > 0) SaveCoinInBase("Two", "2 руб.", 0);
        //    if ((int)(HttpContext.Application["5 руб."]) > 0) SaveCoinInBase("Five", "5 руб.", 0);
        //    if ((int)(HttpContext.Application["10 руб."]) > 0) SaveCoinInBase("Ten", "10 руб.", 0);
        //    HttpContext.Application["1 руб."] = 0;
        //    HttpContext.Application["2 руб."] = 0;
        //    HttpContext.Application["5 руб."] = 0;
        //    HttpContext.Application["10 руб."] = 0;
        //}
        public void DefaultViewBag()
        {
            ViewBag.BThereisCoffee = true;
            ViewBag.BThereisTea = true;
            ViewBag.BThereisWater = true;
            ViewBag.BThereisAir = true;
            ViewBag.BThereisBeetlejuice = true;
            ViewBag.BThereisYupi = true;
        }
        private Dictionary<int, int> CalculateChange(int Money)
        {
            Dictionary<int, int> Dic = new Dictionary<int, int>();
            int[] FaceValues = { 10, 5, 2, 1 };
            foreach (int item in FaceValues)
            {
                if (Money / item == 0) continue;
                Dic.Add(item, Money / item);
                Money %= item;
                if (Money == 0) break;
            }
            return Dic;
        }
        private string StringToCoin(string StringNameButton)
        {
            string text = "";
            switch (StringNameButton)
            {
                case "1 руб.":text="One";
                    break;
                case "2 руб.":
                    text = "Two";
                    break;
                case "5 руб.":
                    text = "Five";
                    break;
                case "10 руб.":
                    text = "Ten";
                    break;
            }
            return (text);
        }
        private void SaveCoinInBase(string NameCoin,int ValueCountCoin)
        {
            Coins coin = repository.Coins.FirstOrDefault(d => d.SNameCoin == NameCoin);
            coin.iCountCoin += ValueCountCoin;
            repository.SaveCoin(coin);
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.SumMoney = 0;
            ViewBag.RestOfMoney = 0;
            DefaultViewBag();
            if ( Request.QueryString["id"] == "secret")
                return Redirect(Url.Action("Index", "Admin")); 
            else return View(repository.Coins);
        }
        [HttpPost]
        public ActionResult Index(string SumMoney, string clickonbutton,string clickbuttoncoin)
        {
            //ViewBag.Title = HttpContext.Application["1 руб."];
            //(HttpContext.Application["LicenseFile"] as string);
            int iSumInController = 0;
            int.TryParse(SumMoney, out iSumInController);
            //Sum += k;
            ViewBag.SumMoney = SumMoney;
            ViewBag.RestOfMoney = 0;
            DefaultViewBag();
            foreach (var d in repository.Drinks)
            {
                if ((d.Name == "Coffee") & (d.BThereIsDrink) & (iSumInController >= d.Price)) ViewBag.BThereisCoffee = false;

                if ((d.Name == "Tea") & (d.BThereIsDrink) & (iSumInController >= d.Price)) ViewBag.BThereisTea = false;

                if ((d.Name == "Water") & (d.BThereIsDrink) & (iSumInController >= d.Price)) ViewBag.BThereisWater = false;

                if ((d.Name == "Air") & (d.BThereIsDrink) & (iSumInController >= d.Price)) ViewBag.BThereisAir = false;

                if ((d.Name == "Beetlejuice") & (d.BThereIsDrink) & (iSumInController >= d.Price)) ViewBag.BThereisBeetlejuice = false;

                if ((d.Name == "Yupi") & (d.BThereIsDrink) & (iSumInController >= d.Price)) ViewBag.BThereisYupi = false;

            }
            if (!string.IsNullOrEmpty(clickbuttoncoin)) SaveCoinInBase(StringToCoin(clickbuttoncoin),1);
 
            if (!string.IsNullOrEmpty(clickonbutton))
            {
                Drinks Drink = repository.Drinks.FirstOrDefault(d => d.Name == clickonbutton);
                Drink.iCount--;
                repository.SaveProduct(Drink);
                clickonbutton = "";
                clickbuttoncoin = "";
                ViewBag.RestOfMoney = iSumInController - (int)Drink.Price;
                foreach (KeyValuePair<int, int> item in CalculateChange(iSumInController-(int)Drink.Price))
                {
                    switch (item.Key)
                    {
                        case 1:
                            SaveCoinInBase("One", -item.Value);
                            break;
                        case 2:
                            SaveCoinInBase("Two", -item.Value);
                            break;
                        case 5:
                            SaveCoinInBase("Five", -item.Value);
                            break;
                        case 10:
                            SaveCoinInBase("Ten", -item.Value);
                            break;
                    }
                }
                ViewBag.SumMoney = 0;
                DefaultViewBag();
                //return Redirect(Url.Action("Index", "Home"));
            }
            return View(repository.Coins);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(repository.Drinks);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}