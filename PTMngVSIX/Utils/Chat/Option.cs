namespace PTMngVSIX.Utils.Chat
{
	public class Option
	{
		public bool IncludeDocumentLanguage { get; set; }
		public bool IncludeSolutionStructure { get; set; }
		public bool IncludeProjectStructure { get; set; }
		public bool IncludeActiveDocument { get; set; }
		public bool IncludeParentClass { get; set; }
		public bool IncludeParentFunction { get; set; }
		public bool IncludeSelection { get; set; }
		public bool IncludeFillInMiddle { get; set; }
		public bool IncludeError { get; set; }

		public Option Commit()
		{
			var commited = new Option
			{
				IncludeSolutionStructure = IncludeSolutionStructure,
				IncludeProjectStructure = IncludeProjectStructure,
				IncludeActiveDocument = IncludeActiveDocument,
				IncludeParentClass = IncludeParentClass,
				IncludeParentFunction = IncludeParentFunction,
				IncludeSelection = IncludeSelection,
				IncludeFillInMiddle = IncludeFillInMiddle,
				IncludeError = IncludeError
			};

			IncludeSolutionStructure = false;
			IncludeProjectStructure = false;
			IncludeActiveDocument = false;
			IncludeParentClass = false;
			IncludeParentFunction = false;
			IncludeSelection = false;
			IncludeFillInMiddle = false;
			IncludeError = false;

			return commited;
		}

		public bool HasInclude => IncludeSolutionStructure ||
			IncludeProjectStructure ||
			IncludeActiveDocument ||
			IncludeParentClass ||
			IncludeParentFunction ||
			IncludeSelection ||
			IncludeFillInMiddle ||
			IncludeError;
	}
}
