namespace Extensionator.Tests {
    public class ListExtensionTests {

        [Fact]
        public void Should_Map_Enumerable_With_Index() {
            var people = new List<string>() { "john", "esmeralda", "dragon", "powerlifter" };

            foreach ((string name, int index) in people.WithIndex()) {
                Assert.Contains(name, people);
                Assert.Equal(name, people[index]);
            }
        }

        [Fact]
        public void Should_Return_The_Diff_From_Two_Enumerables() {
            var people = new List<string>() { "john", "esmeralda", "dragon", "powerlifter" };
            var otherPeople = new List<string>() { "john", "esmeralda", "fox", "crossfitero" };

            var diff = people.Diff(otherPeople);

            Assert.Equal(2, diff.Count);
            Assert.Contains("dragon", diff);
            Assert.Contains("powerlifter", diff);
        }

        [Fact]
        public void Should_Remove_And_Return_First_Element_With_Pop_Front_Functions() {
            var people = new List<string>() { "john", "esmeralda", "dragon", "powerlifter" };
            int originalSize = people.Count;

            var item = people.PopFront();
            Assert.Equal("john", item);
            Assert.Equal(originalSize - 1, people.Count);

            item = people.PopFront();
            Assert.Equal("esmeralda", item);
            Assert.Equal(originalSize - 2, people.Count);

        }

        [Fact]
        public void Should_Remove_And_Return_First_Element_With_Pop_Back_Functions() {
            var people = new List<string>() { "john", "esmeralda", "dragon", "powerlifter" };
            int originalSize = people.Count;

            var item = people.PopBack();
            Assert.Equal("powerlifter", item);
            Assert.Equal(originalSize - 1, people.Count);

            item = people.PopBack();
            Assert.Equal("dragon", item);
            Assert.Equal(originalSize - 2, people.Count);

        }

        [Fact]
        public void Should_Remove_And_Return_Element_On_Selected_Index_With_Pop_At_Functions() {
            var people = new List<string>() { "john", "esmeralda", "dragon", "powerlifter" };
            int originalSize = people.Count;

            var item = people.PopAt(1);
            Assert.Equal("esmeralda", item);
            Assert.Equal(originalSize - 1, people.Count);

            item = people.PopAt(1);
            Assert.Equal("dragon", item);
            Assert.Equal(originalSize - 2, people.Count);
        }

        [Fact]
        public void Should_Retrieve_The_Last_Index_From_An_Enumerable() {
            var people = new List<string>() { "john", "esmeralda", "dragon", "powerlifter" };
            int[] numbers = [1, 2, 3, 4, 5];
            int[] empty = [];

            Assert.Equal(people.Count - 1, people.LastIndex());
            Assert.Equal(numbers.Length - 1, numbers.LastIndex());

            // -1 when it's empty
            Assert.Equal(-1, empty.LastIndex());
        }

        [Fact]
        public void Should_Get_Middle_Element() {
            int[] numbers = [1, 2, 3, 4, 5];
            int[] numbers2 = [1, 2, 4, 5];
            int[] numbers3 = [1, 2];

            Assert.Equal(3, numbers.MiddleElement());
            Assert.Equal(2, numbers2.MiddleElement());

            Assert.Throws<ArgumentException>(() => numbers3.MiddleElement());
        }

        [Fact]
        public void Should_Filter_Enumerable_String_Without_Empty_Values() {
            string[] names = ["zeus", "", "thor", "loki", "", ""];

            Assert.Equal(6, names.Length);

            var filteredNames = names.WhereNotEmpty();

            Assert.Equal(3, filteredNames.Count());
            Assert.True(filteredNames.All(name => !string.IsNullOrEmpty(name)));
        }

        [Fact]
        public void Should_Calculate_The_Mean_From_Int_Enumerable() {
            int[] numbers = [1, 2, 3, 4, 5];

            Assert.Equal(0, Array.Empty<int>().Mean());
            Assert.Equal(3, numbers.Mean());
            Assert.Equal(3, numbers.Average());
        }

        [Fact]
        public void Should_Calculate_The_Mean_From_Float_Enumerable() {
            List<float> numbers = [3.14f, 2.72f, 1.6f, 4.5f, 9.2f];

            Assert.Equal(0, Array.Empty<int>().Mean());
            Assert.Equal(4.23199987f, numbers.Mean());
            Assert.Equal(4.23199987f, numbers.Average());
        }

        [Fact]
        public void Should_Get_Random_Value_From_A_List() {
            string[] names = ["zeus", "thor", "loki", "atenea", "gaia"];

            foreach (var _ in Enumerable.Range(1, 50)) {
                string name = names.RandomElement();

                Assert.Contains(name, names);
            }

            var random = new Random(42);

            foreach (var _ in Enumerable.Range(1, 50)) {
                string name = names.RandomElementUsing(random);

                Assert.Contains(name, names);
            }

        }

        [Fact]
        public void Should_Get_Random_Values_From_A_List() {
            string[] names = ["zeus", "thor", "loki", "atenea", "gaia"];

            foreach (var _ in Enumerable.Range(1, 25)) {
                var items = names.RandomElements(2);

                Assert.Equal(2, items.Count());
                Assert.True(items.All(item => names.Contains(item)));
            }

            var random = new Random(42);

            foreach (var _ in Enumerable.Range(1, 25)) {
                var items = names.RandomElementsUsing(3, random);

                Assert.Equal(3, items.Count());
                Assert.True(items.All(item => names.Contains(item)));
            }
        }

        [Fact]
        public void Should_Detect_The_Frequency_Of_An_Element_From_An_Enumerable() {
            int[] numbers = [1, 1, 3, 6, 6, 6, 6, 8, 9];

            Assert.Equal(2, numbers.FrequencyOf(1));
            Assert.Equal(1, numbers.FrequencyOf(3));
            Assert.Equal(4, numbers.FrequencyOf(6));
        }

        [Fact]
        public void Should_Transform_All_Strings_To_Lower() {
            string[] names = ["ZeUS", "THOR", "LOKI", "atENEA", "GAIa"];


            foreach (var name in names.ToLower())
                Assert.True(name.All(char.IsLower));

        }

        [Fact]
        public void Should_Transform_All_Strings_To_Upper() {
            string[] names = ["zeus", "thor", "loki", "atenea", "gaia"];

            foreach (var name in names.ToUpper())
                Assert.True(name.All(char.IsUpper));
        }

        [Fact]
        public void Should_Divide_An_Enumerable_Into_Chunks_Of_Desired_Size() {
            Assert.Empty(Enumerable.Empty<int>().ChunkBy(2));

            var sequence = new List<string> { "a", "b", "c", "d", "e", "f" };
            var chunks = sequence.ChunkBy(2);

            Assert.Equal(3, chunks.Count);
            Assert.Equal(["a", "b"], chunks[0]);
            Assert.Equal(["c", "d"], chunks[1]);
            Assert.Equal(["e", "f"], chunks[2]);

            var sequence2 = new List<int> { 1, 2, 3, 4, 5 };
            var chunks2 = sequence2.ChunkBy(2);

            Assert.Equal(3, chunks.Count);
            Assert.Equal([1, 2], chunks2[0]);
            Assert.Equal([3, 4], chunks2[1]);
            Assert.Equal([5], chunks2[2]);
        }

        [Fact]
        public void Should_Create_List_With_One_Item() {
            Assert.Equal(["hello"], "hello".Only());
            Assert.Equal([2000], 2000.Only());
            Assert.Equal([""], "".Only());

        }
    }
}
