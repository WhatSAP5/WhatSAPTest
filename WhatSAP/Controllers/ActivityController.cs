﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhatSAP.Models;

namespace WhatSAP.Controllers
{
    [Route("activity")]
    public class ActivityController : Controller
    {
        private readonly WhatSAPContext _context;

        public ActivityController(WhatSAPContext context)
        {
            _context = context;
        }

        // GET: Activity
        [Route("")]
        public IActionResult Index(int page = 0)
        {
            var pageSize = 2;
            var totalActivities = _context.Activity.Count();
            var totalPages = totalActivities / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage <= totalPages;

            var activity = _context.Activity.OrderBy(x => x.Rate).Skip(pageSize * page).Take(pageSize).ToArray();

            //var activity = _context.Activity.ToArray();

            return View(activity);

            //var whatSAPContext = _context.Activity.Include(a => a.Address).Include(a => a.Client);
            //return View(await whatSAPContext.ToListAsync());
        }

        // GET: Activity/Details/5
        [Route("{id}")]
        public async Task<IActionResult> Details(long id)
        {
            var activity = await _context.Activity
                .Include(a => a.Address)
                .Include(a => a.Client)
                .SingleOrDefaultAsync(m => m.ActivityId == id);

            
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // GET: Activity/Create
        [Authorize]
        [HttpGet, Route("create")]
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "Address1");
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email");
            return View();
        }

        // POST: Activity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost, Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityId,ActivityName,Description,ActivityDate,AddressId,Price,Capacity,ClientId")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "Address1", activity.AddressId);
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email", activity.ClientId);
            return View(activity);
        }

        // GET: Activity/Edit/5
        [Authorize]
        [HttpGet, Route("edit")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activity.SingleOrDefaultAsync(m => m.ActivityId == id);
            if (activity == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "Address1", activity.AddressId);
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email", activity.ClientId);
            return View(activity);
        }

        // POST: Activity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost, Route("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ActivityId,ActivityName,Description,ActivityDate,AddressId,Price,Capacity,ClientId")] Activity activity)
        {
            if (id != activity.ActivityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(activity.ActivityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "Address1", activity.AddressId);
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "Email", activity.ClientId);
            return View(activity);
        }

        // GET: Activity/Delete/5
        [Authorize]
        [HttpGet, Route("delete")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activity
                .Include(a => a.Address)
                .Include(a => a.Client)
                .SingleOrDefaultAsync(m => m.ActivityId == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // POST: Activity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var activity = await _context.Activity.SingleOrDefaultAsync(m => m.ActivityId == id);
            _context.Activity.Remove(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityExists(long id)
        {
            return _context.Activity.Any(e => e.ActivityId == id);
        }

        //Search by Title
        // SEARCH: Activity/Search/{keyworkd}
        [HttpPost, Route("search/{keyword}")]
        public IActionResult SearchResult(string keyword)
        {
            if (keyword == null)
            {
                return NotFound();
            }

            var result = _context.Activity.Where(x => x.ActivityName.Contains(keyword)).ToArray();
            return View(result);
        }

        //Search by Price
        // SEARCH: Activity/Search/{keyworkd}
        [HttpPost, Route("search/{keyword}")]
        public IActionResult SearchResult(double price)
        {
            var result = _context.Activity.Where(x => x.Price <= price).ToArray();
            return View(result);
        }

        [HttpGet, Route("search/{category}")]
        public IActionResult SearchResult(long? categoryId, string category)
        {
            if (categoryId == null)
            {
                return NotFound();
            }
            
            var result = _context.Activity.Where(x => x.CategoryId == categoryId).ToArray();
            return View(result);
        }

        //Search by Keyword
        // SEARCH: Activity/Search/{category}
        [HttpPost, Route("search/{category}")]
        public IActionResult SearchResult(long categoryId, string category)
        {
            var result = _context.Activity.Where(x => x.CategoryId == categoryId).ToArray();
            return View(result);
        }
    }
}
