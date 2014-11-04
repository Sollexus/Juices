using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Juices.DAL.Entities.Product
{
	public class Product : Entity {
		public string Name { get; set; }

		[Required]
		public ICollection<Content> Contents { get; set; }

		[Required]
		public ICollection<Technology> Technologies { get; set; }

		public string Description { get; set; }

		public static Product GetDefault() {
			return new Product {Id = 1, Name = "Название", Description = "Описание"};
		}
	}
}