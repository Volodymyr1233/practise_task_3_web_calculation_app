using System.Text.Json;

namespace CalculationApplicationWeb.Models
{
    public class CalculationRequest
    {
        public class CalculationResponse
        {
            public int result { get; set; }
        }
       

        public static async Task<int> SendRequest(int firstNumber, int secondNumber, string operation)
        {
            string base_url = "https://localhost:7007/api/Calculation/";
            using (var client = new HttpClient())
            {
                var requestBody = new
                {
                    a = firstNumber,    
                    b = secondNumber
                };
                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                string operationString = operation.ToLower() switch
                {
                    "+" => "add",
                    "-" => "subtract",
                    "*" => "multiply",
                    _ => throw new ArgumentException("Invalid operation")
                };
                var response = await client.PostAsync(base_url + operationString, content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var calculationResponse = JsonSerializer.Deserialize<CalculationResponse>(responseBody);
                return calculationResponse.result;
            }
        }
    }
}
