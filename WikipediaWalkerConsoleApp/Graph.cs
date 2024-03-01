namespace WikipediaWalker
{
    public class Graph
    {
        private Dictionary<string, Dictionary<string, int>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<string, Dictionary<string, int>>();
        }

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
        public List<string> Dijkstra(string start, string finish)
        {
            int INF = 1_000_000 + 9;

            Dictionary<string, int> data = adjacencyList.Keys.ToDictionary(key => key, key => INF);
            Dictionary<string, string> cameFrom = adjacencyList.Keys.ToDictionary(key => key, key => "");

            HashSet<string> visited = new HashSet<string>();

            data[start] = 0;

            for (int iteration = 0; iteration < adjacencyList.Count; ++iteration)
            {
                string argMinTraffic = "";
                int minTraffic = INF;

                foreach (var vertex in data)
                {
                    if (!visited.Contains(vertex.Key) && vertex.Value < minTraffic)
                    {
                        minTraffic = vertex.Value;
                        argMinTraffic = vertex.Key;
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

            List<string> path = new List<string> { finish };
            while (cameFrom[finish] != "")
            {
                path.Add(cameFrom[finish]);
                finish = cameFrom[finish];
            }

            path.Reverse();
            return path;
        }

        public void PrintGraph()
        {
            foreach (var vertex in adjacencyList)
            {
                var start = vertex.Key;
                foreach (var edge in vertex.Value)
                {
                    var end = edge.Key;
                    var weight = edge.Value;
                    Console.WriteLine($"({start}, {end}, вес {weight})");
                }
            }
        }
    }
}