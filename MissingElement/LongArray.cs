namespace MissingElement
{
    internal class LongArray//<T> where T : IComparable
    {
        private readonly int size;
        private readonly int min;
        private readonly int max;
        private int[] collection;

        public LongArray(int maxValue)
        {
            min = 1;
            max = maxValue;
            this.size = maxValue - 1;

            Random rng = new(DateTime.Now.Microsecond);
            collection = Enumerable.Range(min, max).OrderBy(x => rng.Next()).Take(size).ToArray();
        }

        public void Foo()
        {
            int i = 0;
            int t = 1;
            do
            {
                print();
                t = collection[i];
                collection[i] = i + 1;
                i = t - 1;
            } while (true);
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