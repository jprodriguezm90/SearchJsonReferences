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

            while (item.MoveNext())
            {
                var itemc = item.Current;
                searchWord(itemc, "xxx");
            }
            Console.ReadLine();

        }

        private static KeyValuePair<string, string> searchWord(JToken itemc, string searchKey)
        {
            var childrens = itemc.Children();
            if (childrens.ToList().Count == 0)
            {
                return new KeyValuePair<string,string>("f", "");
            }

            var enumerator = childrens.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                var itemChildrens = item.Children().ToList();
                var count = itemChildrens.Count;
                var i = itemChildrens.First();
                if (i.HasValues)
                {
                    return searchWord(enumerator.Current,searchKey);
                }
                else {
                    var values = item.ToArray();
                    if (values != null && values.Length > 0) {
                        var found = values[0].ToString().Contains(searchKey);
                        if (found) { 
                            //return parent
                        }
                    }
                    Console.WriteLine(":D");
                }
              
            }
            return new KeyValuePair<string, string>("blah","blah");
        }

        public static string getJSON()
        {
            string fs = File.ReadAllText("json.txt");

            return fs;
        }
    }
}
