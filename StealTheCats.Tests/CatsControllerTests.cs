using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StealAllTheCats.API.Controllers;
using StealAllTheCats.Business.Interfaces;
using StealAllTheCats.Dto.Cats;
using StealAllTheCats.Dto.Tags;
using StealAllTheCats.Utilities;

public class CatsControllerTests
{
    private readonly Mock<ICatRepository> _catRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CatsController _controller;

    public CatsControllerTests()
    {
        _catRepositoryMock = new Mock<ICatRepository>();
        _mapperMock = new Mock<IMapper>();
        _controller = new CatsController(_catRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task FetchCats_ReturnsBadRequest_WhenExceptionThrown()
    {
        // Arrange
        _catRepositoryMock.Setup(c => c.AddCatAsync(It.IsAny<List<AddCatRequestDto>>()))
                          .ThrowsAsync(new System.Exception());

        // Act
        var result = await _controller.FetchCats(1);

        // Assert
        var internalError = Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetCat_ReturnsOk_WhenCatExists()
    {
        // Arrange
        var cat = new GetCatResponseDto
        {
            Id = 1,
            CatApiId = "x5",
            Height = 800,
            Width = 600,
            Created = System.DateTime.Now,
            ImagePath = "Cat Photos/x5.jpg",
            Url = "www.google.gr"
        };

        _catRepositoryMock.Setup(c => c.GetCatAsync(1)).ReturnsAsync(cat);

        // Act
        var result = await _controller.GetCat(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(cat, okResult.Value);
    }

    [Fact]
    public async Task GetCat_ReturnsNotFound_WhenCatDoesNotExist()
    {
        // Arrange
        _catRepositoryMock.Setup(c => c.GetCatAsync(1)).ReturnsAsync((GetCatResponseDto)null);

        // Act
        var result = await _controller.GetCat(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetCatsByTag_ReturnsOk_WhenCatsExist()
    {
        var tags = new List<GetTagResponseDto>();
        tags.Add(new GetTagResponseDto { Id = 1, Name = "cute", Created = System.DateTime.Now, CatId = 1 });

        // Arrange
        var pagedCats = new PaginatedResult<GetCatResponseDto>
        {
            Items = new List<GetCatResponseDto> { new GetCatResponseDto { Id = 1, Height = 100, Width = 200, Url = "www.google.gr", Tags = tags } }
        };
        _catRepositoryMock.Setup(c => c.GetCatsAsync("cute", 1, 10)).ReturnsAsync(pagedCats);

        // Act
        var result = await _controller.GetCatsByTag("cute", 1, 10);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(pagedCats, okResult.Value);
    }

    [Fact]
    public async Task GetCatsByTag_ReturnsNotFound_WhenNoCatsExist()
    {
        // Arrange
        var emptyPagedCats = new PaginatedResult<GetCatResponseDto>
        {
            Items = new List<GetCatResponseDto>()
        };

        _catRepositoryMock.Setup(c => c.GetCatsAsync("cute", 1, 10)).ReturnsAsync(emptyPagedCats);

        // Act
        var result = await _controller.GetCatsByTag("cute", 1, 10);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DownloadFileAsync_ReturnsNotFound_WhenImageDoesNotExist()
    {
        // Arrange
        var cat = new GetCatResponseDto { Id = 1, Height = 100, Width = 200, Url = "www.google.gr", ImagePath = "cat.png" };
        _catRepositoryMock.Setup(c => c.GetCatAsync(1)).ReturnsAsync(cat);

        // Act
        var result = await _controller.DownloadFileAsync(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task FetchCats_ReturnsBadRequest_WhenInvalidCatCount()
    {
        // Act
        var result = await _controller.FetchCats(-1); // Invalid catCount

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}