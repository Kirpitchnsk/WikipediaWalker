using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WikipediaWalker;

namespace WikipediaWalkerWinFormsDevExpressApp
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {

        public MainForm()
        {
            InitializeComponent();
            saveToFileButton.Visible = false;
            graphVisualizer.Visible = false;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void reverseButton_Click(object sender, EventArgs e)
        {
            var templateText = startArticleField.Text;
            startArticleField.Text = endArticleField.Text;
            endArticleField.Text = templateText;
        }

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

            var graphVisualization = new GraphVisualization(graphVisualizer);

            graphVisualization.DrawGraph(graph, startArticle, endArticle);

        }
    }
}
