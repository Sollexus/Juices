using System.Data.Entity;

namespace Juices.DAL
{
    public class JuicyContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public JuicyContext() : base("name=JuicyContext")
        {
        }

		public DbSet<Chemical> Chemicals { get; set; }

		public DbSet<Technology> Technologies { get; set; }

		public DbSet<Product> Products { get; set; }

	    protected override void OnModelCreating(DbModelBuilder _) {
			//_.Entity<Content>().HasRequired()
		    //modelBuilder.Entity<Product>().HasRequired(p => p.Contents)
	    }
    }
}
