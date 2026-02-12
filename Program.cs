using System;
using System.Collections.Generic;
using System.IO;
using TextCopy;

namespace GUID_GEN
{
    internal class Program
    {
        private static readonly HashSet<string> Generated = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private static readonly HashSet<string> Blacklisted = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private static readonly string GuidStorePath = "generated_guids.txt";
        private static readonly string BlacklistPath = "blacklisted_guids.txt";

        static void Main(string[] args)
        {
            Console.Title = "GUID Generator";
            LoadBlacklistedGuids();
            LoadStoredGuids();
            ShowMenu();

            string currentGuid = null;

            while (true)
            {
                var key = Console.ReadKey(intercept: true);

                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        currentGuid = GenerateUniqueGuid();
                        DisplayGuid(currentGuid);
                        break;

                    case ConsoleKey.Spacebar:
                        if (currentGuid != null)
                        {
                            new Clipboard().SetText(currentGuid);
                            Console.WriteLine("Copied to clipboard!");
                            Console.WriteLine("Press Enter to generate again or Space to copy.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("No GUID yet. Press Enter to generate one first.");
                        }
                        break;

                    case ConsoleKey.Escape:
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Press Enter to generate, Space to copy, Esc to quit.");
                        break;
                }
            }
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("         GUID GENERATOR MENU       ");
            Console.WriteLine("===================================");
            Console.WriteLine("Press Enter : Generate new GUID");
            Console.WriteLine("Press Space : Copy GUID to clipboard");
            Console.WriteLine("Press Esc   : Exit");
            Console.WriteLine("===================================");
            Console.WriteLine($"Blacklisted GUIDs: {Blacklisted.Count}");
            Console.WriteLine($"Generated GUIDs: {Generated.Count}");
            Console.WriteLine("===================================");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void LoadBlacklistedGuids()
        {
            if (File.Exists(BlacklistPath))
            {
                foreach (var line in File.ReadAllLines(BlacklistPath))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        Blacklisted.Add(line.Trim());
                }
            }
            else
            {
                Console.WriteLine($"Warning: Blacklist file '{BlacklistPath}' not found. No GUIDs will be blacklisted.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }

        private static void LoadStoredGuids()
        {
            if (File.Exists(GuidStorePath))
            {
                foreach (var line in File.ReadAllLines(GuidStorePath))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        Generated.Add(line.Trim());
                }
            }
        }

        private static string GenerateUniqueGuid()
        {
            while (true)
            {
                string candidate = Guid.NewGuid().ToString("D").ToLowerInvariant();

                // Check if the GUID is in the blacklist or already generated
                if (!Blacklisted.Contains(candidate) && !Generated.Contains(candidate))
                {
                    Generated.Add(candidate);
                    File.AppendAllText(GuidStorePath, $"[GUID]: {candidate} | [Timestamp]: {DateTime.Now:yyyy-MM-dd HH:mm:ss} {Environment.NewLine}");
                    Blacklisted.Add(candidate);
                    File.AppendAllText(BlacklistPath, $"\"{candidate}\"{Environment.NewLine}");
                    return candidate;
                }

                // If we hit a blacklisted or duplicate GUID (extremely rare), loop continues
            }
        }

        private static void DisplayGuid(string guid)
        {
            const int boxWidth = 70;
            string label = "Generated GUID: ";
            string fullGuid = guid; // 36 characters

            Console.WriteLine();
            Console.WriteLine("╔" + new string('═', boxWidth) + "╗");

            // Start line with label
            Console.Write("║ " + label);

            // Track visible length
            int visibleLength = 1 + label.Length + fullGuid.Length; // 1 for space after ║

            // Print color-coded segments
            string[] segments = guid.Split('-');
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(segments[0] + "-");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(segments[1] + "-");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(segments[2] + "-");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(segments[3] + "-");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(segments[4]);
            Console.ResetColor();

            // Pad remaining space
            int padding = boxWidth - visibleLength;
            Console.Write(new string(' ', padding));
            Console.WriteLine(" ║");

            Console.WriteLine("╚" + new string('═', boxWidth) + "╝");

            Console.WriteLine($"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine("Press Enter to generate again.");
            Console.WriteLine("Press Space to copy.");
            Console.WriteLine("Press Esc to exit.");
            Console.WriteLine();
        }
    }
}