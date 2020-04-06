using RestSharp;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
	public class ApiCall : IApiCall
	{
		public async Task<bool> CheckUserAutorization(string token)
		{
			var client = new RestClient("https://localhost:5001/api/LoginCrud/GetUser");
			var request = new RestRequest(Method.GET)
				.AddParameter("token", token, ParameterType.GetOrPost);

			IRestResponse response = await client.ExecuteAsync(request);

			var user = response.Content;	//TODO Recibir el User	

			bool isAutorized = false;

			//TODO: Logica de calculo expiracion de token

			return isAutorized;

		}
	}
}
