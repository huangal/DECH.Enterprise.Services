using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DECH.Enterprise.Services.Customers.Ioc.Models
{
    public static class HealthCheckResponses
    {
        public static Task WriteJsonResponse(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions { Indented = true };

            using var writer = new Utf8JsonWriter(context.Response.BodyWriter, options);

            writer.WriteStartObject();
            writer.WriteString("Status", report.Status.ToString());

            if (report.Entries.Count > 0)
            {
                writer.WriteStartArray("Checks");

                foreach (var (key, value) in report.Entries)
                {
                    writer.WriteStartObject();
                    writer.WriteString("key", key);
                    writer.WriteString("status", value.Status.ToString());
                    writer.WriteString("description", value.Description);
                    writer.WriteStartArray("data");
                    foreach (var (dataKey, dataValue) in value.Data.Where(d => d.Value is object))
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName(dataKey);
                        JsonSerializer.Serialize(writer, dataValue, dataValue.GetType());
                        writer.WriteEndObject();
                    }
                    writer.WriteEndArray();
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
            }

            writer.WriteEndObject();

            return Task.CompletedTask;
        }

        public static Task WriteStatusResponse(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                Status = report.Status.ToString(),
                Checks = report.Entries.Select(x => new
                {
                    Component = x.Key,
                    Status = x.Value.Status.ToString(),
                    Description = x.Value.Description,
                    Data =  x.Value.Data.Where(d => d.Value is object)

                }),
                Duration = report.TotalDuration
            };
            context.Response.WriteAsync(JsonSerializer.Serialize(response));
            return Task.CompletedTask;
        }
    }
}
