# xDDoS

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET 6.0](https://img.shields.io/badge/.NET-6.0-blue.svg)](https://dotnet.microsoft.com/)

---

## Hakkında

**xDDoS**, modern C# ile geliştirilmiş, çoklu platform destekli (Windows, Linux, Termux) yüksek performanslı bir DDoS/yük testi aracıdır. HTTP, TCP ve UDP flood modülleri içerir. Eğitim ve test amaçlıdır.

---

## 🚨 Yasal Uyarı

> **UYARI:**
> Bu araç yalnızca **eğitim**, **test** ve **kendi sistemlerinizde** yük testi yapmak için tasarlanmıştır.
> Gerçek, izinsiz saldırılar **yasalara aykırıdır** ve tüm sorumluluk kullanıcıya aittir.

---

## Özellikler

- HTTP/HTTPS Flood (asenkron, yüksek hızlı)
- TCP Flood (asenkron, yüksek hızlı)
- UDP Flood (asenkron, yüksek hızlı)
- Kullanıcı dostu CLI arayüzü
- Windows x64 ve Termux (Linux/ARM64) desteği
- Tek dosya (onefile) derleme
- Gömülü PDB (debug) desteği
- Kolay özelleştirilebilir modüller

---

## Gereksinimler

- .NET 6.0 SDK ([İndir](https://dotnet.microsoft.com/en-us/download/dotnet/6.0))
- Windows x64 veya Linux/ARM64 (Termux)

---

## Derleme

**Windows x64 için:**

```sh
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=embedded -o ./bin/win-x64/
```

**Termux (Linux/ARM64) için:**

```sh
dotnet publish -c Release -r linux-arm64 --self-contained true -p:PublishSingleFile=true -p:DebugType=embedded -o ./bin/linux-arm64/
```

> 32 bit Linux için self-contained binary desteklenmez. Sadece framework-dependent olarak derleyebilirsiniz.

---

## Kullanım

**Windows:**

```sh
cd bin/win-x64/
xDDoS.exe
```

**Termux / Linux:**

```sh
cd bin/linux-arm64/
chmod +x xDDoS
./xDDoS
```

---

## Modlar

- **HTTP/HTTPS Flood:**
  - Hedef URL'ye çoklu asenkron GET isteği gönderir.
- **TCP Flood:**
  - Hedef IP ve port'a çoklu TCP bağlantısı açar.
- **UDP Flood:**
  - Hedef IP ve port'a çoklu UDP paketi gönderir.

---

## Gelişmiş Özellikler

- User-Agent ve Proxy desteği (obfuscateli dosya ile)
- Süreye göre saldırı (istek sayısı yerine)
- Anlık ve toplam istatistikler (isteğe bağlı PHP backend ile)

---

## Ekran Görüntüsü

> Terminal ekran görüntüsü veya gif ekleyebilirsiniz.

---

## Katkı ve Lisans

- Katkıda bulunmak için PR gönderebilir veya issue açabilirsiniz.
- [MIT Lisansı](LICENSE) ile lisanslanmıştır.

---

## Sorumluluk Reddi

Bu yazılımın kötüye kullanımı tamamen kullanıcı sorumluluğundadır.
Geliştirici, izinsiz saldırılardan sorumlu tutulamaz. 