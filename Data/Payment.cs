﻿namespace DoAn_API.Data
{
    public class Payment
    {

        public int patientId { get; set; }
        public string patientEmail { get; set; }
        public int appointmentId { get; set; }
        public int paymentId { get; set; }
        public decimal price { get; set; }
        public string paymentMethod { get; set; }
        public string paymentStatus { get; set; }
        public Appointment appointment { get; set; }

    }
}
