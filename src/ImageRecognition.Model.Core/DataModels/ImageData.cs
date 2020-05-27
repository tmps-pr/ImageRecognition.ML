namespace ImageRecognition.Model.Core.DataModels
{
    public abstract class ImageData
    {
        public ImageData(string label, string name)
        {
            Label = label;
            Name = name;
        }

        public readonly string Label;

        public readonly string Name;
    }
}
