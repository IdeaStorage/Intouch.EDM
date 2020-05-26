using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Intouch.Edm.Services
{
    public abstract class BaseHttpService
    {
        protected async Task<T> SendRequestAsync<T>(
            Uri url,
            HttpMethod httpMethod = null,
            IDictionary<string, string> headers = null,
            object requestData = null,
            bool isAddHeader = true)
        {
            var result = default(T);

            // Default to GET
            var method = httpMethod ?? HttpMethod.Get;

            // Serialize request data
            var data = requestData == null
                ? null
                : JsonConvert.SerializeObject(requestData);

            using (var request = new HttpRequestMessage(method, url))
            {
                // Add request data to request
                if (data != null)
                {
                    request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                }

                // Add headers to request
                // Refresh Token da aynı parametreleri header a eklememesi gerekiyor.
                if (headers != null && isAddHeader)
                {
                    foreach (var h in headers)
                    {
                        request.Headers.Add(h.Key, h.Value);
                    }
                }

                // Get response
                try
                {
                    using (var handler = new HttpClientHandler())
                    {
                        using (var client = new HttpClient(handler))
                        {
                            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                            {
                                var content = response.Content == null
                                    ? null
                                    : await response.Content.ReadAsStringAsync();

                                if (response.IsSuccessStatusCode ||
                                     response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                                {
                                    result = JsonConvert.DeserializeObject<T>(content);
                                }
                            }

                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected async Task<T> GetEntityFromJsonAsync<T>(object requestData = null)
        {
            var result = default(T);
            string name = typeof(T).Name.ToLower().Replace("[", "").Replace("]", "").ToString();
            var assembly = typeof(BaseHttpService).GetTypeInfo().Assembly;
            var json = "";

            var stream = assembly.GetManifestResourceStream($"App1.Services.Data.{name}.json");

            using (var reader = new StreamReader(stream))
            {
                json = await reader.ReadToEndAsync();
            }

            result = JsonConvert.DeserializeObject<T>(json);
            await Task.Delay(1000);
            return result;
        }

        protected async Task<T> SaveEntityToJsonAsync<T>(object requestData = null)
        {
            var result = default(T);
            string name = typeof(T).Name.ToLower().Replace("[", "").Replace("]", "").ToString();
            var assembly = typeof(BaseHttpService).GetTypeInfo().Assembly;
            var json = "";

            var stream = assembly.GetManifestResourceStream($"App1.Services.Data.{name}.json");

            using (StreamWriter file = new StreamWriter(stream))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, requestData);
            }

            using (var reader = new StreamReader(stream))
            {
                json = await reader.ReadToEndAsync();
            }

            result = JsonConvert.DeserializeObject<T>(json);
            await Task.Delay(1000);
            return result;
        }
    }
}