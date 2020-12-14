using AutoMapper;
using PcShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PcShop.DAL.Repositories
{
    public class CategoryRepository : IAppRepository<CategoryEntity>
    {
        private readonly IList<CategoryEntity> _categories;
        private readonly IMapper _mapper;

        public CategoryRepository(
            Storage storage,
            IMapper mapper)
        {
            _categories = storage.Categories;
            this._mapper = mapper;
        }

        public IList<CategoryEntity> GetAll()
        {
            return _categories;
        }

        public CategoryEntity GetById(Guid id)
        {
            return _categories.SingleOrDefault(entity => entity.Id == id);
        }


        public Guid Insert(CategoryEntity category)
        {
            category.Id = Guid.NewGuid();
            _categories.Add(category);
            return category.Id;
        }

        public Guid? Update(CategoryEntity categoryUpdated)
        {
            var categoryExisting = _categories.SingleOrDefault(categoryInStorage => categoryInStorage.Id == categoryUpdated.Id);
            if (categoryExisting != null)
            {
                _mapper.Map(categoryUpdated, categoryExisting);
            }

            return categoryExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var categoryToRemove = _categories.Single(category => category.Id.Equals(id));
            _categories.Remove(categoryToRemove);
        }
    }
}