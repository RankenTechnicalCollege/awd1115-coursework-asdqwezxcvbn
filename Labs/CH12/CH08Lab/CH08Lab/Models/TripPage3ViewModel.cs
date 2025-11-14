namespace CH08Lab.Models
{
    public class TripPage3ViewModel
    {
        public List<int> SelectedActivityIds { get; set; } = new List<int>();

        public IEnumerable<Activity> Activities { get; set; } = new List<Activity>();
    }
}
