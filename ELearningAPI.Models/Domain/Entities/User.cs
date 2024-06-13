using ELearningAPI.Models.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ELearningAPI.Models.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Type { get; set; }

    }
}
