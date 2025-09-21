namespace PTMngVSIX.Prompt.OutputPrompt
{
	internal static class OutputDefined
	{
		public static readonly string OChat01 = @"
Output format:
```[chat]
answer
```";

		public static readonly string OCode01 = @"
Output:
If the programming language cannot be determined, use C# by default.

Output format:
```[code]
generated code
```";

		public static readonly string OText01 = @"
Output format:
```[text]
answer
```";

		public static readonly string OChat02 = @"
Output:
Output must always have at least 4 header lines.
Header fields are: version, type, summary, solution, answer.
The version field always has a value of 2.
Leave the 'answer:' line blank; write the final answer below it.

Output format:
version: 2
type: chat | code | text | etc...
summary: brief summary of the request, main keywords to remember for next time
solution: summary of the solution, key steps
answer:
the final answer";

		public static readonly string OCode02 = @"
Output:
Output must always have at least 4 header lines.
Header fields are: version, type, summary, solution, answer.
The version field always has a value of 2.
Leave the 'answer:' line blank; write the final answer below it.
If the programming language cannot be determined, use C# by default.

Output format:
version: 2
type: chat | code | text | etc...
summary: brief summary of the request, main keywords to remember for next time
solution: summary of the solution, key steps
answer:
the final answer";

		public static readonly string OText02 = @"
Output:
Output must always have at least 4 header lines.
Header fields are: version, type, summary, solution, answer.
The version field always has a value of 2.
Leave the 'answer:' line blank; write the final answer below it.

Output format:
version: 2
type: chat | code | text | etc...
summary: brief summary of the request, main keywords to remember for next time
solution: summary of the solution, key steps
answer:
the final answer";

	}
}
