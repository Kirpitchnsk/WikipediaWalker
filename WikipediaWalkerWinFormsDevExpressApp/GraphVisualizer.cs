using System;
using System.Collections.Generic;
using DevExpress.Diagram.Core;
using DevExpress.Utils;
using DevExpress.XtraDiagram;
using WikipediaWalkerClassLibrary;

namespace WikipediaWalkerDevExpressApp
{

    /// <summary>
    /// Класс описывающий отображение графа в элементе DiagramComtrol
    /// </summary>
    public class GraphVisualizer
    {
        private readonly DiagramControl diagramControl;
        private readonly Graph graph;
        private readonly string startArticle;
        private readonly string endArticle;

        public GraphVisualizer(DiagramControl diagramControl, Graph graph, string startArticle, string endArticle)
        {
            this.diagramControl = diagramControl;
            this.graph = graph;
            this.startArticle = startArticle;
            this.endArticle = endArticle;
        }

        public void VisualizeGraph()
        {
            // Очищаем поле для отрисовки
            diagramControl.Items.Clear();

            // Определяем начальную и конечную вершины для выделения цветом
            var startVertex = startArticle; // Начальная вершина
            var endVertex = endArticle;   // Конечная вершина

            // Отображаем вершины графа
            var vertexItems = new Dictionary<string, DiagramShape>();
            var rnd = new Random();

            foreach (var vertex in graph.AdjacencyList.Keys)
            {
                // Начальная ли наша вершина или конечная
                var isStart = vertex == startVertex;
                var isEnd = vertex == endVertex;

                // Генерируем случайное положение вершины
                var position = new PointFloat(rnd.Next(100, diagramControl.Width - 100), rnd.Next(100, diagramControl.Height - 100));

                // В случае если просматриваемая вершина начальная или конечная, она распологаться в опредееленном месте
                if (isStart)
                {
                    position = new PointFloat(10f, diagramControl.Height / 2);
                }
                if (isEnd)
                {
                    position = new PointFloat(diagramControl.Width + 50f, diagramControl.Height / 2);
                }

                // Создаем графический элемент для вершины

                var vertexShape = new DiagramShape
                {
                    Position = position,
                    
                    Content = vertex,
                    
                    Width = 150,
                    Height = 150,
                    BackgroundId = isStart || isEnd ? DiagramThemeColorId.Black: DiagramThemeColorId.Dark
                };

                // Добавляем вершину в диаграмму
                diagramControl.Items.Add(vertexShape);
                vertexItems[vertex] = vertexShape;
            }

            // Отображаем ребра графа
            foreach (var startVertexEntry in graph.AdjacencyList)
            {
                var startVertexName = startVertexEntry.Key;
                foreach (var endVertexEntry in startVertexEntry.Value)
                {
                    var endVertexName = endVertexEntry.Key;

                    // Создаем линию (ребро) между вершинами
                    var edge = new DiagramConnector
                    {
                        BeginItem = vertexItems[startVertexName],
                        EndItem = vertexItems[endVertexName],
                        BeginItemPointIndex = 1,
                        EndItemPointIndex = 1,
                        CanChangeRoute = false,
                        CanDragBeginPoint = false,
                        CanDragEndPoint = false,
                        CanMove = false,
                        CanSelect = false
                    };

                    // Добавляем линию в диаграмму
                    diagramControl.Items.Add(edge);
                }
            }
        }
    }
}