using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;

namespace Demo.PL.Helper
{ 
    public static class DocumentSettings //Helper class using to make upload and delete to file
    {

        public static async Task<string>  UploadFile(IFormFile file, string folderName)
        {
            // 1. Get Located Folder Path 

            //string folderPath = $"D:\\youssef\\rout full-stack\\dotNet\\code\\mvc\\Route.C41.G03\\Route.C41.G03PL\\wwwroot\\Files\\{folderName}";
            //string folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{folderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 2. get file name and make it Unique 

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // 3. get file path 

            string filePath = Path.Combine(folderPath, fileName);

            // 4. save file as streams 

            using var fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return fileName;

        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }


}

