using Python.Runtime;

namespace WikipediaWalkerClassLibrary
{
    public class PythonManager
    {
        /// <summary>
        /// Класс, взаимодействующий с Pyhton.NET. Инициализация движка python
        /// </summary>
        public PythonManager() 
        {
            Runtime.PythonDLL = @"python312.dll";
        }

        /// <summary>
        /// Получение ссылок содержащиеся в статье с использованием python
        /// </summary>
        /// <param name="articleTitle"></param>
        /// <returns>Возвращает список всех связей текущей статьи</returns>
        public List<string> GetLinks_Python(string articleTitle)
        {
            PythonEngine.Initialize();

            using (Py.GIL())
            {
                var pythonScript = Py.Import("wikipedia");
                var result = pythonScript.InvokeMethod("get_page_links", new PyObject[] { new PyString(articleTitle) });
                var pyList = new PyList(result);

                var links = new List<string>();
                foreach (var link in pyList)
                {
                    links.Add(link.ToString());
                }

                return links;
            }
        }

        public string GetArticleInfo_Python(string articleTitle)
        {
            PythonEngine.Initialize();

            using (Py.GIL())
            {
                var pythonScript = Py.Import("wikipedia");
                var result = pythonScript.InvokeMethod("get_article_info", new PyObject[] { new PyString(articleTitle) });
                var articleInfo = new PyString(result);

                return articleInfo.ToString();
            }
        }
    }
}