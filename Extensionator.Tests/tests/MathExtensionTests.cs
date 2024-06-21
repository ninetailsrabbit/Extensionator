namespace Extensionator.Tests {
    public class MathExtensionTests {

        [Fact]
        public void Should_Detect_Common_Values_Around_Zero() {
            int negative = -1;
            float decimalNegative = -10.5f;

            Assert.True(0.IsZero());
            Assert.True(0f.IsZero());
            Assert.True(negative.IsBelowZero());
            Assert.True(negative.IsNegative());
            Assert.True(decimalNegative.IsBelowZero());
            Assert.True(decimalNegative.IsNegative());
            Assert.True(20.IsNotZero());
            Assert.True(20.IsGreaterThanZero());
            Assert.True(20.5f.IsNotZero());
            Assert.True(22.3f.IsGreaterThanZero());
        }

        [Fact]
        public void Should_Normalize_Radians_Angle() {
            Assert.Equal(Math.PI / 2f, (Math.PI / 2f).NormalizeRadiansAngle());
            Assert.Equal(Math.PI, (3f * Math.PI).NormalizeRadiansAngle());

            Assert.Equal(3 * Math.PI / 2f, (-Math.PI / 2).NormalizeRadiansAngle());
            Assert.Equal(Math.PI, (-5f * Math.PI).NormalizeRadiansAngle());
        }

        [Fact]
        public void Should_Detect_Values_In_Between() {
            Assert.True(10.IsBetween(2, 10)); // Inclusive by default
            Assert.False(10.IsBetween(2, 10, false));

            Assert.True(25.4f.IsBetween(25f, 25.5f));// Inclusive by default
            Assert.True(25.4f.IsBetween(25f, 25.4f, false));
        }

        [Fact]
        public void Should_Apply_Thousand_Separator_Correctly() {
            Assert.Equal("999", 999.ThousandSeparator());
            Assert.Equal("1.200.000.000", 1200000000.ThousandSeparator("."));
            Assert.Equal("1,200,000,000", 1200000000.ThousandSeparator()); // Comma by default
            Assert.Equal("1,001", (1001).ThousandSeparator());

            Assert.Equal("-999", (-999).ThousandSeparator());
            Assert.Equal("-1.200.000.000", (-1200000000).ThousandSeparator("."));
            Assert.Equal("-1,200,000,000", (-1200000000).ThousandSeparator()); // Comma by default
            Assert.Equal("-1,001", (-1001).ThousandSeparator());

        }
    }
}
