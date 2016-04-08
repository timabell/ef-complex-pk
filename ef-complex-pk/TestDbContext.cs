using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ef_complex_pk
{
    public class TestDbContext : DbContext
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

        /// <summary>
        /// Gets all the uber widgets.
        /// You could implement variations for your use-cases such as get by id.
        /// This is a workaround for EF refusing to allow a complex type as the primary key of an entity.
        /// If you have a lot of time on your hands you could implement IQueryable.
        /// </summary>
        public IEnumerable<UberWidget> GetUberWidgets()
        {
            return Database.SqlQuery<WidgetSqlDto>("select WidgetId, Name from Widgets")
                .Select(dto => new UberWidget
                {
                    //UberWidgetId = new IdWrap { IdWrapId = dto.WidgetId },
                    Name = dto.Name
                });
        }

        class WidgetSqlDto
        {
            public int WidgetId { get; set; }
            public string Name { get; set; }
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
        public int IdWrapId { get { return 4; } }
    }
}
