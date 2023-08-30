using System.ComponentModel.DataAnnotations;

namespace eApp.Core.Domain.User;

public class Users
{
    public Users (string username, string password, string name, string apartment, int zipcode)
    {
        Username = username;
        Password = password;
        Name = name;
        Apartment = apartment;
        Zipcode=zipcode;
    }

    [Key]
    [Required, StringLength(1)]
    public string Username{get; set;}

    [Required, StringLength(1)]
    public string Password{get; set;}
    [Required, StringLength(1)]
    public string Name{get; set;}
    [Required, StringLength(1)]
    public string Apartment {get; set;}
    [Required]
    public int Zipcode{get; set;}

    //public PrivilegeMode Privilege {get; set;}
    //public AuthenticationStatus Status {get; set;}
        public enum PrivilegeMode
    {
        Admin,
        Employee,
        Resident
    }
    public enum AuthenticationStatus
    {
        Online,
        Away,
        Offline
    }
}