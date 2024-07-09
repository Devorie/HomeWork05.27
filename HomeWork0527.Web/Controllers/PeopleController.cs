using HomeWork0527.Data;
using HomeWork0527.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace HomeWork0527.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private string _connectionString;

        public PeopleController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("getall")]
        public List<Person> GetAll()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetPeople();
        }

        [HttpPost]
        [Route("delete")]
        public void Delete()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeletePeople();
        }

        [HttpGet("generatecsv")]
        public IActionResult GenerateCSV(int amount)
        {
            var repo = new PeopleRepository(_connectionString);
            string csv = repo.GenerateCsv(amount);
            byte[] csvBytes = Encoding.UTF8.GetBytes(csv);
            return File(csvBytes, "text/csv", $"{amount} people.csv");

        }

        [HttpPost("upload")]
        public void Upload(UploadViewModel vm)
        {
            var repo = new PeopleRepository(_connectionString);
            int indexOfComma = vm.Base64Data.IndexOf(',');
            string base64 = vm.Base64Data.Substring(indexOfComma + 1);
            byte[] bytes = Convert.FromBase64String(base64);
            repo.UploadPeople(bytes);
        }
    }
}
