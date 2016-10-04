using System;

namespace ODataService.Models
{
    public class Person : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Female,
        Male,
        Other
    }
}
