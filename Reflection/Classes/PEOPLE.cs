using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Classes
{
	class People
	{
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public int Year { get; set; }

		public People()
		{ }

		public People(string LastName, string FirstName, int Year)
		{
			this.LastName = LastName;
			this.FirstName = FirstName;
			this.Year = Year;
		}
	}
}
