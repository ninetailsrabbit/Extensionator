namespace Extensionator.Tests.tests {
    public class DictionaryExtensionTests {

        [Fact]
        public void Should_Add_Range_Key_Value_To_Existing_Dictionary() {
            Dictionary<string, int> people = new() { { "almudena", 30 }, { "maximiliano", 45 } };

            people.AddRange(new Dictionary<string, int>() { { "lolo", 25 }, { "lola", 50 } });

            Assert.Equal(4, people.Count);
        }

        [Fact]
        public void Should_Add_Or_Update_Value_On_Dictionary() {
            Dictionary<string, int> people = new() { { "almudena", 30 }, { "maximiliano", 45 } };

            people.AddOrUpdate("maximiliano", 55);

            // Should update the maximiliano value and not add new item
            Assert.Equal(2, people.Count);
            Assert.Equal(55, people["maximiliano"]);

            people.AddOrUpdate("eusebio", 30);

            // Should add the new item as it does not exist
            Assert.Equal(3, people.Count);
            Assert.Equal(30, people["eusebio"]);

        }
    }
}
