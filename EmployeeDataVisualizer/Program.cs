using System;
using System.IO;
using System.Threading.Tasks;
using EmployeeDataVisualizer.Services;

namespace EmployeeDataVisualizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Employee Data Visualizer");
            Console.WriteLine("========================");
            
            try
            {
                // Initialize services
                var dataService = new DataService();
                
                // Fetch data from API
                Console.WriteLine("Fetching data from API...");
                var timeEntries = await dataService.GetTimeEntriesAsync();
                
                if (timeEntries.Count == 0)
                {
                    Console.WriteLine("No data retrieved from API. Please check the connection and try again.");
                    return;
                }
                
                Console.WriteLine($"Retrieved {timeEntries.Count} time entries.");
                
                // Process employee work time
                var employeeWorkTimes = dataService.ProcessEmployeeWorkTime(timeEntries);
                
                // Part A: Generate HTML table
                Console.WriteLine("Generating HTML table...");
                string htmlContent = HtmlTableGenerator.GenerateHtmlTable(employeeWorkTimes);
                File.WriteAllText("employee_report.html", htmlContent);
                Console.WriteLine("HTML table saved as: employee_report.html");
                
                // Part B: Generate pie chart
                Console.WriteLine("Generating pie chart...");
                PieChartGenerator.GeneratePieChart(employeeWorkTimes, "employee_pie_chart.png");
                Console.WriteLine("Pie chart saved as: employee_pie_chart.png");
                
                // Display summary
                Console.WriteLine("\nSummary:");
                Console.WriteLine("--------");
                Console.WriteLine($"Total employees: {employeeWorkTimes.Count}");
                Console.WriteLine($"Total hours worked: {employeeWorkTimes.Sum(e => e.TotalHoursWorked):F2} hours");
                Console.WriteLine($"Employees with less than 100 hours: {employeeWorkTimes.Count(e => e.TotalHoursWorked < 100)}");
                
                Console.WriteLine("\nVisualization complete! Check the generated files:");
                Console.WriteLine("- employee_report.html (HTML table)");
                Console.WriteLine("- employee_pie_chart.png (Pie chart)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Stack trace:");
                Console.WriteLine(ex.StackTrace);
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
