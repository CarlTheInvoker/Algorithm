# Backtracing

Backtracing is kind of brute force solution, the time complexity is exponential, but we can find if a solution is valid as quick as possible to avoid traversing all possible values.

What problem could be solved by backtracing?

- The solution of the problem can bo composed step-by-step.
- An operation in 1 step is valid can be decide by previous operations.

And the code of using backtracing is usually like below, and there are many OJ problems can be solved by backtracing.

```C#
void BackTracing(int step, param objects)
{
    if (solved())
    {
        // Return true if want to check if valid solution exist
        // Print the solution is want to get all solution
    }

    foreach(var value in allPossibleValues)
    {
        if (isValid(step, val))
        {
            this.ApplyValue(step, val);
            this.BackTracing(step + 1, objects);
            this.RestoreValue(step, val);
        }
    }
}
```

Related docs: [What kind of problems can be solved by backtracing?](https://www.geeksforgeeks.org/backtracking-introduction/)
