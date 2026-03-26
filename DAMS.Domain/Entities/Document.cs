namespace DAMS.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; private set; }
        public string FileName { get; private set; }
        public string FileType { get; private set; }
        public bool IsValid { get; private set; }
        public Guid AdmissionId { get; private set; }
        public Admission Admission { get; private set; }

        protected Document()
        {
            FileName = string.Empty;
            FileType = string.Empty;
            Admission = null!;
        }

        public Document(string fileName, string fileType, Admission admission)
        {
            Id = Guid.NewGuid();
            FileName = fileName;
            FileType = fileType;
            Admission = admission;
            AdmissionId = admission.Id;
            IsValid = false;
        }

        public void Validate() => IsValid = true;
        public void Invalidate() => IsValid = false;
    }
}