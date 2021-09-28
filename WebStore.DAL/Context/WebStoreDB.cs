﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.DAL.Context
{
    public class WebStoreDB : DbContext
    {
        public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options) { }
        //protected override void OnModelCreating(ModelBuilder)
    }
}
