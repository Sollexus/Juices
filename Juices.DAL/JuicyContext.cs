using System.Data.Entity;
using Juices.DAL.Entities.Product;

namespace Juices.DAL {
	public class JuicyContext : DbContext {
		public JuicyContext()
			: base("name=JuicyContext") {
		}

		public DbSet<Chemical> Chemicals { get; set; }

		public DbSet<Technology> Technologies { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<Content> Contents { get; set; }

		protected override void OnModelCreating(DbModelBuilder _) {
		}
	}
}
