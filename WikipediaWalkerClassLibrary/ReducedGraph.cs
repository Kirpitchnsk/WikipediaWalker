namespace WikipediaWalkerClassLibrary
{
    /// <summary>
    /// Класс который хранит в себе граф, сокращающий число отображаемых путей и выделяющий длины путей
    /// </summary>
    public class ReducedGraph:Graph
    {
        public List<string> AllPathsAsArrows { get; private set; }

        private int maxPath;
        private int maxDistanceLength;

        /// <summary>
        /// Создается граф с ограниченным числом путей и ограничивающий максимальную длину пути
        /// </summary>
        /// <param name="allPathsAsArrows">Граф в виде списка всех путей</param>
        /// <param name="maxPath">Максимальное число отображаемых путей, по умолчнанию не ограничено</param>
        /// <param name="maxDistanceLength">Максимальная длина пути, по умолчанию не ограничена</param>
        public ReducedGraph(List<string> allPathsAsArrows, int maxPath = int.MaxValue, int maxDistanceLength = int.MaxValue)
        {
            // Создание сокращенного графа
            for (int i = 0; i < allPathsAsArrows.Count; i++)
            {
                var arrowPath = allPathsAsArrows[i];

                var path = arrowPath.Split(" -> ");
                if (path.Length - 1 <= maxDistanceLength)
                {
                    for (int j = 0; j < path.Length - 1; j++)
                    {
                        AddEdge(path[j], path[j + 1], j + 1);
                    }
                }
                if (i > maxPath) break;
            }

            AllPathsAsArrows = allPathsAsArrows;
            this.maxDistanceLength = maxDistanceLength;
            this.maxPath = maxPath;
        }
    }
}
