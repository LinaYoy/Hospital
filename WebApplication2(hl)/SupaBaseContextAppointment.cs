using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Supabase;
using Supabase.Interfaces;
using Supabase.Postgrest.Attributes;
using WebApplication2_hl_.Controllers;

namespace WebApplication2_hl_
{
    public class SupaBaseContextAppointment
    {

        //Получаем список всех приемов
        public async Task<List<Appointment>> GetAppointments(Supabase.Client _supabaseClient, Func<Task<string>> getAllAppointments)
        {
            var result = await _supabaseClient.From<Appointment>().Get();
            return result.Models;
        }


        //Добавление нового приема в БД
        public async Task<bool> InsertAppointment(Supabase.Client _supabaseClient, Appointment appointment)
        {
            try
            {
                await _supabaseClient.From<Appointment>().Insert(appointment);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errors: {ex.Message}");
                return false;
            }
        }


        //Обновление id пациента
        public async Task<bool> UpdatePatientId(Supabase.Client _supabaseClient, Appointment updatedAppointment)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Appointment>()
                    .Where(x => x.Appointment_id == updatedAppointment.Appointment_id)
                    .Set(x => x.Patient_id, updatedAppointment.Patient_id)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        //Обновление id доктора
        public async Task<bool> UpdateDoctortId(Supabase.Client _supabaseClient, Appointment updatedAppointment)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Appointment>()
                    .Where(x => x.Appointment_id == updatedAppointment.Appointment_id)
                    .Set(x => x.Doctor_id, updatedAppointment.Doctor_id)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Обновление даты приема
        public async Task<bool> UpdateAppointmentDate(Supabase.Client _supabaseClient, Appointment updatedAppointment)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Appointment>()
                    .Where(x => x.Appointment_id == updatedAppointment.Appointment_id)
                    .Set(x => x.Appointment_date, updatedAppointment.Appointment_date)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // обновление диагноза 
        public async Task<bool> UpdateDiagnosis(Supabase.Client _supabaseClient, Appointment updatedAppointment)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Appointment>()
                    .Where(x => x.Appointment_id == updatedAppointment.Appointment_id)
                    .Set(x => x.Diagnosis, updatedAppointment.Diagnosis)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        //обновление описания способа лечения
        public async Task<bool> UpdateTreatment(Supabase.Client _supabaseClient, Appointment updatedAppointment)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Appointment>()
                    .Where(x => x.Appointment_id == updatedAppointment.Appointment_id)
                    .Set(x => x.Treatment, updatedAppointment.Treatment)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        //удаляем приема по id
        public async Task<bool> DeleteAppointment(Supabase.Client _supabaseClient, int appointmentId)
        {
            try
            {
                await _supabaseClient.From<Appointment>().Where(x => x.Appointment_id == appointmentId).Delete();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении приема: {ex.Message}");
                return false; // Возвращаем false в случае ошибки
            }
        }

    }
}

