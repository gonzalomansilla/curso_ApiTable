using Model.Model;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
	public class ApiCall : IApiCall
	{
		private const int MINUTES_EXPIRATION_TOKEN = 10;

		public async Task<bool> UserIsAuthorized(string token)
		{
			var client = new RestClient("https://localhost:5001");
			var request = new RestRequest("/api/LoginCrud/GetUser")
				.AddParameter("guid", token, ParameterType.GetOrPost);

			IRestResponse response = await client.ExecuteAsync(request);

			User user;
			if (response.IsSuccessful)
				user = new JsonDeserializer().Deserialize<User>(response);
			else
				return false;

			if (user != null)
			{
				DateTimeOffset lastLoginDate = DateTimeOffset.Parse(user.LastLoginDate.ToString());
				DateTimeOffset actualLoginDate = DateTime.Now;

				TimeSpan difference = actualLoginDate - lastLoginDate;

				if (difference.TotalMinutes < MINUTES_EXPIRATION_TOKEN) return true;
			}

			return false;
		}

	}
}
