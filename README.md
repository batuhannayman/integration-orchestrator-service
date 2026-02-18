# 🚀 Integration Orchestrator

Modüler, genişletilebilir ve üretim ortamı odaklı tasarlanmış bir **.NET Worker tabanlı entegrasyon orkestrasyon sistemi**.

Bu proje; sistemler arası veri senkronizasyonunu, zamanlanmış ve dayanıklı (resilient) şekilde çalıştırmak için geliştirilmiştir.

---

## 🎯 Amaç

Farklı sistemler arasında (CRM, ERP, stok servisleri vb.) çalışan entegrasyon işlemlerini:

- Zamanlanmış olarak çalıştırmak
- Hatalara karşı retry mekanizmasıyla korumak
- Structured logging ile izlenebilir kılmak
- Yeni entegrasyon tiplerini kodu bozmadan ekleyebilmek

---

## 🏗 Mimari Yaklaşım

Proje **Clean Architecture** prensiplerine göre katmanlı tasarlanmıştır:

Domain → İş kuralları
Application → Use-case & abstractions
Infrastructure→ DB, external sistemler
Worker → Background execution engine
API → Job yönetimi


Worker katmanı, çalıştıracağı işi bilmez.  
Sadece `JobType`’a göre doğru strategy’yi çözer ve çalıştırır.

Bu sayede sistem:

- Open/Closed Principle’a uygundur
- Kolay genişletilebilir
- Test edilebilir
- Üretim ortamına hazırdır

---

## 🧠 Temel Özellikler

### ✔ Strategy Pattern ile Dinamik Job Çözümleme
Yeni bir entegrasyon eklemek için sadece yeni bir strategy yazmak yeterlidir.

### ✔ Background Scheduler
Belirlenen interval’a göre job’lar otomatik tetiklenir.

### ✔ Retry Mekanizması
Geçici hatalarda job tekrar denenir.

### ✔ Structured Logging (Serilog)
Tüm loglar sorgulanabilir ve üretim ortamına uygundur.


### ✔ Execution Duration Tracking
Her job çalışmasının süresi ölçülür.

---

## ⚙ Kullanılan Teknolojiler

- .NET 8
- Worker Services
- EF Core (SQLite)
- Serilog
- Dependency Injection
- Clean Architecture

---

## 🔄 Örnek Senaryo

**CRM → ERP Senkronizasyonu**

1. CRM’den veri çekilir  
2. Gerekli dönüşüm yapılır  
3. ERP sistemine gönderilir  
4. Loglanır  
5. Hata durumunda retry uygulanır  

---

## 📈 Bu Proje Ne Gösteriyor?

- Background processing bilgisi
- Katmanlı mimari tasarımı
- Genişletilebilir entegrasyon altyapısı
- Üretim odaklı logging yaklaşımı
- Dayanıklı sistem tasarım prensipleri

---

## 🚧 Geliştirme Planı

- Event-driven mimariye geçiş (RabbitMQ / Kafka)
- Paralel job execution
- Distributed locking
- Health checks & metrics
- Docker containerization

---

> Bu proje, gerçek dünya entegrasyon senaryoları düşünülerek tasarlanmış modüler bir süreç yönetim altyapısıdır.

