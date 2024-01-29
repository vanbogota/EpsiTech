using SearchElementInArray;

namespace _SearchElementInArray.Tests
{
    public class ProgramTests
    {
        int[] sortedArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        [Fact]
        public void SearchElementInSortedArray_ElementExists_ReturnsCorrectIndex()
        {
            // Arrange
            int target = 7;

            //Act
            int result = Program.SearchElementInSortedArray(sortedArray, target);

            //Assert
            Assert.Equal(6, result);
        }

        [Fact]
        public void SearchElementInSortedArray_ElementDoesNotExist_ReturnsMinusOne()
        {
            // Arrange
            int target = 11;

            //Act
            int result = Program.SearchElementInSortedArray(sortedArray, target);

            //Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void SearchElementInSortedArray_EmptyArray_ThrowsArgumentNullException()
        {
            // Arrange
            int[] emptyArray = Array.Empty<int>();
            int target = 5;

            //Act, Assert
            Assert.Throws<ArgumentNullException>(() => Program.SearchElementInSortedArray(emptyArray, target));
        }
    }
}