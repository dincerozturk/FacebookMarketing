using FM.Application.Interfaces.Repositories;
using FM.Domain.Entities.Facebook;
using FM.Infrastructure.Contexts;

namespace FM.Infrastructure.Repositories
{
    public class GroupRepository : BaseRepository<FbPostGroup, long>, IGroupRepository
    {
        public GroupRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}