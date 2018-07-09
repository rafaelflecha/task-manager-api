using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Models;
using TaskManagerAPI.Repositories;

namespace TaskManager.API.Repositories
{
    public class AppTaskRepository : BaseRepository, IAppTaskRepository
    {
        private readonly TaskManagerDbContext _dbContext;
        public AppTaskRepository(TaskManagerDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(AppTask newTask)
        {
            _dbContext.AppTasks.Add(newTask);
            _dbContext.SaveChanges();
        }

        public void Delete(int taskId)
        {
            var appTask = new AppTask { Id = taskId };
            _dbContext.Entry(appTask).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public IEnumerable<AppTask> GetAll()
        {
            return _dbContext.AppTasks;
        }

        public void Update(int taskId, AppTask updatedTask)
        {
            var oldTask = _dbContext.AppTasks.Where(p => p.Id == taskId).FirstOrDefault();
            oldTask.Title = updatedTask.Title;
            oldTask.Description = updatedTask.Description;
            oldTask.UpdatedAt = DateTime.Now;
            oldTask.CreatedAt = oldTask.CreatedAt;
            oldTask.IsCompleted = updatedTask.IsCompleted;
            
            if (updatedTask.IsCompleted)
                oldTask.CompletedAt = DateTime.Now;
                
            _dbContext.Entry(oldTask).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}