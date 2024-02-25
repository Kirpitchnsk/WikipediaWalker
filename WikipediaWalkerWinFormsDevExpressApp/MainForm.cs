using System;
using WikipediaWalkerClassLibrary;
using WikipediaWalkerDevExpressApp;

namespace WikipediaWalkerWinFormsDevExpressApp
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {

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
            var startArticle = startArticleField.Text;
            var endArticle = endArticleField.Text;

            var graph = PathFinder.FindShortestPaths(startArticle, endArticle);
            var shortestDistance = graph.Dijkstra(startArticle, endArticle);
            var countPaths = graph.CountPaths(startArticle, endArticle);

            saveToFileButton.Visible = true;
            graphVisualizer.Visible = true;

            resultLabel.Text = $"Всего найдено {countPaths} путей c \n" +
                $"Минимальным расстоянием {shortestDistance.Count} между \n {startArticle} и {endArticle}";

            var graphVisualization = new GraphVisualizatior(graphVisualizer);

            graphVisualization.DrawGraph(graph, startArticle, endArticle);

        }

        private void saveToFileButton_Click(object sender, EventArgs e)
        {

        }
    }
}
