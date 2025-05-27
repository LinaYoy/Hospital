using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace WebApplication2_hl_
{
    [Table("appointments")]
    public class Appointment : BaseModel
    {
        [PrimaryKey("appointment_id")]
        public int Appointment_id { get; set; }
        [Column("patient_id")]
        public int Patient_id { get; set; }
        [Column("doctor_id")]
        public int Doctor_id { get; set; }
        [Column("appointment_date")]
        public DateTime Appointment_date { get; set; }
        [Column("diagnosis")]
        public string Diagnosis { get; set; }
        [Column("treatment")]
        public string Treatment { get; set; }

    }

}

/*
 * yt vjue cj[hfybnm
 * */