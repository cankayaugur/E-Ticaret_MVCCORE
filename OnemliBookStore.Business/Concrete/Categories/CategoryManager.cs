using OnemliBookStore.Business.Abstract.Categories;
using OnemliBookStore.Dal.Abstract;
using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Business.Concrete.Categories
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public void Create(Category entity)
        {
            _categoryRepository.Create(entity);
        }

        public void Delete(Category entity)
        {
            _categoryRepository.Delete(entity);
        }

        public void DeleteFromCategory(int bookId, int categoryId)
        {
            _categoryRepository.DeleteFromCategory(bookId, categoryId);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            return _categoryRepository.GetByIdWithProducts(categoryId);
        }

        public void Update(Category entity)
        {
            _categoryRepository.Update(entity);
        }
            
        public string ErrorMessage { get; set; }

        public bool Validate(Category entity)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "kategori ismi girmelisiniz \n";
                isValid = false;
            }
            return isValid;
        }
    }
}
