using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SazeNegar.Core.Models;
using SazeNegar.Core.Utility;
using SazeNegar.Infrastructure.Repositories;
using SazeNegar.Infratructure.Repositories;
using SazeNegar.Web.ViewModels;

namespace SazeNegar.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly StaticContentDetailsRepository _contentRepo;
        private readonly GalleriesRepository _galleryRepo;
        private readonly GalleryVideosRepository _galleryVideosRepo;
        private readonly TestimonialsRepository _testimonialRepo;
        private readonly ContactFormsRepository _contactFormRepo;
        private readonly AppointmentRepository _appointmentRepo;
        private readonly OurTeamRepository _ourTeamRepo;
        private readonly CertificatesRepository _certificatesRepo;
        private readonly ProjectsRepository _projectsRepo;
        private readonly ArticlesRepository _articlesRepository;
        private readonly PartnersRepository _partnersRepo;
        private readonly ServicesRepository _servicesRepository;
        private readonly OurServiceRepository _ourServiceRepository;

        private readonly CartRepository _cartRepository;

        public HomeController(StaticContentDetailsRepository contentRepo, GalleriesRepository galleryRepo, TestimonialsRepository testimonialRepo, ContactFormsRepository contactFormRepo, OurTeamRepository ourTeamRepo, CertificatesRepository certificatesRepo, GalleryVideosRepository galleryVideosRepo, ProjectsRepository projectsRepo,AppointmentRepository appointmentRepo,ArticlesRepository articlesRepo,PartnersRepository partnersRepo, ServicesRepository servicesRepo,OurServiceRepository ourServiceRepo, CartRepository cartRepository)
        {
            _contentRepo = contentRepo;
            _galleryRepo = galleryRepo;
            _testimonialRepo = testimonialRepo;
            _contactFormRepo = contactFormRepo;
            _ourTeamRepo = ourTeamRepo;
            _certificatesRepo = certificatesRepo;
            _galleryVideosRepo = galleryVideosRepo;
            _projectsRepo = projectsRepo;
            _appointmentRepo = appointmentRepo;
            _articlesRepository = articlesRepo;
            _partnersRepo = partnersRepo;
            _servicesRepository = servicesRepo;
            _ourServiceRepository = ourServiceRepo;

            _cartRepository = cartRepository;

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Navbar()
        {
            ViewBag.ContactUsPage = true;
            var contactUsContent = new ContactUsViewModel();
            contactUsContent.ContactInfo = _contentRepo.Get((int)StaticContents.ContactInfo);
            contactUsContent.Email = _contentRepo.Get((int)StaticContents.Email);
            contactUsContent.Address = _contentRepo.Get((int)StaticContents.Address);
            contactUsContent.Phone = _contentRepo.Get((int)StaticContents.Phone);
            contactUsContent.Youtube = _contentRepo.Get((int)StaticContents.Youtube);
            contactUsContent.Instagram = _contentRepo.Get((int)StaticContents.Instagram);
            contactUsContent.Twitter = _contentRepo.Get((int)StaticContents.Twitter);
            contactUsContent.Pinterest = _contentRepo.Get((int)StaticContents.Pinterest);
            contactUsContent.Facebook = _contentRepo.Get((int)StaticContents.Facebook);
            return PartialView(contactUsContent);
        }
        public ActionResult Banner()
        {
            var bannerContent = _contentRepo.GetContentByTypeId((int)StaticContentTypes.Banner).OrderByDescending(s => s.Id).FirstOrDefault();
            return PartialView(bannerContent);
        }
        public ActionResult Carousel()
        {
            var carouselContent = _cartRepository.GetCarts().OrderByDescending(e => e.Id);
            ViewBag.cars = _cartRepository.GetCars();
            return PartialView(carouselContent);
        }
        public ActionResult Gallery()
        {
            var galleryContent = _galleryRepo.GetAll();
            return PartialView(galleryContent);
        }
        public ActionResult CompanyHistory()
        {
            var content = _contentRepo.GetContentByTypeId((int)StaticContentTypes.CompanyHistory).FirstOrDefault();
            return PartialView(content);
        }
        public ActionResult Testimonials()
        {
            var content = _testimonialRepo.GetAll();
            return PartialView(content);
        }
        public ActionResult GallerySlider()
        {
            var galleryContent = _galleryRepo.GetAll();
            return PartialView(galleryContent);
        }
        public ActionResult ContactUsForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUsForm(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _contactFormRepo.Add(contactForm);
                return RedirectToAction("ContactUsSummary");
            }
            return View(contactForm);
        }

        public ActionResult Appointment()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Appointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _appointmentRepo.Add(appointment);
                return RedirectToAction("ContactUsSummary");
            }
            return PartialView(appointment);
        }

        public ActionResult ContactUsSummary()
        {
            return View();
        }

        [Route("OurTeam")]
        public ActionResult OurTeamPage()
        {
            var ourTeam = _ourTeamRepo.GetAll();
            return View(ourTeam);
        }
        public ActionResult Footer()
        {
            ViewBag.ContactUsPage = true;
            var contactUsContent = new ContactUsViewModel();
            contactUsContent.ContactInfo = _contentRepo.Get((int)StaticContents.ContactInfo);
            contactUsContent.Email = _contentRepo.Get((int)StaticContents.Email);
            contactUsContent.Address = _contentRepo.Get((int)StaticContents.Address);
            contactUsContent.Phone = _contentRepo.Get((int)StaticContents.Phone);
            contactUsContent.Youtube = _contentRepo.Get((int)StaticContents.Youtube);
            contactUsContent.Instagram = _contentRepo.Get((int)StaticContents.Instagram);
            contactUsContent.Twitter = _contentRepo.Get((int)StaticContents.Twitter);
            contactUsContent.Pinterest = _contentRepo.Get((int)StaticContents.Pinterest);
            contactUsContent.Facebook = _contentRepo.Get((int)StaticContents.Facebook);
            return PartialView(contactUsContent);
        }
        [Route("Gallery")]
        public ActionResult GalleryPage()
        {
            var images = _galleryRepo.GetAll();
            var videos = _galleryVideosRepo.GetAll();
            var vm = new GalleryPageViewModel()
            {
                Images = images,
                Videos = videos
            };
            return View(vm);
        }
        [Route("Certificates")]
        public ActionResult Certificates()
        {
            var certificates = _certificatesRepo.GetAll();
            return View(certificates);
        }

        //[Route("AboutUs")]
        public ActionResult About()
        {
            var vm = new AboutUsViewModel();
            var image = _contentRepo.GetStaticContentDetail((int)StaticContents.AboutOurHistory).Image;
            vm.Image = image;
            var title = _contentRepo.GetStaticContentDetail((int)StaticContents.AboutOurHistory).Title;
            vm.Title = title;
            var des = _contentRepo.GetStaticContentDetail((int)StaticContents.AboutOurHistory).ShortDescription;
            vm.ShortDescription = des;

            return View(vm);
        }

        //[Route("ContactUs")]
        public ActionResult Contact()
        {
            ViewBag.ContactUsPage = true;
            var contactUsContent = new ContactUsViewModel();
            contactUsContent.ContactInfo = _contentRepo.Get((int)StaticContents.ContactInfo);
            contactUsContent.Email = _contentRepo.Get((int)StaticContents.Email);
            contactUsContent.Address = _contentRepo.Get((int)StaticContents.Address);
            contactUsContent.Phone = _contentRepo.Get((int)StaticContents.Phone);
            contactUsContent.Youtube = _contentRepo.Get((int)StaticContents.Youtube);
            contactUsContent.Instagram = _contentRepo.Get((int)StaticContents.Instagram);
            contactUsContent.Twitter = _contentRepo.Get((int)StaticContents.Twitter);
            contactUsContent.Pinterest = _contentRepo.Get((int)StaticContents.Pinterest);
            contactUsContent.Facebook = _contentRepo.Get((int)StaticContents.Facebook);
            return PartialView(contactUsContent);
        }
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string vImagePath = String.Empty;
            string vMessage = String.Empty;
            string vFilePath = String.Empty;
            string vOutput = String.Empty;
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") +
                                    Path.GetExtension(upload.FileName).ToLower();
                    var vFolderPath = Server.MapPath("/Upload/");
                    if (!Directory.Exists(vFolderPath))
                    {
                        Directory.CreateDirectory(vFolderPath);
                    }
                    vFilePath = Path.Combine(vFolderPath, vFileName);
                    upload.SaveAs(vFilePath);
                    vImagePath = Url.Content("/Upload/" + vFileName);
                    vMessage = "Image was saved correctly";
                }
            }
            catch
            {
                vMessage = "There was an issue uploading";
            }
            vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
            return Content(vOutput);
        }

        public ActionResult Service()
        {
            var services = _servicesRepository.GetAll();
            return View(services);
        }

        public ActionResult Projects()
        {
            var projects = _projectsRepo.GetAll();
            ViewBag.Categories = _projectsRepo.GetCategories();

            return View(projects);
        }

        public ActionResult RecentProjects()
        {
            var recentprojects = _projectsRepo.GetTopProjects(5);
            
            return PartialView(recentprojects);
        }

        public ActionResult AboutOurHistory()
        {
            var services = _servicesRepository.GetServices();
            ViewBag.Categories = _servicesRepository.GetServiceCategories();

            return PartialView(services);
        }

        public ActionResult OurServices()
        {
            var ourservices = _ourServiceRepository.GetOurServices();
            return PartialView(ourservices);
        }

        public ActionResult Blog(int? id = null, string searchString = null)
        {
            //ViewBag.BlogImage = _contentRepo.GetStaticContentDetail((int)StaticContents.BlogImage).Image;
            var articles = new List<Article>();
            if (id == null)
            {
                articles = _articlesRepository.GetArticles();
                if (!string.IsNullOrEmpty(searchString))
                {
                    ViewBag.BreadCrumb = $"جستجو {searchString}";
                    articles = articles.Where(a =>
                        a.Title.ToLower().Trim().Contains(searchString.ToLower().Trim()) || a.ShortDescription.ToLower()
                            .Trim().Contains(searchString.ToLower().Trim()) || a.Description.ToLower()
                            .Trim().Contains(searchString.ToLower().Trim()) || a.ArticleTags.Any(t => t.Title.ToLower().Trim().Contains(searchString.ToLower().Trim()))).ToList();
                }
            }
            else
            {
                var category = _articlesRepository.GetCategory(id.Value);
                if (category != null)
                {
                    ViewBag.BreadCrumb = category.Title;
                    articles = _articlesRepository.GetArticlesByCategory(id.Value);
                }
            }

            var articlelistVm = new List<ArticleListViewModel>();
            foreach (var item in articles)
            {
                var vm = new ArticleListViewModel(item);
                vm.Role = _articlesRepository.GetAuthorRole(item.UserId);
                articlelistVm.Add(vm);
            }
            return PartialView(articlelistVm);

            //var content = _articlesRepository.GetAll();
            //return PartialView(content);
        }

        public ActionResult HeaderTop()
        {
            ViewBag.ContactUsPage = true;
            var contactUsContent = new ContactUsViewModel();
            contactUsContent.ContactInfo = _contentRepo.Get((int)StaticContents.ContactInfo);
            contactUsContent.Email = _contentRepo.Get((int)StaticContents.Email);
            contactUsContent.Address = _contentRepo.Get((int)StaticContents.Address);
            contactUsContent.Phone = _contentRepo.Get((int)StaticContents.Phone);
            contactUsContent.Youtube = _contentRepo.Get((int)StaticContents.Youtube);
            contactUsContent.Instagram = _contentRepo.Get((int)StaticContents.Instagram);
            contactUsContent.Twitter = _contentRepo.Get((int)StaticContents.Twitter);
            contactUsContent.Pinterest = _contentRepo.Get((int)StaticContents.Pinterest);
            contactUsContent.Facebook = _contentRepo.Get((int)StaticContents.Facebook);
            return View(contactUsContent);
        }

        public ActionResult PartnersSection()
        {
            var partners = _partnersRepo.GetAll();
            return PartialView(partners);
        }

        public ActionResult Portfolio()
        {
            var projects = _projectsRepo.GetAll();
            ViewBag.Categories = _projectsRepo.GetCategories();

            return View(projects);
        }
    }
}