using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace WebApplication2_hl_
{
    [Table("doctors")]
    public class Doctor : BaseModel
    {
        [PrimaryKey("doctor_id")]
        public int Doctor_id { get; set; }
        [Column("full_name")]
        public string Full_name { get; set; }
        [Column("specialization")]
        public string Specialization { get; set; }
        [Column("department")]
        public string Department { get; set; }
    
    }

}

/*
 * yt vjue cj[hfybnm
 * */