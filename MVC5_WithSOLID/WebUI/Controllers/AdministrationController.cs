using Contracts;
using Model;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AdministrationController : Controller
    {

        IRepositoryBase<Customer> _customers;
        IRepositoryBase<Product> _products;
        //Constructor
        public AdministrationController(IRepositoryBase<Customer> customers,IRepositoryBase<Product> products)
        {
            _customers = customers;
            _products = products;
        }

        // GET: Administration
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Lists products
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductsList()
        {
            // get all products and pass to view
            var model = _products.GetAll();
      
            return View(model);
        }

        public ActionResult CreateProduct()
        {
            
            return View(new Product());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreateProduct(Product u)
        {
            //Create new Product
            //var model = new Product();
           
            
            _products.Insert(u);
            _products.Save();
            //ViewBag.Message = "Success!";
           
            return View();
            
        }

        
        public ActionResult EditProduct(int id)
        {

            return View(_products.GetByID(id));
           
            //return View("Create", new ProductInput().InjectFrom(repo.Get(id)));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(int id,Product u)
        {

            u = _products.GetByID(id);
             //View(service.Get(id));
            _products.Update(u);
            _products.Save();
            
            //ViewBag.Message = "Success!";

            return View();
            
        }

       
        public ActionResult DeleteProduct(int id)
        {

            _products.Delete(_products.GetByID(id));
            _products.Save();
            return View("Index");

            //return View("Create", new ProductInput().InjectFrom(repo.Get(id)));
        }




    }
}