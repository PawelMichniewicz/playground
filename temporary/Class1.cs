using System;
using System.Collections.Generic;
using System.Text;

class MainClass2
{

    public static string StringChallenge(string str)
    {
        int val = Calculate(str);
        Console.WriteLine($"str: {str}, value {val}");
        string betterRoman = Romanize(val);
        return betterRoman;
    }

    private static string Romanize(int value)
    {
        Console.WriteLine($"value: {value}");
        StringBuilder roman = new StringBuilder();
        do
        {
            if (value > 1000)
            {
                value = value - 1000;
                roman.Append('M');
                continue;
            }
            if (value > 500)
            {
                value = value - 500;
                roman.Append('D');
                continue;
            }
            if (value > 100)
            {
                value = value - 100;
                roman.Append('C');
                continue;
            }
            if (value > 50)
            {
                value = value - 50;
                roman.Append('L');
                continue;
            }
            if (value > 10)
            {
                value = value - 10;
                roman.Append('X');
                continue;
            }
            if (value > 5)
            {
                value = value - 5;
                roman.Append('V');
                continue;
            }
            if (value > 1)
            {
                value = value - 1;
                roman.Append('I');
                continue;
            }
        } while (value > 0);

        return roman.ToString();
    }

    private static int Calculate(string str)
    {
        Dictionary<char, int> mapping = new Dictionary<char, int>()
    {
      {'I', 1},
      {'V', 5},
      {'X', 10},
      {'L', 50},
      {'C', 100},
      {'D', 500},
      {'M', 1000}
    };

        int result = 0;
        foreach (var c in str)
        {
            result += mapping[c];
        }
        return result;
    }

    static void Main2()
    {
        // keep this function call here
        Console.WriteLine(StringChallenge("XXXVVIIIIIIIIII"));
        //Console.WriteLine(StringChallenge(Console.ReadLine()));
    }

}