using DevExpress.XtraEditors;
using System;
using System.IO;
using System.Windows.Forms;
using WikipediaWalkerClassLibrary;
using WikipediaWalkerDevExpressApp;

namespace WikipediaWalkerWinFormsDevExpressApp
{
    public partial class MainForm : XtraForm
    {
        private ReducedGraph graph;
        private int shortestDistance;
        private int numberOfPaths;
        private string startArticle;
        private string endArticle;
        private ArticleManager articleManager;
        private int maxNumberPaths;
        private int maxLengthPath;
        private string startArticleInfo;
        private string endArticleInfo;

        public MainForm()
        {
            InitializeComponent();
            maxLengthPath = int.MaxValue;
        }

        /// <summary>
        /// Смена местами качальной и конечной статьи
        /// </summary>
        private void reverseButton_Click(object sender, EventArgs e)
        {
            var templateText = startArticleField.Text;
            startArticleField.Text = endArticleField.Text;
            endArticleField.Text = templateText;
        }

        /// <summary>
        /// При нажатии на эту кнопку будут выведены расстояние и изображение графа путей
        /// </summary>
        private void findPathButton_Click(object sender, EventArgs e)
        {
            articleManager = new ArticleManager();

            startArticle = articleManager.ConvertArticleToReadbleForm(startArticleField.Text);
            endArticle = articleManager.ConvertArticleToReadbleForm(endArticleField.Text);
            startArticleField.Text = startArticle.ToString();
            endArticleField.Text = endArticle.ToString();

            if (articleManager.IsArticleExists(startArticle) && articleManager.IsArticleExists(endArticle))
            {
                if (!InputValidator.CheckNumber(distanceNumberField.Text, out maxNumberPaths))
                {
                    XtraMessageBox.Show("Введен неверный формат числа");
                    return;
                }

                saveToFileButton.Visible = true;
                graphVisualizer.Visible = true;

                UpdateData();
            }
            else
            {
                resultLabel.Text = "Incorrect input or one of arricles does not exists";
            }
        }

        private void UpdateData()
        {
            var pythonManager = new PythonManager();

            graph = PathFinder.FindShortestPaths(startArticle, endArticle, maxNumberPaths, maxLengthPath);
            var fullGraph = new ReducedGraph(graph.AllPathsAsArrows);
            shortestDistance = fullGraph.Dijkstra(startArticle, endArticle).Count;
            numberOfPaths = fullGraph.CountPaths(startArticle, endArticle);
            startArticleInfo = pythonManager.GetArticleInfo_Python(startArticle);
            endArticleInfo = pythonManager.GetArticleInfo_Python(endArticle);

            resultLabel.Text = $"Found {numberOfPaths} paths \n" +
                $"with shortest distance equals {shortestDistance} between \n {startArticle} и {endArticle}";

            var graphVisualization = new GraphVisualizer(graphVisualizer, graph, startArticle, endArticle);
            graphVisualization.VisualizeGraph();
        }

        /// <summary>
        /// При нажатии на кнопку сохранения будет открыто диалоговое окно с возможностью ввесли название файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToFileButton_Click(object sender, EventArgs e)
        {
            // Создаем объект диалогового окна сохранения файла
            var saveFileDialog = new SaveFileDialog();

            // Устанавливаем свойства диалогового окна
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            // Если пользователь нажал "ОК" в диалоговом окне
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Получаем путь к файлу из диалогового окна
                string filePath = saveFileDialog.FileName;

                try
                {
                    // Создаем объект jsonConverter
                    var jsonConverter = new JsonConverter<SaveData>();

                    // Создаем объект сохраняемых данных
                    var saveData = new SaveData(startArticle, endArticle, shortestDistance, numberOfPaths, startArticleInfo, endArticleInfo);

                    // Преобразуем объект в JSON строку
                    var json = jsonConverter.ConvertToJson(saveData);

                    // Записываем JSON строку в файл
                    File.WriteAllText(filePath, json);

                    XtraMessageBox.Show("The data has been successfully saved in file JSON.", "Saving is complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"An error occurred durning saving: {ex.Message}", "Error saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void distanceChooseRadioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var edit = sender as RadioGroup;
            maxLengthPath = edit.SelectedIndex + 1;
            if (maxLengthPath == 4)
            {
                maxLengthPath = int.MaxValue;
            }
            if (graphVisualizer.Visible == true)
            {
                UpdateData();
            }
        }
    }
}
