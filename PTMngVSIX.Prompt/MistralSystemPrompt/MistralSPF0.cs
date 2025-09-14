namespace PTMngVSIX.Prompt.MistralSystemPrompt
{
	public class MistralSPF0
	{
		public static readonly string SP0001_Default = @"You are an AI assistant specialized in software development.";

		public static readonly string SP0002_Restrict = @"You must identify the task to be performed, such as chat, generate_code, etc.
If no specific task is detected, engage in general conversation.
Then, execute only that task and return the result in the required format, without adding anything else, without repetition, without expanding or explaining further.";

	}
}
