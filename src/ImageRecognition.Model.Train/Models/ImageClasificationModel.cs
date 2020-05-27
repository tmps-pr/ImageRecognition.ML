using ImageRecognition.Model.Core.Abstract;
using ImageRecognition.Model.Train.Options;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Vision;
using System;
using System.IO;

namespace ImageRecognition.Model.Train.Models
{
    public class ImageClasificationModel : IMLModel
    {
        private readonly ModelOptions _options;
        public ImageClasificationModel(ModelOptions options)
        {
            _options = options;
            Context = new MLContext(1);
            Context.Log += Context_Log;
            Input = options.Input;
        }
        public string Input { get;  }
        private void Context_Log(object sender, LoggingEventArgs e)
        {
            if (e.Message.StartsWith("[Source=ImageClassificationTrainer;"))
            {
                Console.WriteLine(e.Message);
            }
        }

        public MLContext Context { get; }

        public MulticlassClassificationMetrics Evaluate(IDataView data, ITransformer model)
        {
            var predictionsDataView = model.Transform(data);
            var metrics = Context.MulticlassClassification.Evaluate(predictionsDataView, "LabelAsKey", predictedLabelColumnName: "PredictedLabel");
            return metrics;
        }

        public void Predict()
        {
            throw new NotImplementedException();
        }

        public void Save(ITransformer model, DataViewSchema scheme)
        {
            var modelPath = Path.Combine(_options.Output, $"{_options.Name}.zip");
            Context.Model.Save(model, scheme, modelPath);
        }

        public ITransformer Train(IDataView data)
        {
            var options = new ImageClassificationTrainer.Options()
            {
                BatchSize = _options.BatchSize,
                FeatureColumnName = "Image",
                LabelColumnName = "LabelAsKey",
                LearningRate = _options.LearningRate,
                Epoch = _options.Epoch,
                Arch = ImageClassificationTrainer.Architecture.InceptionV3,
                WorkspacePath =_options.Output
            };

            var pipeline = Context.MulticlassClassification.Trainers.ImageClassification(options)
                    .Append(Context.Transforms.Conversion.MapKeyToValue(
                        outputColumnName: "PredictedLabel",
                        inputColumnName: "PredictedLabel"));
            var model = pipeline.Fit(data);
            return model;
        }
    }
}
