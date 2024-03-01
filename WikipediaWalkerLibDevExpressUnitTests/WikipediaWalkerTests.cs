using WikipediaWalkerClassLibrary;

[TestFixture]
public class WikipediaWalkerTests
{
    private ArticleManager articleManager;
    private Dictionary<string, List<string>> articleLinks;
    private JsonConverter<SaveData> jsonConverter;

    [SetUp]
    public void Setup()
    {
        articleManager = new ArticleManager();
        articleLinks = articleManager.ReadArticleLinks();
        jsonConverter = new JsonConverter<SaveData>();
    }

    [Test]
    public void CheckSpelling_Test()
    {
        Assert.IsTrue(articleManager.IsArticleExists("London"));
        Assert.IsFalse(articleManager.IsArticleExists("Crac"));
        Assert.IsTrue(articleManager.IsArticleExists("Ottoman Empire"));
        Assert.IsFalse(articleManager.IsArticleExists("Z"));
    }

    [Test]
    public void ReadArticleLinks_Test()
    {
        // Вызов тестируемой функции
        var result = articleManager.ReadArticleLinks();

        // Проверка, что возвращенный результат не равен null
        Assert.IsNotNull(result);

        // Проверка, что все статьи и связи прочитаны корректно
        Assert.AreEqual(articleLinks.Count, result.Count);
        foreach (var kvp in articleLinks)
        {
            Assert.IsTrue(result.ContainsKey(kvp.Key));
            CollectionAssert.AreEquivalent(kvp.Value, result[kvp.Key]);
        }
    }

    [Test]
    public void AddEdge_Test()
    {
        // Создание экземпляра класса Graph
        var graph = new Graph();

        // Добавление ребра
        graph.AddEdge("A", "B", 5);

        // Проверка, что ребро добавлено корректно
        Assert.IsTrue(graph.AdjacencyList.ContainsKey("A"));
        Assert.IsTrue(graph.AdjacencyList.ContainsKey("B"));
        Assert.AreEqual(5, graph.AdjacencyList["A"]["B"]);
    }

    [Test]
    public void Dijkstra_Test()
    {
        // Создание экземпляра класса Graph
        var graph = new Graph();

        // Добавление рёбер с весами
        graph.AddEdge("A", "B", 4);
        graph.AddEdge("A", "C", 2);
        graph.AddEdge("B", "C", 5);
        graph.AddEdge("B", "D", 10);
        graph.AddEdge("C", "D", 3);
        graph.AddEdge("D", "E", 7);

        // Вызов метода Dijkstra
        var shortestPath = graph.Dijkstra("A", "E");

        // Проверка, что кратчайший путь найден корректно
        Assert.IsNotNull(shortestPath);
        Assert.IsNotEmpty(shortestPath);
        Assert.AreEqual(4, shortestPath.Count);
        Assert.AreEqual("A", shortestPath[0]);
        Assert.AreEqual("C", shortestPath[1]);
        Assert.AreEqual("D", shortestPath[2]);
        Assert.AreEqual("E", shortestPath[3]);
    }

    [Test]
    public void CountPaths_Test()
    {
        // Создание графа
        var graph = new Graph();
        graph.AddEdge("A", "B", 1);
        graph.AddEdge("A", "C", 1);
        graph.AddEdge("B", "C", 1);
        graph.AddEdge("C", "D", 1);

        // Подсчет путей от A к D
        var result = graph.CountPaths("A", "D");

        // Проверка, что количество путей равно ожидаемому
        Assert.AreEqual(2, result);
    }

    [Test]
    public void FindShortestPaths_Test()
    {
        // Создание экземпляра класса Graph
        var graph = PathFinder.FindShortestPaths("Moscow", "London");

        // Проверка, что граф создан успешно
        Assert.IsNotNull(graph);

        // Проверка, что граф содержит ожидаемые вершины и рёбра
        Assert.IsTrue(graph.AdjacencyList.ContainsKey("Moscow"));
        Assert.IsTrue(graph.AdjacencyList.ContainsKey("London"));
        Assert.AreEqual(1, graph.AdjacencyList["Moscow"]["London"]);
    }
    public void ConvertToJson_WhenValidObjectProvided_ReturnsJsonString()
    {
        // Arrange
        var data = new SaveData("Moscow","London",2, "Moscow -> London",526,"Start Info","End Info");

        // Act
        var jsonString = jsonConverter.ConvertToJson(data);

        // Assert
        Assert.IsNotNull(jsonString);
        Assert.IsNotEmpty(jsonString);
        Assert.IsTrue(jsonString.Contains("Moscow"));
        Assert.IsTrue(jsonString.Contains("London"));
        Assert.IsTrue(jsonString.Contains("2"));
        Assert.IsTrue(jsonString.Contains("Moscow -> London"));
        Assert.IsTrue(jsonString.Contains("526"));
        Assert.IsTrue(jsonString.Contains("Start Info"));
        Assert.IsTrue(jsonString.Contains("End Info"));
    }

    [Test]
    public void ConvertFromJson_WhenValidJsonStringProvided_ReturnsObject()
    {
        // Arrange
        string jsonString = "{\"StartArticle\":\"Moscow\",\"EndArticle\":\"London\",\"ShortestDistance\":2,\"ShortestPath\":\"Moscow -> London\",\"NumberOfPath\":526,\"StartArticleInfo\":\"\",\"EndArticleInfo\":\"\"}";

        // Act
        SaveData deserializedData = jsonConverter.ConvertFromJson(jsonString);

        // Assert
        Assert.IsNotNull(deserializedData);
        Assert.AreEqual("Moscow", deserializedData.StartArticle);
        Assert.AreEqual(2, deserializedData.ShortestDistance);
        Assert.AreEqual("Moscow -> London", deserializedData.ShortestPath);
        Assert.AreEqual(526, deserializedData.NumberOfPath);
        Assert.AreEqual("London", deserializedData.EndArticle);
        Assert.AreEqual("", deserializedData.StartArticleInfo);
        Assert.AreEqual("", deserializedData.EndArticleInfo);
    }

    [Test]
    public void ConvertFromJson_WhenInvalidJsonStringProvided_ReturnsDefaultObject()
    {
        // Arrange
        var invalidJsonString = "invalid json string";

        // Act
        var deserializedData = jsonConverter.ConvertFromJson(invalidJsonString);

        // Assert
        Assert.IsNull(deserializedData);
    }
}
