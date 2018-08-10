namespace Company.Models
{
    public class UserInfo : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
        public int DoB_Day { get; set; }
        public int DoB_Month { get; set; }
        public int DoB_Year { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Notes { get; set; }
    }
}
