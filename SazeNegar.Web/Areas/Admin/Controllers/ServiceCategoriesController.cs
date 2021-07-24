using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SazeNegar.Core.Models;
using SazeNegar.Infrastructure;
using SazeNegar.Infrastructure.Repositories;

namespace SazeNegar.Web.Areas.Admin.Controllers
{
    public class ServiceCategoriesController : Controller
    {
        private readonly ServiceCategoriesRepository _repo;
        public ServiceCategoriesController(ServiceCategoriesRepository repo)
        {
            _repo = repo;
        }

        // GET: Admin/ProductCategories
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Admin/ProductCategories/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/ProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceCategory serviceCategory)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(serviceCategory);
                return RedirectToAction("Index");
            }

            return View(serviceCategory);
        }

        // GET: Admin/ProductCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCategory serviceCategory = _repo.Get(id.Value);
            if (serviceCategory == null)
            {
                return HttpNotFound();
            }
            return PartialView(serviceCategory);
        }

        // POST: Admin/ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceCategory serviceCategory)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(serviceCategory);
                return RedirectToAction("Index");
            }
            return View(serviceCategory);
        }

        // GET: Admin/ProductCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCategory serviceCategory = _repo.Get(id.Value);
            if (serviceCategory == null)
            {
                return HttpNotFound();
            }
            return PartialView(serviceCategory);
        }

        // POST: Admin/ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
