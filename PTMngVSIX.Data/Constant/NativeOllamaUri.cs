namespace PTMngVSIX.Data.Constant
{
	public static class NativeOllamaUri
	{
		public const string Generate = "/api/generate"; // POST
		public const string Chat = "/api/chat"; // POST
		public const string Embed = "/api/embed"; // POST
		public const string Pull = "/api/pull"; // POST
		public const string Create = "/api/create"; // POST
		public const string Show = "/api/show"; // POST
		public const string Delete = "/api/delete"; // POST
		public const string Copy = "/api/copy"; // POST
		public const string Tags = "/api/tags"; // GET
		public const string PS = "/api/ps"; // GET
		public const string BlobsDigest = "/api/blobs/:digest"; // GET
		public const string Version = "/api/version"; // GET
	}
}
