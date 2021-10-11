using kayitsistemi2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kayitsistemi2.Repository
{
    public interface ITaskModelRepository
    {
        IEnumerable<TaskModel> GetAll();
        TaskModel GetById(int TaskId);
        void Create(TaskModel taskModel);
        void Update(TaskModel taskModel);
        void Delete(TaskModel taskModel);
        void Insert(TaskModel taskModel);
        void Save();
    }
}
