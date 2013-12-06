using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace LiteStatus.Services
{
	public class LiteUpdateService
	{
		public LiteUpdateService ()
		{
		}

		public BalanceData GetBalance()
		{
			if(!LiteSettings.IsValid)
				throw new Exception("Settings not valid!");
			try{

				WebClient client = new WebClient();
				client.Headers.Add ("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

				string data = string.Empty;
				Rootobject result = null;
				using (StreamReader reader = new StreamReader (client.OpenRead(string.Format("{0}&action=getuserbalance", LiteSettings.Url)))) {
					data = reader.ReadToEnd();
					result = JsonConvert.DeserializeObject<Rootobject> (data);
				}
				return result != null ? result.getuserbalance.data : null;
			}
			catch(Exception ex) {
			}
			return null;
		}
	}

	public class Rootobject
	{
		public Getuserbalance getuserbalance { get; set; }
	}

	public class Getuserbalance
	{
		public string version { get; set; }
		public float runtime { get; set; }
		public BalanceData data { get; set; }
	}

	public class BalanceData
	{
		public float confirmed { get; set; }
		public float unconfirmed { get; set; }
		public float orphaned { get; set; }
	}
}

