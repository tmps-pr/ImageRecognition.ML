using ImageRecognition.Model.Core.DataModels;
using System.Collections.Generic;

namespace ImageRecognition.Model.Core.Abstract
{
    public interface IGetImageService
    {
        public IEnumerable<PathImageData> GetPathImageDataFromFolder(string folder);

    }
}
