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
    public class BlogController : Controller
    {
        private readonly ArticlesRepository _articlesRepo;
        private readonly StaticContentDetailsRepository _contentRepo;

        public BlogController(ArticlesRepository articlesRepo, StaticContentDetailsRepository contentRepo)
        {
            _articlesRepo = articlesRepo;
            _contentRepo = contentRepo;
        }

        // GET: Blog
        //[Route("Blog")]
        //[Route("Blog/{id}/{title}")]
        public ActionResult Index(int pageNumber = 1)
        {
            var vm = new List<ArticleListViewModel>();
            var take = 3;
            var skip = pageNumber * take - take;
            var count = 0;
            var articles = _articlesRepo.GetArticlesList(skip, take);
            foreach (var item in articles)
            {
                vm.Add(new ArticleListViewModel(item));
            }
            //if (!string.IsNullOrEmpty(null))
            //{
            //    articles = _articlesRepo.GetArticlesList(skip, take, null);
            //    count = _articlesRepo.GetArticlesCount();
            //    ViewBag.SearchString = null;
            //    ViewBag.Title = $"جستجو: {null}";
            //}
            count = _articlesRepo.GetArticlesCount();
            var pageCount = (int)Math.Ceiling((double)count / take);
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.Facebook = _contentRepo.GetStaticContentDetail((int)StaticContents.Facebook).Link;
            ViewBag.Instagram = _contentRepo.GetStaticContentDetail((int)StaticContents.Instagram).Link;
            ViewBag.Twitter = _contentRepo.GetStaticContentDetail((int)StaticContents.Twitter).Link;
            return View(vm);
        }
        [HttpPost]
        public ActionResult Index(int pageNumber = 1, string searchString = null)
        {
            var articles = _articlesRepo.GetAll();
            var vm = new List<ArticleListViewModel>();
            var take = 3;
            var skip = pageNumber * take - take;
            var count = 0;
            foreach (var item in articles)
            {
                vm.Add(new ArticleListViewModel(item));
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                vm = new List<ArticleListViewModel>();
                articles = _articlesRepo.GetArticlesList(skip, take, searchString);
                foreach (var item in articles)
                {
                    vm.Add(new ArticleListViewModel(item));
                }
                count = vm.Count;
                ViewBag.SearchString = searchString;
                ViewBag.Title = $"جستجو: {searchString}";
                var pageCount2 = (int)Math.Ceiling((double)count / take);
                ViewBag.PageCount = pageCount2;
                ViewBag.CurrentPage = pageNumber;
                return View(vm);
            }
            count = _articlesRepo.GetArticlesCount();
            var pageCount = (int)Math.Ceiling((double)count / take);
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.Facebook = _contentRepo.GetStaticContentDetail((int)StaticContents.Facebook).Link;
            ViewBag.Instagram = _contentRepo.GetStaticContentDetail((int)StaticContents.Instagram).Link;
            ViewBag.Twitter = _contentRepo.GetStaticContentDetail((int)StaticContents.Twitter).Link;
            return View(vm);
        }

        public ActionResult TopArticlesSection(int? take = null)
        {
            var getCount = 3;
            if (take != null)
                getCount = take.Value;

            var articles = _articlesRepo.GetTopArticles(getCount);
            var vm = new List<TopArticlesViewModel>();
            foreach (var item in articles)
            {
                vm.Add(new TopArticlesViewModel(item));
            }
            return PartialView(vm);
        }
        public ActionResult ArticleCategoriesSection()
        {
            var categories = _articlesRepo.GetArticleCategories();
            var articleCategoriesVm = new List<ArticleCategoriesViewModel>();
            foreach (var item in categories)
            {
                var vm = new ArticleCategoriesViewModel();
                vm.Id = item.Id;
                vm.Title = item.Title;
                vm.ArticleCount = _articlesRepo.GetArticlesCount(item.Id);
                articleCategoriesVm.Add(vm);
            }
            return PartialView(articleCategoriesVm);
        }
        //[Route("Blog/Post/{id}/{title}")]
        public ActionResult Details(int id)
        {
            _articlesRepo.UpdateArticleViewCount(id);
            var article = _articlesRepo.GetArticle(id);
            var articleDetailsVm = new ArticleDetailsViewModel(article);
            var articleComments = _articlesRepo.GetArticleComments(id);
            var articleCommentsVm = new List<ArticleCommentViewModel>();

            foreach (var item in articleComments)
                articleCommentsVm.Add(new ArticleCommentViewModel(item));

            articleDetailsVm.ArticleComments = articleCommentsVm;
            var articleTags = _articlesRepo.GetArticleTags(id);
            articleDetailsVm.Tags = articleTags;
            return View(articleDetailsVm);
        }
        [HttpPost]
        public ActionResult PostComment(CommentFormViewModel form)
        {
            if (ModelState.IsValid)
            {
                var comment = new ArticleComment()
                {
                    ArticleId = form.ArticleId,
                    ParentId = form.ParentId,
                    Name = form.Name,
                    Email = form.Email,
                    Message = form.Message,
                    AddedDate = DateTime.Now,
                };
                _articlesRepo.AddComment(comment);
                return RedirectToAction("Details", new {id = form.ArticleId});
            }
            return RedirectToAction("Details", new { id = form.ArticleId });
        }

        public ActionResult LatestArticlesSection()
        {
            var articles = _articlesRepo.GetLatestArticles(5);
            var vm = new List<LatestArticlesViewModel>();
            foreach (var item in articles)
            {
                vm.Add(new LatestArticlesViewModel(item));
            }
            return PartialView(vm);
        }
        public ActionResult ArticleTags()
        {
            var articlesTags = _articlesRepo.GetArticleListTags();
            return PartialView(articlesTags);
        }

        public ActionResult ArticleTag(int id)
        {
            var articlesTags = _articlesRepo.GetArticleTags(id);
            return PartialView(articlesTags);
        }
    }
}