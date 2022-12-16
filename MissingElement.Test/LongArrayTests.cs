namespace MissingElement.Test
{
    public class LongArrayTests
    {
        [Fact]
        public void Test1()
        {
            int[] testData = { 4, 1, 5, 3 };
            LongArray uat = new LongArray(5, testData);
            Assert.True(uat.DifferenceOfSums() == 2);
        }

        [Fact]
        public void Test2()
        {
            int[] testData = { 4, 1, 5, 3 };
            LongArray uat = new LongArray(5, testData);
            Assert.True(uat.IndexJumping() == 2);
        }
    }
}