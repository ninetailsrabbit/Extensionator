
namespace Extensionator.Tests {
    public class DictionaryExtensionTests {

        [Fact]
        public void Should_Merge_Two_Dictionaries() {
            Dictionary<string, string> source = new() { { "Superman", "Flight" } };
            Dictionary<string, string> target = new() { { "Batman", "Gadgets" } };

            var result = source.Merge(target);

            Assert.True(result.ContainsAllKeys([.. source.Keys, .. target.Keys]));
        }


        [Fact]
        public void Should_Invert_Key_Value_On_Dictionary() {
            Dictionary<string, string> source = new() { { "Superman", "Flight" }, { "Batman", "Gadgets" } };

            var result = source.InvertKeyValue();

            Assert.Contains(["Flight", "Gadgets"], result.Keys.ToArray());
            Assert.Contains(["Superman", "Batman"], result.Values.ToArray());

            result = result.InvertKeyValue();

            Assert.Contains(["Superman", "Batman"], result.Keys.ToArray());
            Assert.Contains(["Flight", "Gadgets"], result.Values.ToArray());
        }


        [Fact]
        public void Should_Reverse_Lookup_Key_By_Value() {
            Dictionary<string, int> dimensions = new() { { "length", 10 }, { "width", 20 }, { "height", 30 }, { "deep", 30 } };

            Assert.Null(dimensions.ReverseLookup(5));
            Assert.Equal("length", dimensions.ReverseLookup(10));
            Assert.Equal("width", dimensions.ReverseLookup(20));
            Assert.Contains(["height", "deep"], dimensions.ReverseLookupMultiple(30).ToArray());
            Assert.Empty(dimensions.ReverseLookupMultiple(300));
        }

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

        [Fact]
        public void Should_Detect_Keys_On_Dictionary() {
            Dictionary<string, int> people = new() { { "almudena", 30 }, { "maximiliano", 45 }, { "eusebio", 30 } };

            Assert.True(people.ContainsAnyKey(["almudena", "superman", "power ranger"]));
            Assert.True(people.ContainsAnyKey(["almudena", "maximiliano"]));
            Assert.False(people.ContainsAnyKey(["titan", "platanito"]));

            Assert.True(people.ContainsAllKeys(["maximiliano"]));
            Assert.True(people.ContainsAllKeys(["maximiliano", "eusebio"]));
            Assert.True(people.ContainsAllKeys(["almudena", "maximiliano", "eusebio"]));
            Assert.False(people.ContainsAllKeys(["john doe", "almudena", "maximiliano", "eusebio"]));
        }
    }
}
