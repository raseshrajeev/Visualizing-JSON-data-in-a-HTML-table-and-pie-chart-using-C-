using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EmployeeDataVisualizer.Models;

namespace EmployeeDataVisualizer.Services
{
    public class DataService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

        public DataService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<TimeEntry>> GetTimeEntriesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TimeEntry>>(ApiUrl);
                return response ?? new List<TimeEntry>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
                return new List<TimeEntry>();
            }
        }

        public List<EmployeeWorkTime> ProcessEmployeeWorkTime(List<TimeEntry> timeEntries)
        {
            var employeeWorkTimes = timeEntries
                .Where(entry => !string.IsNullOrWhiteSpace(entry.EmployeeName))
                .GroupBy(entry => entry.EmployeeName)
                .Select(group => new EmployeeWorkTime
                {
                    EmployeeName = group.Key,
                    TotalHoursWorked = group.Sum(entry => (entry.EndTimeUtc - entry.StarTimeUtc).TotalHours)
                })
                .OrderByDescending(e => e.TotalHoursWorked)
                .ToList();

            var totalHours = employeeWorkTimes.Sum(e => e.TotalHoursWorked);
            foreach (var employee in employeeWorkTimes)
            {
                employee.PercentageOfTotal = (employee.TotalHoursWorked / totalHours) * 100;
            }

            return employeeWorkTimes;
        }
    }
}
