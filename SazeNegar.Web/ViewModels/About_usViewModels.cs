using SazeNegar.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SazeNegar.Web.ViewModels
{
    public class About_usViewModel
    {
        public List<StaticContentDetail> About_usHeader { get; set; }
        public List<StaticContentDetail> About_us { get; set; }
        public List<StaticContentDetail> About_usPic { get; set; }
        public List<StaticContentDetail> About_usBanner { get; set; }
    }
}