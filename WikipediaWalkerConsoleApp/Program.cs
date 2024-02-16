using System;
using System.ComponentModel.DataAnnotations;
using Python.Runtime;

namespace WikipediaWalker
{
    class Program
    {
        // Функция для чтения связей из файла и сохранения их в хэш-таблицу
        private static Dictionary<string, List<string>> ReadArticleLinks(string filePath)
        {
            Dictionary<string, List<string>> links = new Dictionary<string, List<string>>();

            try
            {
                // Чтение связей из файла
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split("\t");
                    if (parts.Length == 2)
                    {
                        string article1 = parts[0].Trim();
                        string article2 = parts[1].Trim();

                        // Добавление связей в хэш-таблицу
                        if (!links.ContainsKey(article1))
                        {
                            links[article1] = new List<string>();
                        }
                        links[article1].Add(article2);

                        if (!links.ContainsKey(article2))
                        {
                            links[article2] = new List<string>();
                        }
                        links[article2].Add(article1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка чтения файла {filePath}: {ex.Message}");
            }

            return links;
        }

        // Функция для поиска связей для заданной статьи
        private static List<string> FindArticleLinks(Dictionary<string, List<string>> articleLinks, string articleToFind)
        {
            List<string> links = new List<string>();

            // Проверяем, есть ли заданная статья в хэш-таблице
            if (articleLinks.ContainsKey(articleToFind))
            {
                // Возвращаем список связей для заданной статьи
                links.AddRange(articleLinks[articleToFind]);
            }

            return links;
        }
        public static List<string> GetLinks(string articleTitle)
        {
            PythonEngine.Initialize();

            using (Py.GIL())
            {
                var pythonScript = Py.Import("wikipedia");
                var result = pythonScript.InvokeMethod("get_all_links", new PyObject[] { new PyString(articleTitle) });
                var pyList = new PyList(result);

                var links = new List<string>();
                foreach (var link in pyList)
                {
                    links.Add(link.ToString());
                }

                return links;
            }
        }

        static List<string> FindShortestPaths(string startArticle, string endArticle, Dictionary<string, List<string>> articleLinks)
        {
            var startVisited = new HashSet<string>();
            var endVisited = new HashSet<string>();

            // Инициализация очередей для двунаправленного поиска
            var startQueue = new Queue<Tuple<string, List<string>>>();
            var endQueue = new Queue<Tuple<string, List<string>>>();

            // Инициализация стартовых путей
            startQueue.Enqueue(new Tuple<string, List<string>>(startArticle, new List<string> { startArticle }));
            endQueue.Enqueue(new Tuple<string, List<string>>(endArticle, new List<string> { endArticle }));

            // Инициализация списка для хранения всех найденных путей
            var allPaths = new List<List<string>>();

            while (startQueue.Count > 0 && endQueue.Count > 0)
            {
                // Поиск вперед от начальной статьи
                var (startArticleName, startPath) = startQueue.Dequeue();
                startVisited.Add(startArticleName);

                // Получение ссылок для текущей статьи
                var startLinks = FindArticleLinks(articleLinks, startArticleName);

                foreach (var link in startLinks)
                {
                    if (!startVisited.Contains(link) && !startPath.Contains(link))
                    {
                        var newPath = new List<string>(startPath);
                        newPath.Add(link);
                        startQueue.Enqueue(new Tuple<string, List<string>>(link, newPath));

                        // Проверка, достигли ли мы конечной статьи
                        if (endVisited.Contains(link) && link == endArticle)
                        {
                            var endPaths = endQueue.Peek().Item2;
                            newPath.AddRange(endPaths);
                            allPaths.Add(newPath);
                        }
                    }
                }

                // Поиск вперед от конечной статьи
                var (endArticleName, endPath) = endQueue.Dequeue();
                endVisited.Add(endArticleName);

                // Получение ссылок для текущей статьи
                var endLinks = FindArticleLinks(articleLinks, endArticleName);

                foreach (var link in endLinks)
                {
                    if (!endVisited.Contains(link) && !endPath.Contains(link))
                    {
                        var newPath = new List<string>(endPath);
                        newPath.Insert(0, link);
                        endQueue.Enqueue(new Tuple<string, List<string>>(link, newPath));

                        // Проверка, достигли ли мы начальной статьи
                        if (startVisited.Contains(link) && link == startArticle)
                        {
                            var startPathReversed = startQueue.Peek().Item2;
                            startPathReversed.Reverse();
                            newPath.AddRange(startPathReversed);
                            allPaths.Add(newPath);
                        }
                    }
                }
            }

            // Преобразование всех найденных путей в список стрелок
            var allPathsAsArrows = new List<string>();

            foreach (var path in allPaths)
            {
                var isPath = false;
                var arrows = new List<string>();

                foreach(var link in path)
                {
                    if(link == startArticle) isPath = true;
                    if (link == endArticle)
                    {
                        arrows.Add(link);
                        break;
                    }
                    if(isPath)
                    {
                        arrows.Add(link);
                    }
                }
                var newPath = String.Join(" -> ", arrows);
                if (!allPathsAsArrows.Contains(newPath)) allPathsAsArrows.Add(newPath);
            }

            return allPathsAsArrows;
        }

        static void Main(string[] args)
        {
            Runtime.PythonDLL = @"C:\Users\nskru\AppData\Local\Programs\Python\Python312\python312.dll";

            string startArticle = "Russia";
            string endArticle1 = "Energy";
            string endArticle2 = "Moscow";
            string endArticle3 = "London";

            var articleLinks = ReadArticleLinks("links.txt");

            var shortestPaths = FindShortestPaths(startArticle, endArticle3, articleLinks);

            Console.WriteLine($"Кратчайшие пути между '{startArticle}' и '{endArticle3}':");

            foreach (var path in shortestPaths)
            {
                Console.WriteLine(path);
            }
        }
    }
}