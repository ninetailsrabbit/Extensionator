namespace Extensionator.Tests {
    public class RandomExtensionTests {

        private readonly Random _rng = new(42);

        [Fact]
        public void Should_Generate_A_Float_Between_The_Range_Provided() {

            foreach (var _ in Enumerable.Range(0, 100)) {
                var value = _rng.NextFloat(1, 10);

                Assert.True(value >= 1 && value <= 10);
            }
        }

        [Fact]
        public void Should_Generate_A_Valid_Random_Angle() {
            foreach (var _ in Enumerable.Range(0, 100)) {
                var angle = _rng.NextAngle();

                Assert.True(angle >= 0 && angle <= Math.Tau);
            }
        }

        [Fact]
        public void Should_Retrieve_One_Random_Value_From_Provided() {
            int[] numbers = [1, 100, 90, 39, 86];

            foreach (var _ in Enumerable.Range(0, 50)) {
                var result = _rng.OneOf(numbers);

                Assert.Contains(result, numbers);
            }

            // Using params
            int[] numbers2 = [1, 2, 4, 50, 1000, 900];

            foreach (var _ in Enumerable.Range(0, 50)) {
                var result = _rng.OneOf(1, 2, 4, 50, 1000, 900);

                Assert.Contains(result, numbers2);
            }
        }

    }
}
