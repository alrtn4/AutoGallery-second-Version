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

        public ActionResult Looking_For()
        {
            var Looking_For_Content = _contentRepo.GetSomeStaticContentDetail((int) StaticContentTypes.Looking_For, 4);
            return PartialView(Looking_For_Content);
        }
        public ActionResult Popular()
        {
            var Popular_Content = _contentRepo.GetSomeStaticContentDetail((int)StaticContentTypes.Popular, 6);
            return PartialView(Popular_Content);
        }
        public ActionResult Blog()
        {
            var Blog_Content = _contentRepo.GetSomeStaticContentDetail((int)StaticContentTypes.Blog, 3);
            return PartialView(Blog_Content);
        }
        public ActionResult FlipBanner()
        {
            var FlipBanner_Content = _contentRepo.GetSomeStaticContentDetail((int)StaticContentTypes.FlipBanner, 1);
            return PartialView(FlipBanner_Content);
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
        public ActionResult About_us()
        {
            ViewBag.About_usContent = _contentRepo.GetAboutUs((int) StaticContentTypes.About_us, 5);
            ViewBag.About_usPic = _contentRepo.GetAboutUs((int) StaticContentTypes.About_usPic, 1)[0];
            ViewBag.About_usBanner = _contentRepo.GetAboutUs((int) StaticContentTypes.FlipBanner, 1)[0];

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Image = _contentRepo.GetSomeStaticContentDetail((int) StaticContentTypes.ContactImage, 1)[0].Image;
            ViewBag.Phone = _contentRepo.Get((int) StaticContents.Phone).ShortDescription;
            ViewBag.Email = _contentRepo.Get((int) StaticContents.Email).ShortDescription;
            ViewBag.Fax = _contentRepo.Get((int) StaticContents.Fax).ShortDescription;
            ViewBag.Web = _contentRepo.Get((int) StaticContents.Web).ShortDescription;
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactForm contactForm)
        {
            ViewBag.Image = _contentRepo.GetSomeStaticContentDetail((int)StaticContentTypes.ContactImage, 1)[0].Image;
            ViewBag.Phone = _contentRepo.Get((int)StaticContents.Phone).ShortDescription;
            ViewBag.Email = _contentRepo.Get((int)StaticContents.Email).ShortDescription;
            ViewBag.Fax = _contentRepo.Get((int)StaticContents.Fax).ShortDescription;
            ViewBag.Web = _contentRepo.Get((int)StaticContents.Web).ShortDescription;

            _contactFormRepo.Add(contactForm);
            return View(contactForm);
        }
    }
}