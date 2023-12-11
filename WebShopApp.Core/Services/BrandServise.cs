﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebShopApp.Core.Contracts;
using WebShopApp.Infrastructure.Data.Domain;

using WebStoreApp.Infrastucture.Data;

namespace WebShopApp.Core.Services
{
    public class BrandServise : IBrandService
    {
        private readonly ApplicationDbContext _context;
        public BrandServise(ApplicationDbContext context)
        {
            _context = context;
        }

        public Brand GerBrandById(int brandId)
        {
            return _context.Brands.Find(brandId);
        }

        public List<Brand> GetBrands()
        {
            List<Brand> brands = _context.Brands.ToList();
            return brands;
        }

        public List<Product> GetProductsByBrand(int brandId)
        {
            return _context.Products
                .Where(x=> x.BrandId == brandId).ToList();
        }
    }
}
