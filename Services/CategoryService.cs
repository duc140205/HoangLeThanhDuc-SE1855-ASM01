using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository icategoryRepository;

        public CategoryService()
        {
            // Assuming a concrete implementation of ICategoryRepository is available
            icategoryRepository = new CategoryRepository(); // Replace with actual repository instantiation
        }
        public void AddCategory(Category category)
        {
            icategoryRepository.AddCategory(category);
        }

        public void DeleteCategory(int categoryId)
        {
            icategoryRepository.DeleteCategory(categoryId);
        }

        public List<Category> GetCategories()
        {
            return icategoryRepository.GetCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            return icategoryRepository.GetCategoryById(categoryId);
        }

        public List<Category> SearchCategories(string searchTerm)
        {
            return icategoryRepository.SearchCategories(searchTerm);
        }

        public void UpdateCategory(Category category)
        {
            icategoryRepository.UpdateCategory(category);
        }
    }
}
