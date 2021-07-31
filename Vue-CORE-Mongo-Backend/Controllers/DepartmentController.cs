using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;
using Vue_CORE_Mongo_Backend.Models;

namespace Vue_CORE_Mongo_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient mongoClient = new MongoClient(_configuration.GetConnectionString("AppCon"));
            var dbList = mongoClient.GetDatabase("testDB").GetCollection<Department>("Department").AsQueryable();
            return new JsonResult(dbList);
        }

        [HttpPost]
        public JsonResult Post(Department dep)
        {
            MongoClient mongoClient = new MongoClient(_configuration.GetConnectionString("AppCon"));
            int lastDepartmentId = mongoClient.GetDatabase("testDB").GetCollection<Department>("Department").AsQueryable().Count();
            dep.DepartmentId = lastDepartmentId + 1;
            mongoClient.GetDatabase("testDB").GetCollection<Department>("Department").InsertOne(dep);
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            MongoClient mongoClient = new MongoClient(_configuration.GetConnectionString("AppCon"));

            var filter = Builders<Department>.Filter.Eq("DepartmentId", dep.DepartmentId);
            var update = Builders<Department>.Update.Set("DepartmentName", dep.DepartmentName);
            mongoClient.GetDatabase("testDB").GetCollection<Department>("Department").UpdateOne(filter, update);
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient mongoClient = new MongoClient(_configuration.GetConnectionString("AppCon"));
            var filter = Builders<Department>.Filter.Eq("DepartmentId", id);
            mongoClient.GetDatabase("testDB").GetCollection<Department>("Department").DeleteOne(filter);
            return new JsonResult("Deleted Successfully");
        }
    }
}
