<H2>#PhoneBookApp</H2>
<br>
Senaryo
Birbirleri ile haberleşen minimum iki microservice'in olduğu bir yapı tasarlayarak, basit 
bir telefon rehberi uygulaması oluşturulması sağlanacaktır.
Beklenen işlevler:
<br>
• Rehberde kişi oluşturma<br>
• Rehberde kişi kaldırma<br>
• Rehberdeki kişiye iletişim bilgisi ekleme<br>
• Rehberdeki kişiden iletişim bilgisi kaldırma<br>
• Rehberdeki kişilerin listelenmesi<br>
• Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin 
getirilmesi<br>
• Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor 
talebi<br>
• Sistemin oluşturduğu raporların listelenmesi<br>
• Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi<br>
Teknik Tasarım
Kişiler: Sistemde teorik anlamda sınırsız sayıda kişi kaydı yapılabilecektir. Her kişiye 
bağlı iletişim bilgileri de yine sınırsız bir biçimde eklenebilmelidir.
Karşılanması beklenen veri yapısındaki gerekli alanlar aşağıdaki gibidir:
• UUID<br>
• Ad<br>
• Soyad<br>
• Firma<br>
• İletişim Bilgisi<br>
o Bilgi Tipi: Telefon Numarası, E-mail Adresi, Konum<br>
o Bilgi İçeriği<br>
Rapor: Rapor talepleri asenkron çalışacaktır. Kullanıcı bir rapor talep ettiğinde, sistem 
arkaplanda bu çalışmayı darboğaz yaratmadan sıralı bir biçimde ele alacak; rapor 
tamamlandığında ise kullanıcının "raporların listelendiği" endpoint üzerinden raporun 
durumunu "tamamlandı" olarak gözlemleyebilmesi gerekmektedir.<br>
Rapor basitçe aşağıdaki bilgileri içerecektir:<br>
• Konum Bilgisi<br>
• O konumda yer alan rehbere kayıtlı kişi sayısı<br>
• O konumda yer alan rehbere kayıtlı telefon numarası sayısı<br>
Veri yapısı olarak da:<br>
• UUID<br>
• Raporun Talep Edildiği Tarih<br>
• Rapor Durumu (Hazırlanıyor, Tamamlandı)<br>
<br>
###Nasıl Çalıştırılır
PhoneBookApp uygulamasının çalıştırılması için aşağıdaki adımları takip edin:

####1. Projeyi Kopyalayın
İlk olarak, projeyi yerel bilgisayarınıza kopyalamak için GitHub'dan klonlayın:

>> gh repo clone bora-yilmaz270/PhoneBookApp

####2. Docker Kurulumu
<p>Kurulum için gerekli olan dosyalar docker.com adresinden indirilir ve işletim sistemine göre kurulum yapılır.</p>

####3. Mongo Db   
<p>Docker hub üzerinden (https://hub.docker.com/_/mongo)  adresinde verilen ilgili kurulum komutlarını kullanarak mongo db image’ını indirip container olarak çalıştırabilirsiniz. </p>
>> docker pull mongo 
>>docker run --name some-mongo -d mongo:tag

####4. RabbitMQ 
<p>Docker hub üzerinden (https://hub.docker.com/_/rabbitmq) adresinde verilen ilgili kurulum komutlarını kullanarak Rabbitmq image’ını indirip container olarak çalıştabilirsiniz. </p>
>>docker pull rabbitmq
>> docker run -d --hostname my-rabbit --name some-rabbit -e RABBITMQ_DEFAULT_VHOST=my_vhost rabbitmq:3-management

<p>
İlgili kurulumlar tamamlandıktan sonra projeyi VS ortamında çalıştırabilirsiniz.
Not: Docker arayüzden mongo db ve rabbitmq container’ların çalıştığını kontrol edebilirsiniz.
</p>

<br>
###HTTP İSTEKLERİ
<HR/>
PhoneBookApi<br>
Contacts
GET https://localhost:7066/api/Contacts<br>
POST https://localhost:7066/api/Contacts<br>
GET https://localhost:7066/api/Contacts/{id}<br>
DELETE https://localhost:7066/api/Contacts/{id}<br>
<br>
ContactInfos
GET https://localhost:7066/api/ContactInfos<br>
POST https://localhost:7066/api/ContactInfos<br>
DELETE https://localhost:7066/api/ContactInfos/{id}<br>
<br>
POST https://localhost:7066/api/Contacts
ContactCreateDto:<br>

name	string

lastName	string

company	string


POST https://localhost:7066/api/ContactInfos
ContactInfoCreateDto:<br>

contactId	string

infoType	string

Value	string






PhoneBookReportApi

GET https://localhost:7260/api/Report<br>
POST https://localhost:7260/api/Report<br>
GET https://localhost:7260/api/Report/{id}<br>
GEThttps://localhost:7260/api/Report/GetDetailsByReportIdAsync/{id}<br>
GET https://localhost:7260/api/Report/GetAllReportDetailAsync<br>
