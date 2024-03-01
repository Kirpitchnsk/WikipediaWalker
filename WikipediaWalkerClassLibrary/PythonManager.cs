using Python.Runtime;
using System.Text;

namespace WikipediaWalkerClassLibrary
{
    public class PythonManager
    {

        private static string pythonScriptName = "python_script.py";
        private static string filePath;
        private static int maxPythonVersion = 12;

        private static string FindPythonDllPath()
        {
            var pathVariable = Environment.GetEnvironmentVariable("Path");

            for (int i = 8; i <= maxPythonVersion; i++)
            {
                var version = "3"+i;
                
                if (pathVariable != null)
                {
                    var paths = pathVariable.Split(';');

                    foreach (var path in paths)
                    {
                        try
                        {
                            var pythonDllPath = Directory.GetFiles(path, $"python{version}.dll", SearchOption.TopDirectoryOnly).FirstOrDefault();

                            if (pythonDllPath != null)
                            {
                                return pythonDllPath;
                            }
                        }
                        catch(DirectoryNotFoundException)
                        {
                            continue;
                        }
                        catch(ArgumentException)
                        {
                            continue;
                        }
                        catch(UnauthorizedAccessException)
                        {
                            continue;
                        }
                    }
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Класс, взаимодействующий с Pyhton.NET. Инициализация движка python
        /// </summary>
        public PythonManager() 
        {
            //Здесь нужен путь в компьютере к этому файлу
            //Runtime.PythonDLL = "C:\\Users\\nskru\\AppData\\Local\\Programs\\Python\\Python312\\python312.dll";
            Runtime.PythonDLL = FindPythonDllPath();
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