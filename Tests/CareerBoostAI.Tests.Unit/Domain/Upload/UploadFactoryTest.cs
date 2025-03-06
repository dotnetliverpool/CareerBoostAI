using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.UploadContext;
using CareerBoostAI.Domain.UploadContext.ValueObjects;

namespace CareerBoostAI.Tests.Unit.Domain.Upload;

public class UploadFactoryTest
{
    [Theory]
    [InlineData("https://example.com/file", "email", "document", ".pdf")]
    [InlineData("https://example.com/image", "web", "image", ".jpg")] 
    public void Document_Create_ShouldReturnDocument_WhenValidDataIsProvided(
        string address, string medium, string fileName, string extension)
    {
        // Act
        var document = Document.Create(address, medium, fileName, extension);

        // Assert
        document.ShouldNotBeNull();
        document.Address.ShouldBe(address);
        document.Medium.ShouldBe(medium);
        document.FileName.ShouldBe(fileName);
        document.Extension.ShouldBe(extension);
    }
    
    [Theory]
    [InlineData(null, "email", "document", ".pdf", "Document.Address")] // Null address
    [InlineData("https://example.com/file", null, "document", ".pdf", "Document.Medium")] // Null medium
    [InlineData("https://example.com/file", "email", null, ".pdf", "Document.FileName")] // Null fileName
    [InlineData("https://example.com/file", "email", "document", null, "Document.Extension")] // Null extension
    [InlineData("", "email", "document", ".pdf", "Document.Address")] // Empty address
    [InlineData("https://example.com/file", "", "document", ".pdf", "Document.Medium")] // Empty medium
    [InlineData("https://example.com/file", "email", "", ".pdf", "Document.FileName")] // Empty fileName
    [InlineData("https://example.com/file", "email", "document", "", "Document.Extension")] // Empty extension
    public void Document_Create_ShouldThrowEmptyArgumentException_WhenNullOrEmptyDataIsProvided(
        string address, string medium, string fileName, string extension, string expectedParamName)
    {
        // Act & Assert
        var exception = Should.Throw<EmptyArgumentException>(() => Document.Create(address, medium, fileName, extension));
        exception.Message.ShouldContain(expectedParamName);
    }
    
    [Theory]
    [InlineData("user1@example.com", "https://example.com/file1", "email", "document1", ".pdf")]
    [InlineData("user2@example.com", "https://example.com/file2", "upload", "document2", ".docx")]
    [InlineData("user3@example.com", "https://example.com/file3", "api", "document3", ".txt")]
    public void Create_ShouldReturnUpload_WhenValidDataIsProvided(
        string email, string address, string medium, string fileName, string extension)
    {
        // Arrange
        var id = Guid.NewGuid();
        var dateTimeProvider = new TestDateTimeProvider();

        // Act
        var upload = new UploadFactory(dateTimeProvider).Create(
            id, email, address, medium, fileName, extension);

        // Assert
        upload.ShouldNotBeNull();
        upload.Id.ShouldBe(EntityId.Create(id));
        upload.Id.Value.ShouldBe(id);
        upload.UserEmailAddress.ShouldNotBeNull();
        upload.UserEmailAddress.ShouldBe(Email.Create(email));
        upload.Document.ShouldNotBeNull();
        upload.Document.ShouldBe(Document.Create(address, medium, fileName, extension));
        upload.CreationDateTime.ShouldBe(dateTimeProvider.UtcNow);
    }
}