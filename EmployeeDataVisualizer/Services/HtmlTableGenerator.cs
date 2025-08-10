using System.Collections.Generic;
using System.Text;
using EmployeeDataVisualizer.Models;

namespace EmployeeDataVisualizer.Services
{
    public class HtmlTableGenerator
    {
        public static string GenerateHtmlTable(List<EmployeeWorkTime> employees)
        {
            var html = new StringBuilder();
            
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html lang=\"en\">");
            html.AppendLine("<head>");
            html.AppendLine("    <meta charset=\"UTF-8\">");
            html.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            html.AppendLine("    <title>Employee Work Time Report</title>");
            html.AppendLine("    <style>");
            html.AppendLine("        body { font-family: Arial, sans-serif; margin: 20px; }");
            html.AppendLine("        h1 { color: #333; }");
            html.AppendLine("        table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
            html.AppendLine("        th, td { padding: 12px; text-align: left; border-bottom: 1px solid #ddd; }");
            html.AppendLine("        th { background-color: #4CAF50; color: white; }");
            html.AppendLine("        tr:nth-child(even) { background-color: #f2f2f2; }");
            html.AppendLine("    </style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("    <h1>Employee Work Time Report</h1>");
            html.AppendLine("    <p>Employees ordered by total time worked (descending)</p>");
            html.AppendLine("    <table>");
            html.AppendLine("        <thead>");
            html.AppendLine("            <tr>");
            html.AppendLine("                <th>Name</th>");
            html.AppendLine("                <th>Total Time Worked (hours)</th>");
            html.AppendLine("            </tr>");
            html.AppendLine("        </thead>");
            html.AppendLine("        <tbody>");

            foreach (var employee in employees)
            {
                html.AppendLine("            <tr>");
                html.AppendLine($"                <td>{employee.EmployeeName}</td>");
                html.AppendLine($"                <td>{employee.TotalHoursWorked:F2}</td>");
                html.AppendLine("            </tr>");
            }

            html.AppendLine("        </tbody>");
            html.AppendLine("    </table>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            return html.ToString();
        }
    }
}
