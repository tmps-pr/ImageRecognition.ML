namespace ImageRecognition.Model.Core.DataModels
{
    public class InMemoryImageData : ImageData
    {
        public InMemoryImageData(byte[] bytes, string label, string name) : base(label, name)
        {
            Bytes = bytes;
        }

        private readonly byte[] Bytes;
    }
}
