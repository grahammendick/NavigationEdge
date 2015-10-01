using System.Collections.Generic;
using System.Linq;

namespace NavigationEdgeApi.Models
{
	public class PersonRepository
	{
		private List<Person> people  = new List<Person>
		{
			new Person{ Id = 1, Name = "Bell Halvorson", DateOfBirth = "01/01/1980", Email = "bell@navigation.com", Phone = "555 0001" },
			new Person{ Id = 2, Name = "Aditya Larson", DateOfBirth = "01/02/1980", Email = "aditya@navigation.com", Phone = "555 0002" },
			new Person{ Id = 3, Name = "Rashawn Schamberger", DateOfBirth = "01/03/1980", Email = "rashawn@navigation.com", Phone = "555 0003" },
			new Person{ Id = 4, Name = "Rupert Grant", DateOfBirth = "01/04/1980", Email = "rupert@navigation.com", Phone = "555 0004" },
			new Person{ Id = 5, Name = "Opal Carter", DateOfBirth = "01/05/1980", Email = "opal@navigation.com", Phone = "555 0005" },
			new Person{ Id = 6, Name = "Candida Christiansen", DateOfBirth = "01/06/1980", Email = "candida@navigation.com", Phone = "555 0006" },
			new Person{ Id = 7, Name = "Haven Stroman", DateOfBirth = "01/07/1980", Email = "haven@navigation.com", Phone = "555 0007" },
			new Person{ Id = 8, Name = "Celine Leannon", DateOfBirth = "01/08/1980", Email = "celine@navigation.com", Phone = "555 0008" },
			new Person{ Id = 9, Name = "Ryan Ruecker", DateOfBirth = "01/09/1980", Email = "ryan@navigation.com", Phone = "555 0009" },
			new Person{ Id = 10, Name = "Kaci Hoppe", DateOfBirth = "01/10/1980", Email = "kaci@navigation.com", Phone = "555 0010" },
			new Person{ Id = 11, Name = "Fernando Dietrich", DateOfBirth = "01/11/1980", Email = "fernando@navigation.com", Phone = "555 0011" },
			new Person{ Id = 12, Name = "Emelie Lueilwitz", DateOfBirth = "01/12/1980", Email = "emelie@navigation.com", Phone = "555 0012" }
		};

		public IQueryable<Person> People
		{
			get
			{
				return people.AsQueryable();
			}
		}
	}
}