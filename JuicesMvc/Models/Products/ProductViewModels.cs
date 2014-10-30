using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Juices.DAL;

namespace JuicesMvc.Models.Products {
	public class ProductViewModel {
		public int Id { get; set; }
		[Required, DisplayName("Название")]
		public string Name { get; set; }
		[DisplayName("Вещества")]
		public virtual IEnumerable<Content> Contents { get; set; }
		[DisplayName("Технологии")]
		public virtual ICollection<Technology> Technologies { get; set; }
		[Required, DisplayName("Описание")]
		public string Description { get; set; }

		public static Product GetDefault() {
			return new Product { Id = 1, Name = "Название", Description = "Описание" };
		}
	}

	public class EditProductViewModel {
		public int Id { get; set; }
		[Required, DisplayName("Название")]
		public string Name { get; set; }
		[DisplayName("Вещества")]
		public virtual IEnumerable<int> Contents { get; set; }
		[DisplayName("Технологии")]
		public virtual ICollection<int> Technologies { get; set; }
		[Required, DisplayName("Описание")]
		public string Description { get; set; }
	}
}