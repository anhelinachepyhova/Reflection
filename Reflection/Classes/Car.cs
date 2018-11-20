using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Classes
{
	public class Car
	{
		public string Model { get; set; }
		public DateTime Year { get; set; }
		public int Speed { get; set; }

		public Car()
		{ }

		public Car(string Model, DateTime Year, int Speed)
		{
			this.Model = Model;
			this.Year = Year;
			this.Speed = Speed;
		}
	}
}
