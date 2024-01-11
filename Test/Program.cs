// See https://aka.ms/new-console-template for more information

using System.Text;
using Newtonsoft.Json;
using Test;
using MemoryStream = System.IO.MemoryStream;

var httpClient = new HttpClient();

var data = new TestObject("Success");
var js = JsonSerializer.Create();
using var ms = new MemoryStream();
await using var streamWriter = new StreamWriter(ms, Encoding.UTF8);
await using var jsonTextWriter = new JsonTextWriter(streamWriter);
js.Serialize(jsonTextWriter,data);
await jsonTextWriter.FlushAsync();
ms.Seek(0, SeekOrigin.Begin);
             
try
{
    var content = new StreamContent(ms);
    content.Headers.Add("Content-Type", "application/json");
    var response = await httpClient.PutAsync("https://localhost:7104/WeatherForecast",content);
              
    response.EnsureSuccessStatusCode();

    string responseBody = await response.Content.ReadAsStringAsync();

    Console.WriteLine("Response: " + responseBody);
}
catch (HttpRequestException e)
{
    Console.WriteLine("\nException Caught!");
    Console.WriteLine("Message: {0} ", e.Message);
}