using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using SazeNegar.Core.Models;
using SazeNegar.Infrastructure;
using SazeNegar.Infrastructure.Repositories;
using SazeNegar.Web.ViewModels;

namespace SazeNegar.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class CarModelController : Controller
    {
        private readonly CarModelRepository _repo;
        private readonly MyDbContext db; 
        public CarModelController(CarModelRepository repo, MyDbContext myDbContext)
        {
            _repo = repo;
            db = myDbContext;
        }
        // GET: Admin/ArticleCategories
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public ActionResult CarModelCarClass(int carModelId)
        {
            #region Create Role Permissions

            List<CarModelCarClassViewModel> CarModelCarCLasses = new List<CarModelCarClassViewModel>();
            foreach (var item in db.CarClasses.ToList())
            {
                CarModelCarClassViewModel carModelCarClassViewModel = new CarModelCarClassViewModel()
                {
                    CarClassID = item.Id,
                    CarClassTitle = item.Title,
                    Access = db.CarModelCarClasses
                        .Where(a => a.CarModelId == carModelId && a.CarClassId == item.Id).Any()
                };
                if(item.IsDeleted != true)
                    CarModelCarCLasses.Add(carModelCarClassViewModel);
            }

            #endregion

            ViewBag.CarModelName = db.CarModels.Find(carModelId).Model;
            ViewBag.CarModelID = carModelId;

            ViewBag.TreeItems = CarModelCarCLasses.OrderBy(a => a.CarClassTitle).Select(r => new TreeViewItemModel()
            {
                Text = r.CarClassTitle,
                Id = r.CarClassID.ToString(),
                Checked = r.Access
            }).ToList();

            return View(CarModelCarCLasses);
        }



        [HttpPost]
        public ActionResult CarModelCarClass(int carModelId, string[] selectedCarClasses)
        {

            if (selectedCarClasses == null)
            {
                return RedirectToAction("CarModelCarClass", new { carModelId });
            }

            List<int> selectCarClasses = new List<int>();
            selectCarClasses.AddRange(selectedCarClasses.Select(ro => Convert.ToInt32(ro)));

            foreach (var carClass in db.CarClasses.ToList())
            {
                #region Add carClass if is in selectedCarClasses and is not in CarModelCarClasses

                if (selectCarClasses.Contains(carClass.Id) && !db.CarModelCarClasses
                        .Where(a => a.CarModelId == carModelId && a.CarClassId == carClass.Id).Any())
                {
                    CarModelCarClass mCarClass = new CarModelCarClass()
                    {
                        CarModelId = carModelId,
                        CarClassId = carClass.Id
                    };
                    db.CarModelCarClasses.Add(mCarClass);
                    db.SaveChanges();
                }

                #endregion

                #region Delete CarModel if is in CarModelCarClass  and is not in selectCarClasses

                if (!selectCarClasses.Contains(carClass.Id) && db.CarModelCarClasses
                        .Where(a => a.CarModelId == carModelId && a.CarClassId == carClass.Id).Any())
                {
                    CarModelCarClass mCarClass = db.CarModelCarClasses
                        .Where(a => a.CarModelId == carModelId && a.CarClassId == carClass.Id).FirstOrDefault();
                    db.CarModelCarClasses.Remove(mCarClass);
                    db.SaveChanges();
                }

                #endregion
            }

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/CarModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                db.CarModels.Add(carModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carModel);
        }

        // GET: Admin/ArticleCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carModel = _repo.Get(id.Value);
            if (carModel == null)
            {
                return HttpNotFound();
            }
            return View(carModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(carModel);
                return RedirectToAction("Index");
            }
            return View(carModel);
        }

        // GET: Admin/ArticleCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carModel = _repo.Get(id.Value);
            if (carModel == null)
            {
                return HttpNotFound();
            }
            return PartialView(carModel);
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
