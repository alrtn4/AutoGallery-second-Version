using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SazeNegar.Core.Models;
using SazeNegar.Infrastructure;
using SazeNegar.Infrastructure.Repositories;

namespace SazeNegar.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class CarClassController : Controller
    {
        private readonly CarClassRepository _repo;
        public CarClassController(CarClassRepository repo)
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarClass carClass)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(carClass);
                return RedirectToAction("Index");
            }

            return View(carClass);
        }

        // GET: Admin/ArticleCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarClass carClass = _repo.Get(id.Value);
            if (carClass == null)
            {
                return HttpNotFound();
            }
            return View(carClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarClass carClass)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(carClass);
                return RedirectToAction("Index");
            }
            return View(carClass);
        }

        // GET: Admin/ArticleCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarClass carClass = _repo.Get(id.Value);
            if (carClass == null)
            {
                return HttpNotFound();
            }
            return PartialView(carClass);
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
