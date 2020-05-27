using ImageRecognition.Model.Core.Abstract;
using ImageRecognition.Model.Core.DataModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImageRecognition.Model.Infrastructure.Services
{
    public class GetImageFromFolderService : IGetImageService
    {
        public IEnumerable<PathImageData> GetPathImageDataFromFolder(string folder)
        {
            var images = Directory.GetFiles(folder, "*", searchOption: SearchOption.AllDirectories)
                .Where(x => string.Equals(Path.GetExtension(x), ".jpg", System.StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(Path.GetExtension(x), ".png", System.StringComparison.OrdinalIgnoreCase))
                .Select(x => new PathImageData(x, Directory.GetParent(x).Name, Path.GetFileNameWithoutExtension(x)))
                .ToList()
                ;
            return images;
        }
    }
}
