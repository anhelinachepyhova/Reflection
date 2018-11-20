using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
	class Program
	{
		static void Main(string[] args)
		{
			List<System.Object> ListPropertis = new List<System.Object>();

			bool flag_exit = false;
			try
			{
				do
				{
					switch (Hellper.Class1.GetParEnum(typeof(Enums.Menu)))
					{
						case 1:
							{
								method1(ref ListPropertis);
								flag_exit = false;
								break;
							}
						case 2:
							{
								method2(ListPropertis);
								flag_exit = false;
								break;
							}
						case 3:
							{
								readfile(ref ListPropertis);
								flag_exit = false;
								break;
							}
						case 4:
							{
								method4(ListPropertis);
								Console.ReadLine();
								flag_exit = false;
								break;
							}
						case 5:
							{
								flag_exit = true;
								break;
							}
						default:
							{
								Console.WriteLine("It isn't right value. Try again!");
								flag_exit = false;
								break;
							}
					}
				} while (flag_exit == false);
			}
			catch (Exception)
			{
				Console.WriteLine("Sorry, The End.");
			}
			Console.ReadLine();
		} 

		static public void method1(ref List<object> ListPropertis)
		{
			switch (Hellper.Class1.GetParEnum(typeof(Enums.TypeClass)))
			{
				case 1:
					{
						ListPropertis.Add(createObject<Classes.Car>(typeof(Classes.Car), null));
						break;
					}
				case 2:
					{
						ListPropertis.Add(createObject<Classes.People>(typeof(Classes.People), null));
						break;
					}
				case 3:
					{
						ListPropertis.Add(createObject<Classes.Animal>(typeof(Classes.Animal), null));
						break;
					}
				default:
					{
						Console.WriteLine("It isn't right value. Try again!");
						break;
					}
			}
		}

		static public void method2(List<object> ListPropertis)
		{

			foreach (System.Object obj in ListPropertis)
				{
					var o = obj.GetType();
					PropertyInfo[] props = o.GetProperties();
					string str = $"{o.Name}; ";
					foreach (var prop in props)
						{
							str = $"{str}{prop.GetValue(obj)}; ";
						}
					writefile(str);
				}
		}

		static public void method4(List<object> ListPropertis)
		{
			Console.Clear();
			foreach (System.Object obj in ListPropertis)
			{
				var o = obj.GetType();
				PropertyInfo[] props = o.GetProperties();
				string str = $"{o.Name};";
				foreach (var prop in props)
				{
					str = $"{str}{prop.GetValue(obj)}; ";
				}
				Console.WriteLine(str);
			}
		}

		static Type getTypeClass(string str)
		{
			Type t = null;

			if (str == "Car")
				 t = typeof(Classes.Car);
			else if (str == "Animal")
				 t = typeof(Classes.Animal);
			else if (str == "People")
				 t = typeof(Classes.People);

			return t;
		}

		static public void readfile(ref List<object> ListPropertis)
		{
			string readPath = @".\class.txt";
			string line, str;
			bool class_flag;
			Type par = null;
			object[] param = new object[3];
			using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
			{
				while ((line = sr.ReadLine()) != null)
				{
					int i, j = 0;
					str = null;
					class_flag = false;
					for (i = 0; i < line.Length; i++)
					{	
						if (!line[i].Equals(';') && (class_flag == false))
						{
							str = $"{str}{line[i]}";
						}
						else if (class_flag == false)
						{
							par = getTypeClass(str);
							class_flag = true;
							str = null;
							continue;
						}

						if (class_flag == true)
						{
							if (!line[i].Equals(';'))
							{
								str = $"{str}{line[i]}";
							}
							else
							 {
								param[j] = str;
								j++;
								str = null;
								continue;
							}
						}
						
					}
					if (par == typeof(Classes.Car))
						{
							ListPropertis.Add(createObject<Classes.Car>(par, param));
						}
					else if (par == typeof(Classes.Animal))
					{
						ListPropertis.Add(createObject<Classes.Animal>(par, param));
					}
					else if (par == typeof(Classes.People))
					{
						ListPropertis.Add(createObject<Classes.People>(par, param));
					}

				}
			}
		}

		public static void writefile(string str)
		{
			string writePath = @".\class.txt";
			using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
			{
				sw.WriteLine(str);
			}
		}

		static public string getShowParam(string name)
		{
			Console.Write($"Please, input value for field - {name} : ");
			return Console.ReadLine();
		}

		public static T createObject<T>(Type par, params object[] arr)
		{
			Console.Clear();
			foreach (ConstructorInfo constr in par.GetConstructors())
			{
				object[] obj;

				ParameterInfo[] pars = constr.GetParameters();
				if (pars.Length == 3)
				{
					obj = new object[pars.Length];
					object o;
					for (int i = 0; i < obj.Length; i++)
					{
							object objT;
							string str = null;
							Type type = Type.GetType(pars[i].ParameterType.FullName);

							if (type != typeof(string))
							{
								objT = (object)Activator.CreateInstance(type);
								object[] arraypar;
								do
								{
									if (arr == null)
										str = getShowParam(pars[i].Name);
									else str = (string)arr[i];

									MethodInfo method = objT.GetType().GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string), objT.GetType().MakeByRefType() }, null);
									arraypar = new object[] { str, objT };
									o = method.Invoke(null, arraypar);
								} while (o.Equals(false));

								obj[i] = (object)arraypar[1];
							}
							else
							{
								if (arr == null)
									obj[i] = getShowParam(pars[i].Name);
								else obj[i] = (string)arr[i];
							}
					}
					return (T)Activator.CreateInstance(par, obj);
				}
			}
			return default(T);
		}
	}
}
;