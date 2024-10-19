namespace DoAn_API.Data
{
    public class Schedule
    {
        public DateTime dateTime { get; set; }
        public int hour;
        public int minute;
        public bool isBooked;
        public int scheduleId;
        public Doctor doctor { get; set; }
        public Appointment appointment { get; set; }  // Optional, null if the slot is unbooked

    }
}
