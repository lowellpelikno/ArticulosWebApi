using Microsoft.EntityFrameworkCore;

namespace ArticulosWebApi.Model;

public partial class PruebasApiCoreContext : DbContext
{
    public PruebasApiCoreContext()
    {
    }

    public PruebasApiCoreContext(DbContextOptions<PruebasApiCoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PruebasApiCore.mssql.somee.com;Database=PruebasApiCore;Persist Security Info=True;User ID=jifv_SQLLogin_1; Password=yts3feciz6; MultipleActiveResultSets=True;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__articulo__3214EC07F3D67F57");

            entity.ToTable("articulos");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(8, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
