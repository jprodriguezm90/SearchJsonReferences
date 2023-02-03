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
            var item = l.AsEnumerable().GetEnumerator();
            List<ComponentJson> result = null;

            while (item.MoveNext())
            {
                var itemc = item.Current;
                result = searchWord(itemc, result, "xxx");
            }
            Console.ReadLine();

        }

        private static List<ComponentJson> searchWord(JToken itemc, List<ComponentJson> result, string searchKey)
        {
            if (result == null)
            {
                result = new List<ComponentJson>();
            }

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
                        result = searchWord(enumerator.Current, result, searchKey);
                    }
                    else
                    {
                        result = BrowserLeaves(searchKey, result, item);
                    }
                }
                else
                {
                    result = BrowserLeaves(searchKey, result, item);
                }

            }
            return result;
        }

        private static List<ComponentJson> BrowserLeaves(string searchKey, List<ComponentJson> result, JToken item)
        {
            var values = item.ToArray();
            if (values != null && values.Length > 0)
            {
                var found = values[0].ToString().Contains(searchKey);
                if (found)
                {
                    var parent = item.Parent.Path;
                    var parentstr = parent.Substring(parent.LastIndexOf('.') + 1);

                    result.Add(new ComponentJson(parentstr,item.Parent.ToString(),item.ToString()));
                }
            }
            else
            {
                var found = item.ToString().Contains(searchKey);
                if (found)
                {
                    result.Add(new ComponentJson(item.Parent.Path, item.Parent.ToString(), item.ToString()));
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

    public class ComponentJson
    {
        public string Root { get; set; }
        public string Context { get; set; }
        public string Item { get; set; }    
        public ComponentJson(string root, string context, string item) 
        {
            Root = root;    
            Context = context;
            Item = item;
        }
        public override string ToString()
        {
            return $"{Root} : {Context} : {Item}";
        }
    }
}
