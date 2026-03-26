namespace DAMS.Application;

public class AdmissionDto
{
    public Guid Id { get; set; }
    public string CandidateName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public int DocumentCount { get; set; }
}