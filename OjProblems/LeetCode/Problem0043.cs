using System.Linq;
using System.Reflection.Metadata;
using System.Text;

public class Solution
{
    public string Multiply(string num1, string num2)
    {
        if (num1 == "0" || num2 == "0")
        {
            return "0";
        }

        var a = num1.Reverse().ToList();
        var b = num2.Reverse().ToList();
        int[] res = new int[num1.Length + num2.Length];
        for (int i = 0; i < res.Length; ++i)
        {
            res[i] = 0;
        }

        for (int i = 0; i < a.Count(); ++i)
        {
            for (int j = 0; j < b.Count(); ++j)
            {
                int k = i + j;
                res[k] += ConvertChar(a[i]) * ConvertChar(b[j]);
                res[k + 1] += res[k] / 10;
                res[k] %= 10;
            }
        }

        string str = string.Join("", res.Reverse());
        return str.TrimStart('0');
    }

    private int ConvertChar(char c)
    {
        return (int) (c - '0');
    }
}