using Xunit;
using Moq;
using System.Collections.Generic;

public class CopierTests
{
    [Fact]
    public void CopySingleCharacters_CopiesUntilNewline()
    {
        // Arrange
        var mockSource = new Mock<ISource>();
        var mockDestination = new Mock<IDestination>();
        var characterQueue = new Queue<char>(new[] { 'S', 'H', 'I', 'V', 'A', 'M', 'B', 'U', '\n' });

        mockSource.Setup(source => source.RetrieveSingleCharacter()).Returns(() => characterQueue.Dequeue());

        var copierInstance = new Copier(mockSource.Object, mockDestination.Object);

        // Act
        copierInstance.CopySingleCharacters();

        // Assert
        mockDestination.Verify(destination => destination.AcceptSingleCharacter(It.IsAny<char>()), Times.Exactly(8));
        mockDestination.Verify(destination => destination.AcceptSingleCharacter('S'), Times.Once);
        mockDestination.Verify(destination => destination.AcceptSingleCharacter('H'), Times.Once);
        mockDestination.Verify(destination => destination.AcceptSingleCharacter('I'), Times.Once);
        mockDestination.Verify(destination => destination.AcceptSingleCharacter('V'), Times.Once);
        mockDestination.Verify(destination => destination.AcceptSingleCharacter('A'), Times.Once);
        mockDestination.Verify(destination => destination.AcceptSingleCharacter('M'), Times.Once);
        mockDestination.Verify(destination => destination.AcceptSingleCharacter('B'), Times.Once);
        mockDestination.Verify(destination => destination.AcceptSingleCharacter('U'), Times.Once);
    }

    [Fact]
    public void CopyMultipleCharacters_CopiesBatchUntilNewline()
    {
        // Arrange
        var mockSource = new Mock<ISource>();
        var mockDestination = new Mock<IDestination>();
        var batchCharacters = new[] { 'S', 'H', 'I', 'V', 'A', 'M', 'B', 'U', '\n' };

        mockSource.Setup(source => source.RetrieveMultipleCharacters(It.IsAny<int>())).Returns(batchCharacters);

        var copierInstance = new Copier(mockSource.Object, mockDestination.Object);

        // Act
        copierInstance.CopyMultipleCharacters(9);

        // Assert
        mockDestination.Verify(destination => destination.AcceptMultipleCharacters(It.Is<char[]>(chars => chars.Length == 8)), Times.Once);
    }

    [Fact]
    public void CopySingleCharacters_ImmediateNewlineStopsCopy()
    {
        // Arrange
        var mockSource = new Mock<ISource>();
        var mockDestination = new Mock<IDestination>();
        var characterQueue = new Queue<char>(new[] { '\n' });

        mockSource.Setup(source => source.RetrieveSingleCharacter()).Returns(() => characterQueue.Dequeue());

        var copierInstance = new Copier(mockSource.Object, mockDestination.Object);

        // Act
        copierInstance.CopySingleCharacters();

        // Assert
        mockDestination.Verify(destination => destination.AcceptSingleCharacter(It.IsAny<char>()), Times.Never);
    }
}