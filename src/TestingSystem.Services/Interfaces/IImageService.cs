namespace TestingSystem.Services.Interfaces;

public interface IImageService
{
    byte[]? LoadImageFromFile(string filePath);
    byte[]? ResizeImage(byte[] imageData, int maxWidth, int maxHeight);
    bool IsValidImage(byte[] imageData);
    string GetImageContentType(byte[] imageData);
    string GetImageContentTypeFromExtension(string filePath);
    long GetImageSizeInKB(byte[] imageData);
}