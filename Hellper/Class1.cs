using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Hellper
{
	public static class Class1
	{
		static public int GetParEnum(Type enums)
		{
			string par;
			int result = 0;
			do
			{
				Console.Clear();
				Console.WriteLine("Please, choose variant operation:");
				if (enums.IsEnum)
				{
					foreach (var p in Enum.GetValues(enums))
					{
						Console.WriteLine($"{(int)p}.{p}");
					}
				}
				else
				{
					throw new Exception();
				}
				par = Console.ReadLine();
			} while (!int.TryParse(par, out result));
			return result;
		}

		static public bool TryParse<T>(string par, out T obj) where T : struct
		{
			obj = default(T);
			Type type = obj.GetType();
			MethodInfo method = obj.GetType().GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string), obj.GetType().MakeByRefType() }, null);
			object[] arraypar = { par, obj };
			object o = method.Invoke(null, arraypar);

			if (o.Equals(true))
			{
				obj = (T)arraypar[1];
				return true;
			}
			else
				return false;
		}
	}
}
