using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstProject.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services;
using Omu.ValueInjecter;
using Store.Domain.Entities;

namespace FirstProject.MVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        // GET: Customers
        public ActionResult Index()
        {
            var allCustomers = customerService.GetAllCustomers();
            
            return View(allCustomers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            var getCustomerById = customerService.FindMyCustomerById(id);

            CustomerViewModel model = new CustomerViewModel();

            model.InjectFrom(getCustomerById);

            return View(model);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel model)
        {

            
            if (ModelState.IsValid)
            {
                Customers customerToAdd = new Customers();
              
                customerToAdd.InjectFrom(model);

                var addedCustomer = customerService.AddCustomer(customerToAdd);

                if (addedCustomer == null)
                {
                    ModelState.AddModelError("Companyname", "The Company name must be unique!");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }



        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            var customerToUpdate = customerService.FindMyCustomerById(id);

            CustomerViewModel model = new CustomerViewModel();

            model.InjectFrom(customerToUpdate);

            return View(model);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerViewModel model)
        {
           

                if (ModelState.IsValid)
                {
                Customers customersToUpdate = new Customers();

                customersToUpdate.InjectFrom(model);

                var updateNewCustomer = customerService.UpdateCustomer(customersToUpdate);

                if (updateNewCustomer== null)
                {
                    ModelState.AddModelError("CompanyName", "The Company Name must be unique!");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            var customerToDelete = customerService.FindMyCustomerById(id);

            CustomerViewModel model = new CustomerViewModel();

            model.InjectFrom(customerToDelete);

            return View(model);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CustomerViewModel model)
        {

            Customers deleteCustomer = new Customers();

            deleteCustomer = customerService.FindMyCustomerById(id);

            model.InjectFrom(deleteCustomer);

            customerService.DeleteCustomer(deleteCustomer);

            return RedirectToAction(nameof(Index));
        }
    }
}