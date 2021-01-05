using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Notes.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Data
{
    public static class WorkWithFile
    {

        public const string DEFAULT_PATH = @"C:\Users\Admin Admin\Desktop\Data";
        private static readonly List<string> enabledTypes = new List<string>()
        {
            "application/pdf",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "application/msword",
            "text/plain",
            "application/vnd.oasis.opendocument.presentation",
            "application/vnd.oasis.opendocument.spreadsheet",
            "application/vnd.oasis.opendocument.text",
            "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            "application/x-shockwave-flash",
            "application/vnd.ms-excel",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "application/xml",
            "text/xml",


        };

        public static async Task SaveFileAsync(IFormFile file, string generatedPath)
        {
            if (enabledTypes.Any(enabledType => enabledType == file.ContentType))
            {
                await using var stream = File.Create(generatedPath);
                await using var getBytes = new MemoryStream();

                await file.CopyToAsync(getBytes);
                var bytes = getBytes.ToArray();
                await stream.WriteAsync(bytes, 0, (int)file.Length);

                getBytes.Close();
                stream.Close();
            }
            else throw new InvalidOperationException();
        }
      
    }
}
