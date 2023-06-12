using BankLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static BankLibrary.DTOs.RandomApiDTO;

namespace BankLibrary.Services
{
    public class RandomAPIservice : IRandomAPIservice
    {
        private readonly HttpClient _httpClient;

        public RandomAPIservice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RandomApiDTO.User>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("https://randomuser.me/api/?results=4");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var userResult = JsonSerializer.Deserialize<RandomApiDTO.UserResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return userResult.results; // Get all the users
        }
    }
}
