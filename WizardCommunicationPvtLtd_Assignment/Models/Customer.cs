using System.ComponentModel.DataAnnotations;

namespace WizardCommunicationPvtLtd_Assignment.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

}