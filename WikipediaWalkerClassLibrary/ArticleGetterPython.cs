using Python.Runtime;

namespace WikipediaWalkerClassLibrary
{
    public class ArticleGetterPython
    {
        /// <summary>
        /// Класс, взаимодейтсвующий с Pyhton.NET. Инициализация движка python
        /// </summary>
        public ArticleGetterPython() 
        {
            Runtime.PythonDLL = @"C:\Users\nskru\AppData\Local\Programs\Python\Python312\python312.dll";
        }

        /// <summary>
        /// Получение ссылок содержащиеся в статье с использованием python
        /// </summary>
        /// <param name="articleTitle"></param>
        /// <returns>Возвращает список всех связей текущей статьи</returns>
        public static List<string> GetLinks(string articleTitle)
        {
            PythonEngine.Initialize();

            using (Py.GIL())
            {
                var pythonScript = Py.Import("wikipedia");
                var result = pythonScript.InvokeMethod("get_all_links", new PyObject[] { new PyString(articleTitle) });
                var pyList = new PyList(result);

                var links = new List<string>();
                foreach (var link in pyList)
                {
                    links.Add(link.ToString());
                }

                return links;
            }
        }
    }
}