using System;
using System.Collections.Generic;

namespace ODataService.Models
{
    public class Person : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public ICollection<Person> Family { get; set; }
    }

    public enum Gender
    {
        Female,
        Male,
        Other
    }
}
