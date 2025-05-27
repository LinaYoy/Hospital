using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace WebApplication2_hl_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly Supabase.Client _supabaseClient;
        private readonly SupaBaseContextDoctors _supabaseContext;


        public DoctorController(Supabase.Client supabaseClient, SupaBaseContextDoctors supaBaseContext)
        {
            _supabaseClient = supabaseClient;
            _supabaseContext = supaBaseContext;
        }

        //�������� ������ ���� ��������, ������� ���� � ���� ������
        [HttpGet("GetAllDoctors", Name = "GetAllDoctors")]
        public async Task<string> GetAllDoctors()
        {
            try
            {
                var result = await _supabaseContext.GetDoctors(_supabaseClient, GetAllDoctors);
                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //����� ���������� ������ �������
        [HttpPost("InsertDoctor", Name = "InsertDoctor")]
        public async Task<ActionResult> InsertDoctor([FromBody] DoctorData doctorData)
        {
            try
            {
                if (string.IsNullOrEmpty(doctorData.Full_name) || string.IsNullOrEmpty(doctorData.Specialization))
                {
                    return BadRequest("��� ��� ��� ������������� ������.");
                }
                else
                {
                    Doctor newDoctor = new Doctor
                    {
                        Full_name = doctorData.Full_name,
                        Specialization = doctorData.Specialization,
                        Department = doctorData.Department,
                    };
                    bool result = await _supabaseContext.InsertDoctor(_supabaseClient, newDoctor);
                    if (result == true)
                    {
                        return Ok("����������� ������ �������");
                    }
                    else
                    {
                        return BadRequest("�� ������� �������� ������� � ��");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("����������� ������");
            }
        }


        //���������� ����� �������
        [HttpPut("UpdateDoctorName", Name = "UpdateDoctorName")]
        public async Task<ActionResult> UpdateDoctor([FromBody] UpdateDName doctorData)
        {
            try
            {
                if (doctorData.Doctor_id <= 0 || string.IsNullOrEmpty(doctorData.Full_name))
                {
                    return BadRequest("��� ���� ������ ���� ��������� � ���������.");
                }

                var updatedDoctor = new Doctor
                {
                    Doctor_id = doctorData.Doctor_id,
                    Full_name = doctorData.Full_name,
                };

                // ��������� �������
                bool result = await _supabaseContext.UpdateDoctorName(_supabaseClient, updatedDoctor);
                if (result)
                {
                    return Ok("������ ������� ������� ���������");
                }
                else
                {
                    return BadRequest("�� ������� �������� ������ �������");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"������: {ex.Message}");
                return BadRequest("����������� ������");
            }
        }

        // update specialization
        [HttpPut("UpdateDoctorSpecialization", Name = "UpdateDoctorSpecialization")]
        public async Task<ActionResult> UpdateDoctorSpecialization([FromBody] dSpecialization dSpec)
        {
            try
            {
                if (dSpec.Doctor_id <= 0 || string.IsNullOrEmpty(dSpec.Specialization))
                {
                    return BadRequest("��� ���� ������ ���� ��������� � ���������.");
                }

                var updatedDoctorSpecialization = new Doctor
                {
                    Doctor_id = dSpec.Doctor_id,
                    Specialization = dSpec.Specialization,
                };

                // ��������� �������
                bool result = await _supabaseContext.UpdateDoctorSpecialization(_supabaseClient, updatedDoctorSpecialization);
                if (result)
                {
                    return Ok("������ ������� ������� ���������");
                }
                else
                {
                    return BadRequest("�� ������� �������� ������ �������");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"������: {ex.Message}");
                return BadRequest("����������� ������");
            }
        }


        //Update doctorDepartment (02.04)
        [HttpPut("UpdateDoctorDepartment", Name = "UpdateDoctorDepartment")]
        public async Task<ActionResult> UpdateDoctorDepartment([FromBody] doctorDepartment docDep)
        {
            try
            {
                if (docDep.Doctor_id <= 0 || string.IsNullOrEmpty(docDep.Department))
                {
                    return BadRequest("��� ���� ������ ���� ��������� � ���������.");
                }

                var updatedDoctorDepartment = new Doctor
                {
                    Doctor_id = docDep.Doctor_id,
                    Department = docDep.Department,
                };

                // ��������� �������
                bool result = await _supabaseContext.UpdateDoctorDepartment(_supabaseClient, updatedDoctorDepartment);
                if (result)
                {
                    return Ok("������ ������� ������� ���������");
                }
                else
                {
                    return BadRequest("�� ������� �������� ������ �������");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"������: {ex.Message}");
                return BadRequest("����������� ������");
            }
        }


       //�������� ��������

        [HttpDelete("DeleteDoctor", Name = "DeleteDoctor")]

        public async Task<ActionResult> DeleteDoctor(int doctor_id)
        {
            try
            {
                bool success = await _supabaseContext.DeleteDoctor(_supabaseClient, doctor_id);
                if (success)
                {
                    return Ok("������ ������� ������");
                }
                else
                {
                    return NotFound($"������ � id {doctor_id} �� ������");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"������ �������: {ex.Message}");
            }
        }

    }

    //��������� ������!

    //��� ���������� ����� ������������
    public class UpdateDName
    {
        [JsonProperty("doctor_id")]
        public int Doctor_id { get; set; }
        [JsonProperty("full_name")]
        public string Full_name { get; set; }
    }

    //UpdateSpecialization
    public class dSpecialization
    {
        [JsonProperty("doctor_id")]
        public int Doctor_id { get; set; }
        [JsonProperty("specialization")]
        public string Specialization { get; set; }
    }


    //Update doctorDepartment
    public class doctorDepartment
    {
        [JsonProperty("doctor_id")]
        public int Doctor_id { get; set; }
        [JsonProperty("department")]
        public string Department { get; set; }
    }


    //���������� ������ ������� 
    public class DoctorData
    {
        [JsonProperty("doctor_id")]
        public int Doctor_id { get; set; }
        [JsonProperty("full_name")]
        public string Full_name { get; set; }
        [JsonProperty("specialization")]
        public string Specialization { get; set; }
        [JsonProperty("department")]
        public string Department { get; set; }

    }

    public class deleteDoctor
    {
        [JsonProperty("doctor_id")]
        public int Doctor_id { get; set; }
        [JsonProperty("full_name")]
        public string Full_name { get; set; }
        [JsonProperty("specialization")]
        public string Specialization { get; set; }
        [JsonProperty("department")]
        public string Department { get; set; }
    }


}



