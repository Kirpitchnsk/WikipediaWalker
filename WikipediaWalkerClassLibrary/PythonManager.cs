using Python.Runtime;
using System.Text;

namespace WikipediaWalkerClassLibrary
{
    public class PythonManager
    {

        private static string pythonScriptName = "python_script.py";
        private static string filePath;

        /// <summary>
        /// Класс, взаимодействующий с Pyhton.NET. Инициализация движка python
        /// </summary>
        public PythonManager() 
        {
            //Здесь нужен путь в компьютере к этому файлу
            Runtime.PythonDLL = "C:\\Users\\nskru\\AppData\\Local\\Programs\\Python\\Python312\\python312.dll";
            // Получаем путь к папке проекта
            var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            // Формируем полный путь к файлу
            filePath = Path.Combine(projectDirectory, pythonScriptName);

            var newFileName = "wiki.py";
            if (!File.Exists(newFileName))
            {
                File.Copy(filePath,newFileName);
            }
            pythonScriptName = newFileName.Substring(0, newFileName.Length - 3);
        }

        /// <summary>
        /// Получение ссылок содержащиеся в статье с использованием python
        /// </summary>
        /// <param name="articleTitle"></param>
        /// <returns>Возвращает список всех связей текущей статьи</returns>
        public List<string> GetLinks_Python(string articleTitle)
        {
            if(filePath != null) 
            {
                PythonEngine.Initialize();

                using (Py.GIL())
                {
                    var pythonScript = Py.Import(pythonScriptName);
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
            else
            {
                return new List<string>();
            }
        }

        public string GetArticleInfo_Python(string articleTitle)
        {
            if(filePath != null)
            {
                PythonEngine.Initialize();

                using (Py.GIL())
                {
                    var pythonScript = Py.Import(pythonScriptName);
                    var result = pythonScript.InvokeMethod("get_wikipedia_info", new PyObject[] { new PyString(articleTitle) });
                    var articleInfo = new PyString(result);

                    return articleInfo.ToString();
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}