namespace WikipediaWalkerClassLibrary
{
    public class SaveData
    {
        public string StartArticle { get;private set; }
        public string EndArticle { get; private set; }
        public int ShortestDistance { get; private set; }
        public int NumberOfPath { get; private set; }
        public string StartArticleInfo { get; private set; }
        public string EndArticleInfo { get; private set; }

        public SaveData(string startArticle, string endArticle, int shortestDistance, int numberOfPath, string startArticleInfo,string endArticleInfo)
        {
            StartArticle = startArticle;
            EndArticle = endArticle;
            ShortestDistance = shortestDistance;
            NumberOfPath = numberOfPath;
            StartArticleInfo = startArticleInfo;
            EndArticleInfo = endArticleInfo;
        }
    }
}
