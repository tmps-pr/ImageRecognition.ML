using Microsoft.ML.Data;

namespace ImageRecognition.Model.Core.DataModels
{
    public class ImagePrediction
    {
        [ColumnName("Score")]
        public float[] Score;

        [ColumnName("PredictedLabel")]
        public string[] PredictedLabel;
    }
}
