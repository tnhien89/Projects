using FastDeploy.Utilities.Attributes;

namespace FastDeploy.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }   
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        [IgnoreParam]
        public string? UserStatus { get; set; }
        [IgnoreParam]
        public long CreatedAt { get; set; }
        [IgnoreParam]
        public long CreatedBy { get; set; }
        public long LastUpdateAt { get; set;}
        public long LastUpdatedBy { get; set;}

        public UserModel()
        { 
        }

        public UserModel (long id, string userId, string username, string password, string email, string firstName, string lastName, string phoneNumber, string userStatus, long createdAt, long createdBy, long lastUpdateAt, long lastUpdatedBy)
        {
            Id = id;
            UserId = userId;
            Username = username;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            UserStatus = userStatus;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            LastUpdateAt = lastUpdateAt;
            LastUpdatedBy = lastUpdatedBy;
        }
    }

    public class UserLoginModel
    { 
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}