using System.Data.Entity;

namespace ef_complex_pk
{
    public class TestDbContext: DbContext
    {
        public IDbSet<Widget> Widgets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<IdWrap>();
            //modelBuilder.Entity<Widget>().HasKey(e => e.WidgetId);
        }
    }

    public class Widget
    {
        public IdWrap WidgetId { get; set; }
        public string Name { get; set; }
    }

    public class IdWrap
    {
        public int IdWrapId { get; set; }
    }
}
