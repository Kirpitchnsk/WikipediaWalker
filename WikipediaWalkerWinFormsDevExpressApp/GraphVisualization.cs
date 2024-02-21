using DevExpress.Diagram.Core;
using DevExpress.Mvvm.Native;
using DevExpress.Utils.Extensions;
using DevExpress.XtraDiagram;
using System;
using System.Collections.Generic;
using WikipediaWalkerClassLibrary;

namespace WikipediaWalkerDevExpressApp
{
    public class GraphVisualization
    {
        private DiagramControl diagramControl;
        private Dictionary<string, DiagramShape> vertexItems;

        public GraphVisualization(DiagramControl diagramControl)
        {
            this.diagramControl = diagramControl;
            this.vertexItems = new Dictionary<string, DiagramShape>();
        }

        private void AddDiagramShape(string vertex, int index, int totalVertices)
        {
            var shape = new DiagramShape();
            shape.Width = 30;
            shape.Height = 15;

            float radius = 1500; 
            float centerX = diagramControl.Width / 2; 
            float centerY = diagramControl.Height / 2;
            double angle = (2 * Math.PI * index) / totalVertices; 
            float x = centerX + (float)(radius * Math.Cos(angle));
            float y = centerY + (float)(radius * Math.Sin(angle));

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

            int totalVertices = graph.adjacencyList.Count;
            int index = 0;

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
