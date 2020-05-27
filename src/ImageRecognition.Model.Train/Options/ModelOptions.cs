namespace ImageRecognition.Model.Train.Options
{
    public class ModelOptions
    {
        public string Name { get; set; }

        public int Epoch { get; set; }

        public int BatchSize { get; set; }

        public float LearningRate { get; set; }

        public string Input { get; set; }

        public string Output { get; set; }
    }
}
