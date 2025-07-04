# xDDoS

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET 6.0](https://img.shields.io/badge/.NET-6.0-blue.svg)](https://dotnet.microsoft.com/)

---

## Hakkında

**xDDoS**, modern C# ile geliştirilmiş, çoklu platform destekli (Windows, Linux, Termux) yüksek performanslı bir DDoS/yük testi aracıdır, Eğitim ve test amaçlıdır.

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
---

## Gereksinimler

- Linux x64/arm64 desteklemektedir.
- Windows x64/x86/arm64 desteklemektedir.

---

## Kullanım

**Windows:**

```sh
xDDoS.exe
```

**Termux / Linux:**

```sh
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
- Süreye göre saldırı

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
