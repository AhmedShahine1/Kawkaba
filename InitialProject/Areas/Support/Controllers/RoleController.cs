using AutoMapper;
using Kawkaba.Core.DTO;
using Kawkaba.Core.DTO.AuthViewModel.RoleModel;
using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.RepositoryLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Kawkaba.Areas.Support.Controllers
{
    [Area("Support")]
    //[Authorize(Policy = "Support Developer")]
    public class RoleController : Controller
    {
        private readonly IMapper mapper;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IMemoryCache memoryCache;
        private const string CacheKey = "rolesCache";

        public RoleController(RoleManager<ApplicationRole> _roleManager, IMapper _mapper, IMemoryCache _memoryCache)
        {
            roleManager = _roleManager;
            mapper = _mapper;
            memoryCache = _memoryCache;
        }

        // GET: RoleController
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewData["Title"] = "Roles";

                if (!memoryCache.TryGetValue(CacheKey, out IEnumerable<ApplicationRole>? AllRoles))
                {
                    AllRoles = await roleManager.Roles.ToListAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5), // Cache for 5 minutes
                        SlidingExpiration = TimeSpan.FromMinutes(2) // Reset cache if accessed within 2 minutes
                    };
                    memoryCache.Set(CacheKey, AllRoles, cacheEntryOptions);
                }
                return View(AllRoles);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "خطا في جلب البيانات",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        // GET: RoleController/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create Role";
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleDTO roleModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = mapper.Map<ApplicationRole>(roleModel);
                    var result = await roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        memoryCache.Remove(CacheKey); // Clear cache
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, string.Join("; ", result.Errors.Select(e => e.Description)));
                    }
                }
                return View(roleModel);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "خطا في حقظ البيانات",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        // GET: RoleController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                ViewData["Title"] = "Edit Role";
                var role = await roleManager.FindByIdAsync(id);
                ViewBag.Id = id;
                var roleModel = mapper.Map<RoleDTO>(role);
                return View(roleModel);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "خطا في جلب البيانات",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }
        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleDTO roleDTO,string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = await roleManager.FindByIdAsync(id);
                    role.Name = roleDTO.RoleName;
                    role.Description = roleDTO.RoleDescription;
                    role.ArName = roleDTO.RoleAr;
                    var result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        memoryCache.Remove(CacheKey); // Clear cache
                        ViewBag.Message = "تم تعديل البيانات بنجاح";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, string.Join("; ", result.Errors.Select(e => e.Description)));
                    }
                }
                return View(roleDTO);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "خطا في تعديل البيانات",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }

                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    memoryCache.Remove(CacheKey); // Clear cache
                    ViewBag.Message = "تم حذف البيانات بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, string.Join("; ", result.Errors.Select(e => e.Description)));
                }

                return View(nameof(Index));
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "خطا في حذف البيانات",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }
    }
}
