namespace BirdStore.Models.Repositories.IRepository
{
    public interface IBirdRepository : IRepository<Bird>
    {
        void Save();
        void Update(Bird obj);
    }
}
