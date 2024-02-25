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
            var startArticle = ArticleManager.InputArticleCorrect(startArticleField.Text);
            var endArticle = ArticleManager.InputArticleCorrect(endArticleField.Text);
            startArticleField.Text = startArticle.ToString();
            endArticleField.Text= endArticle.ToString();

            if(ArticleManager.CheckSpelling(startArticle,endArticle))
            {
                var graph = PathFinder.FindShortestPaths(startArticle, endArticle);
                var shortestDistance = graph.Dijkstra(startArticle, endArticle);
                var countPaths = graph.CountPaths(startArticle, endArticle);

                saveToFileButton.Visible = true;
                graphVisualizer.Visible = true;

                resultLabel.Text = $"Found {countPaths} paths \n" +
                    $"with shortest distance equals {shortestDistance.Count} between \n {startArticle} и {endArticle}";

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

        }
    }
}
