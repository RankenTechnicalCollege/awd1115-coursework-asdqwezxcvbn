namespace HOT3.Areas.Admin.ViewModel
{
    public class ManageRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
