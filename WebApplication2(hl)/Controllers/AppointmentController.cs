using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using WebApplication2_hl_;
using WebApplication2_hl_.Controllers;

namespace WebApplication2_h1_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly Supabase.Client _supabaseClient;
        private readonly SupaBaseContextAppointment _supabaseContext;


        public AppointmentController(Supabase.Client supabaseClient, SupaBaseContextAppointment supaBaseContext)
        {
            _supabaseClient = supabaseClient;
            _supabaseContext = supaBaseContext;
        }

        //Получаем список всех приемов, которые есть в базе данных
        [HttpGet("GetAllAppointments", Name = "GetAllAppointments")]
        public async Task<string> GetAllAppointments()
        {
            try
            {
                var result = await _supabaseContext.GetAppointments(_supabaseClient, GetAllAppointments);
                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //Метод добавления нового приема
        [HttpPost("InsertAppointment", Name = "InsertAppointment")]
        public async Task<ActionResult> InsertAppointment([FromBody] AppointmentData appointmentData)
        {
            try
            {
                if (string.IsNullOrEmpty(appointmentData.Diagnosis))
                {
                    return BadRequest("Диагноз не может отсутствовать.");
                }
                else
                {
                    Appointment newAppointment = new Appointment
                    {
                        Patient_id = appointmentData.Patient_id,
                        Doctor_id = appointmentData.Doctor_id,
                        Appointment_date = appointmentData.Appointment_date,
                        Diagnosis = appointmentData.Diagnosis,
                        Treatment = appointmentData.Treatment,

                    };
                    bool result = await _supabaseContext.InsertAppointment(_supabaseClient, newAppointment);
                    if (result == true)
                    {
                        return Ok("Добавление нового приема прошло успешно");
                    }
                    else
                    {
                        return BadRequest("Не удалось добавить прием в БД");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Неизвестная ошибка");
            }
        }

        // Обновление id пациента
        [HttpPut("UpdatePatientId", Name = "UpdatePatientId")]
        public async Task<ActionResult> UpdatePatientId([FromBody] UpdatePatientId patientId)
        {
            try
            {
                if (patientId.Appointment_id <= 0 || int.IsNegative(patientId.Patient_id))
                {
                    return BadRequest("Все поля должны быть заполнены и корректны.");
                }

                var updatedAppointment = new Appointment
                {
                    Appointment_id = patientId.Appointment_id,
                    Patient_id = patientId.Patient_id,
                };

                bool result = await _supabaseContext.UpdatePatientId(_supabaseClient, updatedAppointment);
                if (result)
                {
                    return Ok("Номер id пациента успешно обновлен");
                }
                else
                {
                    return BadRequest("Не удалось обновить номер id пациента");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return BadRequest("Неизвестная ошибка");
            }
        }

        // Обновление id доктора
        [HttpPut("UpdateDoctortId", Name = "UpdateDoctortId")]
        public async Task<ActionResult> UpdateDoctortId([FromBody] UpdateDoctorId doctorId)
        {
            try
            {
                if (doctorId.Appointment_id <= 0 || int.IsNegative(doctorId.Doctor_id))
                {
                    return BadRequest("Все поля должны быть заполнены и корректны.");
                }

                var updatedAppointment = new Appointment
                {
                    Appointment_id = doctorId.Appointment_id,
                    Doctor_id = doctorId.Doctor_id,
                };

                bool result = await _supabaseContext.UpdateDoctortId(_supabaseClient, updatedAppointment);
                if (result)
                {
                    return Ok("Id доктора успешно обновлен");
                }
                else
                {
                    return BadRequest("Не удалось обновить Id доктора");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return BadRequest("Неизвестная ошибка");
            }
        }

        // Обновление даты приема
        [HttpPut("UpdateAppointmentDate", Name = "UpdateAppointmentDate")]
        public async Task<ActionResult> UpdateAppointmentDate([FromBody] UpdateAppointmentDate updateData)
        {
            try
            {
                if (updateData.Appointment_id <= 0)
                    return BadRequest("Некорректный id приема.");

                if (string.IsNullOrWhiteSpace(updateData.Appointment_date))
                    return BadRequest("Дата приема обязательна.");

                // Парсинг строки в DateTime
                if (!DateTime.TryParse(updateData.Appointment_date, out DateTime parsedDate))
                    return BadRequest("Некорректный формат даты. Используйте ГГГГ-ММ-ДД.");

                if (parsedDate > DateTime.Now)
                    return BadRequest("Дата не может быть в будущем.");

                var updatedAppointment = new Appointment
                {
                    Appointment_id = updateData.Appointment_id,
                    Appointment_date = parsedDate
                };

                bool result = await _supabaseContext.UpdateAppointmentDate(_supabaseClient, updatedAppointment);
                if (result)
                    return Ok("Дата приема успешно обновлена");
                else
                    return BadRequest("Не удалось обновить дату приема");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return BadRequest("Произошла ошибка при обновлении даты");
            }
        }

        // Обновление диагноза пациента
        [HttpPut("UpdateDiagnosis", Name = "UpdateDiagnosis")]
        public async Task<ActionResult> UpdateDiagnosis([FromBody] UpdateDiagnosis updateDiagnosis)
        {
            try
            {
                if (updateDiagnosis.Appointment_id <= 0 || string.IsNullOrEmpty(updateDiagnosis.Diagnosis))
                {
                    return BadRequest("Все поля должны быть заполнены и корректны.");
                }

                var updatedAppointment = new Appointment
                {
                    Appointment_id = updateDiagnosis.Appointment_id,
                    Diagnosis = updateDiagnosis.Diagnosis,
                };

                bool result = await _supabaseContext.UpdateDiagnosis(_supabaseClient, updatedAppointment);
                if (result)
                {
                    return Ok("Диагноз пациента успешно обновлен");
                }
                else
                {
                    return BadRequest("Не удалось обновить диагноз пациента");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return BadRequest("Неизвестная ошибка");
            }
        }

        // Обновление способа лечения
        [HttpPut("UpdateTreatment", Name = "UpdateTreatment")]
        public async Task<ActionResult> UpdateTreatment([FromBody] UpdateTreatment treatmentUpdate)
        {
            try
            {
                if (treatmentUpdate.Appointment_id <= 0 || string.IsNullOrEmpty(treatmentUpdate.Treatment))
                {
                    return BadRequest("Все поля должны быть заполнены и корректны.");
                }

                var updatedAppointment = new Appointment
                {
                    Appointment_id = treatmentUpdate.Appointment_id,
                    Treatment = treatmentUpdate.Treatment,
                };

                bool result = await _supabaseContext.UpdateTreatment(_supabaseClient, updatedAppointment);
                if (result)
                {
                    return Ok("Способ лечения успешно обновлен");
                }
                else
                {
                    return BadRequest("Не удалось обновить способ лечения");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return BadRequest("Неизвестная ошибка");
            }
        }

        // Удаление приема
        [HttpDelete("DeleteAppointment", Name = "DeleteAppointment")]
        public async Task<ActionResult> DeleteAppointment(int Appointment_id)
        {
            try
            {
                bool success = await _supabaseContext.DeleteAppointment(_supabaseClient, Appointment_id);
                if (success)
                {
                    return Ok("Прием успешно удален");
                }
                else
                {
                    return NotFound($"Прием с id {Appointment_id} не найден");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка сервера: {ex.Message}");
            }
        }

    }

    //Добавление нового приема 
        public class AppointmentData
        {
            [JsonProperty("appointment_id")]
            public int Appointment_id { get; set; }
            [JsonProperty("patient_id")]
            public int Patient_id { get; set; }
            [JsonProperty("doctor_id")]
            public int Doctor_id { get; set; }
            [JsonProperty("appointment_date")]
            public DateTime Appointment_date { get; set; }
            [JsonProperty("diagnosis")]
            public string Diagnosis { get; set; }
            [JsonProperty("treatment")]
            public string Treatment { get; set; }

        }

    // Для обновления id patient
    public class UpdatePatientId
    {
        [JsonProperty("appointment_id")]
        public int Appointment_id { get; set; }
        [JsonProperty("patient_id")]
        public int Patient_id { get; set; }
    }

    // Для обновления id doctor
    public class UpdateDoctorId
    {
        [JsonProperty("appointment_id")]
        public int Appointment_id { get; set; }
        [JsonProperty("doctor_id")]
        public int Doctor_id { get; set; }
    }

    // Для обновления даты приема
    public class UpdateAppointmentDate
    {
        [JsonProperty("appointment_id")]
        public int Appointment_id { get; set; }

        [JsonProperty("appointment_date")]
        public string Appointment_date { get; set; } // string - для удобства парсинга 
    }

    // Для обновления диагноза
    public class UpdateDiagnosis
    {
        [JsonProperty("appointment_id")]
        public int Appointment_id { get; set; }
        [JsonProperty("diagnosis")]
        public string Diagnosis { get; set; }
    }

    // Для обновления описания способа лечения
    public class UpdateTreatment
    {
        [JsonProperty("appointment_id")]
        public int Appointment_id { get; set; }
        [JsonProperty("treatment")]
        public string Treatment { get; set; }
    }

}
