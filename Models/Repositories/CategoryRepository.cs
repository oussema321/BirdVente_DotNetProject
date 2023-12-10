using BirdStore.Data;
using BirdStore.Models.Repositories.IRepository;

namespace BirdStore.Models.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _db;
        public CategoryRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
