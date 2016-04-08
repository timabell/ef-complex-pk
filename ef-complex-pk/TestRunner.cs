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
                const string widgetName = "fred";
                context.Widgets.Add(new Widget {WidgetId = 1, Name = widgetName});
                context.SaveChanges();

                var uberWidget = context.Database
                    .SqlQuery<UberWidget>("select WidgetId, Name from Widgets")
                    .First();

                Assert.AreEqual(widgetName, uberWidget.Name);
            }
        }
    }
}
