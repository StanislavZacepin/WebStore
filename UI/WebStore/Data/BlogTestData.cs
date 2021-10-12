using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Models;

namespace WebStore.Data
{
    public class BlogTestData
    {
        private static List<Blog> _Blogs
        {
            get => Enumerable.Range(1, 10).
                        Select(i => new Blog
                        {
                        id = i,
                        User = $"Иван{i}",
                        Time =  DateTime.Now,
                        Date = DateTime.Today,
                        Title = $"Заголовок{i}",
                        article = $"Краткое Содержания{i}",
                        Fullarticle = $"Полное Содержание{i}",
                        }).ToList();
        }
        public static List<Blog> Blogs { get; } = _Blogs;
    }
}

