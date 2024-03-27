using Dapper;
using System;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_project.DataAcess.Repository.IRepository
{
    public interface ISpcalls:IDisposable
    {
        void Execute(string procedureName, DynamicParameters param=null);
        IEnumerable<T> List<T>(string procedureName, DynamicParameters param=null);
        T Single<T>(string procedureName, DynamicParameters param=null);
        T OneRecord<T>(string procedureName, DynamicParameters param=null);
        Tuple<IEnumerable<T1>,IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null);
    }
}
