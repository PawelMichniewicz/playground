using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

class Result
{

    /*
     * Complete the 'timeConversion' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING s as parameter.
     */

    public static int lonelyinteger(List<int> a)
    {
        List<int> temp2 = Enumerable.Repeat(0, 100).ToList();
        //temp2.Reverse()
        var temp = a.Distinct();

        return 0;
    }

    public static string timeConversion(string s)
    {
        var result = string.Empty;

        bool isPM = s.Contains("PM");
        var temp = s.Replace("PM", string.Empty).Replace("AM", string.Empty);
        var split = temp.Split(':');

        var h = split[0];
        var m = split[1];
        var sec = split[2];

        int.TryParse(h, out int hours);
        if (isPM)
        {
            if (hours != 12)
                h = (hours + 12).ToString();
        }
        else
        {
            if (hours == 12)
                h = "00";
        }

        return $"{h}:{m}:{s}";
    }

}

#nullable enable



class Solution
{

    public static List<int> breakingRecords(List<int> scores)
    {
        int maxRecord = scores[0];
        int minRecord = scores[0];
        int maxRecordCount = 0;
        int minRecordCount = 0;
        foreach (var item in scores)
        {
            if (item > maxRecord)
            {
                maxRecord = item;
                maxRecordCount++;
                continue;
            }

            if (item < minRecord)
            {
                minRecord = item;
                minRecordCount++;
                continue;
            }
        }

        // return new List<int>() {maxRecordCount, minRecordCount};
        return [maxRecordCount, minRecordCount];
    }

    public static void Main(string[] args)
    {
        int year = 1910;
        switch (year)
        {
            case 1918:
                Console.WriteLine("trasition");
                break;
            case < 1918:
                Console.WriteLine("julian");
                break;
            case > 1918:
                Console.WriteLine("gerg");
                break;
        }

        DateTime programmersDay = new DateTime(2000, 1, 1, new JulianCalendar());
        programmersDay.AddDays(254);
        var t = programmersDay.ToString("dd.MM.yyyy");

        Console.WriteLine(t);


        Console.WriteLine();

        //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        //string s = Console.ReadLine();

        //string result = Result.timeConversion(s);

        //textWriter.WriteLine(result);

        //textWriter.Flush();
        //textWriter.Close();
    }
}
