using Xunit;
using Moq;
using ECommerce.API.Services;
using ECommerce.API.Models;
using ECommerce.API.Repository;
using System.Collections.Generic;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public void CreateUser_AssignsAdminRole_WhenUsernameEndsWithAdmin()
    {
        // Arrange
        var user = new User { Username = "test.admin" };
        _userRepositoryMock.Setup(repo => repo.Create(It.IsAny<User>()))
            .Returns<User>(u => u);

        // Act
        var result = _userService.CreateUser(user);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsAdmin);
        _userRepositoryMock.Verify(repo => repo.Create(It.Is<User>(u => u.IsAdmin)), Times.Once);
    }

    [Fact]
    public void CreateUser_DoesNotAssignAdminRole_WhenUsernameDoesNotEndWithAdmin()
    {
        // Arrange
        var user = new User { Username = "test.user" };
        _userRepositoryMock.Setup(repo => repo.Create(It.IsAny<User>()))
            .Returns<User>(u => u);

        // Act
        var result = _userService.CreateUser(user);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsAdmin);
        _userRepositoryMock.Verify(repo => repo.Create(It.Is<User>(u => !u.IsAdmin)), Times.Once);
    }

    [Fact]
    public void GetUser_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var user = new User { Id = 1, Username = "test.user" };
        _userRepositoryMock.Setup(repo => repo.GetById(1)).Returns(user);

        // Act
        var result = _userService.GetUser(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test.user", result.Username);
        _userRepositoryMock.Verify(repo => repo.GetById(1), Times.Once);
    }

    [Fact]
    public void GetUser_ReturnsNull_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetById(1)).Returns((User?)null);

        // Act
        var result = _userService.GetUser(1);

        // Assert
        Assert.Null(result);
        _userRepositoryMock.Verify(repo => repo.GetById(1), Times.Once);
    }

    [Fact]
    public void GetAllUsers_ReturnsListOfUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Username = "user1" },
            new User { Id = 2, Username = "user2" }
        };
        _userRepositoryMock.Setup(repo => repo.GetAll()).Returns(users);

        // Act
        var result = _userService.GetAllUsers();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _userRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public void UpdateUser_UpdatesUserSuccessfully()
    {
        // Arrange
        var existingUser = new User { Id = 1, Username = "old.user", IsAdmin = false };
        var updatedUser = new User { Username = "new.admin" };

        _userRepositoryMock.Setup(repo => repo.GetById(1)).Returns(existingUser);
        _userRepositoryMock.Setup(repo => repo.Update(It.IsAny<User>())).Returns<User>(u => u);

        // Act
        var result = _userService.UpdateUser(1, updatedUser);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("new.admin", result.Username);
        Assert.True(result.IsAdmin);
        _userRepositoryMock.Verify(repo => repo.Update(It.Is<User>(u => u.Username == "new.admin" && u.IsAdmin)), Times.Once);
    }

    [Fact]
    public void DeleteUser_RemovesUser_WhenUserExists()
    {
        // Arrange
        var user = new User { Id = 1, Username = "testuser" };
        _userRepositoryMock.Setup(repo => repo.GetById(1)).Returns(user);

        // Act
        _userService.DeleteUser(1);

        // Assert
        _userRepositoryMock.Verify(repo => repo.Delete(It.Is<User>(u => u.Id == 1)), Times.Once);
    }

    [Fact]
    public void DeleteUser_DoesNothing_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetById(1)).Returns((User?)null);

        // Act
        _userService.DeleteUser(1);

        // Assert
        _userRepositoryMock.Verify(repo => repo.Delete(It.IsAny<User>()), Times.Never);
    }
}
