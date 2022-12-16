namespace MissingElement
{
    public class LongArray
    {
        private readonly int size;
        private readonly int min;
        private readonly int max;
        private int[] collection;

        public LongArray(int maxValue) : this(maxValue, Array.Empty<int>())
        {
            Random rng = new(DateTime.Now.Microsecond);
            collection = Enumerable.Range(min, max).OrderBy(x => rng.Next()).Take(size).ToArray();
        }

        public LongArray(int maxValue, int[] collection)
        {
            min = 1;
            max = maxValue;
            size = maxValue - 1;

            this.collection = collection;
        }

        public int DifferenceOfSums()
        {
            long sumInCollection = collection.Select(x => (long)x).Sum();
            long sumOfRange = (min + max) * max / 2;

            print();

            return (int)(sumOfRange - sumInCollection);
        }

        public int IndexJumping()
        {
            int result = max;
            int j = 0;
            int t = 1;

            bool maxPresent = false;

            for (int i = 0; i < size; i++)
            {
                j = i;
                if (maxPresent)
                {
                    
                }
                else
                {

                }
                while (collection[j] != j + 1)
                {
                    //print();
                    t = collection[j];
                    collection[j] = j + 1;
                    j = t - 1;
                    if (collection[j] == max)
                    {
                        maxPresent = true;
                        break;
                    }
                };
            }

            return result;
        }

        private void print()
        {
            Console.Write("{");
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{collection[i]},");
            }
            Console.WriteLine("}");

        }
    }
}