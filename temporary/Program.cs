using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class MainClass
{
    private readonly struct RomanDigit
    {
        public int Arabic { get; }
        public char Roman { get; }
        public RomanDigit(int d, char c)
        {
            Arabic = d;
            Roman = c;
        }
    }

    private static readonly List<RomanDigit> mapping = new()
    {
        new RomanDigit( 1000, 'M'),
        new RomanDigit( 500, 'D'),
        new RomanDigit( 100, 'C'),
        new RomanDigit( 50, 'L'),
        new RomanDigit( 10, 'X'),
        new RomanDigit( 5, 'V'),
        new RomanDigit( 1, 'I')
    };

    public static string StringChallenge(string str)
    {
        return Romanize(Calculate(str));
    }

    private static string Romanize(int value)
    {
        StringBuilder builder = new();

        while (value > 0)
        {
            foreach (var v in mapping)
            {
                if (value >= v.Arabic)
                {
                    builder.Append(v.Roman);
                    value -= v.Arabic;
                    break;
                }
            }
        }

        return builder.ToString();
    }


    private static int Calculate(string str)
    {
        return str.Select(c => mapping.First(x => x.Roman == c).Arabic).Sum();
    }

    static void Main()
    {
        // keep this function call here
        Console.WriteLine(StringChallenge("DDDXXXXXXVVVIIIIIIIIIIIIII"));
        //Console.WriteLine(StringChallenge(Console.ReadLine()));
    }

}
