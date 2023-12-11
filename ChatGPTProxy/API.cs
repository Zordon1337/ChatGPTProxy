using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTProxy
{
    internal class API
    {
        public static async Task<string> Ask(string apikey, string question)
        {
            string answer = string.Empty;

            // used for counting how much time it took for ChatGPT to respond
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (HttpClient client = new HttpClient())
            {
                // adding header with api key to auth
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apikey}");

                string url = "https://api.openai.com/v1/chat/completions";


                string jsonData = $@"{{
                    ""model"": ""gpt-3.5-turbo"",
                    ""messages"": [{{""role"": ""user"", ""content"": ""{question}""}}],
                    ""temperature"": 0.7
                }}";

                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                // stopping stop watch since we have response already
                stopwatch.Stop();
                //Console.WriteLine($"{apikey} and {question}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var chatCompletion = JsonConvert.DeserializeObject<ChatCompletion>(result);

                   
                    answer = $"|{chatCompletion.choices[0].message.content}|{chatCompletion.usage.total_tokens}|{chatCompletion.usage.completion_tokens}|{chatCompletion.usage.prompt_tokens}|{stopwatch.ElapsedMilliseconds}";
                    Console.WriteLine($"Got Request, ChatGPT responed in {stopwatch.ElapsedMilliseconds}ms");
                    /*Console.WriteLine($"Message returned: {chatCompletion.choices[0].message.content}");
                    Console.WriteLine($"Total tokens: {chatCompletion.usage.total_tokens} Completion tokens: {chatCompletion.usage.completion_tokens} Prompt tokens: {chatCompletion.usage.prompt_tokens}");
                    Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} milliseconds");*/


                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    answer = "ERROR";
                }
            }
            return answer;
        }
    }
}
public class ChatCompletion
{
    public string id { get; set; }
    public string @object { get; set; }
    public long created { get; set; }
    public string model { get; set; }
    public Choice[] choices { get; set; }
    public Usage usage { get; set; }
    public object system_fingerprint { get; set; }
}

public class Choice
{
    public int index { get; set; }
    public Message message { get; set; }
    public string finish_reason { get; set; }
}

public class Message
{
    public string role { get; set; }
    public string content { get; set; }
}

public class Usage
{
    public int prompt_tokens { get; set; }
    public int completion_tokens { get; set; }
    public int total_tokens { get; set; }
}
