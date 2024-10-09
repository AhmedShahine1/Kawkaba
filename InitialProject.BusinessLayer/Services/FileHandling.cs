using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.RepositoryLayer.Interfaces;
using Kawkaba.Core.Entity.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Kawkaba.BusinessLayer.Services;

public class FileHandling : IFileHandling
{
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IUnitOfWork unitOfWork;

    public FileHandling(IWebHostEnvironment _webHostEnvironment, IUnitOfWork _unitOfWork)
    {
        webHostEnvironment = _webHostEnvironment;
        unitOfWork = _unitOfWork;
    }

    #region Photo Handling

    public async Task<string> UploadFile(IFormFile file, Paths paths, string oldFilePath = null)
    {
        var uploads = Path.Combine(webHostEnvironment.WebRootPath, paths.Name);
        if (!Directory.Exists(uploads))
        {
            Directory.CreateDirectory(uploads);
        }

        var uniqueFileName = $"{RandomString(10)}_{file.FileName}";
        var filePath = Path.Combine(uploads, uniqueFileName);

        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        var image = new Images
        {
            Name = uniqueFileName,
            pathId = paths.Id,
            path = paths
        };
        await unitOfWork.ImagesRepository.AddAsync(image);
        await unitOfWork.SaveChangesAsync();

        if (!string.IsNullOrEmpty(oldFilePath) && File.Exists(Path.Combine(webHostEnvironment.WebRootPath, oldFilePath)))
        {
            File.Delete(Path.Combine(webHostEnvironment.WebRootPath, oldFilePath));
        }

        return image.Id;
    }

    public async Task<string> DefaultProfile(Paths paths)
    {
        var uploads = Path.Combine(webHostEnvironment.WebRootPath, paths.Name);
        var sourcePath = Path.Combine(webHostEnvironment.WebRootPath, "asset", "user.jpg");
        if (!Directory.Exists(uploads))
        {
            Directory.CreateDirectory(uploads);
        }
        var uniqueFileName = $"{RandomString(10)}_UserIcon.jpg";
        var destinationPath = Path.Combine(uploads, uniqueFileName);
        File.Copy(sourcePath, destinationPath, true);
        var image = new Images
        {
            Name = uniqueFileName,
            pathId = paths.Id,
            path = paths
        };
        await unitOfWork.ImagesRepository.AddAsync(image);
        await unitOfWork.SaveChangesAsync();
        return image.Id;
    }

    public async Task<string> GetFile(string imageId)
    {
        var image = await unitOfWork.ImagesRepository
            .FindByQuery(x => x.Id == imageId)
            .Include(s => s.path)
            .FirstOrDefaultAsync();

        if (image == null)
        {
            throw new FileNotFoundException("Image not found.");
        }

        return Path.Combine($"/{image.path.Name}/{image.Name}");
    }

    public async Task<string> UpdateFile(IFormFile file, Paths paths, string imageId)
    {
        var image = await unitOfWork.ImagesRepository
            .FindByQuery(x => x.Id == imageId)
            .Include(s => s.path)
            .FirstOrDefaultAsync();

        if (image == null)
        {
            throw new ArgumentException("Image not found");
        }

        var uploads = Path.Combine(webHostEnvironment.WebRootPath, paths.Name);
        if (!Directory.Exists(uploads))
        {
            Directory.CreateDirectory(uploads);
        }

        var uniqueFileName = $"{RandomString(10)}_{file.FileName}";
        var filePath = Path.Combine(uploads, uniqueFileName);

        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        var oldFilePath = Path.Combine(webHostEnvironment.WebRootPath, $"{image.path.Name}/{image.Name}");
        image.Name = uniqueFileName;
        image.pathId = paths.Id;
        image.path = paths;

        unitOfWork.ImagesRepository.Update(image);
        await unitOfWork.SaveChangesAsync();

        if (File.Exists(oldFilePath))
        {
            File.Delete(oldFilePath);
        }

        return image.Id;
    }

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    #endregion Photo Handling
}
