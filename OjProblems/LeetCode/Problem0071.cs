namespace OjProblems.LeetCode
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Problem0071
    {
        public class Solution
        {
            public string SimplifyPath(string path)
            {
                Stack<string> stack = new Stack<string>();
                foreach (string str in path.Split("/"))
                {
                    if (str == "." || str == string.Empty)
                    {
                        continue;
                    }

                    if (str == "..")
                    {
                        if (stack.Any())
                        {
                            stack.Pop();
                        }
                    }
                    else
                    {
                        stack.Push(str);
                    }
                }

                string result = "";
                while (stack.Any())
                {
                    result = "/" + stack.Pop() + result;
                }

                if (result == "")
                {
                    result = "/";
                }

                return result;
            }
        }
    }
}
