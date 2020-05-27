using ImageRecognition.Model.Core.Abstract;
using ImageRecognition.Model.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ImageRecognition.Model.Train
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly IDataService _dataService;
        private readonly IEnumerable<IMLModel> _models;
        public App(ILogger<App> logger, IDataService dataService, IEnumerable<IMLModel> models)
        {
            _logger = logger;
            _dataService = dataService;
            _models = models;
        }
        public void Run()
        {
            foreach (var model in _models)
            {
                var data = _dataService.GetTrainTestData(model.Input, model.Context, new GetImageFromFolderService());
                var trainedModel = model.Train(data.TrainSet);
                var metrics = model.Evaluate(data.TestSet, trainedModel);
                model.Save(trainedModel, data.TrainSet.Schema);
            }
        }
    }
}
