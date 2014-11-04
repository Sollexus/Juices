using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JuicesMvc.Dtos.Products {
	public class EditProductDto {
		public int Id { get; set; }

		[Required(ErrorMessage = "Нужно задать название")]
		public string Name { get; set; }

		public IEnumerable<ContentDto> Contents { get; set; }

		[Required(ErrorMessage = "Нужно задать описание")]
		public string Description { get; set; }
	}

	public class ContentDto {
		[Required]
		public int Id { get; set; }

		[Required]
		public int ChemicalId { get; set; }
	}
}