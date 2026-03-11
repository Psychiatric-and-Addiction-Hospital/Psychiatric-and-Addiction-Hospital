using System.Text.Json;
using System.Text.Json.Serialization;

namespace Psychiatric_and_Addiction_Hospital.Extesion
{
    public static class JsonSerializerExtension
    {
        public static IServiceCollection AddCustomJsonSerializer(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            return services;
        }
    }
}
