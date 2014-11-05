using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juices.DAL.Entities.Product
{
	[DisplayName("Содержание")]
	public class Content : Entity {
		public int ChemicalId { get; set; }
		
		[Required, ForeignKey("ChemicalId")]
		public virtual Chemical Chemical { get; set; }

		public int ProductId { get; set; }
		
		[Required, ForeignKey("ProductId")]
		public virtual Product Product { get; set; }

		public int Order { get; set; }

		[DisplayName("Не указан")]
		public bool IsNotSpecified { get; set; }
	}
}