namespace mindEaseAPI.Parameters
{
    public class get_all_details
    {
        public string doctorName { get;set; }

        public DateTime appointmentDate { get;set; }

        public string appointmentSession { get; set; }

        public string clientName { get; set; }

        public string clientGender { get; set; }

        public bool activeAppointment { get; set; }

        public string bookingId { get; set; }
    }
}
