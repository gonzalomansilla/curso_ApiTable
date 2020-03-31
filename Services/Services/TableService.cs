using Common.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Context;
using Model.Model;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
	public class TableService : ITableService
	{
		private readonly ILogger<TableService> _logger;
		private readonly MyContext _myContext;

		public TableService(ILogger<TableService> logger, MyContext myContext)
		{
			_logger = logger;
			_myContext = myContext;
			_logger.LogInformation("Contructor TableService");
		}

		public async Task<Person> AddPerson(PersonDTO qPerson)
		{
			Person newPersonEntity = new Person() {
				Name = qPerson.Name, 
				DNI = qPerson.Dni,
				SurName = qPerson.SurName
			};

			try
			{
				var state = await _myContext.Persons.AddAsync(newPersonEntity);
				_myContext.SaveChanges();
			}
			catch (Exception e)
			{
				_logger.LogWarning($"Exception: {e.InnerException.Message}");
				return null;
			}

			return newPersonEntity;
		}

		public async Task<int> DeletePerson(string dniPerson)
		{
			var qPerson = await _myContext.Persons
				.Where((p) => p.DNI == long.Parse(dniPerson))
				.FirstOrDefaultAsync();

			if (qPerson == null)
				return 0;

			_myContext.Persons.Remove(qPerson);
			int rowsAffected = await _myContext.SaveChangesAsync();

			return rowsAffected;
		}

		public async Task<List<Person>> GetPersons()
		{
			var tablePersons = await _myContext.Persons.ToListAsync();

			return tablePersons;
		}
	}
}
