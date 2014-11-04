using System.ComponentModel.DataAnnotations;

namespace Juices.DAL
{
	public abstract class Entity {
		[Key]
		public int Id { get; set; }
	}
}