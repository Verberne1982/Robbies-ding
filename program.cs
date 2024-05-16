using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace YourNamespace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.Configure(app =>
                    {
                        app.UseRouting();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapPost("/api/gegevens", async context =>
                            {
                                try
                                {
                                    var requestBody = await new System.IO.StreamReader(context.Request.Body).ReadToEndAsync();
                                    var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);

                                    if (data.ContainsKey("id"))
                                    {
                                        var id = data["id"];
                                        var result = GetGegevensByID(id);
                                        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                                    }
                                    else if (data.ContainsKey("naam"))
                                    {
                                        var naam = data["naam"];
                                        var result = GetGegevensByNaam(naam);
                                        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                                    }
                                    else if (data.ContainsKey("locatie"))
                                    {
                                        var locatie = data["locatie"];
                                        var result = GetGegevensByLocatie(locatie);
                                        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                                    }
                                    else if (data.ContainsKey("beschrijving"))
                                    {
                                        var beschrijving = data["beschrijving"];
                                        var result = GetGegevensByBeschrijving(beschrijving);
                                        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                                    }
                                    else
                                    {
                                        context.Response.StatusCode = 400;
                                        await context.Response.WriteAsync("Ongeldig verzoek.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    context.Response.StatusCode = 500;
                                    await context.Response.WriteAsync($"Er is een interne serverfout opgetreden: {ex.Message}");
                                }
                            });

                            endpoints.MapGet("/", async context =>
                            {
                                await context.Response.WriteAsync("Welkom op de hoofdpagina!");
                            });
                        });
                    });
                });

        private static List<Dictionary<string, string>> GetGegevensByID(string id)
        {
            var connectionString = "server=robbiesding;user=root;password=roland;database=robbiesDing";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM robbiesDing WHERE ID = {id}";
            using var reader = command.ExecuteReader();
            var result = new List<Dictionary<string, string>>();
            while (reader.Read())
            {
                var dict = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dict[reader.GetName(i)] = reader[i].ToString();
                }
                result.Add(dict);
            }
            return result;
        }

        private static List<Dictionary<string, string>> GetGegevensByNaam(string naam)
        {
            var connectionString = "server=robbiesding;user=root;password=roland;database=robbiesDing";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM robbiesDing WHERE naam = '{naam}'";
            using var reader = command.ExecuteReader();
            var result = new List<Dictionary<string, string>>();
            while (reader.Read())
            {
                var dict = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dict[reader.GetName(i)] = reader[i].ToString();
                }
                result.Add(dict);
            }
            return result;
        }

        private static List<Dictionary<string, string>> GetGegevensByLocatie(string locatie)
        {
            var connectionString = "server=robbiesding;user=root;password=roland;database=robbiesDing";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM robbiesDing WHERE locatie = '{locatie}'";
            using var reader = command.ExecuteReader();
            var result = new List<Dictionary<string, string>>();
            while (reader.Read())
            {
                var dict = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dict[reader.GetName(i)] = reader[i].ToString();
                }
                result.Add(dict);
            }
            return result;
        }

        private static List<Dictionary<string, string>> GetGegevensByBeschrijving(string beschrijving)
        {
            var connectionString = "server=robbiesding;user=root;password=roland;database=robbiesDing";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM robbiesDing WHERE beschrijving = '{beschrijving}'";
            using var reader = command.ExecuteReader();
            var result = new List<Dictionary<string, string>>();
            while (reader.Read())
            {
                var dict = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dict[reader.GetName(i)] = reader[i].ToString();
                }
                result.Add(dict);
            }
            return result;
        }
    }
}
