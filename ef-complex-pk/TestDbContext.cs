using System.Data.Entity;

namespace ef_complex_pk
{
    public class TestDbContext: DbContext
    {
        public IDbSet<Widget> Widgets { get; set; }
    }

    public class Widget
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
