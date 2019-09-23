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
    public class ShippersController : Controller
    {
        private readonly IShipperService shipperService;

        public ShippersController(IShipperService shipperService)
        {
            this.shipperService = shipperService;
        }

        // GET: Shippers
        public ActionResult Index()
        {
            var myShippers = shipperService.GetAllShippers();
            return View(myShippers);
        }

        // GET: Shippers/Details/5
        public ActionResult Details(int id)
        {
            var getShipperById = shipperService.GetShippersById(id);

            ShippersViewModel model = new ShippersViewModel();

            model.InjectFrom(getShipperById);

            return View(model);
        }

        // GET: Shippers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shippers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShippersViewModel model)
        {

            if (ModelState.IsValid)
            {
                Shippers shippers = new Shippers();
                shippers.InjectFrom(model);
                var addedShipper = shipperService.AddShipper(shippers);

                if (addedShipper == null)
                {
                    ModelState.AddModelError("Companyname", "Shipper name must be unique!");
                    return View(model);
                }
                
                    return RedirectToAction(nameof(Index));
                
            }
            else
            {
                return View(model);
            }
        }

        // GET: Shippers/Edit/5
        public ActionResult Edit(int id)
        {
            var shipperToUpdate = shipperService.GetShippersById(id);

            ShippersViewModel model = new ShippersViewModel();

            model.InjectFrom(shipperToUpdate);

            return View(model);
        }

        // POST: Shippers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ShippersViewModel model)
        {

            if (ModelState.IsValid)
            {
                Shippers shipperToUpdate = new Shippers();

                shipperToUpdate.InjectFrom(model);

                var updateNewShippper = shipperService.UpdateShipper(shipperToUpdate);

                if (updateNewShippper == null)
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

        // GET: Shippers/Delete/5
        public ActionResult Delete(int id)
        {
            var shipperToDelete = shipperService.GetShippersById(id);

            ShippersViewModel model = new ShippersViewModel();

            model.InjectFrom(shipperToDelete);

            return View(model);
        }

        // POST: Shippers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ShippersViewModel model)
        {

            Shippers deleteShipper = new Shippers();

            deleteShipper = shipperService.GetShippersById(id);

            model.InjectFrom(deleteShipper);

            shipperService.DeleteShipper(deleteShipper);

            return RedirectToAction(nameof(Index));
        }
    }
}