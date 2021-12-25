namespace RoutesList.Build.Models
{
    public class RoutesInformationModel
    {
        public int Id { get; set; }
        public string Controller_name { get; set; }
        public string Action_name { get; set; }
        public string Display_name { get; set; }
        public string Template { get; set; }
        public string Method_name { get; set; }
    }
}
