namespace WikipediaWalkerClassLibrary
{
    public class ArticleManager
    { 
        /// <summary>
        /// Хеш таблица для хранения списков связанных статей
        /// </summary>
        private static Dictionary<string, List<string>> articleLinks;

        /// <summary>
        /// Хранение данных из файла со связями
        /// </summary>
        private static string distancesBetweenArticles;
        
        /// <summary>
        /// Конструктор в котором инициализируются значения переменных
        /// </summary>
        public ArticleManager()
        {
            distancesBetweenArticles = Resource1.links;
            articleLinks = ReadArticleLinks();
        }

        /// <summary>
        /// Преобразование названия статьи к виду первая буква заглавная, остальные прописные
        /// </summary>
        /// <param name="article">Назавние статьи</param>
        /// <returns>Врзврат преобразованной строки/returns>
        public static string InputArticleCorrect(string article)
        {
            if(article.Length > 1)
            {
                var currentArticle = article.Trim();
                var firstLetter = currentArticle[0].ToString().ToUpper();
                var changedArticle = currentArticle.ToLower().ToCharArray();
                changedArticle[0] = firstLetter[0];
                return new string(changedArticle);
            }
            else
            {
                return article;
            }
        }

        /// <summary>
        /// Функция для проверки статей на существование
        /// </summary>
        /// <param name="article1">Первая статья</param>
        /// <param name="article2">Вторая статья</param>
        public static bool CheckSpelling(string article1, string article2)
        {
            var allArticles = Resource1.articles.Split("\n").ToList();
            return allArticles.Contains(article1) && allArticles.Contains(article2);  
        }

        /// <summary>
        /// Функция для чтения связей из файла и сохранения их в хэш-таблицу
        /// </summary>
        /// <returns>Возвращает хэш-таблицу со связями между статьями</returns>
        /// <exception cref="Exception">Файл не существует</exception>
        public static Dictionary<string, List<string>> ReadArticleLinks()
        {
            var links = new Dictionary<string, List<string>>();

            try
            {
                // Чтение связей из файла
                var lines = distancesBetweenArticles.Split("\n");

                foreach (var line in lines)
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
                throw new Exception($"Ошибка чтения файла {distancesBetweenArticles}: {ex.Message}");
            }

            return links;
        }

        /// <summary>
        /// Функция для поиска связей для заданной статьи
        /// </summary>
        /// <param name="articleToFind">Необходимая статья</param>
        /// <returns>Возвращает список связей статьи</returns>
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