using System;

namespace EmployeeDataVisualizer.Models
{
    public class TimeEntry
    {
        public string Id { get; set; }
        // Removed EmployeeId as it is not in JSON
        public string EmployeeName { get; set; }
        public DateTime StarTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public string EntryNotes { get; set; }
        public DateTime? DeletedOn { get; set; }
        // Removed ActiveFlag as it is not in JSON
    }
}
