using System;
using System.Collections.Generic;
using System.Text;

namespace PcShop.DAL.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }

        EntityTypeEnum EntityType { get; }
    }
}
