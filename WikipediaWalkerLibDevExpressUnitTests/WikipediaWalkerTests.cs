using WikipediaWalkerClassLibrary;

[TestFixture]
public class WikipediaWalkerTests
{
    private Dictionary<string, List<string>> articleLinks;
    private JsonConverter<Person> jsonConverter;

    [SetUp]
    public void Setup()
    {
        // Инициализация данных перед каждым тестом
        articleLinks = new Dictionary<string, List<string>>
        {
            { "Article1", new List<string> { "Link1", "Link2" } },
            { "Article2", new List<string> { "Link3", "Link4" } },
            { "Article3", new List<string> { "Link5", "Link6" } }
            // Добавьте другие статьи и их связи при необходимости
        };
        jsonConverter = new JsonConverter<Person>();
    }

    [Test]
    public void ReadArticleLinks_Test()
    {
        // Вызов тестируемой функции
        var result = ArticleGetter.ReadArticleLinks();

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

    [TestCase("Article1", new string[] { "Link1", "Link2" })]
    [TestCase("Article2", new string[] { "Link3", "Link4" })]
    [TestCase("Article3", new string[] { "Link5", "Link6" })]
    public void FindArticleLinks_Test(string article, string[] expectedLinks)
    {
        // Вызов тестируемой функции

        var articleGetter = new ArticleGetter();
        var result = articleGetter.FindArticleLinks(article);

        // Проверка, что возвращенный результат соответствует ожидаемым связям
        CollectionAssert.AreEquivalent(expectedLinks, result);
    }

    [Test]
    public void AddEdge_Test()
    {
        // Создание экземпляра класса Graph
        var graph = new Graph();

        // Добавление ребра
        graph.AddEdge("A", "B", 5);

        // Проверка, что ребро добавлено корректно
        Assert.IsTrue(graph.adjacencyList.ContainsKey("A"));
        Assert.IsTrue(graph.adjacencyList.ContainsKey("B"));
        Assert.AreEqual(5, graph.adjacencyList["A"]["B"]);
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
        List<string> shortestPath = graph.Dijkstra("A", "E");

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
        var graph = PathFinder.FindShortestPaths("A", "E");

        // Проверка, что граф создан успешно
        Assert.IsNotNull(graph);

        // Проверка, что граф содержит ожидаемые вершины и рёбра
        Assert.IsTrue(graph.adjacencyList.ContainsKey("A"));
        Assert.IsTrue(graph.adjacencyList.ContainsKey("B"));
        Assert.IsTrue(graph.adjacencyList.ContainsKey("C"));
        Assert.IsTrue(graph.adjacencyList.ContainsKey("D"));
        Assert.IsTrue(graph.adjacencyList.ContainsKey("E"));
        Assert.AreEqual(4, graph.adjacencyList["A"]["B"]);
        Assert.AreEqual(2, graph.adjacencyList["A"]["C"]);
        Assert.AreEqual(5, graph.adjacencyList["B"]["C"]);
        Assert.AreEqual(10, graph.adjacencyList["B"]["D"]);
        Assert.AreEqual(3, graph.adjacencyList["C"]["D"]);
        Assert.AreEqual(7, graph.adjacencyList["D"]["E"]);
    }

    [Test]
    public void ConvertToJson_WhenValidObjectProvided_ReturnsJsonString()
    {
        // Arrange
        var person = new Person { Name = "John", Age = 30 };

        // Act
        string jsonString = jsonConverter.ConvertToJson(person);

        // Assert
        Assert.IsNotNull(jsonString);
        Assert.IsNotEmpty(jsonString);
        Assert.IsTrue(jsonString.Contains("John"));
        Assert.IsTrue(jsonString.Contains("30"));
    }

    [Test]
    public void ConvertFromJson_WhenValidJsonStringProvided_ReturnsObject()
    {
        // Arrange
        string jsonString = "{\"Name\":\"Jane\",\"Age\":25}";

        // Act
        Person deserializedPerson = jsonConverter.ConvertFromJson(jsonString);

        // Assert
        Assert.IsNotNull(deserializedPerson);
        Assert.AreEqual("Jane", deserializedPerson.Name);
        Assert.AreEqual(25, deserializedPerson.Age);
    }

    [Test]
    public void ConvertFromJson_WhenInvalidJsonStringProvided_ReturnsDefaultObject()
    {
        // Arrange
        var invalidJsonString = "invalid json string";

        // Act
        var deserializedPerson = jsonConverter.ConvertFromJson(invalidJsonString);

        // Assert
        Assert.IsNull(deserializedPerson);
    }
}
