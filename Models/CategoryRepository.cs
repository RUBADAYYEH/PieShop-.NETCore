
using Microsoft.EntityFrameworkCore;

namespace PieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PieShopDbContext _pieShopDbContext;
        public CategoryRepository(PieShopDbContext pieShopDbContext)
        {
            _pieShopDbContext = pieShopDbContext;
        }

        public IEnumerable<Category> AllCategories
        {
            get
            {
                return _pieShopDbContext.Categories.OrderBy(c => c.CategoryName);
            }
        }
    }
}
