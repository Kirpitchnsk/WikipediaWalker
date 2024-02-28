namespace WikipediaWalkerClassLibrary
{
    public class ReducedGraph:Graph
    {
        public List<string> AllPathsAsArrows { get; private set; }

        private int maxPath;
        private int maxDistanceLength;

        public ReducedGraph(List<string> allPathsAsArrows, int maxPath = int.MaxValue, int maxDistanceLength = int.MaxValue)
        {
            for (int i = 0; i < allPathsAsArrows.Count; i++)
            {
                string? arrowPath = allPathsAsArrows[i];

                var path = arrowPath.Split(" -> ");
                if (path.Length <= maxDistanceLength)
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
