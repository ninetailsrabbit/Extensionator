namespace Extensionator.Tests {
    public class EnumExtensionTests {

        public enum STATES {
            PATROL,
            IDLE,
            ATTACK,
            ESCAPE,
            HIDE
        }


        [Fact]
        public void Should_Get_A_Random_Enum_Value_All_The_Time_Is_Randomized_With_EnumExtension_Class() {

            foreach (var _ in Enumerable.Range(1, 50)) {
                var value = EnumExtension.RandomEnum<STATES>();

                Assert.True(Enum.IsDefined(typeof(STATES), value));
            }
        }

        [Fact]
        public void Should_Get_A_Random_Enum_Value_All_The_Time_Is_Randomized_With_Value_Extension_Method() {

            foreach (var _ in Enumerable.Range(1, 50)) {
                var value = STATES.PATROL.RandomEnum(); ;

                Assert.True(Enum.IsDefined(typeof(STATES), value));
            }
        }

        [Fact]
        public void Should_Convert_A_String_Into_A_Enum_Value() {
            string value = "paTrol";

            Assert.Throws<ArgumentException>(() => value.ToEnum<STATES>());

            value = "PATROL";
            Assert.Equal(STATES.PATROL, value.ToEnum<STATES>());

            value = "JUMP";
            Assert.Equal(STATES.IDLE, value.ToEnumOrDefault(STATES.IDLE));
        }

    }
}
