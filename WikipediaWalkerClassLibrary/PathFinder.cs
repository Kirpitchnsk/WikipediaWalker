namespace WikipediaWalkerClassLibrary
{
    public class PathFinder
    {
        /// <summary>
        /// Нахождение всех путей между статьями, построение общего графа путей
        /// </summary>
        /// <param name="startArticle">Начальная статья</param>
        /// <param name="endArticle">Конечная статья</param>
        /// <returns>Возвращает граф путей</returns>
        public static Graph FindShortestPaths(string startArticle, string endArticle)
        {
            var articleGetter = new ArticleManager();

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
                var startLinks = articleGetter.FindArticleLinks(startArticleName);

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
                var endLinks = articleGetter.FindArticleLinks(startArticleName);

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

                foreach (var link in path)
                {
                    if (link == startArticle) isPath = true;
                    if (link == endArticle)
                    {
                        arrows.Add(link);
                        break;
                    }
                    if (isPath)
                    {
                        arrows.Add(link);
                    }
                }
                var newPath = String.Join(" -> ", arrows);
                if (!allPathsAsArrows.Contains(newPath)) allPathsAsArrows.Add(newPath);
            }

            var graph = new Graph();

            foreach (var arrowPath in allPathsAsArrows)
            {
                var path = arrowPath.Split(" -> ");
                for (int i = 0; i < path.Length - 1; i++)
                {
                    graph.AddEdge(path[i], path[i + 1], i + 1);
                }
            }

            return graph;
        }
    }
}