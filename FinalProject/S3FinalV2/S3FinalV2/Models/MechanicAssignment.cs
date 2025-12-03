using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3FinalV2.Models
{
    public class MechanicAssignment
    {
        [Key]
        public int AssignmentId { get; set; }

        [ForeignKey("MechanicId")]
        public Mechanics MechanicId { get; set; }

        [ForeignKey("AssignedJobId")]
        public AssignedJobs JobId { get; set; }

        public float TimeAssigned { get; set; } 

        public float TimeCompleted { get; set; }

        public string MarkAsCompleted(float completionTime)
        {
            try
            {
                string AssignedMechanic = MechanicId.Name;
                bool? IsCompleted = JobId.IsCompleted;
                string Name = JobId.JobsId.Name;
                float CompletionTime = JobId.ActualCompTime ?? 0;

                if (IsCompleted == true)
                {
                    return $"Job {Name} (ID: {JobId}) is already marked as completed.\nPlease Select A New Job!";
                }
                if (IsCompleted == false)
                {
                    CompletionTime = completionTime;
                    IsCompleted = true;
                }

                return $"Job {Name} (ID: {JobId}) marked as completed by {AssignedMechanic}. " + $"Completion time: {CompletionTime} hours.";
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }

        public override string ToString()
        {
            string AssignedMechanic = MechanicId.Name;
            bool? IsCompleted = JobId.IsCompleted;
            string Name = JobId.JobsId.Name;
            float CompletionTime = JobId.ActualCompTime ?? 0;
            string Description = JobId.JobsId.Description;
            string Priority = JobId.Priority;

            if (IsCompleted == true)
            {
                return $"Job ID: {JobId}, Name: {Name}, Priority: {Priority}, " + $"Assigned Mechanic: {AssignedMechanic}, Is Completed: {IsCompleted}, " + $"Completion Time: {CompletionTime} hours.";
            }
            else
            {
                return $"Job ID: {JobId}, Name: {Name}, Description: {Description}, Priority: {Priority}, " + $"Assigned Mechanic: {AssignedMechanic}, Is Completed: {IsCompleted}";
            }
        }
    }
}