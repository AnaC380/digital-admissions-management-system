namespace DAMS.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; private set; }
        public string FileName { get; private set; }
        public string FileType { get; private set; }
        public bool IsValid { get; private set; }

        protected Document() { }

        public Document(string fileName, string fileType)
        {
            Id = Guid.NewGuid();
            FileName = fileName;
            FileType = fileType;
            IsValid = false;
        }

        public void Validate() => IsValid = true;
        public void Invalidate() => IsValid = false;
    }
}
