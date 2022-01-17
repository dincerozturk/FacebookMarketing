using System;
using FM.Application.Interfaces.Repositories;
using FM.Domain.Entities;
using FM.Infrastructure.Contexts;

namespace FM.Infrastructure.Repositories
{
    public class HealthHistoryRepository : BaseRepository<HealthHistory, Guid>, IHealthHistoryRepository
    {
        public HealthHistoryRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
