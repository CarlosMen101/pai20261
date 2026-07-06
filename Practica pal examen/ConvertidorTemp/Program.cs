using System;

double ToFahrenheit(double c) => c * 9.0 / 5.0 + 32.0;

Console.WriteLine("Convertidor Celsius -> Fahrenheit");
Console.Write("Introduce temperatura en °C (o pulsa Enter para salir): ");
string? line;
while ((line = Console.ReadLine()) != null && line.Trim() != "")
{
    if (double.TryParse(line.Replace(',', '.'), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double c))
    {
        double f = ToFahrenheit(c);
        Console.WriteLine($"{c} °C = {f:F2} °F");
    }
    else
    {
        Console.WriteLine("Entrada no válida. Introduce un número (ej. 23.5).");
    }
    Console.Write("Introduce temperatura en °C (o pulsa Enter para salir): ");
}
