using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Juices.DAL;
using Juices.DAL.Entities.Product;

namespace JuicesMvc.Models.Products {
	public class ContentViewModel
	{
		public Chemical Chemical { get; set; }
		
		//TODO: there is a way to deal with circular referenced in JSON.NET
		
		public int Order { get; set; }

		[DisplayName("Не указан")]
		public bool IsNotSpecified { get; set; }
	}
		 
	public class ProductViewModel {
		public int Id { get; set; }

		[Required, DisplayName("Название")]
		public string Name { get; set; }
		
		[DisplayName("Вещества")]
		public IEnumerable<ContentViewModel> Contents { get; set; }

		[DisplayName("Технологии")]
		public ICollection<Technology> Technologies { get; set; }

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