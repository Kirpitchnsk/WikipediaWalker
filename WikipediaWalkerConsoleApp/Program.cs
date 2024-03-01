namespace WikipediaWalker
{
    class Program
    {
        static void Main(string[] args)
        {
            var startArticle = "Russia";
            var endArticle1 = "Energy";
            var endArticle2 = "Moscow";
            var endArticle3 = "London";

            var shortestPaths = PathFinder.FindShortestPaths(startArticle, endArticle2);

            Console.WriteLine($"Кратчайшие пути между '{startArticle}' и '{endArticle2}':");

            shortestPaths.PrintGraph();
        }
    }
}
