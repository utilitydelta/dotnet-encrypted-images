using Xunit;

namespace UtilityDelta.EncryptedImages.Test
{
    public class TestScaleCalc
    {
        [Fact]
        public void Test1()
        {
            var result = ScaleCalc.Downsize(100, 20, null, 15);
            Assert.Equal(75, result.Height);
            Assert.Equal(15, result.Width);
        }

        [Fact]
        public void Test2()
        {
            var result = ScaleCalc.Downsize(100, 20, null, 20);
            Assert.Equal(100, result.Height);
            Assert.Equal(20, result.Width);
        }

        [Fact]
        public void Test3()
        {
            var result = ScaleCalc.Downsize(100, 20, null, 21);
            Assert.Equal(100, result.Height);
            Assert.Equal(20, result.Width);
        }

        [Fact]
        public void Test4()
        {
            var result = ScaleCalc.Downsize(100, 20, 60, 15);
            Assert.Equal(60, result.Height);
            Assert.Equal(12, result.Width);
        }

        [Fact]
        public void Test5()
        {
            var result = ScaleCalc.Downsize(100, 20, 10, null);
            Assert.Equal(10, result.Height);
            Assert.Equal(2, result.Width);
        }
    }
}