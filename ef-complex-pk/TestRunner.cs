using System.Data.Entity;
using System.Linq;
using NUnit.Framework;

namespace ef_complex_pk
{
    [TestFixture]
    public class TestRunner
    {
        [Test]
        public void RunIt()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<TestDbContext>());
            using (var context = new TestDbContext())
            {
                context.Widgets.Count();
            }
        }
    }
}
