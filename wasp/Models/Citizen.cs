using wasp.Models;

namespace wasp.Models
{
    public class Citizen : User
    {
        public int PhoneNum { get; set; }
        public bool IsBlocked { get; set; }

        public Citizen() : base()
        {
            PhoneNum = 0;
            IsBlocked = false;
        }
    }
}