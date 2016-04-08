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
                const int widgetId = 5;
                context.Widgets.Add(new Widget {WidgetId = widgetId, Name = widgetName});
                context.SaveChanges();

                var uberWidget = context.Database
                    .SqlQuery<UberWidget>("select WidgetId as UberWidgetId_IdWrapId, Name from Widgets")
                    .First();

                Assert.AreEqual(widgetId, uberWidget.UberWidgetId);
                Assert.AreEqual(widgetName, uberWidget.Name);
            }
        }
    }
}
