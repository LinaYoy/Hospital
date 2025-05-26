using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Supabase;
using Supabase.Interfaces;
using Supabase.Postgrest.Attributes;
using WebApplication2_hl_.Controllers;

namespace WebApplication2_hl_
{
    public class SupaBaseContextPatient
    {
        // Получаем список всех пациентов
        public async Task<List<Patient>> GetPatients(Supabase.Client _supabaseClient)
        {
            var result = await _supabaseClient.From<Patient>().Get();
            return result.Models;
        }

        // Добавление нового пациента в БД
        public async Task<bool> InsertPatient(Supabase.Client _supabaseClient, Patient patient)
        {
            try
            {
                await _supabaseClient.From<Patient>().Insert(patient);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errors: {ex.Message}");
                return false;
            }
        }

        // Обновление имени пациента, Name
        public async Task<bool> UpdatePatientName(Supabase.Client _supabaseClient, Patient updatedPatient)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Patient>()
                    .Where(x => x.Patient_id == updatedPatient.Patient_id)
                    .Set(x => x.Full_name, updatedPatient.Full_name)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Обновление даты рожждения пациента
        public async Task<bool> UpdatePatientBirth(Supabase.Client _supabaseClient, Patient patientBirth)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Patient>()
                    .Where(x => x.Patient_id == patientBirth.Patient_id)
                    .Set(x => x.birth_date, patientBirth.birth_date)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Обновление адреса пациента
        public async Task<bool> UpdatePatientAddress(Supabase.Client _supabaseClient, Patient patientAddress)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Patient>()
                    .Where(x => x.Patient_id == patientAddress.Patient_id)
                    .Set(x => x.Address, patientAddress.Address)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Обновление номера телефона пациента
        public async Task<bool> UpdatePatientPhone(Supabase.Client _supabaseClient, Patient patientPhone)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Patient>()
                    .Where(x => x.Patient_id == patientPhone.Patient_id)
                    .Set(x => x.Phone, patientPhone.Phone)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Обновление номера страхового полиса пациента
        public async Task<bool> UpdatePatientInsuranceNumber(Supabase.Client _supabaseClient, Patient patientInsNum)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Patient>()
                    .Where(x => x.Patient_id == patientInsNum.Patient_id)
                    .Set(x => x.Insurance_number, patientInsNum.Insurance_number)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Удаление пациента
        public async Task<bool> DeletePatient(Supabase.Client _supabaseClient, int patientId)
        {
            try
            {
                await _supabaseClient.From<Patient>().Where(x => x.Patient_id == patientId).Delete();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении пациента: {ex.Message}");
                return false;
            }
        }
    }
}


