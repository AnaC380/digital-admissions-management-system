using DAMS.Domain.Entities;
using Xunit;

namespace DAMS.Tests
{
    public class AdmissionTests
    {
        [Fact]
        public void CreateAdmission_WithValidData_ShouldSetStatusToPending()
        {
            // Arrange
            var candidateName = "Ana Carolina";
            var userId = Guid.NewGuid();

            // Act
            var admission = new Admission(candidateName, userId);

            // Assert
            Assert.Equal(candidateName, admission.CandidateName);
            Assert.Equal(userId, admission.CreatedByUserId);
        }

        [Fact]
        public void CreateAdmission_WithEmptyName_ShouldThrowException()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Admission("", userId));
        }

        [Fact]
        public void Approve_WhenNotInReview_ShouldThrowException()
        {
            // Arrange
            var admission = new Admission("Ana Carolina", Guid.NewGuid());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => admission.Approve());
        }
    }
}