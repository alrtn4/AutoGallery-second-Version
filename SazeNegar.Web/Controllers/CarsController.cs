using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SazeNegar.Core.Models;
using SazeNegar.Core.Utility;
using SazeNegar.Infrastructure.Repositories;
using SazeNegar.Web.ViewModels;

namespace SazeNegar.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsRepository _carsRepo;
        private readonly StaticContentDetailsRepository _contentRepo;

        public CarsController(CarsRepository carsRepo, StaticContentDetailsRepository contentRepo)
        {
            _carsRepo = carsRepo;
            _contentRepo = contentRepo;
        }

        // GET: Blog
        //[Route("Blog")]
        //[Route("Blog/{id}/{title}")]
        public ActionResult Index(int pageNumber = 1, string searchString = null)
        {
            var cars = _carsRepo.GetAll();
            var vm = new List<Cars>();
            var take = 3;
            var skip = pageNumber * take - take;
            var count = 0;
            //foreach (var item in cars)
            //{
            //    vm.Add(item);
            //}
            vm = _carsRepo.GetCarsList(skip, take, searchString);
            if (!string.IsNullOrEmpty(searchString))
            {
                cars = _carsRepo.GetCarsList(skip, take, searchString);
                count = _carsRepo.GetCarsCount();
                ViewBag.SearchString = searchString;
                ViewBag.Title = $"جستجو: {searchString}";
            }
            count = _carsRepo.GetCarsCount();
            var pageCount = (int)Math.Ceiling((double)count / take);
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.Facebook = _contentRepo.GetStaticContentDetail((int) StaticContents.Facebook).Link;
            ViewBag.Instagram = _contentRepo.GetStaticContentDetail((int)StaticContents.Instagram).Link;
            ViewBag.Twitter = _contentRepo.GetStaticContentDetail((int)StaticContents.Twitter).Link;
            List<IQueryable<CarModel>> carClassList = null;
            foreach (var item in vm)
            {
                carClassList.Add(_carsRepo.GetCarClasses(item));
            }

            ViewBag.carClassList = carClassList;
            return View(vm);
        }

        public ActionResult Sidebar()
        {
            return View();
        }
        public ActionResult Titlebar()
        {
            return View();
        }
        //public ActionResult TopCarsSection(int? take = null)
        //{
        //    var getCount = 4;
        //    if (take != null)
        //        getCount = take.Value;

        //    var articles = _carsRepo.GetTopCars(getCount);
        //    var vm = new List<TopCarsViewModel>();
        //    foreach (var item in articles)
        //    {
        //        vm.Add(new TopCarsViewModel(item));
        //    }
        //    return PartialView(vm);
        //}
        //public ActionResult ArticleCategoriesSection()
        //{
        //    var categories = _articlesRepo.GetArticleCategories();
        //    var articleCategoriesVm = new List<ArticleCategoriesViewModel>();
        //    foreach (var item in categories)
        //    {
        //        var vm = new ArticleCategoriesViewModel();
        //        vm.Id = item.Id;
        //        vm.Title = item.Title;
        //        vm.ArticleCount = _articlesRepo.GetArticlesCount(item.Id);
        //        articleCategoriesVm.Add(vm);
        //    }
        //    return PartialView(articleCategoriesVm);
        //}
        ////[Route("Blog/Post/{id}/{title}")]
        //public ActionResult Details(int id)
        //{
        //    _articlesRepo.UpdateArticleViewCount(id);
        //    var article = _articlesRepo.GetArticle(id);
        //    var articleDetailsVm = new ArticleDetailsViewModel(article);
        //    var articleComments = _articlesRepo.GetArticleComments(id);
        //    var articleCommentsVm = new List<ArticleCommentViewModel>();

        //    foreach (var item in articleComments)
        //        articleCommentsVm.Add(new ArticleCommentViewModel(item));

        //    articleDetailsVm.ArticleComments = articleCommentsVm;
        //    var articleTags = _articlesRepo.GetArticleTags(id);
        //    articleDetailsVm.Tags = articleTags;
        //    return View(articleDetailsVm);
        //}
        //[HttpPost]
        //public ActionResult PostComment(CommentFormViewModel form)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var comment = new ArticleComment()
        //        {
        //            ArticleId = form.ArticleId,
        //            ParentId = form.ParentId,
        //            Name = form.Name,
        //            Email = form.Email,
        //            Message = form.Message,
        //            AddedDate = DateTime.Now,
        //        };
        //        _articlesRepo.AddComment(comment);
        //        return RedirectToAction("ContactUsSummary", "Home");
        //    }
        //    return RedirectToAction("Details", new { id = form.ArticleId });
        //}

        //public ActionResult LatestArticlesSection()
        //{
        //    var articles = _articlesRepo.GetLatestArticles(5);
        //    var vm = new List<LatestArticlesViewModel>();
        //    foreach (var item in articles)
        //    {
        //        vm.Add(new LatestArticlesViewModel(item));
        //    }
        //    return PartialView(vm);
        //}
        //public ActionResult ArticleTags()
        //{
        //    var articlesTags = _articlesRepo.GetArticleListTags();
        //    return PartialView(articlesTags);
        //}

        //public ActionResult ArticleTag(int id)
        //{
        //    var articlesTags = _articlesRepo.GetArticleTags(id);
        //    return PartialView(articlesTags);
        //}
    }
}