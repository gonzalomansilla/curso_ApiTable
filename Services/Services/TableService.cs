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
			var personExist = await _myContext.Persons
				.Where(p => p.DNI == qPerson.Dni)
				.FirstOrDefaultAsync();

			if (personExist != null)
				return new Person() { DNI = -1 };

			Person newPersonEntity = new Person() {
				Name = qPerson.Name, 
				DNI = qPerson.Dni,
				SurName = qPerson.SurName
			};

			var state = await _myContext.Persons.AddAsync(newPersonEntity);
			var save = _myContext.SaveChanges();

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
