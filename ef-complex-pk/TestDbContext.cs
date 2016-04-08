using System.Data.Entity;

namespace ef_complex_pk
{
    public class TestDbContext: DbContext
    {
        public IDbSet<Widget> Widgets { get; set; }
        public IDbSet<Frobnitzer> Frobnitzers { get; set; }
        // no IDbSet for UberWidgets as you can't have a complex type as a PK in ef

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<IdWrap>();

            //modelBuilder.Entity<Widget>().HasKey(e => e.WidgetId);

            // doesn't work because it pulls the uberwiget in and barfs on lack of key, back to square one.
            //modelBuilder.Entity<UberWidget>().Property(e => e.UberWidgetId.IdWrapId).HasColumnName("WidgetId");
        }
    }

    /// <summary>
    ///  thing to get ef to build us a sql table
    /// </summary>
    public class Widget
    {
        public int WidgetId { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// just to see what ef generates as a default table structure
    /// </summary>
    public class Frobnitzer
    {
        public int Id { get; set; }
        public IdWrap ComplexPropIdWrap { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// "view" on the table, read-only
    /// </summary>
    public class UberWidget
    {
        public IdWrap UberWidgetId { get; set; }
        public string Name { get; set; }
    }

    public class IdWrap
    {
        public int IdWrapId { get; set; }
    }
}
