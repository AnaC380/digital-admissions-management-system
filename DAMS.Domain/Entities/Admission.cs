using DAMS.Domain.Enums;

namespace DAMS.Domain.Entities
{
    public class Admission
    {
        private const string InvalidApproveMessage =
        "Admission must be in review to be approved.";

        private const string InvalidStartReviewMessage =
            "Admission must be pending to start review.";

        private const string InvalidRejectMessage =
            "Approved admission cannot be rejected.";

        public Guid Id { get; private set; }
        public string CandidateName { get; private set; } = string.Empty;
        public AdmissionStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Guid CreatedByUserId { get; private set; }
        public User CreatedBy { get; private set; } = null!;

        public ICollection<Document> Documents { get; private set; } = new List<Document>();


        protected Admission() { }

        public Admission(string candidateName, User createdBy)
        {
            if (string.IsNullOrWhiteSpace(candidateName))
                throw new ArgumentException("Candidate name is required.", nameof(candidateName));

            Id = Guid.NewGuid();
            CandidateName = candidateName;
            CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));

            CreatedByUserId = createdBy.Id;
            Status = AdmissionStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddDocument(Document document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            Documents.Add(document);
        }


        public void StartReview()
        {
            if (Status != AdmissionStatus.Pending)
                throw new InvalidOperationException(InvalidStartReviewMessage);

            Status = AdmissionStatus.InReview;
        }
        public void Approve()
        {
            if (Status != AdmissionStatus.InReview)
                throw new InvalidOperationException(InvalidApproveMessage);

            Status = AdmissionStatus.Approved;
        }

        public void Reject()
        {
            if (Status == AdmissionStatus.Approved)
                throw new InvalidOperationException(InvalidApproveMessage);

            Status = AdmissionStatus.Rejected;
        }
    }
}
