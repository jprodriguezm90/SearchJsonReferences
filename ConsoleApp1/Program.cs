using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JObject obj = JObject.Parse(getJSON());

            var l = obj.Children();
            var jtokenCount = l.ToList().Count;

            var enumerable = l.AsEnumerable();

            var item = enumerable.GetEnumerator();
            var result = "";

            while (item.MoveNext())
            {
                var itemc = item.Current;
                result += searchWord(itemc, "www.zoetisus.com");
            }
            Console.ReadLine();

        }

        private static string searchWord(JToken itemc, string searchKey)
        {
            var result = "";
            var childrens = itemc.Children();
            if (childrens.ToList().Count == 0)
            {
                return result;
            }

            var enumerator = childrens.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (item.HasValues)
                {
                    var itemChildrens = item.Children().ToList();
                    var i = itemChildrens.First();
                    if (i.HasValues)
                    {
                        result += searchWord(enumerator.Current, searchKey);
                    }
                    else
                    {
                        var values = item.ToArray();
                        if (values != null && values.Length > 0)
                        {
                            var found = values[0].ToString().Contains(searchKey);
                            if (found)
                            {
                                result += item.Parent.ToString();
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static string getJSON()
        {
            string fs = File.ReadAllText("json.txt");

            return fs;
        }
    }
}
