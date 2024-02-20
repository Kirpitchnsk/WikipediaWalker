using Python.Runtime;
using System.Collections.Generic;

namespace WikipediaWalker
{
    public class ArticleGetterPython
    {
        public ArticleGetterPython() 
        {
            Runtime.PythonDLL = @"C:\Users\nskru\AppData\Local\Programs\Python\Python312\python312.dll";
        }

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