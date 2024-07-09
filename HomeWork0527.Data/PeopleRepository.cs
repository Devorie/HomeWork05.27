using Faker;
using HomeWork0527.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Faker;
namespace HomeWork0527.Data
{
    public class PeopleRepository
    {
        private readonly string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetPeople()
        {
            using var context = new PeopleDataContext(_connectionString);
            return context.People.ToList();
        }


        public void DeletePeople()
        {
            using var context = new PeopleDataContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"DELETE FROM People");

        }


        public string GenerateCsv(int amount)
        {
            var people = Generate(amount);
            var writer = new StringWriter();
            var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
            csvWriter.WriteRecords(people);

            return writer.ToString();
        }

        public void UploadPeople(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            using var reader = new StreamReader(memoryStream);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            var people = csvReader.GetRecords<Person>().ToList();
            using var context = new PeopleDataContext(_connectionString);
            context.AddRange(people);
            context.SaveChanges();
        }


        static List<Person> Generate(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ => new Person
            {
                FirstName = Name.First(),
                LastName = Name.Last(),
                Age = RandomNumber.Next(20, 80),
                Address = Faker.Address.StreetAddress(),
                Email = Faker.Internet.Email()
            }).ToList(); ;
        }

    }
}
