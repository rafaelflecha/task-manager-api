using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Repositories;

namespace TaskManager.API.Repositories
{
    public class BaseRepository
    {
        private readonly TaskManagerDbContext _dbContext;

        public BaseRepository(TaskManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}