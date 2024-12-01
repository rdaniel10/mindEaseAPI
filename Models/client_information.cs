using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mindEaseAPI.Models
{
    public class client_information
    {
        [Key]
        [Required]
        [MaxLength(250)]
        public string clientId { get; set; }

        public string clientName { get; set; }

        public string clientEmail { get; set; }

        public string clientNumber { get; set; }

        public int clientAge { get; set; }

        public string clientGender { get; set; }

        public string clientUsername { get; set; }

        public string clientPassword { get; set; }

        [DefaultValue(false)]
        public bool reservedAppointment { get; set; }
    }
}
