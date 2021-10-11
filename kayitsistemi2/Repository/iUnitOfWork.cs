using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kayitsistemi2.Repository
{
    public interface IIUnitOfWork
    {
        ITaskModelRepository TaskModelRepository { get; }
        void Save();
    }
}
