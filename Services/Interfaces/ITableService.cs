using Common.DTO;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
	public interface ITableService
	{
		public Task<List<Person>> GetPersons();
		public Task<Person> AddPerson(PersonDTO person);
		public Task<int> DeletePerson(string dniPerson);
	}
}
