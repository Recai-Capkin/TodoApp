using kayitsistemi2.Data;
using kayitsistemi2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kayitsistemi2.Services
{
    public class UnitOfWork : IIUnitOfWork
    {
        private readonly KayitdbContext _kayitdbContext;
        private ITaskModelRepository _taskModelRepository;

        public UnitOfWork(KayitdbContext kayitdbContext)
        {
            _kayitdbContext = kayitdbContext;
        }

        public ITaskModelRepository TaskModelRepository
        {
            get
            {
                return _taskModelRepository ??= new TaskModelRepo(_kayitdbContext);
            }
        }

        public void Save()
        {
            _kayitdbContext.SaveChanges();
        }
    }
}
