namespace Admin.Web.DAL
{
    using System.Data.Entity;

    public partial class DbModel : DbContext
    {
        public DbModel()
            : base("name=DbModel")
        {
        }

        public virtual DbSet<MenuGroup> MenuGroups { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuItem>()
                .HasMany(e => e.MenuItems1)
                .WithOptional(e => e.MenuItem1)
                .HasForeignKey(e => e.ParentId);
        }
    }
}
