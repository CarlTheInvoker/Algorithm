namespace OjProblems.LeetCode
{
    using System.Collections.Generic;

    internal class Problem0722
    {
        public class Solution
        {
            private int index = 0;
            private string[] input;

            public IList<string> RemoveComments(string[] source)
            {
                this.input = source;
                List<string> result = new List<string>();

                while (this.index < this.input.Length)
                {
                    string next = this.RemoveCommentsNotInBlockComment();
                    if (!string.IsNullOrEmpty(next))
                    {
                        result.Add(next);
                    }
                }

                return result;
            }

            private string RemoveCommentsNotInBlockComment()
            {
                if (this.index < this.input.Length)
                {
                    string result;
                    int startIndexOfLineComment = this.input[this.index].IndexOf("//");
                    int startIndexOfBlockComment = this.input[this.index].IndexOf("/*");

                    if (startIndexOfLineComment == -1 && startIndexOfBlockComment == -1)
                    {
                        result = this.input[this.index];
                        this.index++;
                        return result;
                    }

                    if (startIndexOfBlockComment == -1 ||
                        (startIndexOfLineComment != -1 && startIndexOfLineComment < startIndexOfBlockComment))
                    {
                        result = this.input[this.index].Substring(0, startIndexOfLineComment);
                        this.index++;
                        return result;
                    }

                    result = this.input[this.index].Substring(0, startIndexOfBlockComment);
                    this.input[this.index] = this.input[this.index].Substring(startIndexOfBlockComment + 2);
                    result += this.RemoveCommentInBlockComment();
                    return result;
                }

                return string.Empty;
            }

            private string RemoveCommentInBlockComment()
            {
                while (this.index < this.input.Length)
                {
                    int endIndexOfEndComment = this.input[this.index].IndexOf("*/");
                    if (endIndexOfEndComment != -1)
                    {
                        this.input[this.index] = this.input[this.index].Substring(endIndexOfEndComment + 2);
                        return this.RemoveCommentsNotInBlockComment();
                    }

                    this.index++;
                }

                return string.Empty;
            }
        }
    }
}
