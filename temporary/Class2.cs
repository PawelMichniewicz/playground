namespace temporary
{
    public class Solution
    {

        private class DataPair
        {
            public int Value { get; set; } = -1;
            public int Index { get; set; } = -1;

            public override int GetHashCode()
            {
                return this.Value.GetHashCode();
            }
        }

        public int[] TwoSum(int[] nums, int target)
        {
            HashSet<int> hs = new(nums);
            for (int i = 0; i < nums.Length; ++i)
            {
                var temp = target - nums[i];
                if (hs.Contains(temp))
                {
                    int j = Array.IndexOf(nums, temp, i + 1);
                    if (j != -1)
                    {
                        return [i, j];
                    }
                }
            }

            return [-1, -1];
        }
    }
}
