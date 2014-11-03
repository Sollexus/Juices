using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Juices.DAL {
	public abstract class Entity {
		[Key]
		public int Id { get; set; }
	}

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

	[DisplayName("Содержание")]
	public class Content : Entity {
		[Required]
		public virtual Chemical Chemical { get; set; }

/*
		[Required]
		public virtual Product Product { get; set; }
*/
//TODO: there is a way to deal with circular referenced in JSON.NET

		public int Order { get; set; }

		[DisplayName("Не указан")]
		public bool IsNotSpecified { get; set; }
	}

	[DisplayName("Вещество")]
	public class Chemical : Entity {
		[Required, DisplayName("Название")]
		public string Name { get; set; }

		[Range(0, 100), DisplayName("Рейтинг")]
		public int Rating { get; set; }

		public static Chemical GetDefault() {
			return new Chemical {Id = -1, Name = "Название", Rating = 10};
		}
	}

	public class Technology : Entity {
		[Required, DisplayName("Название")]
		public string Name { get; set; }

		[Range(0, 100), DisplayName("Рейтинг")]
		public int Rating { get; set; }

		[Required, DisplayName("Описание")]
		public string Description { get; set; }

		public static Technology GetDefault() {
			return new Technology {Id = -1, Name = "Название", Rating = 10, Description = "Описание"};
		}
	}
}