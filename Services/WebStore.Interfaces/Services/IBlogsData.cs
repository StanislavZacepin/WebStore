using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Models;

namespace WebStore.Interfaces.Services
{
    public interface IBlogsData
    {
        IEnumerable<Blog> GetAll();

        Blog GetById(int id);

        int Add(Blog blog);

        void Update(Blog blog);

        bool Delete(int id);
    }
}
