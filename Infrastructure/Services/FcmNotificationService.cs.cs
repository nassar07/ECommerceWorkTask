using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Common.Services
{
    public class FcmNotificationService : IFcmNotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverKey;
        private readonly string _senderId;

        public FcmNotificationService(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _serverKey = config["Firebase:ServerKey"];
            _senderId = config["Firebase:SenderId"];
        }

        public async Task SendNotificationAsync(string fcmToken, string title, string body)
        {
            var message = new
            {
                to = fcmToken,
                notification = new
                {
                    title,
                    body
                }
            };

            var json = JsonSerializer.Serialize(message);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
            request.Headers.Authorization = new AuthenticationHeaderValue("key", _serverKey);
            request.Headers.TryAddWithoutValidation("Sender", _senderId);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ FCM Error: {error}");
            }
        }
    }
}
