using WebApplication2_hl_;

internal class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        string? url = builder.Configuration["SupabaseSetting:ApiUrl"];
        string? key = builder.Configuration["SupabaseSetting:ApiKey"];
        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true
        };
        Supabase.Client supabase = new Supabase.Client(url, key, options);

        SupaBaseContextDoctors supabaseContextDoctors = new();
        SupaBaseContextPatient supabaseContextPatient = new();
        SupaBaseContextAppointment supabaseContextAppointment = new();

        builder.Services.AddSingleton(supabase);
        builder.Services.AddSingleton(supabaseContextDoctors);
        builder.Services.AddSingleton(supabaseContextPatient);
        builder.Services.AddSingleton(supabaseContextAppointment);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}