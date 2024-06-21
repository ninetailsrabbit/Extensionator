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

        [Fact]
        public void Should_Calculate_Factorial_From_Number() {
            Assert.Equal(1, 0.Factorial());
            Assert.Equal(1, 1.Factorial());
            Assert.Equal(2, 2.Factorial());

            Assert.Equal(6, 3.Factorial());
            Assert.Equal(120, 5.Factorial());
            Assert.Equal(40320, 8.Factorial());
            Assert.Equal(40320, 8.Factorial());
            Assert.Equal(362880, 9.Factorial());
            Assert.Equal(3628800, 10.Factorial());
        }

        [Fact]
        public void Should_Calculate_Factorial_Sequence_From_Number() {
            Assert.Equal([1], 0.FactorialsFrom());
            Assert.Equal([1], 1.FactorialsFrom());
            Assert.Equal([1, 1, 2, 6, 24, 120], 5.FactorialsFrom());
            Assert.Equal([1, 1, 2, 6, 24, 120, 720, 5040, 40320], 8.FactorialsFrom());
        }

        [Fact]
        public void Should_Transform_Number_To_Ordinal_Representation() {
            Assert.Equal("1st", 1.ToOrdinal());
            Assert.Equal("2nd", 2.ToOrdinal());
            Assert.Equal("3rd", 3.ToOrdinal());
            Assert.Equal("4th", 4.ToOrdinal());
            Assert.Equal("21st", 21.ToOrdinal());  // "st" for teens (11-19)
            Assert.Equal("32nd", 32.ToOrdinal());  // "nd" for tens ending in 2 (20-29)
            Assert.Equal("43rd", 43.ToOrdinal());  // "rd" for tens ending in 3 (30-39)
            Assert.Equal("54th", 54.ToOrdinal());  // "th" for others
            Assert.Equal("101st", 101.ToOrdinal()); // Special case for hundreds ending in "01"
            Assert.Equal("111th", 111.ToOrdinal()); // Special case for hundreds ending in "11"
            Assert.Equal("212th", 212.ToOrdinal()); // Regular case for hundreds
        }

        [Fact]
        public void Should_Format_Number_To_Pretty() {
            Assert.Equal("123", 123f.PrettyNumber());
            Assert.Equal("1.23K", 1234f.PrettyNumber());
            Assert.Equal("1.23M", 1234567f.PrettyNumber());
            Assert.Equal("1.23B", 1234567890f.PrettyNumber());
            Assert.Equal("1.23T", 1234567890123f.PrettyNumber());

            Assert.Equal("-123", (-123f).PrettyNumber());
            Assert.Equal("-1.23K", (-1234f).PrettyNumber());
            Assert.Equal("-1.23M", (-1234567f).PrettyNumber());
            Assert.Equal("-1.23B", (-1234567890f).PrettyNumber());
            Assert.Equal("-1.23T", (-1234567890123f).PrettyNumber());

            string[] customSuffixes = ["", "mio", "giga", "tera"];
            Assert.Equal("123", 123f.PrettyNumber(customSuffixes));
            Assert.Equal("1.23mio", 1234f.PrettyNumber(customSuffixes));
            Assert.Equal("1.23giga", 1234567f.PrettyNumber(customSuffixes));
            Assert.Equal("1.23tera", 1234567890f.PrettyNumber(customSuffixes));

            Assert.Equal("1.23K", (1234.567f).PrettyNumber());
            Assert.Equal("1M", (999999.999f).PrettyNumber());
        }

        [Fact]
        public void Should_Convert_A_Number_To_Its_Binary_Representation() {
            Assert.Equal("0", 0.ToBinary());
            Assert.Equal("1", 1.ToBinary());
            Assert.Equal("10", 2.ToBinary());
            Assert.Equal("101", 5.ToBinary());
            Assert.Equal("1100", 12.ToBinary());
            Assert.Equal("11111111", 255.ToBinary()); // Max value for a byte

            Assert.Throws<OverflowException>(() => (-1).ToBinary()); // Negative overflow
        }

        [Fact]
        public void Should_Transform_Number_Into_Time_Representation() {
            Assert.Equal("02:03", 123.456f.ToFormattedSeconds());
            Assert.Equal("02:03:45", 123.456f.ToFormattedSeconds(true)); // Include milliseconds
        }
    }
}
