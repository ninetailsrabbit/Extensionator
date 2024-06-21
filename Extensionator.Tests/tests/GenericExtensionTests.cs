namespace Extensionator.Tests {
    public class GenericExtensionTests {

        struct Point {
            public int X;
            public int Y;
        }

        [Fact]
        public void Should_Detect_If_A_Value_Its_On_Enumerable() {
            List<Point> points = [new() { X = 1, Y = 2 }, new() { X = 3, Y = 4 }, new() { X = 5, Y = 6 }];

            Point targetPoint = new() { X = 3, Y = 4 };
            Point notIncludedPoint = new() { X = 3, Y = 40 };

            Assert.True(targetPoint.In(points));
            Assert.False(notIncludedPoint.In(points));

            Assert.True(3.In([1, 2, 3, 4]));
            Assert.False(30.In([1, 2, 3, 4]));

        }

        [Fact]
        public void Should_Detect_If_A_Value_Its_On_Params() {
            List<Point> points = [new() { X = 1, Y = 2 }, new() { X = 3, Y = 4 }, new() { X = 5, Y = 6 }];

            Point targetPoint = new() { X = 3, Y = 4 };
            Point notIncludedPoint = new() { X = 3, Y = 40 };

            Assert.True(targetPoint.In(points[0], points[1], points[2]));
            Assert.False(notIncludedPoint.In(points[0], points[1], points[2]));

            Assert.True(3.In(1, 2, 3, 4));
            Assert.False(30.In(1, 2, 3, 4));
        }
    }

}