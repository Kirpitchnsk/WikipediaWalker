namespace WikipediaWalkerClassLibrary
{
    public class Graph
    {
        public Dictionary<string, Dictionary<string, int>> adjacencyList { get; private set; }

        public Graph()
        {
            adjacencyList = new Dictionary<string, Dictionary<string, int>>();
        }

        /// <summary>
        /// Add edge into graph
        /// </summary>
        /// <param name="start">Start vertice</param>
        /// <param name="end">End vertice</param>
        /// <param name="weight">Weight of edge
        /// </param>
        public void AddEdge(string start, string end, int weight)
        {

            if (!adjacencyList.ContainsKey(start))
            {
                adjacencyList[start] = new Dictionary<string, int>();
            }
            if (!adjacencyList.ContainsKey(end))
            {
                adjacencyList[end] = new Dictionary<string, int>();
            }

            adjacencyList[start][end] = weight;
        }

        /// <summary>
        /// Dijkstra algorithm of graph.
        /// </summary>
        /// <param name="start">Start veritce</param>
        /// <param name="finish">End vertice</param>
        /// <returns>List of verticises</returns>
        public List<string> Dijkstra(string start, string finish)
        {
            const int INF = 1_000_000 + 9;

            var data = adjacencyList.Keys.ToDictionary(key => key, key => INF);
            var cameFrom = adjacencyList.Keys.ToDictionary(key => key, key => "");

            var visited = new HashSet<string>();

            data[start] = 0;

            for (int i = 0; i < adjacencyList.Count; ++i)
            {
                var argMinTraffic = "";
                var minTraffic = INF;

                foreach (var vertex in data)
                {
                    if (!visited.Contains(vertex.Key) && vertex.Value < minTraffic)
                    {
                        minTraffic = vertex.Value;
                        argMinTraffic = vertex.Key.Trim();
                    }
                }

                if (data[argMinTraffic] == INF)
                {
                    break;
                }

                visited.Add(argMinTraffic);

                if (adjacencyList.ContainsKey(argMinTraffic))
                {
                    foreach (var toVertex in adjacencyList[argMinTraffic])
                    {
                        var newPathTraffic = data[argMinTraffic] + toVertex.Value;
                        if (data[toVertex.Key] > newPathTraffic)
                        {
                            data[toVertex.Key] = newPathTraffic;
                            cameFrom[toVertex.Key] = argMinTraffic;
                        }
                    }
                }
            }

            var path = new List<string> { finish };
            while (cameFrom[finish] != "")
            {
                path.Add(cameFrom[finish]);
                finish = cameFrom[finish];
            }

            path.Reverse();
            return path;
        }

        public int CountPaths(string startVertex, string endVertex)
        {
            // Инициализация счетчика путей
            var count = 0;

            // Вызываем вспомогательную функцию для подсчета путей
            CountPathsDFS(startVertex, endVertex, ref count);

            return count;
        }

        private void CountPathsDFS(string currentVertex, string endVertex, ref int count)
        {
            // Если текущая вершина равна конечной, увеличиваем счетчик путей
            if (currentVertex == endVertex)
            {
                count++;
                return;
            }

            // Проходим по всем соседним вершинам и рекурсивно вызываем эту функцию для каждой вершины
            foreach (var neighbor in adjacencyList[currentVertex])
            {
                CountPathsDFS(neighbor.Key, endVertex, ref count);
            }
        }
    }
}