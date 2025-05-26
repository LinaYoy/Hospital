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
        public string Patient_id { get; set; }
        [Column("doctor_id")]
        public string Doctor_id { get; set; }
        [Column("appointment_date")]
        public string Appointment_date { get; set; }
        [Column("diagnosis")]
        public string Diagnosis { get; set; }
        [Column("insurance_number")]
        public int Insurance_number { get; set; }

    }

}

/*
 * yt vjue cj[hfybnm
 * */