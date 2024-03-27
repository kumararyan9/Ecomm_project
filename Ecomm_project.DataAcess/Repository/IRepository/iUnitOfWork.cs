using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_project.DataAcess.Repository.IRepository
{
    public interface iUnitOfWork
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        ISpcalls Spcalls { get; }
        IProductRep ProductRep { get; }
        IApplicationUserRepository ApplicationUser { get; }
        ICompanyRepository Company { get; }
    void Save();
    }
}
