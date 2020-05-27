using Microsoft.ML;
using Microsoft.ML.Data;
using static Microsoft.ML.DataOperationsCatalog;

namespace ImageRecognition.Model.Core.Abstract
{
    public interface IMLModel
    {
        public void Predict();

        public MulticlassClassificationMetrics Evaluate(IDataView data, ITransformer model);

        public ITransformer Train(IDataView data);

        void Save(ITransformer model, DataViewSchema scheme);

        public MLContext Context { get;  }

        public string Input { get; }
    }
}
