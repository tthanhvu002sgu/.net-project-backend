namespace DoAn_API.Models
{
    public class Schedule
    {
        public DateTime dateTime { get; set; }
        public TimeSpan time;
        public bool isBooked;
        public int scheduleId;
        public Doctor doctor { get; set; }
        public bool IsBooked { get; set; } // True if a patient has booked this slot
        public bool IsDoctorUnavailable { get; set; } // True if the doctor is unavailable (e.g., vacation, sick)

        // Method to check if the schedule should be visible
        public bool IsVisible()
        {
            // If the slot is booked or the doctor is unavailable, hide the schedule
            return !IsBooked && !IsDoctorUnavailable;
        }

    }
}
