using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3FinalV2.Models
{
    public class MechanicAssignment
    {
        [Key]
        public int AssignmentId { get; set; }

        public int MechanicId { get; set; }
        public Mechanics? Mechanic { get; set; }

        public int JobId { get; set; }
        public AssignedJobs? Job { get; set; }

        public float TimeAssigned { get; set; } 

        public float TimeCompleted { get; set; }

        public string MarkAsCompleted(float completionTime)
        {
            try
            {
                string AssignedMechanic = Mechanic.Name;
                bool? IsCompleted = Job.IsCompleted;
                string Name = Job.Jobs.Name;
                float CompletionTime = Job?.ActualCompTime ?? 0;

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
            string AssignedMechanic = Mechanic.Name;
            bool? IsCompleted = Job.IsCompleted;
            string Name = Job.Jobs.Name;
            float CompletionTime = Job.ActualCompTime ?? 0;
            string Description = Job.Jobs.Description;
            string Priority = Job.Priority;

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