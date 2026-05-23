using System;
using System.IO;

namespace MewTour.Utility;

public class Logger
{
    private static bool Logging = true;
    private static string LogFilePath => $"Logs/mewtour_logs/{DateTime.Now:yyyy-MM-dd}.log";
    
    public static void Log(string msg)
    {
        if (!Logging) return;
        
        try
        {
            string fullPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                LogFilePath
            );

            string? dir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.AppendAllText(fullPath, $"[{DateTime.Now:yyyy.MM.dd - HH:mm:ss}] || {msg} \n");
        }
        catch
        {
            // ignored
        }
    }

    public static void ClearLog()
    {
        try
        {
            string fullPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                LogFilePath
            );
            
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
        catch
        {
            // ignored
        }
    }
}