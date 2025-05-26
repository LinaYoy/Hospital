using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Supabase;
using Supabase.Interfaces;
using Supabase.Postgrest.Attributes;
using WebApplication2_hl_.Controllers;

namespace WebApplication2_hl_
{
    public class SupaBaseContextDoctors
    {

        //Получаем список всех докторов
        public async Task<List<Doctor>> GetDoctors(Supabase.Client _supabaseClient, Func<Task<string>> getAllDoctors)
        {
            var result = await _supabaseClient.From<Doctor>().Get();
            return result.Models;
        }


        //Добавление нового доктора в БД
        public async Task<bool> InsertDoctor(Supabase.Client _supabaseClient, Doctor doctor)
        {
            try
            {
                await _supabaseClient.From<Doctor>().Insert(doctor);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errors: {ex.Message}");
                return false;
            }
        }


        //Обновление имени пользователя
        public async Task<bool> UpdateDoctorName(Supabase.Client _supabaseClient, Doctor updatedDoctor)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Doctor>()
                    .Where(x => x.Doctor_id == updatedDoctor.Doctor_id)
                    .Set(x => x.Full_name, updatedDoctor.Full_name)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        //01.04 - обновление логина пользоавтеля
        public async Task<bool> UpdateDoctorSpecialization(Supabase.Client _supabaseClient, Doctor dSpecialization)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Doctor>()
                    .Where(x => x.Doctor_id == dSpecialization.Doctor_id)
                    .Set(x => x.Specialization, dSpecialization.Specialization)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        //обновление department
        public async Task<bool> UpdateDoctorDepartment(Supabase.Client _supabaseClient, Doctor doctorDepartment)
        {
            try
            {
                var update = await _supabaseClient
                    .From<Doctor>()
                    .Where(x => x.Doctor_id == doctorDepartment.Doctor_id)
                    .Set(x => x.Department, doctorDepartment.Department)
                    .Update();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        //удаляем доктора по id
        public async Task<bool> DeleteDoctor(Supabase.Client _supabaseClient, int doctorId)
        {
            try
            {
                await _supabaseClient.From<Doctor>().Where(x => x.Doctor_id == doctorId).Delete();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении доктора: {ex.Message}");
                return false; // Возвращаем false в случае ошибки
            }
        }

    }
}

