using InitialProject.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace InitialProject.BusinessLayer.Services;

public class FileHandling : IFileHandling
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileHandling(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    #region Photo Handling

    public async Task<string> UploadFile(IFormFile file, string folder, string oldFilePAth = null)
    {
        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, $"Files/{folder}");
        if (!Directory.Exists(uploads))
        {
            Directory.CreateDirectory(uploads);
        }
        var uniqueFileName = RandomString(10) + "_" + file.FileName;
        var filePath = Path.Combine(uploads, uniqueFileName);
        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        var path = Path.Combine($"/Files/{folder}", uniqueFileName);
        var old = $"{_webHostEnvironment.WebRootPath}/{oldFilePAth}";
        if (oldFilePAth != null && File.Exists(old))
        {
            File.Delete(old);
        }
        return path;
    }

    public async Task<string> UploadPhotoBase64(string stringFile, string folderName = "Student", string oldFilePAth = null)
    {
        var mystr = stringFile.Split(',').ToList();
        var type = mystr[0].Split('/').ToList()[1].Split(';').ToList()[0];
        var byteFile = Convert.FromBase64String(mystr[1]);

        var stream = new MemoryStream(byteFile);
        IFormFile file = new FormFile(stream, 0, byteFile.Length, "Name", folderName);

        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, $"File/{folderName}");
        if (!Directory.Exists(uploads))
        {
            Directory.CreateDirectory(uploads);
        }
        var uniqueFileName = RandomString(10) + "_" + file.FileName + "." + type;
        var filePath = Path.Combine(uploads, uniqueFileName);
        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        var path = Path.Combine($"/File/{folderName}", uniqueFileName);
        var old = $"{_webHostEnvironment.WebRootPath}/{oldFilePAth}";
        if (oldFilePAth != null && File.Exists(old))
        {
            File.Delete(old);
        }
        return path;
    }

    public async Task<string> UploadPhotoByte(byte[] byteFile, string folder = "Student", string oldFilePAth = null)
    {
        var stream = new MemoryStream(byteFile);
        IFormFile file = new FormFile(stream, 0, byteFile.Length, "Name", folder);
        return await UploadFile(file, folder, oldFilePAth);
    }

    public static string RandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public string GetFile(string foo)
    {
        return _webHostEnvironment.WebRootFileProvider.GetFileInfo(foo)?.PhysicalPath;
    }

    #endregion Photo Handling
}