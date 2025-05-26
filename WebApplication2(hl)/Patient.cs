using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace WebApplication2_hl_
{
    [Table("patients")]
    public class Patient : BaseModel
    {
        [PrimaryKey("patient_id")]
        public int Patient_id { get; set; }
        [Column("full_name")]
        public string Full_name { get; set; }
        [Column("birth_date")]
        public DateTime birth_date { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("insurance_number")]
        public string Insurance_number { get; set; }
    }
}
