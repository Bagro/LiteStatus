using System;

namespace LiteStatus
{
	public class LiteSettings
	{
		public static string Url {
			get { return string.Format("http://www.mine-litecoin.com/index.php?page=api&api_key={0}&id={1}", ApiKey, Id); }
		}

		public static string ApiKey { get; set; }

		public static int Id { get; set; }

		public static bool IsValid {
			get {
				return !string.IsNullOrWhiteSpace (ApiKey) && Id > 0;
			}
		}
	}
}

