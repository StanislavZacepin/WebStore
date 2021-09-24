using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Services.Interfaces
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
