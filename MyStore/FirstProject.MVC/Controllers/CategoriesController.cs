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
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        
        
        // GET: Categories
        public ActionResult Index()
        {
            var allCategories = categoryService.GetAllCategories();

            return View(allCategories);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {

            var getCategoryById = categoryService.GetCategoryById(id);

            CategoriesViewModel model = new CategoriesViewModel();

            model.InjectFrom(getCategoryById);

            return View(model);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriesViewModel model)
        {
            if (ModelState.IsValid)
            {
                Categories categoriesToCreate = new Categories();

                categoriesToCreate.InjectFrom(model);

                var createNewCategory = categoryService.AddCategory(categoriesToCreate);

                if (createNewCategory == null)
                {
                    ModelState.AddModelError("Categoryname", "Category name must be Unique!");
                    return View(model);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View(model);
            }
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {

            var categoryToUpdate = categoryService.GetCategoryById(id);

            CategoriesViewModel model = new CategoriesViewModel();

            model.InjectFrom(categoryToUpdate);

            return View(model);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoriesViewModel model)
        {
            if (ModelState.IsValid)
            {
                Categories categoriesToUpdate = new Categories();

                categoriesToUpdate.InjectFrom(model);

                var updateNewCategory = categoryService.UpdateCategory(categoriesToUpdate);

                if (updateNewCategory == null)
                {
                    ModelState.AddModelError("Categoryname", "Category name must be Unique!");

                    return View(model);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View(model);
            }
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {
            var categoryToDelete = categoryService.GetCategoryById(id);

            CategoriesViewModel model = new CategoriesViewModel();

            model.InjectFrom(categoryToDelete);

            return View(model);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CategoriesViewModel model)
        {

            Categories deleteCategory = new Categories();

            deleteCategory = categoryService.GetCategoryById(id);

            model.InjectFrom(deleteCategory);

            categoryService.DeleteCategory(deleteCategory);

            return RedirectToAction(nameof(Index));

        }
    }
}