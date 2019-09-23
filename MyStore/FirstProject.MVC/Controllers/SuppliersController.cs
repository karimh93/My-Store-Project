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
    public class SuppliersController : Controller
    {
        private readonly ISupplierService supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }
        // GET: Suppliers
        public ActionResult Index()
        {
            List<Suppliers> allSuppliers = supplierService.GetAllSuppliers();
            return View(allSuppliers);
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int id)
        {
            var getSupplierById = supplierService.GetSupplierById(id);

            SuppliersViewModel model = new SuppliersViewModel();

            model.InjectFrom(getSupplierById);

            return View(model);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SuppliersViewModel model)
        {
            if (ModelState.IsValid)
            {
                Suppliers suppliers = new Suppliers();
                suppliers.InjectFrom(model);
                var addedSupplier = supplierService.Add(suppliers);

                if (addedSupplier == null)
                {
                    ModelState.AddModelError("Companyname", "Company name must be unique!");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View(model);
            }

        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int id)
        {

            var supplierToUpdate = supplierService.GetSupplierById(id);

            SuppliersViewModel model = new SuppliersViewModel();

            model.InjectFrom(supplierToUpdate);

            return View(model);
        }

        // POST: Suppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SuppliersViewModel model)
        {

            if (ModelState.IsValid)
            {

                Suppliers existingSupplier = supplierService.GetSupplierById(id);
                existingSupplier.InjectFrom(model);
                var updateSupplier=supplierService.Update(existingSupplier);

                if (updateSupplier == null)
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




        // GET: Suppliers/Delete/5
        public ActionResult Delete(int id)
        {
            var supplierToDelete = supplierService.GetSupplierById(id);

            SuppliersViewModel model = new SuppliersViewModel();

            model.InjectFrom(supplierToDelete);

            return View(model);
        }

        // POST: Suppliers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SuppliersViewModel model)
        {
            Suppliers deleteSupplier = new Suppliers();

            deleteSupplier = supplierService.GetSupplierById(id);

            model.InjectFrom(deleteSupplier);

            supplierService.DeleteSupplier(deleteSupplier);

            return RedirectToAction(nameof(Index));
        }
    }
}