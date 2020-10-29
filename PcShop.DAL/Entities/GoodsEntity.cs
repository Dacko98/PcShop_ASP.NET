﻿using System;
using System.Collections.Generic;
using AutoMapper;

namespace PcShop.DAL.Entities
{
    public class GoodsEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Weight { get; set; }
        public int CountInStock { get; set; }

        public Guid ManufacturerId { get; set; }
        public virtual ManufacturerEntity Manufacturer { get; set; }

    }
    public class GoodsEntityMapperProfile : Profile
    {
        public GoodsEntityMapperProfile()
        {
            CreateMap<GoodsEntity, GoodsEntity>();
        }
    }

}
