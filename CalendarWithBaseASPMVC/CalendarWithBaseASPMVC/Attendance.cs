namespace CalendarWithBaseASPMVC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attendance")]
    public partial class Attendance
    {
        public Guid AppointmentID { get; set; }

        public Guid PersonID { get; set; }

        public bool Accepted { get; set; }

        public Guid AttendanceID { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] timestamp { get; set; }

        public virtual Appointment Appointment { get; set; }

        public virtual Person Person { get; set; }
    }
}
