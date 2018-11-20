using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Classes
{
	class Animal
	{
		public string Type { get; set; }
		public string Color { get; set; }
		public string View { get; set; }

		public Animal()
		{ }

		public Animal(string Type, string Color, string View)
		{
			this.Type = Type;
			this.Color = Color;
			this.View = View;
		}
	}
}
