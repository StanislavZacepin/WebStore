using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.Services.InMemory
{
    public class InMemoryBlogData : IBlogsData
    {
        private readonly ILogger<InMemoryBlogData> _Logger;
        private int _CurrentMaxId;


        public InMemoryBlogData(ILogger<InMemoryBlogData> Logger)
        {
            _Logger = Logger;
            _CurrentMaxId = BlogTestData.Blogs.Max(e => e.id);
        }

        public int Add(Blog blog)
        {
            if (blog is null) throw new ArgumentNullException(nameof(blog));

            if (BlogTestData.Blogs.Contains(blog)) return blog.id;

            blog.id = ++_CurrentMaxId;
            BlogTestData.Blogs.Add(blog);

            return blog.id;
        }

        public bool Delete(int id)
        {
            var db_blog = GetById(id);
            if (db_blog is null) return false;

            BlogTestData.Blogs.Remove(db_blog);

            return true;
        }

        public IEnumerable<Blog> GetAll() => BlogTestData.Blogs;

        public Blog GetById(int id) => BlogTestData.Blogs.SingleOrDefault(e => e.id == id);

        public void Update(Blog blog)
        {
            if (blog is null) throw new ArgumentNullException(nameof(blog));

            if (BlogTestData.Blogs.Contains(blog)) return;// толко для реализации в памяти!!

            var db_blog = GetById(blog.id);
            if (db_blog is null) return;

            db_blog.Title = blog.Title;
            db_blog.User = blog.User;
            db_blog.Time = blog.Time;
            db_blog.Date = blog.Date;
            db_blog.article = blog.article;
            db_blog.Fullarticle = blog.Fullarticle;


            //db.SaveChanges();

        }
    }
}


