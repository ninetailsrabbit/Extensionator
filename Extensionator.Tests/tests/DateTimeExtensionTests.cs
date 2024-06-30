using Moq;

namespace Extensionator.Tests {
    public interface IDateTimeHelper {
        DateTime GetDateTimeNow();
    }

    public class DateTimeHelper : IDateTimeHelper {
        public DateTime GetDateTimeNow() => DateTime.Now;
    }

    public class DateTimeExtensionTests {
        [Fact]
        public void Should_Add_Weeks_Correctly() {
            var today = DateTime.Now;
            var currentDate = DateTime.Now;

            var dateModified = today.AddWeeks(1);

            Assert.Equal(currentDate.AddDays(7).Day, dateModified.Day);
        }

        [Fact]
        public void Should_Calculate_Age_From_Specific_Year() {
            var mock = new Mock<IDateTimeHelper>();
            mock.Setup(fake => fake.GetDateTimeNow()).Returns(new DateTime(2021, 10, 10));

            var helper = new DateTimeHelper();

            DateTime date = new(1991, 10, 20);

            Assert.Equal(33, date.Age());
        }
    }
}
