namespace WikipediaWalkerClassLibrary
{
    public class ArticleManager
    { 
        /// <summary>
        /// Хеш таблица для хранения ссылок на статьи
        /// </summary>
        private static Dictionary<string, List<string>> articleLinks;

        /// <summary>
        /// Список для хранения строк со статьей и ссылкой на нее
        /// </summary>
        private static List<string> distancesBetweenArticles;

        private static List<string> listOfArticles;
        
        /// <summary>
        /// Конструктор в котором инициализируются значения переменных
        /// </summary>
        public ArticleManager()
        {
            distancesBetweenArticles = Resource1.links.Split(new string[] { "\n","\r","\r\n" }, StringSplitOptions.None).ToList();
            listOfArticles = Resource1.articles.Split(new string[] { "\n", "\r", "\r\n" }, StringSplitOptions.None).ToList();
            articleLinks = ReadArticleLinks();
        }

        /// <summary>
        /// Преобразование названия статьи к виду первая буква заглавная, остальные прописные
        /// </summary>
        /// <param name="article">Название статьи</param>
        /// <returns>Возврат преобразованной строки</returns>
        public string ConvertArticleToReadbleForm(string article)
        {
            if(article.Length > 1)
            {
                var currentArticle = article.Trim().Replace("_"," ");
                return new string(currentArticle);
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
        public bool IsArticleExists(string article)
        {
            return listOfArticles.Contains(article.Trim().Replace(" ", "_"));
        }

        /// <summary>
        /// Функция для чтения связей из файла и сохранения их в хэш-таблицу
        /// </summary>
        /// <returns>Возвращает хэш-таблицу со связями между статьями</returns>
        /// <exception cref="Exception">Файл не существует</exception>
        public Dictionary<string, List<string>> ReadArticleLinks()
        {
            var links = new Dictionary<string, List<string>>();

            try
            {
                // Чтение связей из файла
                var lines = distancesBetweenArticles;

                foreach (var line in lines)
                {
                    var parts = line.Split("\t");
                    if (parts.Length == 2)
                    {
                        var article1 = parts[0].Trim().Replace("_"," ");
                        var article2 = parts[1].Trim().Replace("_", " ");

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