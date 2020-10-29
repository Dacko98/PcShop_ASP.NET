using AutoMapper;
using PcShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.DAL.Entities;

namespace PcShop.DAL.Repositories
{
    public class CategoryRepository : IAppRepository<CategoryEntity>
    {
        private readonly IList<CategoryEntity> categorys;
        private readonly IMapper mapper;

        public CategoryRepository(
            Storage storage,
            IMapper mapper)
        {
            categorys = storage.Categories;
            this.mapper = mapper;
        }

        public IList<CategoryEntity> GetAll()
        {
            return categorys;
        }

        public CategoryEntity GetById(Guid id)
        {
            return categorys.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(CategoryEntity category)
        {
            category.Id = Guid.NewGuid();
            categorys.Add(category);
            return category.Id;
        }

        public Guid? Update(CategoryEntity categoryUpdated)
        {
            var categoryExisting = categorys.SingleOrDefault(categoryInStorage => categoryInStorage.Id == categoryUpdated.Id);
            if (categoryExisting != null)
            {
                mapper.Map(categoryUpdated, categoryExisting);
            }

            return categoryExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var categoryToRemove = categorys.Single(category => category.Id.Equals(id));
            categorys.Remove(categoryToRemove);
        }
    }
}