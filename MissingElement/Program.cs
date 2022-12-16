namespace MissingElement
{
    // array with size of N
    // shuffled non repeating values with max at N+1
    // one value is missing. find which one
    // EXAMPLE:
    // array size 10, idexes 0 - 9
    // values range: 1 - 10
    // array: {9, 1, 8, 10, 3, 6, 5, 7, 4} - 2 is missing
    internal class Program
    {
        static void Main(string[] args)
        {
            LongArray temp = new LongArray(10);
            Console.WriteLine(temp.DifferenceOfSums());

            Console.WriteLine(temp.IndexJumping());
        }
    }
}