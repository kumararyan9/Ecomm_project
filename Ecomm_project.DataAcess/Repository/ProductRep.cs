﻿using Ecomm_project.DataAcess.Data;
using Ecomm_project.DataAcess.Repository.IRepository;
using Ecomm_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_project.DataAcess.Repository
{
    public class ProductRep:Repository<Product>,IProductRep
    {
        private readonly ApplicationDbContext _context;
        public ProductRep(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
    }
}