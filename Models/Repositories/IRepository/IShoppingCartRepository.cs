namespace BirdStore.Models.Repositories.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Save();
        void Update(ShoppingCart obj);
    }
}
