using System;
using System.Linq.Expressions;
using AutoMapper;
using Juices.DAL.Entities.Product;
using JuicesMvc.Dtos.Products;
using JuicesMvc.Models.Products;

namespace JuicesMvc.App_Start {
	public static class MapperConfig {
		public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
				this IMappingExpression<TSource, TDestination> map,
				Expression<Func<TDestination, object>> selector) {
			map.ForMember(selector, config => config.Ignore());
			return map;
		}

		public static void Configure() {
			Mapper.AllowNullDestinationValues = false;
			
			Mapper.CreateMap<Product, ProductViewModel>();
			Mapper
				.CreateMap<EditProductDto, Product>()
				.Ignore(p => p.Contents)
				.Ignore(p => p.Technologies);

			Mapper.CreateMap<Content, ContentViewModel>();
		}
	}
}