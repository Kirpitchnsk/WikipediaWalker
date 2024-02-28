using System;
using System.Collections.Generic;
using DevExpress.Diagram.Core;
using DevExpress.Utils;
using DevExpress.XtraDiagram;
using WikipediaWalkerClassLibrary;

namespace WikipediaWalkerDevExpressApp
{
    public class GraphVisualizer
    {
        private readonly DiagramControl _diagramControl;
        private readonly Graph _graph;
        private readonly string _startArticle;
        private readonly string _endArticle;

        public GraphVisualizer(DiagramControl diagramControl, Graph graph, string startArticle, string endArticle)
        {
            _diagramControl = diagramControl;
            _graph = graph;
            _startArticle = startArticle;
            _endArticle = endArticle;

            // Отобразить граф при создании экземпляра класса
            VisualizeGraph();
        }

        public void VisualizeGraph()
        {
            _diagramControl.Items.Clear();

            // Определяем начальную и конечную вершины для выделения цветом
            var startVertex = _startArticle; // Начальная вершина
            var endVertex = _endArticle;   // Конечная вершина

            // Отображаем вершины графа
            var vertexItems = new Dictionary<string, DiagramShape>();
            var rnd = new Random();
            var counter = 0;
            foreach (var vertex in _graph.AdjacencyList.Keys)
            {
                var isStart = vertex == startVertex;
                var isEnd = vertex == endVertex;

                // Генерируем случайное положение вершины
                var position = new PointFloat(rnd.Next(100, _diagramControl.Width - 100), rnd.Next(100, _diagramControl.Height - 100));

                if (isStart) position = new PointFloat(10f, _diagramControl.Height / 2);
                if (isEnd) position = new PointFloat(_diagramControl.Width + 50f, _diagramControl.Height / 2);

                // Создаем графический элемент для вершины

                var vertexShape = new DiagramShape
                {
                    Position = position,
                    Content = vertex,
                    Width = 150,
                    Height = 150,
                    BackgroundId = isStart || isEnd ? DiagramThemeColorId.Black: DiagramThemeColorId.Accent6_1
                };

                // Добавляем вершину в диаграмму
                _diagramControl.Items.Add(vertexShape);
                vertexItems[vertex] = vertexShape;
                counter++;
            }

            // Отображаем ребра графа
            foreach (var startVertexEntry in _graph.AdjacencyList)
            {
                var startVertexName = startVertexEntry.Key;
                var startPosition = vertexItems[startVertexName];
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
                    _diagramControl.Items.Add(edge);
                }
            }
        }
    }
}