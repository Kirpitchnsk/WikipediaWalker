using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using WikipediaWalkerClassLibrary;
using WikipediaWalkerDevExpressApp;

namespace WikipediaWalkerWinFormsDevExpressApp
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        Graph graph;
        int shortestDistance;
        int numberOfPaths;
        string startArticle;
        string endArticle;

        public MainForm()
        {
            InitializeComponent();
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
            startArticle = ArticleManager.InputArticleCorrect(startArticleField.Text);
            endArticle = ArticleManager.InputArticleCorrect(endArticleField.Text);
            startArticleField.Text = startArticle.ToString();
            endArticleField.Text= endArticle.ToString();

            if(ArticleManager.CheckSpelling(startArticle,endArticle))
            {
                graph = PathFinder.FindShortestPaths(startArticle, endArticle);
                shortestDistance = graph.Dijkstra(startArticle, endArticle).Count;
                numberOfPaths = graph.CountPaths(startArticle, endArticle);

                saveToFileButton.Visible = true;
                graphVisualizer.Visible = true;

                resultLabel.Text = $"Found {numberOfPaths} paths \n" +
                    $"with shortest distance equals {shortestDistance} between \n {startArticle} и {endArticle}";

                var graphVisualization = new GraphVisualizatior(graphVisualizer);

                graphVisualization.DrawGraph(graph, startArticle, endArticle);
            }
            else
            {
                resultLabel.Text = "Incorrect input or one of arricles does not exists";
            }
           
        }

        private void saveToFileButton_Click(object sender, EventArgs e)
        {
            // Создаем объект диалогового окна сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();

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
                    var saveData = new SaveData(startArticle,endArticle,shortestDistance,numberOfPaths,"","");

                    // Преобразуем объект в JSON строку
                    string json = jsonConverter.ConvertToJson(saveData);

                    // Записываем JSON строку в файл
                    System.IO.File.WriteAllText(filePath, json);

                    XtraMessageBox.Show("Данные успешно сохранены в файл JSON.", "Сохранение завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Произошла ошибка при сохранении файла: {ex.Message}", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
