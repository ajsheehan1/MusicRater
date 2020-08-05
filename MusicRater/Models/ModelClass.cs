using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicRater.Models
{
    public class ModelClass
    {
        [AllowHtml] public String Topic { get; set; }

        [AllowHtml] public String Doc { get; set; }

    }
}