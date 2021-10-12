using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.ViewModels
{
    public class BlogViewModel
    {
        public int id { get; set; }

        public DateTime Time { get; set; }

        public DateTime Date { get; set; }
        public string User { get; set; }

        public string Title { get; set; }
        public string article { get; set; }
        public string Fullarticle { get; set; }
    }
}
