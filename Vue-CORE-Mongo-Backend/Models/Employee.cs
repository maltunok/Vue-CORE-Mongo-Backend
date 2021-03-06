using MongoDB.Bson;

namespace Vue_CORE_Mongo_Backend.Models
{
    public class Employee
    {
        public ObjectId Id { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string Department { get; set; }

        public string DateOfJoining { get; set; }

        public string PhotoFileName { get; set; }
    }
}