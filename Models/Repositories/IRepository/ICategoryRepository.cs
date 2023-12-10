namespace BirdStore.Models.Repositories.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Save();
        void Update(Category obj);

    }
}
