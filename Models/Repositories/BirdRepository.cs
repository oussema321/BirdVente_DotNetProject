using BirdStore.Data;
using BirdStore.Models.Repositories.IRepository;

namespace BirdStore.Models.Repositories
{
    public class BirdRepository : Repository<Bird>, IBirdRepository
    {

        private AppDbContext _db;
        public BirdRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(Bird obj)
        {
            var objFromDb = _db.Birds.Find(obj.Id);

            if (objFromDb != null)
            {
                // Mettez à jour les propriétés de l'objet existant avec les nouvelles valeurs
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
      
                objFromDb.Color = obj.Color;
                objFromDb.Price = obj.Price;
             
                objFromDb.CategoryId = obj.CategoryId;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }

                // Enregistrez les modifications dans la base de données
                _db.SaveChanges();
            }


        }
    }
}
