using CATodo.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CATodo.DAL {
    public class CATodoContext : DbContext {
        public DbSet<TodoEntity> Todos { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }

        public CATodoContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<CategoryEntity>(e => {
                e.HasKey(c => c.CategoryId);
                e.HasIndex(c => c.Name).IsUnique();
                e.Property(c => c.Name).HasMaxLength(50);
                e.Property(c => c.Color).HasMaxLength(20);
                e.ToTable("Category");
            });

            modelBuilder.Entity<TodoEntity>(e => {
                e.HasKey(t => t.TodoId);
                e.Property(t => t.Title).HasMaxLength(50);
                e.Property(t => t.Description).HasMaxLength(255);
                e.ToTable("Todo");
            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges() {
            var entries = ChangeTracker.Entries<CAEntityBase>().ToList();
            entries
                .Where(e => e.State is EntityState.Added or EntityState.Modified)
                .ToList()
                .ForEach(e => {
                    if(e.State is EntityState.Added)
                        e.Entity.RowId = Guid.NewGuid();
                    e.Entity.LastUpdated = DateTime.Now;
                });
            return base.SaveChanges();
        }
    }
}
