namespace RegisterAPI.Models
{
    public class UserFiesta : User
    {
        public string? Edad { get; set; }
        public string? UsoDeApp {get; set;}
        public string? CuandoSeraFiesta { get; set; } 
    }
}