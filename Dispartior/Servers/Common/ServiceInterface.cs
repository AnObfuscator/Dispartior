using System;
using System.Net.Http;
using Dispartior.Messaging.Messages;

namespace Dispartior.Servers.Common
{
	public class ServiceInterface
	{
		private readonly HttpClient httpClient;

		public ServiceInterface(string address, int port)
		{
			var serviceAddress = string.Format("http://{0}:{1}", address, port);
			httpClient = new HttpClient();
			httpClient.BaseAddress = new Uri(serviceAddress);
		}

		public bool Post(BaseMessage message, string url)
		{
			var json = message.Serialize();
			var body = new StringContent(json);
			var response = httpClient.PostAsync(url, body).Result;
			return response.IsSuccessStatusCode;
		}

		public bool Post<T>(BaseMessage message, string url, out T result)
		{
			var json = message.Serialize();
			var body = new StringContent(json);
			var response = httpClient.PostAsync(url, body).Result;

			if (response.IsSuccessStatusCode)
			{
				var responseContent = response.Content.ReadAsStringAsync().Result;
				result = BaseMessage.Deserialize<T>(responseContent);
			}
			else
			{
				result = default(T);
			}

			return response.IsSuccessStatusCode;
		}
	}
}

