using FileHandling.Models.Domain;

namespace FileHandling.Repository.Abstract
{
    public interface IProductRepository
    {
        bool Add(Product model);

        bool Delete(string name);

        public string GetName(string name);
    }
}
