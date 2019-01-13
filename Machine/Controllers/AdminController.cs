using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Machine.Concrete;
using Machine.Models;

namespace Machine.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private EFProductRepository repository = new EFProductRepository();
        public ViewResult Index()
        {
            return View(repository.Drinks);
        }
        public ViewResult Edit(int productId)
        {
            Drinks drink = repository.Drinks
            .FirstOrDefault(p => p.ProductID == productId);
            return View(drink);
        }
        [HttpPost]
        public ActionResult Edit(Drinks drink)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(drink);
                TempData["message"] = string.Format("{0} has been saved", drink.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(drink);
            }
        }
        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Drinks deletedProduct = repository.DeleteDrink(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}