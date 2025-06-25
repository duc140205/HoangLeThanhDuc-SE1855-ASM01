using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        private static List<Category> categories = new List<Category>
    {
        new Category { CategoryId = 1, CategoryName = "Electronics" },
        new Category { CategoryId = 2, CategoryName = "Books" },
        new Category { CategoryId = 3, CategoryName = "Clothing" },
        new Category { CategoryId = 4, CategoryName = "Sports" },
        new Category { CategoryId = 5, CategoryName = "Toys" },
        new Category { CategoryId = 6, CategoryName = "Home & Kitchen" },
        new Category { CategoryId = 7, CategoryName = "Health & Beauty" },
        new Category { CategoryId = 8, CategoryName = "Automotive" }
    };

        //CRUD methods
        public static List<Category> GetCategories() => new List<Category>(categories);

        public static Category GetCategoryById(int id) =>
            categories.FirstOrDefault(c => c.CategoryId == id);

        public static void AddCategory(Category category) =>
            categories.Add(category);

        public static void UpdateCategory(Category category)
        {
            var existing = categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existing != null)
            {
                existing.CategoryName = category.CategoryName;
                existing.Description = category.Description;
            }
        }

        public static void DeleteCategory(int id)
        {
            var toRemove = categories.FirstOrDefault(c => c.CategoryId == id);
            if (toRemove != null)
            {
                categories.Remove(toRemove);
            }
        }
        public static List<Category> SearchCategories(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Category>(categories);
            return categories
                .Where(c => c.CategoryName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
