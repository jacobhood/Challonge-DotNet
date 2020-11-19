namespace Challonge.Objects
{
    public class MatchAttachmentAsset
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }

        public MatchAttachmentAsset(byte[] content, string fileName)
        {
            FileName = fileName;
            Content = content;
        }
    }
}
