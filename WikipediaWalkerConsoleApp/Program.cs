using System;
using System.ComponentModel.DataAnnotations;
using Python.Runtime;

namespace WikipediaWalker
{
    class Program
    {
        public static List<string> GetLinks(string articleTitle)
        {
            PythonEngine.Initialize();

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
        static List<string> FindShortestPath(string startArticle, string endArticle)
        {
            HashSet<string> startVisited = new HashSet<string>();
            HashSet<string> endVisited = new HashSet<string>();

            Queue<Tuple<string, List<string>>> startQueue = new Queue<Tuple<string, List<string>>>();
            Queue<Tuple<string, List<string>>> endQueue = new Queue<Tuple<string, List<string>>>();


            startQueue.Enqueue(new Tuple<string, List<string>>(startArticle, new List<string> { startArticle }));
            endQueue.Enqueue(new Tuple<string, List<string>>(endArticle, new List<string> { endArticle }));

            List<string> shortestPath = null;

            while (startQueue.Count > 0 && endQueue.Count > 0)
            {
                var (startArticleName, startPath) = startQueue.Dequeue();
                startVisited.Add(startArticleName);

                var startLinks = GetLinks(startArticleName);

                foreach (var link in startLinks)
                {
                    if (!startVisited.Contains(link))
                    {
                        var newPath = new List<string>(startPath);
                        newPath.Add(link);
                        startQueue.Enqueue(new Tuple<string, List<string>>(link, newPath));

                        if (endVisited.Contains(link))
                        {
                            var endPaths = endQueue.Peek().Item2;
                            newPath.AddRange(endPaths);
                            shortestPath = newPath;
                            break;
                        }
                    }
                }

                var (endArticleName, endPath) = endQueue.Dequeue();
                endVisited.Add(endArticleName);

                var endLinks = GetLinks(endArticleName);

                foreach (var link in endLinks)
                {
                    if (!endVisited.Contains(link))
                    {
                        var newPath = new List<string>(endPath);
                        newPath.Insert(0, link);
                        endQueue.Enqueue(new Tuple<string, List<string>>(link, newPath));

                        if (startVisited.Contains(link))
                        {
                            var startPathReversed = startQueue.Peek().Item2;
                            startPathReversed.Reverse();
                            newPath.AddRange(startPathReversed);
                            shortestPath = newPath;
                            break;
                        }
                    }
                }

                if (shortestPath != null)
                {
                    break;
                }
            }

            return shortestPath != null ? new List<string> { string.Join(" -> ", shortestPath) } : new List<string>();
        }


        static void Main(string[] args)
        {
            Runtime.PythonDLL = @"C:\Users\nskru\AppData\Local\Programs\Python\Python312\python312.dll";

            string startArticle = "Russia";
            string endArticle1 = "Facebook like button";
            string endArticle2 = "Moscow";

            var shortestPaths = FindShortestPath(startArticle, endArticle2);

            Console.WriteLine($"Кратчайшие пути между '{startArticle}' и '{endArticle2}':");
            foreach (var path in shortestPaths)
            {
                Console.WriteLine(path);
            }
        }
    }
}

