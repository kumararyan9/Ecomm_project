using Ecomm_project.DataAcess.Data;
using Ecomm_project.DataAcess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_project.DataAcess.Repository
{
    public class UnitOfWork : iUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            CoverType = new CoverTypeRepository(_context);
            Spcalls = new Spcalls(_context);
            ProductRep = new ProductRep(_context);
        }

        public ICategoryRepository Category { private set; get; }

        public ICoverTypeRepository CoverType { private set; get; }

        public ISpcalls Spcalls { private set; get; }

        public IProductRep ProductRep { private set; get; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
