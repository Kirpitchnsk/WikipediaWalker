using DevExpress.Diagram.Core;
using DevExpress.XtraDiagram;
using System;
using System.Collections.Generic;
using WikipediaWalkerClassLibrary;

namespace WikipediaWalkerDevExpressApp
{

    /// <summary>
    /// Класс который изображает граф путей на интерфейсе
    /// </summary>
    public class GraphVisualizatior
    {
        /// <summary>
        /// Инициализация поля для рисования
        /// </summary>
        private DiagramControl diagramControl;

        /// <summary>
        /// Список вершин графа
        /// </summary>
        private Dictionary<string, DiagramShape> vertexItems;

        /// <summary>
        /// Инициализация инструмента для изображения графа
        /// </summary>
        /// <param name="diagramControl">Элемент DiagramControl</param>
        public GraphVisualizatior(DiagramControl diagramControl)
        {
            this.diagramControl = diagramControl;
            vertexItems = new Dictionary<string, DiagramShape>();
        }

        
        private void AddDiagramShape(string vertex, int index, int totalVertices)
        {
            var shape = new DiagramShape();
            shape.Width = 30;
            shape.Height = 15;

            var radius = 1500; 
            var centerX = diagramControl.Width / 2; 
            var centerY = diagramControl.Height / 2;
            var angle = (2 * Math.PI * index) / totalVertices; 
            var x = centerX + (float)(radius * Math.Cos(angle));
            var y = centerY + (float)(radius * Math.Sin(angle));

            shape.Position = new DevExpress.Utils.PointFloat(x, y);
            shape.Content = vertex;
            diagramControl.Items.Add(shape);
            vertexItems[vertex] = shape;
        }

        public void DrawGraph(Graph graph, string startVertex, string endVertex)
        {
            // Очищаем диаграмму перед рисованием нового графа
            diagramControl.Items.Clear();
            vertexItems.Clear();

            var totalVertices = graph.adjacencyList.Count;
            var index = 0;

            // Отображаем узлы
            foreach (var vertex in graph.adjacencyList.Keys)
            {
                AddDiagramShape(vertex, index++, totalVertices);
            }
            // Отображаем ребра
            foreach (var edge in graph.adjacencyList)
            {
                foreach (var item in edge.Value)
                {
                    AddDiagramConnector(edge.Key, item.Key, item.Value);
                }
            }

            // Выделяем начальную вершину
            HighlightVertex(startVertex, DiagramThemeColorId.Accent1);

            // Выделяем конечную вершину
            HighlightVertex(endVertex, DiagramThemeColorId.Accent2);
        }

        private void AddDiagramConnector(string sourceVertex, string targetVertex, int weight)
        {
            var connector = new DiagramConnector();
            connector.BeginItem = vertexItems[sourceVertex];
            connector.EndItem = vertexItems[targetVertex];
            connector.Content = weight.ToString();

            // Установим цвет ребра в зависимости от веса
            if (weight == 1)
            {
                connector.BackgroundId = DiagramThemeColorId.Accent3;
            }
            else if (weight == 2)
            {
                connector.BackgroundId = DiagramThemeColorId.Accent4;
            }
            else
            {
                connector.BackgroundId = DiagramThemeColorId.Accent5;
            }

            diagramControl.Items.Add(connector);
        }

        private void HighlightVertex(string vertex, DiagramThemeColorId color)
        {
            if (vertexItems.ContainsKey(vertex))
            {
                var item = vertexItems[vertex];
                item.BackgroundId = color;
            }
        }
    }

}
