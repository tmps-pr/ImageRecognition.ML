using ImageRecognition.Model.Core.Abstract;
using Microsoft.ML;
using System.Linq;
using static Microsoft.ML.DataOperationsCatalog;
using static Microsoft.ML.Transforms.ValueToKeyMappingEstimator;

namespace ImageRecognition.Model.Train.Services
{
    public class DataService : IDataService
    {
        public TrainTestData GetTrainTestData(string folder, MLContext mlContext, IGetImageService imageService)
        {
            var images = imageService.GetPathImageDataFromFolder(folder);
            var imageDateSet = mlContext.Data.LoadFromEnumerable(images);
            var shuffledDataSet = mlContext.Data.ShuffleRows(imageDateSet);

            var data = mlContext.Transforms.Conversion.MapValueToKey("LabelAsKey", "Label", keyOrdinality: KeyOrdinality.ByValue)
                .Append(mlContext.Transforms.LoadRawImageBytes("Image", folder, "Path"))
                .Fit(shuffledDataSet)
                .Transform(shuffledDataSet)
                ;

            var trainTestData = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
           
            return trainTestData;
        }
    }
}
