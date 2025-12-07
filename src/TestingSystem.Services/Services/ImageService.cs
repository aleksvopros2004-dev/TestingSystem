using System.Drawing;
using System.Drawing.Imaging;
using TestingSystem.Services.Interfaces;

namespace TestingSystem.Services.Services;

public class ImageService : IImageService
{
    public byte[]? LoadImageFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                return null;

            return File.ReadAllBytes(filePath);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public byte[]? ResizeImage(byte[] imageData, int maxWidth, int maxHeight)
    {
        try
        {
            using var ms = new MemoryStream(imageData);
            using var image = System.Drawing.Image.FromStream(ms); // Явно указываем пространство имен

            // Вычисляем новые размеры с сохранением пропорций
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            // Если изображение меньше максимальных размеров, возвращаем оригинал
            if (newWidth >= image.Width && newHeight >= image.Height)
                return imageData;

            // Создаем новое изображение с нужными размерами
            using var newImage = new Bitmap(newWidth, newHeight);
            using var graphics = Graphics.FromImage(newImage);

            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            // Конвертируем в массив байтов
            using var resultMs = new MemoryStream();
            newImage.Save(resultMs, ImageFormat.Jpeg);
            return resultMs.ToArray();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public bool IsValidImage(byte[] imageData)
    {
        try
        {
            using var ms = new MemoryStream(imageData);
            using var image = System.Drawing.Image.FromStream(ms); // Явно указываем пространство имен
            return true;
        }
        catch
        {
            return false;
        }
    }

    public string GetImageContentType(byte[] imageData)
    {
        try
        {
            using var ms = new MemoryStream(imageData);
            using var image = System.Drawing.Image.FromStream(ms); // Явно указываем пространство имен

            if (ImageFormat.Jpeg.Equals(image.RawFormat))
                return "image/jpeg";
            else if (ImageFormat.Png.Equals(image.RawFormat))
                return "image/png";
            else if (ImageFormat.Gif.Equals(image.RawFormat))
                return "image/gif";
            else if (ImageFormat.Bmp.Equals(image.RawFormat))
                return "image/bmp";
            else
                return "image/jpeg"; // По умолчанию
        }
        catch
        {
            return "image/jpeg";
        }
    }

    public string GetImageContentTypeFromExtension(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLower();

        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            _ => "image/jpeg"
        };
    }

    public long GetImageSizeInKB(byte[] imageData)
    {
        return imageData.Length / 1024;
    }
}