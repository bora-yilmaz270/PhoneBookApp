# PhoneBookApp

## Senaryo
PhoneBookApp, birbirleri ile haberleşen en az iki microservice içeren bir yapıda tasarlanmış bir telefon rehberi uygulamasıdır. Uygulama, basit ve etkin telefon rehberi işlevlerini sunar.

### Beklenen İşlevler
- Rehberde kişi oluşturma
- Rehberde kişi kaldırma
- Rehberdeki kişiye iletişim bilgisi ekleme
- Rehberdeki kişiden iletişim bilgisi kaldırma
- Rehberdeki kişilerin listelenmesi
- Rehberdeki bir kişiyle ilgili iletişim bilgilerinin detayları
- Rehberdeki kişilerin konuma göre istatistik raporları
- Sistemin oluşturduğu raporların listelenmesi
- Bir raporun detay bilgilerinin getirilmesi

### Teknik Tasarım
- **Kişiler:** Sınırsız sayıda kişi kaydı ve her kişiye bağlı sınırsız iletişim bilgisi eklenebilir.
- **Veri Yapısı:** UUID, Ad, Soyad, Firma, İletişim Bilgisi (Telefon Numarası, E-mail Adresi, Konum), Bilgi İçeriği.

### Rapor
- Raporlar asenkron çalışır.
- Rapor bilgileri: Konum Bilgisi, O konumdaki kişi sayısı, O konumdaki telefon numarası sayısı.
- Veri yapısı: UUID, Raporun Talep Edildiği Tarih, Rapor Durumu (Hazırlanıyor, Tamamlandı).

## Nasıl Çalıştırılır
PhoneBookApp uygulamasının çalıştırılması için aşağıdaki adımları takip edin:

### 1. Projeyi Kopyalayın
GitHub'dan projeyi klonlayın:
gh repo clone bora-yilmaz270/PhoneBookApp

### 2. Docker Kurulumu
Docker.com adresinden gerekli dosyaları indirip işletim sisteminize kurun.

### 3. Mongo DB
Docker hub üzerinden Mongo DB image'ını indirip container olarak çalıştırın:
docker pull mongo
docker run --name some-mongo -d mongo:tag

### 4. RabbitMQ
Docker hub üzerinden RabbitMQ image'ını indirip container olarak çalıştırın:
docker pull rabbitmq
docker run -d --hostname my-rabbit --name some-rabbit -e RABBITMQ_DEFAULT_VHOST=my_vhost rabbitmq:3-management

Kurulumlar tamamlandıktan sonra projeyi Visual Studio ortamında çalıştırabilirsiniz. Docker arayüzünden Mongo DB ve RabbitMQ container'larının çalıştığını kontrol edin.

## HTTP İstekleri
Aşağıda PhoneBookApi ve PhoneBookReportApi için HTTP istekleri listelenmiştir:

### PhoneBookApi
- `GET https://localhost:7066/api/Contacts`
- `POST https://localhost:7066/api/Contacts`
- `GET https://localhost:7066/api/Contacts/{id}`
- `DELETE https://localhost:7066/api/Contacts/{id}`
- `GET https://localhost:7066/api/ContactInfos`
- `POST https://localhost:7066/api/ContactInfos`
- `DELETE https://localhost:7066/api/ContactInfos/{id}`

### Contact Dto Örnekleri
POST https://localhost:7066/api/Contacts
ContactCreateDto:

name: string
lastName: string
company: string
POST https://localhost:7066/api/ContactInfos
ContactInfoCreateDto:

contactId: string
infoType: string
Value: string

### PhoneBookReportApi
- `GET https://localhost:7260/api/Report`
- `POST https://localhost:7260/api/Report`
- `GET https://localhost:7260/api/Report/{id}`
- `GET https://localhost:7260/api/Report/GetDetailsByReportIdAsync/{id}`
- `GET https://localhost:7260/api/Report/GetAllReportDetailAsync`
