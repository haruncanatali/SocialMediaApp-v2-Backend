using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using SHFT.Domain.Enums;

namespace SHFT.Application.Common.Managers;

public class FileManager
{
    private readonly IHostingEnvironment _hostingEnvironment;

    public FileManager(IHostingEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public string Upload(IFormFile file, FileRoot root)
    {
        string uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", root.ToString());

        bool exists = Directory.Exists(uploadFolder);

        if (!exists)
            Directory.CreateDirectory(uploadFolder);

        if (file.Length <= 0) return "false";
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName).ToLower();
        var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", root.ToString(), fileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        var url = Path.Combine(root.ToString(), fileName);
        return url;
    }
}