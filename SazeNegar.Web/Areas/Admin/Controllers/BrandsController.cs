using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SazeNegar.Core.Models;
using SazeNegar.Infrastructure;
using SazeNegar.Infrastructure.Repositories;
using SazeNegar.Web.ViewModels;

namespace SazeNegar.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class BrandsController : Controller
    {
        private readonly BrandsRepository _repo;
        public BrandsController(BrandsRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/ArticleCategories
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Admin/ArticleCategories/Create
        public ActionResult Create()
        {
            BrandModelViewModel brandModelViewModel = new BrandModelViewModel();
            brandModelViewModel.BrandsList = _repo.GetAll();
            brandModelViewModel.ModelsList = _repo.GetModelsList();

            return View(brandModelViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BrandModelViewModel brandModelViewModel, int selectedModel)
        {
            if (ModelState.IsValid)
            {
                brandModelViewModel.Brands.CarModelId = selectedModel;
                _repo.Add(brandModelViewModel.Brands);
                return RedirectToAction("Index");
            }

            return View(brandModelViewModel);
        }

        // GET: Admin/ArticleCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BrandModelViewModel brandModelViewModel = new BrandModelViewModel();
               brandModelViewModel.Brands = _repo.Get(id.Value);
               brandModelViewModel.ModelsList = _repo.GetModelsList();
               ViewBag.ModelId = _repo.Get(id.Value).CarModelId;
            if (brandModelViewModel.Brands == null)
            {
                return HttpNotFound();
            }
            return View(brandModelViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BrandModelViewModel brandModelViewModel, int selectedModel)
        {
            if (ModelState.IsValid)
            {
                brandModelViewModel.Brands.CarModelId = selectedModel;
                _repo.UpdateModel(brandModelViewModel.Brands);
                _repo.Update(brandModelViewModel.Brands);
                return RedirectToAction("Index");
            }
            return View(brandModelViewModel);
        }

        // GET: Admin/ArticleCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brands brands = _repo.Get(id.Value);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return PartialView(brands);
        }

        // POST: Admin/ArticleCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
