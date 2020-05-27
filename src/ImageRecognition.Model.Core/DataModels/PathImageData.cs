namespace ImageRecognition.Model.Core.DataModels
{
    public class PathImageData : ImageData
    {
        public PathImageData(string path, string label, string name) : base(label, name)
        {
            Path = path;
        }

        public readonly string Path;
    }
}
