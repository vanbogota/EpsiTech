namespace SearchElementInArray
{
    
    public class Program
    {
        //Если при проверке захочется запустить приложение
        static void Main(string[] args)
        {

            int[] sortedArray = { };
            int target = 0;

            Console.WriteLine(SearchElementInSortedArray(sortedArray, target));            
        }

        /// <summary>
        /// Функция для поиска элемента в отсортированном массиве
        /// </summary>
        /// <param name="sortedArray"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int SearchElementInSortedArray(int[] sortedArray, int target)
        {
            if (sortedArray.Length == 0)
            {
                throw new ArgumentNullException("Введен пустой массив.");
            }

            int leftIndex = 0;
            int rightIndex = sortedArray.Length-1;
            
            while (leftIndex <= rightIndex)
            {
                int middleIndex = leftIndex + (rightIndex - leftIndex)/2;
                
                if (sortedArray[middleIndex] == target)
                {
                    return middleIndex;
                }

                if (sortedArray[middleIndex] < target)
                {
                    leftIndex = middleIndex + 1;
                }
                else
                {
                    rightIndex = middleIndex - 1;
                }
            }
            return -1;
        } 
    }
}
