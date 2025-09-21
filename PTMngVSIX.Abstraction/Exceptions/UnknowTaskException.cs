using System;

namespace PTMngVSIX.Abstraction.Exceptions
{
	public class UnknowTaskException : Exception
	{
		public UnknowTaskException()
		{
		}

		public UnknowTaskException(string message) : base(message)
		{
		}
	}
}
