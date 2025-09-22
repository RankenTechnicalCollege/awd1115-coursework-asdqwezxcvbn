namespace AWD1115Final.Models
{
    public class Job
    {
        public int JobId { get; set; }

        public required int SkillLevel { get; set; }

        public required string Name { get; set; }

        public string Description { get; set; }

        public string Priority { get; set; }

        public string CustomerName { get; set; }

        public float CompletionTime { get; set; }

        public string AssignedMechanic { get; set; }

        public bool IsCompleted { get; set; }

        public string MarkAsCompleted(float completionTime)
        {
            try
            {
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
