using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mindEaseAPI.Models
{
    public class booking_information
    {
        [Key]
        [Required]
        [MaxLength(250)]
        public string bookingId { get; set; }

        public string clientId { get; set; }

        public string doctorName { get; set; }

        public DateTime appointmentDate { get; set; }

        public string appointmentSession { get; set; }

        [DefaultValue(false)]
        public bool activeAppointment { get; set; }
    }
}
