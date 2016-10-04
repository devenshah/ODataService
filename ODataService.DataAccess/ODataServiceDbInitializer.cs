using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using Newtonsoft.Json;
using ODataService.Models;

namespace ODataService.DataAccess
{
    public class ODataServiceDbInitializer : DropCreateDatabaseAlways<ODataServiceContext>
    {

        protected override void Seed(ODataServiceContext context)
        {
            SeedCountryData(context);
            SeedPersonData(context);
            
        }

        void SeedCountryData(ODataServiceContext context)
        {
            var json = UTF8Encoding.Default.GetString(Properties.Resources.CountryData);

            var list = JsonConvert.DeserializeObject<List<string>>(json);

            var i = 0;
            var countries = new List<Country>();
            foreach (var name in list)
            {
                i++;
                var country = new Country()
                {
                    Id = i,
                    Name = name
                };
                countries.Add(country);
            }

            context.Countries.AddRange(countries);

            base.Seed(context);
        }

        void SeedPersonData(ODataServiceContext context)
        {
            var personDeven = new Person
            {
                Id = 1,
                FirstName = "Deven",
                LastName = "Shah",
                DateOfBirth = DateTime.Parse("1974-04-20"),
                Gender = Gender.Male
            };
            var personSuman = new Person
            {
                Id = 1,
                FirstName = "Suman",
                LastName = "Shah",
                DateOfBirth = DateTime.Parse("1973-11-16"),
                Gender = Gender.Female
            };
            var personDiya = new Person
            {
                Id = 1,
                FirstName = "Diya",
                LastName = "Shah",
                DateOfBirth = DateTime.Parse("2004-11-23"),
                Gender = Gender.Female
            };
            var personMinal = new Person
            {
                Id = 1,
                FirstName = "Minal",
                LastName = "Chavan",
                DateOfBirth = DateTime.Parse("1971-08-23"),
                Gender = Gender.Female
            };
            var personJay = new Person
            {
                Id = 1,
                FirstName = "Jay",
                LastName = "Chavan",
                DateOfBirth = DateTime.Parse("1993-05-22"),
                Gender = Gender.Male
            };

            personDeven.Family = new List<Person> { personDiya, personSuman };

            context.People.Add(personDeven);
            context.People.Add(personSuman);
            context.People.Add(personDiya);
            context.People.Add(personMinal);
            context.People.Add(personJay);
        }
    }
}
