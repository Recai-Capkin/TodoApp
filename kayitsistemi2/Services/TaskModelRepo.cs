using kayitsistemi2.Data;
using kayitsistemi2.Models;
using kayitsistemi2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kayitsistemi2.Services
{
    public class TaskModelRepo : ITaskModelRepository
    {
        private readonly KayitdbContext _kayitdbContext;

        public TaskModelRepo(KayitdbContext kayitdbContext)
        {
            this._kayitdbContext = kayitdbContext;
        }

        public void Create(TaskModel taskModel)
        {
            _kayitdbContext.AddRange(taskModel);
        }
            
        public void Delete(TaskModel taskModel)
        {
            _kayitdbContext.TaskModels.Remove(taskModel);
        }

        public IEnumerable<TaskModel> GetAll()
        {
            return _kayitdbContext.TaskModels.ToList();
        }

        public TaskModel GetById(int TaskId)
        {
            var task = _kayitdbContext.TaskModels.Where(t => t.TaskId == TaskId).FirstOrDefault();
            return task;
        }

        public void Insert(TaskModel taskModel)
        {
            _kayitdbContext.TaskModels.Add(taskModel);
        }

        public void Save()
        {
            _kayitdbContext.SaveChanges();
        }

        public void Update(TaskModel taskModel)
        {
            _kayitdbContext.TaskModels.Update(taskModel);
        }
    }
}
