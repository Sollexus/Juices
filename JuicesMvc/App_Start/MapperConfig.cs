using AutoMapper;
using Juices.DAL;
using Juices.DAL.Entities.Product;
using JuicesMvc.Models.Products;

namespace JuicesMvc.App_Start {
	public static class MapperConfig {
		public static void Configure() {
			Mapper.CreateMap<Product, ProductViewModel>();
			Mapper.CreateMap<Content, ContentViewModel>();
		}
	}
}