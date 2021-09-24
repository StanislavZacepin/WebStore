using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogsData _BlogsData;
        private readonly ILogger<EmployeesController> _Logger;

        public BlogController(IBlogsData blogsData, ILogger<EmployeesController> logger)
        {
            _BlogsData = blogsData;
            _Logger = logger;
        }
        public IActionResult Index() => View(_BlogsData.GetAll());

        #region Create
        public IActionResult Create() => View("Edit", new BlogViewModel());

        #endregion
        #region Edit
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new BlogViewModel());

            var blog = _BlogsData.GetById((int)id);
            if (blog is null) return NotFound();

            var model = new BlogViewModel
            {
                Title = blog.Title,
                User = blog.User,
                Time = blog.Time,
                Date = blog.Date,
                article = blog.article,
                Fullarticle = blog.Fullarticle,
        };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(BlogViewModel model)
        {
            var blog = new Blog
            {
                Title = model.Title,
                User = model.User,
                Time = model.Time,
                Date = model.Date,
                article = model.article,
                Fullarticle = model.Fullarticle,
            };

            if (blog.id == 0)
                _BlogsData.Add(blog);
            else
                _BlogsData.Update(blog);

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Delete
        public IActionResult Delete(int id)
        {
            if (id < 0) return BadRequest();

            var blog = _BlogsData.GetById(id);
            if (blog is null) return NotFound();

            return View(new BlogViewModel
            {
                id = blog.id,
                User=blog.User,
                Time=blog.Time,
                Date=blog.Date,
                Title = blog.Title,
                article = blog.article,
                Fullarticle = blog.Fullarticle,
                
            });
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _BlogsData.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion


    }
}
