using DataEntities;

namespace TinyShop.Tests;

[TestClass]
public class ProductTests
{
    [TestMethod]
    public void Product_NewInstance_HasDefaultValues()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.AreEqual(0, product.Id, "Id should default to 0");
        Assert.IsNull(product.Name, "Name should default to null");
        Assert.IsNull(product.Description, "Description should default to null");
        Assert.AreEqual(0m, product.Price, "Price should default to 0");
        Assert.IsNull(product.ImageUrl, "ImageUrl should default to null");
    }

    [TestMethod]
    public void Product_SetId_ReturnsCorrectValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Id = 42;

        // Assert
        Assert.AreEqual(42, product.Id, "Id should return the value that was set");
    }

    [TestMethod]
    public void Product_SetName_ReturnsCorrectValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Name = "Test Product";

        // Assert
        Assert.AreEqual("Test Product", product.Name, "Name should return the value that was set");
    }

    [TestMethod]
    public void Product_SetDescription_ReturnsCorrectValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Description = "Test Description";

        // Assert
        Assert.AreEqual("Test Description", product.Description, "Description should return the value that was set");
    }

    [TestMethod]
    public void Product_SetPrice_ReturnsCorrectValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Price = 19.99m;

        // Assert
        Assert.AreEqual(19.99m, product.Price, "Price should return the value that was set");
    }

    [TestMethod]
    public void Product_SetImageUrl_ReturnsCorrectValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.ImageUrl = "https://example.com/image.png";

        // Assert
        Assert.AreEqual("https://example.com/image.png", product.ImageUrl, "ImageUrl should return the value that was set");
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(100)]
    [DataRow(int.MaxValue)]
    [DataRow(0)]
    public void Product_SetId_WithMultipleValues_ReturnsCorrectValue(int expectedId)
    {
        // Arrange
        var product = new Product();

        // Act
        product.Id = expectedId;

        // Assert
        Assert.AreEqual(expectedId, product.Id, $"Id should be {expectedId}");
    }

    [TestMethod]
    [DataRow("Widget")]
    [DataRow("Gadget Pro")]
    [DataRow("")]
    [DataRow(null)]
    public void Product_SetName_WithMultipleValues_ReturnsCorrectValue(string? expectedName)
    {
        // Arrange
        var product = new Product();

        // Act
        product.Name = expectedName;

        // Assert
        Assert.AreEqual(expectedName, product.Name, $"Name should be {expectedName ?? "null"}");
    }

    [TestMethod]
    [DataRow("A short description")]
    [DataRow("A much longer description that contains many words and details about the product")]
    [DataRow("")]
    [DataRow(null)]
    public void Product_SetDescription_WithMultipleValues_ReturnsCorrectValue(string? expectedDescription)
    {
        // Arrange
        var product = new Product();

        // Act
        product.Description = expectedDescription;

        // Assert
        Assert.AreEqual(expectedDescription, product.Description, $"Description should be {expectedDescription ?? "null"}");
    }

    [TestMethod]
    [DataRow("0")]
    [DataRow("9.99")]
    [DataRow("199.99")]
    [DataRow("1000.00")]
    public void Product_SetPrice_WithMultipleValues_ReturnsCorrectValue(string priceString)
    {
        // Arrange
        var product = new Product();
        var expectedPrice = decimal.Parse(priceString);

        // Act
        product.Price = expectedPrice;

        // Assert
        Assert.AreEqual(expectedPrice, product.Price, $"Price should be {expectedPrice}");
    }

    [TestMethod]
    [DataRow("https://example.com/product1.jpg")]
    [DataRow("https://cdn.store.com/images/item.png")]
    [DataRow("/images/local.gif")]
    [DataRow(null)]
    public void Product_SetImageUrl_WithMultipleValues_ReturnsCorrectValue(string? expectedImageUrl)
    {
        // Arrange
        var product = new Product();

        // Act
        product.ImageUrl = expectedImageUrl;

        // Assert
        Assert.AreEqual(expectedImageUrl, product.ImageUrl, $"ImageUrl should be {expectedImageUrl ?? "null"}");
    }
}
