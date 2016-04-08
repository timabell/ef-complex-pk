using System.Data.Entity;

namespace ef_complex_pk
{
    public class TestDbContext: DbContext
    {
        public IDbSet<Widget> Widgets { get; set; }
        // no IDbSet for UberWidgets as you can't have a complex type as a PK in ef

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<IdWrap>();
            //modelBuilder.Entity<Widget>().HasKey(e => e.WidgetId);
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
