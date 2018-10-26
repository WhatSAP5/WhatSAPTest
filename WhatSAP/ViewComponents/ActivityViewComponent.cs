using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatSAP.Models;

namespace WhatSAP.ViewComponents
{
    [ViewComponent]
    public class ActivityViewComponent : ViewComponent
    {
        private readonly WhatSAPContext _context;

        public ActivityViewComponent(WhatSAPContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var activities = _context.Activity.ToArray();
            return View(activities);
        }
    }
}
