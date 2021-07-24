using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SazeNegar.Core.Models;
using SazeNegar.Core.Utility;
using SazeNegar.Infrastructure;
using SazeNegar.Infrastructure.Helpers;
using SazeNegar.Infrastructure.Repositories;
using SazeNegar.Web.ViewModels;

namespace SazeNegar.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class SliderCartController : Controller
    {
        private readonly CartRepository _repo;
        public SliderCartController(CartRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/StaticContentDetails
        public ActionResult Index()
        {
            var content = _repo.GetCarts();
            content = content.OrderByDescending(c => c.Id).ToList();
            return View(content);
        }
        // GET: Admin/StaticContentDetails/Create
        public ActionResult Create()
        {
            //ViewBag.StaticContentTypeId = (int)StaticContentTypes.Slider;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cart cart, HttpPostedFileBase sliderCartImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (sliderCartImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(sliderCartImage.FileName);
                    sliderCartImage.SaveAs(Server.MapPath("~/Files/SliderCart/Temp/" + newFileName));

                    // Resizing Image
                    ImageResizer image = new ImageResizer();
                    image = new ImageResizer(1000, 1000, true);

                    image.Resize(Server.MapPath("~/Files/SliderCart/Temp/" + newFileName),
                        Server.MapPath("~/Files/SliderCart/Image/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("~/Files/SliderCart/Temp/" + newFileName));

                    cart.Image = newFileName;
                }
                #endregion
                _repo.Add(cart);

                return RedirectToAction("Index");
            }
            return View(cart);
        }

        //GET: Admin/StaticContentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = _repo.GetCart(id.Value);
            if (cart == null)
            {
                return HttpNotFound();
            }
            //ViewBag.cart = new SelectList(_repo.GetStaticContentTypes(), "Id", "Name", staticContentDetail.StaticContentTypeId);
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cart cart, HttpPostedFileBase sliderCartImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (sliderCartImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/SliderCart/Image/" + cart.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/SliderCart/Image/" + cart.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(sliderCartImage.FileName);
                    sliderCartImage.SaveAs(Server.MapPath("/Files/SliderCart/Temp/" + newFileName));

                    // Resizing Image
                    ImageResizer image = new ImageResizer();
                    image = new ImageResizer(1000, 1000, true);

                    image.Resize(Server.MapPath("/Files/SliderCart/Temp/" + newFileName),
                        Server.MapPath("/Files/SliderCart/Image/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/SliderCart/Temp/" + newFileName));

                    cart.Image = newFileName;
                }
                #endregion

                _repo.Update(cart);
                return RedirectToAction("Index");
            }
            //ViewBag.StaticContentTypeId = new SelectList(_repo.GetStaticContentTypes(), "Id", "Name", staticContentDetail.StaticContentTypeId);
            return View(cart);
        }
        // get: admin/SliderCart/delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = _repo.GetCart(id.Value);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return PartialView(cart);
        }

        // post: admin/SliderCart/delete/5
        [HttpPost, ActionName("delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cart = _repo.GetCart(id);

            #region delete cart image
            if (cart.Image != null)
            {
                if (System.IO.File.Exists(Server.MapPath("/Files/SliderCart/Image/" + cart.Image)))
                    System.IO.File.Delete(Server.MapPath("/Files/SliderCart/Image/" + cart.Image));
            }
            #endregion

            _repo.Delete(id);
            return RedirectToAction("index");
        }
    }
}
