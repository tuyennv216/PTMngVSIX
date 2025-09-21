namespace PTMngVSIX.Utils.Editor
{
	internal class EditorModel
	{
		public enum InsertNodePosition
		{
			BeforeNode,      // Trước node
			AfterNode,       // Sau node  
			BeforeContent,   // Sau trivia, trước content
			AfterContent     // Sau content, trước trailing trivia
		}

		public enum InsertSelectionPosition
		{
			BeforeLine,		// Dòng phía trên
			Current,		// Vị trí hiện tại
			AfterLine,		// Dòng phía dưới
		}
	}
}
