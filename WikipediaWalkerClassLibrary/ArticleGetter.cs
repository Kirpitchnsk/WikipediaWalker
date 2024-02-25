namespace WikipediaWalkerClassLibrary
{
    public class ArticleGetter
    {
        private static Dictionary<string, List<string>> articleLinks;
        private static string dictancesBetweenArticles;
        public ArticleGetter()
        {
            dictancesBetweenArticles = Resource1.links;
            articleLinks = ReadArticleLinks();
        }
    
        // Функция для чтения связей из файла и сохранения их в хэш-таблицу
        public static Dictionary<string, List<string>> ReadArticleLinks()
        {
            var links = new Dictionary<string, List<string>>();

            try
            {
                // Чтение связей из файла
                var lines = dictancesBetweenArticles.Split("\n");

                foreach (string line in lines)
                {
                    var parts = line.Split("\t");
                    if (parts.Length == 2)
                    {
                        var article1 = parts[0].Trim();
                        var article2 = parts[1].Trim();

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
                throw new Exception($"Ошибка чтения файла {dictancesBetweenArticles}: {ex.Message}");
            }

            return links;
        }
        // Функция для поиска связей для заданной статьи
        public List<string> FindArticleLinks(string articleToFind)
        {
            var links = new List<string>();

            // Проверяем, есть ли заданная статья в хэш-таблице
            if (articleLinks.ContainsKey(articleToFind))
            {
                // Возвращаем список связей для заданной статьи
                links.AddRange(articleLinks[articleToFind]);
            }

            return links;
        }
    }
}