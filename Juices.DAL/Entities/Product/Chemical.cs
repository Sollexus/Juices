using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Juices.DAL.Entities.Product
{
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
}