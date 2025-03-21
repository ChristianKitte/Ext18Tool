using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace GeneratorStarter
{
    class Program
    {
        // Konfigurationsklasse für die JSON-Deserialisierung
        public class Config
        {
            public string Generator { get; set; } = "generator.exe";
            public string Arbeitsordner { get; set; } = "Generator";
            public string Ausgabename { get; set; } = "ausgabe.txt";
        }

        static void Main(string[] args)
        {
            Console.WriteLine("GeneratorStarter wird ausgeführt...");

            try
            {
                // Konfigurationsdatei laden oder erstellen
                Config config = LadeKonfiguration();

                // Prüfen, ob die Zieldatei bereits im aktuellen Verzeichnis existiert
                string aktuellesVerzeichnis = Directory.GetCurrentDirectory();
                string zielDateiPfad = Path.Combine(aktuellesVerzeichnis, config.Ausgabename);

                if (File.Exists(zielDateiPfad))
                {
                    Console.WriteLine("Es befindet sich bereits eine Datei in dem Zielverzeichnis!");
                    Console.WriteLine($"Datei: {zielDateiPfad}");
                    return; // Programm beenden
                }

                // Vollständige Pfade ermitteln
                string generatorPfad = Path.Combine(config.Arbeitsordner, config.Generator);
                string generatorDateiPfad = Path.Combine(config.Arbeitsordner, config.Ausgabename);

                // Überprüfen, ob noch eine alte Datei in dem Generatorpfad vorhanden ist
                if (File.Exists(generatorDateiPfad))
                {
                    Console.WriteLine("Es befindet sich bereits eine Datei in dem Quellverzeichnis!");
                    Console.WriteLine($"Datei: {generatorDateiPfad}");
                    return; // Programm beenden
                }

                // Überprüfen, ob der Generator existiert
                if (!File.Exists(generatorPfad))
                {
                    Console.WriteLine($"Fehler: Generator '{generatorPfad}' wurde nicht gefunden!");
                    return;
                }

                // Generator-Anwendung starten
                Console.WriteLine($"Generator wird gestartet: {generatorPfad}");
                Process generatorProzess = new Process();
                generatorProzess.StartInfo.FileName = generatorPfad;
                generatorProzess.StartInfo.WorkingDirectory = config.Arbeitsordner;
                generatorProzess.StartInfo.UseShellExecute = true;

                generatorProzess.Start();
                Console.WriteLine("Warte auf Beendigung des Generators...");
                generatorProzess.WaitForExit();

                // Kurze Pause, um sicherzustellen, dass die Datei vollständig geschrieben wurde
                Thread.Sleep(500);

                // Überprüfen, ob die Ausgabedatei erzeugt wurde
                if (!File.Exists(generatorDateiPfad))
                {
                    Console.WriteLine($"Fehler: Ausgabedatei '{generatorDateiPfad}' wurde nicht gefunden!");
                    return;
                }

                // Datei in das aktuelle Verzeichnis verschieben
                Console.WriteLine($"Verschiebe Datei von {generatorDateiPfad} nach {zielDateiPfad}");
                File.Move(generatorDateiPfad, zielDateiPfad,
                    false); // 'false' verhindert das Überschreiben (redundant zur vorherigen Prüfung)

                Console.WriteLine("Vorgang erfolgreich abgeschlossen.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler aufgetreten: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Details: {ex.InnerException.Message}");
                }
            }
        }

        static Config LadeKonfiguration()
        {
            string configPath = "config.json";
            Config config;

            // Prüfen, ob Konfigurationsdatei existiert
            if (File.Exists(configPath))
            {
                Console.WriteLine("Konfigurationsdatei wird geladen...");
                string jsonString = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<Config>(jsonString) ?? new Config();
            }
            else
            {
                // Standardkonfiguration erstellen
                Console.WriteLine("Keine Konfigurationsdatei gefunden. Standardkonfiguration wird erstellt...");
                config = new Config();
                string jsonString =
                    JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configPath, jsonString);
                Console.WriteLine($"Standardkonfiguration wurde in '{configPath}' gespeichert.");
                Console.WriteLine("Bitte passe die Konfiguration an und starte das Programm erneut.");
            }

            Console.WriteLine($"Generator: {config.Generator}");
            Console.WriteLine($"Arbeitsordner: {config.Arbeitsordner}");
            Console.WriteLine($"Ausgabename: {config.Ausgabename}");

            return config;
        }
    }
}