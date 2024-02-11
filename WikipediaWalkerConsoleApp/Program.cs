using System;
using System.ComponentModel.DataAnnotations;
using Python.Runtime;

namespace WikipediaWalker
{
    class Program
    {
        public static List<string> getLinks(string articleTitle)
        {
            Runtime.PythonDLL = @"C:\Users\nskru\AppData\Local\Programs\Python\Python312\python312.dll";
            PythonEngine.Initialize();
            // Инициализация движка Python
            using (Py.GIL())
            {
                var pythonScript = Py.Import("wikipedia");
                var result = pythonScript.InvokeMethod("get_all_links", new PyObject[] {new PyString(articleTitle)});
                var pyList = new PyList(result);

                var links = new List<string>();
                foreach(var link in pyList)
                {
                    links.Add(link.ToString());
                }

                return links;
            }
        }
        public static List<string> GetDistances(string startArticleTitle, string endArticleTitle)
        {
            var distanceList = new List<string>();

            return distanceList;
        }
        static void Main(string[] args)
        {
            var articleTitle = "Russia";
            var links = getLinks(articleTitle);

            // Вывод списка ссылок на консоль
            Console.WriteLine($"Ссылки в статье '{articleTitle}':");
            foreach (var link in links)
            {
                Console.WriteLine(link);
            }

            Console.ReadLine();
        }
    }
}

