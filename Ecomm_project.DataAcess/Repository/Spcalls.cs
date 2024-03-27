using Dapper;
using Ecomm_project.DataAcess.Data;
using Ecomm_project.DataAcess.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_project.DataAcess.Repository
{
    public class Spcalls : ISpcalls
    {
        private readonly ApplicationDbContext _context;
        private static string connectionstring = "";
        public Spcalls(ApplicationDbContext context)
        {
            _context = context;
            connectionstring = _context.Database.GetDbConnection().ConnectionString;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Execute(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                sqlcon.Execute( procedureName, param ,commandType:CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                return sqlcon.Query<T>(procedureName,param,commandType:CommandType.StoredProcedure);
            }
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                var result = sqlcon.QueryMultiple(procedureName, param,commandType:CommandType.StoredProcedure);
                var item1 = result.Read<T1>();
                var item2 = result.Read<T2>();
                if(item1!= null && item2 != null)
                {
                    return new Tuple<IEnumerable<T1>,IEnumerable<T2>>(item1, item2 );
                }
                return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(item1),new List<T2>(item2));
            }
        }

        public T OneRecord<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                var value = sqlcon.Query<T>(procedureName, param,commandType:CommandType.StoredProcedure);
                return value.FirstOrDefault();
            }
        }

        public T Single<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                return sqlcon.ExecuteScalar<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
