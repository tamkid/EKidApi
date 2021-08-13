using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKidApi.Repository
{
    public interface IUnitOfWork
    {
        void Save();
        Task Save_Async();
        void CreateTransaction();
        void Commit();
        void Rollback();
        GenericRepo<T> Repository<T>() where T : class;
    }
}
