using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace WebApplication2_hl_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly Supabase.Client _supabaseClient;
        private readonly SupaBaseContextPatient _supabaseContext;

        public PatientController(Supabase.Client supabaseClient, SupaBaseContextPatient supaBaseContext)
        {
            _supabaseClient = supabaseClient;
            _supabaseContext = supaBaseContext;
        }

        // Получаем список всех пациентов
        [HttpGet("GetAllPatients", Name = "GetAllPatients")]
        public async Task<string> GetAllPatients()
        {
            try
            {
                var result = await _supabaseContext.GetPatients(_supabaseClient);
                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        // Метод добавления нового пациента
        [HttpPost("InsertPatient", Name = "InsertPatient")]
        public async Task<ActionResult> InsertPatient([FromBody] PatientData patientData)
        {
            try
            {
                if (string.IsNullOrEmpty(patientData.Full_name) || string.IsNullOrEmpty(patientData.Address))
                {
                    return BadRequest("Имя пациента или его адрес не могут быть пустыми.");
                }
                else
                {
                    Patient newPatient = new Patient
                    {
                        Full_name = patientData.Full_name,
                        birth_date = patientData.Birth_date,
                        Address = patientData.Address,
                        Phone = patientData.Phone,
                        Insurance_number = patientData.Insurance_number,
                    };
                    bool result = await _supabaseContext.InsertPatient(_supabaseClient, newPatient);
                    if (result == true)
                    {
                        return Ok("Пациент успешно добавлен");
                    }
                    else
                    {
                        return BadRequest("Не удалось добавить пациента в БД");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Неизвестная ошибка");
            }
        }

        // Обновление имени пациента
        [HttpPut("UpdatePatientName", Name = "UpdatePatientName")]
        public async Task<ActionResult> UpdatePatientName([FromBody] UpdatePatientName patientData)
        {
            try
            {
                if (patientData.Patient_id <= 0 || string.IsNullOrEmpty(patientData.Full_name))
                {
                    return BadRequest("Все поля должны быть заполнены и корректны.");
                }

                var updatedPatient = new Patient
                {
                    Patient_id = patientData.Patient_id,
                    Full_name = patientData.Full_name,
                };

                bool result = await _supabaseContext.UpdatePatientName(_supabaseClient, updatedPatient);
                if (result)
                {
                    return Ok("Имя пациента успешно обновлено");
                }
                else
                {
                    return BadRequest("Не удалось обновить имя пациента");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return BadRequest("Неизвестная ошибка");
            }
        }

        // Обновление даты рождения
        [HttpPut("UpdatePatientBirth", Name = "UpdatePatientBirth")]
        public async Task<ActionResult> UpdatePatientBirth([FromBody] UpdatePatientBirth patientBirth)
        {
            try
            {
                // Проверяем, что patientBirth не равен null
                if (patientBirth == null)
                {
                    return BadRequest("Данные не переданы");
                }

                // Проверка, что Patient_id положительный
                if (patientBirth.Patient_id <= 0)
                {
                    return BadRequest("Некорректный идентификатор пациента");
                }

                // Проверка, что дата рождения не равна default(DateTime)
                if (patientBirth.Birth_date == default(DateTime))
                {
                    return BadRequest("Дата рождения должна быть указана");
                }

                // Далее, если нужно, можно проверить, что дата не в будущем
                if (patientBirth.Birth_date > DateTime.Now)
                {
                    return BadRequest("Дата рождения не может быть в будущем");
                }

                var updatedPatient = new Patient
                {
                    Patient_id = patientBirth.Patient_id,
                    birth_date = patientBirth.Birth_date,
                };

                bool result = await _supabaseContext.UpdatePatientBirth(_supabaseClient, updatedPatient);
                if (result)
                {
                    return Ok("Дата рождения пациента успешно обновлена");
                }
                else
                {
                    return BadRequest("Не удалось обновить дату рождения пациента");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return BadRequest("Неизвестная ошибка");
            }
        }

        // Обновление адреса пациента
        [HttpPut("UpdatePatientAddress", Name = "UpdatePatientAddress")]
        public async Task<ActionResult> UpdatePatientAddress([FromBody] UpdatePatientAddress patientAddress)
        {
            try
            {
                if (patientAddress.Patient_id <= 0 || string.IsNullOrEmpty(patientAddress.Address))
                {
                    return BadRequest("Все поля должны быть заполнены и корректны.");
                }

                var updatedPatient = new Patient
                {
                    Patient_id = patientAddress.Patient_id,
                    Address = patientAddress.Address,
                };

                bool result = await _supabaseContext.UpdatePatientAddress(_supabaseClient, updatedPatient);
                if (result)
                {
                    return Ok("Алрес пациента успешно обновлен");
                }
                else
                {
                    return BadRequest("Не удалось обновить адрес пациента");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return BadRequest("Неизвестная ошибка");
            }
        }

        // Обновление номера телефона
        [HttpPut("UpdatePatientPhone", Name = "UpdatePatientPhone")]
        public async Task<ActionResult> UpdatePatientPhone([FromBody] UpdatePatientPhone patientPhone)
        {
            try
            {
                if (patientPhone.Patient_id <= 0 || string.IsNullOrEmpty(patientPhone.Phone))
                {
                    return BadRequest("Все поля должны быть заполнены и корректны.");
                }

                var updatedPatient = new Patient
                {
                    Patient_id = patientPhone.Patient_id,
                    Phone = patientPhone.Phone,
                };

                bool result = await _supabaseContext.UpdatePatientPhone(_supabaseClient, updatedPatient);
                if (result)
                {
                    return Ok("Номер телефона пациента успешно обновлен");
                }
                else
                {
                    return BadRequest("Не удалось обновить номер телефона пациента");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return BadRequest("Неизвестная ошибка");
            }
        }

        //Обновление номера страхового полиса пациента
        [HttpPut("UpdatePatientInsuranceNumber", Name = "UpdatePatientInsuranceNumber")]
        public async Task<ActionResult> UpdatePatientInsuranceNumber([FromBody] UpdatePatientInsNum patientInsNum)
        {
            try
            {
                if (patientInsNum.Patient_id <= 0 || string.IsNullOrEmpty(patientInsNum.Insurance_number))
                {
                    return BadRequest("Все поля должны быть заполнены и корректны.");
                }

                var updatedPatient = new Patient
                {
                    Patient_id = patientInsNum.Patient_id,
                    Insurance_number = patientInsNum.Insurance_number,
                };

                bool result = await _supabaseContext.UpdatePatientInsuranceNumber(_supabaseClient, updatedPatient);
                if (result)
                {
                    return Ok("Номер страхового полиса успешно обновлен");
                }
                else
                {
                    return BadRequest("Неизвестная ошибка");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return BadRequest("Неизвестная ошибка");
            }
        }

        // Удаление пациента
        [HttpDelete("DeletePatient", Name = "DeletePatient")]
        public async Task<ActionResult> DeletePatient(int Patient_id)
        {
            try
            {
                bool success = await _supabaseContext.DeletePatient(_supabaseClient, Patient_id);
                if (success)
                {
                    return Ok("Пациент успешно удален");
                }
                else
                {
                    return NotFound($"Пациент с id {Patient_id} не найден");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка сервера: {ex.Message}");
            }
        }
    }

    // Валидация данных

    // Для добавления нового пациента
    public class PatientData
    {
        [JsonProperty("patient_id")]
        public int Patient_id { get; set; }
        [JsonProperty("full_name")]
        public string Full_name { get; set; }
        [JsonProperty("birth_date")]
        public DateTime Birth_date { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("insurance_number")]
        public string Insurance_number { get; set; }   
    }

    // Для обновления номера телефона
    public class UpdatePatientPhone
    {
        [JsonProperty("patient_id")]
        public int Patient_id { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }

    // Для обновления номера страхового полиса пациента
    public class UpdatePatientInsNum
    {
        [JsonProperty("patient_id")]
        public int Patient_id { get; set; }
        [JsonProperty("insurance_number")]
        public string Insurance_number { get; set; }
    }

    // Для обновления названия города
    public class UpdatePatientName
    {
        [JsonProperty("patient_id")]
        public int Patient_id { get; set; }
        [JsonProperty("full_name")]
        public string Full_name { get; set; }
    }

    // Для обновления населения города
    public class UpdatePatientBirth
    {
        [JsonProperty("patient_id")]
        public int Patient_id { get; set; }
        [JsonProperty("birth_date")]
        public DateTime Birth_date { get; set; }
    }

    // Для обновления страны города
    public class UpdatePatientAddress
    {
        [JsonProperty("patient_id")]
        public int Patient_id { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}