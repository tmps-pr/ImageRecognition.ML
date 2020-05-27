using Microsoft.ML;
using static Microsoft.ML.DataOperationsCatalog;

namespace ImageRecognition.Model.Core.Abstract
{
    public interface IDataService
    {
        TrainTestData GetTrainTestData(string folder, MLContext mlContext, IGetImageService imageService);
    }
}
