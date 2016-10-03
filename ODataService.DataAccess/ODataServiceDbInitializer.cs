using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using Newtonsoft.Json;
using ODataService.Models;

namespace ODataService.DataAccess
{
    public class ODataServiceDbInitializer : CreateDatabaseIfNotExists<ODataServiceContext>
    {
        protected override void Seed(ODataServiceContext context)
        {

            var json = UTF8Encoding.Default.GetString(Properties.Resources.CountryData);

            var list = JsonConvert.DeserializeObject<List<string>>(json);

            var i = 0;
            var countries = new List<Country>();
            foreach(var name in list)
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
    }
}
