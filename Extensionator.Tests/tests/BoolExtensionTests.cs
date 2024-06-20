namespace Extensionator.Tests {
    public class BoolExtensionTests {

        [Fact]
        public void Should_Toggle_Boolean_Value() {
            Assert.True(false.Toggle());
            Assert.False(true.Toggle());
        }

        [Fact]
        public void Should_Convert_Boolean_Value_To_Sign() {
            Assert.Equal(-1, false.ToSign());
            Assert.Equal(1, true.ToSign());
        }

        [Fact]
        public void Should_Convert_Parse_Boolean_To_Int() {
            Assert.Equal(0, false.ToInt());
            Assert.Equal(1, true.ToInt());
        }
    }
}