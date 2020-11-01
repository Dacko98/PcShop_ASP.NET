using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Interfaces
{
    public interface IListModel : IModel
    {
        EntityTypeEnum EntityType { get; set; }
    }
}
