using wasp.Models;

namespace wasp.Interfaces
{
    public class Citizen : User
    {
        public int PhoneNum { get; set; }
        public bool isBlocked { get; set; }
    }
}