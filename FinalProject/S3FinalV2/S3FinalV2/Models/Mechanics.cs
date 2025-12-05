using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3FinalV2.Models
{
    public class Mechanics
    {
        [Key]
        public int MechanicId { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public string? Name { get; set; }

        public int SkillLevel { get; set; }

        public float WeeklyHourLimit { get; set; }

        public float TotalHours { get; set; }

        public int[] AssignedJobs { get; set; } = Array.Empty<int>();

        public int[] CompletedJobs { get; set; } = Array.Empty<int>();
        public string AssignJob(int maxJobId, float hours, int skillLevel)
        {
            try
            {
                string outcome = "";
                int jobId = maxJobId;

                if (TotalHours + hours > 40)
                {
                    outcome = $"Cannot assign shift. All Mechanics would exceed 40 hours.";
                }
                else if (TotalHours + hours < 40 && SkillLevel >= skillLevel)
                {
                    var jobList = AssignedJobs.ToList();
                    jobList.Add(jobId);
                    AssignedJobs = jobList.ToArray();

                    TotalHours += hours;

                    outcome = $"Job {jobId} assigned to {Name}.";
                    return outcome;
                }

                return outcome;
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }

        public void CompleteJob(int jobId)
        {
            try
            {
                AssignedJobs = AssignedJobs.Where(job => job != jobId).ToArray();
                var completedList = CompletedJobs.ToList();
                completedList.Add(jobId);
                CompletedJobs = completedList.ToArray();
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public override string ToString()
        {
            return $"{Name}'s Assigned Jobs: {string.Join(", ", AssignedJobs)}\n";
        }

        public void RemoveAssignedJob(int jobId)
        {
            try
            {
                AssignedJobs = AssignedJobs.Where(job => job != jobId).ToArray();
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}