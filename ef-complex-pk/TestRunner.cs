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
                // avoid #1 in the generated db ids to make tests fail if they end up with default int value by adding an extra record first
                context.Widgets.Add(new Widget { Name = "bob" });
                context.SaveChanges();

                const string widgetName = "fred";
                var insertedWidget = new Widget { Name = widgetName };
                context.Widgets.Add(insertedWidget);
                context.SaveChanges();
                var insertedWidgetId = insertedWidget.WidgetId;
                Assert.AreEqual(2, insertedWidgetId);

                var uberWidgets = context.GetUberWidgets().ToList();
                var fredWidget = uberWidgets.Single(w => w.UberWidgetId.IdWrapId == 2);

                Assert.AreEqual(widgetName, fredWidget.Name);
                Assert.AreEqual(insertedWidgetId, fredWidget.UberWidgetId.IdWrapId);
            }
        }
    }
}
