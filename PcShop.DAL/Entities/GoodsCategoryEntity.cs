using System;
using System.Collections.Generic;

namespace PcShop.DAL.Entities
{
    public class GoodsCategoryEntity : IEntity
    {
        public Guid Id { get; set; }

        public Guid GoodsId { get; set; }
        public virtual GoodsEntity Goods { get; set; }

        public Guid CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
    }
}
