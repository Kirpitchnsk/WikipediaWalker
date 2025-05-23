﻿namespace WikipediaWalkerClassLibrary
{
    public class Graph
    {
        /// <summary>
        /// Задание графа списком смежности
        /// </summary>
        public Dictionary<string, Dictionary<string, int>> AdjacencyList { get; private set; }

        /// <summary>
        /// Инициализация графа и списка смежности
        /// </summary>
        public Graph()
        {
            AdjacencyList = new Dictionary<string, Dictionary<string, int>>();
        }

        /// <summary>
        /// Добавление вершины в граф
        /// </summary>
        /// <param name="start">Начальная вершина</param>
        /// <param name="end">Конечная вершина</param>
        /// <param name="weight">Вес вершины</param>
        public void AddEdge(string start, string end, int weight)
        {

            if (!AdjacencyList.ContainsKey(start))
            {
                AdjacencyList[start] = new Dictionary<string, int>();
            }
            if (!AdjacencyList.ContainsKey(end))
            {
                AdjacencyList[end] = new Dictionary<string, int>();
            }

            AdjacencyList[start][end] = weight;
        }


        /// <summary>
        /// Алгоритм Дейкстры
        /// </summary>
        /// <param name="startArticle">Начальная вершина</param>
        /// <param name="endArtcicle">Конечная вершина</param>
        /// <returns>Возвращает список вершин, образующих кратчайший путь</returns>
        public List<string> Dijkstra(string startArticle, string endArtcicle)
        {
            const int INF = 1_000_000 + 9;

            var data = AdjacencyList.Keys.ToDictionary(key => key, key => INF);
            var cameFrom = AdjacencyList.Keys.ToDictionary(key => key, key => "");

            var visited = new HashSet<string>();

            data[startArticle] = 0;

            for (int i = 0; i < AdjacencyList.Count; ++i)
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

                if (AdjacencyList.ContainsKey(argMinTraffic))
                {
                    foreach (var toVertex in AdjacencyList[argMinTraffic])
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

            var path = new List<string> { endArtcicle };
            while (cameFrom[endArtcicle] != "")
            {
                path.Add(cameFrom[endArtcicle]);
                endArtcicle = cameFrom[endArtcicle];
            }

            path.Reverse();
            return path;
        }

        /// <summary>
        /// Счетчик числа путей в графе
        /// </summary>
        /// <param name="startVertex">Начальная вершина</param>
        /// <param name="endVertex">Конечная вершина</param>
        /// <returns>Число путей между двумя вершинами в графе</returns>
        public int CountPaths(string startVertex, string endVertex)
        {
            // Инициализация счетчика путей
            var count = 0;

            // Вызываем вспомогательную функцию для подсчета путей
            CountPathsDFS(startVertex, endVertex, ref count);

            return count;
        }

        /// <summary>
        /// Поиск в глубину в графе
        /// </summary>
        /// <param name="currentVertex">Начальная вершина</param>
        /// <param name="endVertex">Конечная вершина</param>
        /// <param name="count">Ссылка на счетчик путей</param>
        private void CountPathsDFS(string currentVertex, string endVertex, ref int count)
        {
            // Если текущая вершина равна конечной, увеличиваем счетчик путей
            if (currentVertex == endVertex)
            {
                count++;
                return;
            }

            // Проходим по всем соседним вершинам и рекурсивно вызываем эту функцию для каждой вершины
            foreach (var neighbor in AdjacencyList[currentVertex])
            {
                CountPathsDFS(neighbor.Key, endVertex, ref count);
            }
        }
    }
}