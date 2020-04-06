using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
	public interface IApiCall
	{
		public Task<bool> CheckUserAutorization(string token);

	}
}
