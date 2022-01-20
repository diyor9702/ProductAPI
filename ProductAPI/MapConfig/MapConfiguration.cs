using AutoMapper;
using Web.Data.Models;
using Web.Services.ProductDTO;

namespace ProductAPI.MapConfig
{
    public class MapConfiguration : Profile
    {
        public MapConfiguration()
        {
            CreateMap<Product, ProductCreationDTO>().ReverseMap();
        }
    }
}
