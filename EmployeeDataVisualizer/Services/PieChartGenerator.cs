using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SkiaSharp;
using EmployeeDataVisualizer.Models;

namespace EmployeeDataVisualizer.Services
{
    public class PieChartGenerator
    {
        public static void GeneratePieChart(List<EmployeeWorkTime> employees, string outputPath)
        {
            int width = 800;
            int height = 600;
            int margin = 50;
            int chartSize = 400;

            using var bitmap = new SKBitmap(width, height);
            using var canvas = new SKCanvas(bitmap);
            canvas.Clear(SKColors.White);

            SKPaint paint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
            };

            SKPaint textPaint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.Black,
                TextSize = 14,
            };

            // Define colors
            SKColor[] colors = new SKColor[]
            {
                SKColors.Red, SKColors.Blue, SKColors.Green, SKColors.Orange, SKColors.Purple,
                SKColors.Brown, SKColors.Cyan, SKColors.Magenta, SKColors.Yellow, SKColors.Pink,
                SKColors.Lime, SKColors.Teal, SKColors.Indigo, SKColors.Maroon, SKColors.Navy
            };

            double totalHours = employees.Sum(e => e.TotalHoursWorked);
            float startAngle = 0;
            SKRect rect = new SKRect(margin, margin, margin + chartSize, margin + chartSize);

            for (int i = 0; i < employees.Count; i++)
            {
                var employee = employees[i];
                float sweepAngle = (float)((employee.TotalHoursWorked / totalHours) * 360);

                paint.Color = colors[i % colors.Length];
                canvas.DrawArc(rect, startAngle, sweepAngle, true, paint);

                startAngle += sweepAngle;
            }

            // Draw legend
            int legendX = margin + chartSize + 20;
            int legendY = margin;

            for (int i = 0; i < employees.Count; i++)
            {
                var employee = employees[i];
                SKColor color = colors[i % colors.Length];

                paint.Color = color;
                canvas.DrawRect(new SKRect(legendX, legendY + i * 25, legendX + 15, legendY + i * 25 + 15), paint);

                string text = $"{employee.EmployeeName} ({employee.PercentageOfTotal:F1}%)";
                canvas.DrawText(text, legendX + 20, legendY + i * 25 + 12, textPaint);
            }

            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(outputPath);
            data.SaveTo(stream);
        }
    }
}
