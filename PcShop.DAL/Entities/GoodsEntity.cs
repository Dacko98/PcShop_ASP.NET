using System;
using System.Collections.Generic;

namespace PcShop.DAL.Entities
{
    public class GoodsEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Weight { get; set; }
        public string CountInStock { get; set; }

    }
}
