# CryptApp

Herkese merhabalar.

Uygulamanın temel amacı client ile backend arasında apigateway üzerinden asenkron haberleşme sağlayan event driven bir mimari ortaya koymak. Uygulama ile aşağıdaki kullanım durumlarını gerçekleştirmek üzerine odaklanılmıştır.

Kullanım Durumu 1: Client HTTP request ile kendisine verilen API Key ve API Pass ile bir Rest Web API servisine istekte bulunur ve kendisine bir Token döndürülür.
Kullanım Durumu 2: Client, Kullanım Durumu 1'de alınan Token ile belirli süre boyunca Crypto isteğinde bulunabilir. İstek sonucunda da parametre ile gönderilen data Encrypt/Decrypt edilerek Client'a döndürülür.

>Uygulamayı yerel bilgisayarınızda ayağa kaldırmak için docker'ın yüklü ve çalışıyor olması yeterli. powershell penceresinden ".\up-re-create.ps1" komutu ile uygulamayı ayağa kaldırabilirsiniz.

Uygulamada aşağıdaki teknolojiler kullanışmıştır;

- apigateway olarak "traefik" kullanılmıştır.
- client uygulama "vuejs" ile geliştirilmiştir.
- rest api servisleri asp.net 6 ile geliştirilmiştir. (not: .net 8 migration gerçekleştirilmiştir)
- jwt validasyon için "traefik"'in "middleware" yapısından faydalanılmıştır ve sırf jwt token validasyonunu gerçekleştirmesi için ayrı bir api servisi geliştirilmiştir.
- dağıtık cache mimarisi için "redis" kullanılmıştır.
- back-end tarafında işlem tamamlanınca cevabın real-time olarak geri döndürülmesi için "signalr" kullanılmıştır.

Git repository'i indirip docker üzerinden uygulamayı ayağa kaldırdıktan sonra "http://localhost:9000" üzerinden client uygulamasına erişebilirsiniz. Uygulama ilk ayağa kalkarken bir defaya mahsus redis'e aşağıdaki bilgileri otomatik kaydetmektedir

>ApiKey:"UdqoDma93upHRNm2rn0u"
>Password:"123456"

Eğer postman gibi bir rest api test aracı yardımıyla api'leri test etmek isterseniz aşağıdaki adresleri kullanabilirsiniz.

Token almak için url > "http://localhost:9000/api/auth/signin"
Örnek request body;

```json
{
    "ApiKey": "UdqoDma93upHRNm2rn0u",
    "Password": "123456"
}
```

Authentication sürecini anlatan diyagramımız;

![Jwt Authentication Sürecimiz!](https://github.com/brkmustu/CryptApp/blob/main/docs/jwt-auth-sureci.png "Jwt Authentication Sürecimiz")


Şifreleme için url > "http://localhost:9000/api/crypt/encrypt"

```json
{
    "Context": "düz metin"
}
```

Şifrelenmiş metini çözmek için url > "http://localhost:9000/api/crypt/decrypt"

```json
{
    "Context": "metnin şifrelenmiş hali"
}
```

Şifreleme uygulamasının sürecini anlatan diyagramımız;

![Crypt Service Sürecimiz!](https://github.com/brkmustu/CryptApp/blob/main/docs/crypt-service-sureci.png "Crypt Service Sürecimiz")

## Büyük Resim :)

Şimdiye kadar ne geliştirdiğimizi ve nasıl kullanılabileceğini aktardık. Fakat asıl önemli kısım işin mantığını anlatılması en önemli noktalardandır. O halde biz niye böyle karmaşıklığı yüksek mimarilere ihtiyaç duyuyoruz?

Aslında en temel ihtiyaç "soyutlama" ve "esneklik" olsada her şirketin kendi dinamikleri farklıdır. Eğer saniye başına aldığınız istek adedi artık baş ağrıtmaya başladıysa başka bir sebep olabilir. Deployment süreçlerinde takımların kendi başına hareket edebilmesini sağlamak yine dağıtık mimarilerde ön plana çıkan bir diğer husus. Ancak uygulama büyüdükçe, uygulamada karşılaşılan problemleri farklı programlama dilleri ile çözmenize imkan sağlamasıda yine yazılımcıları gıdıklayan başka bir husustur :).

Bu mimarinin avantaj ve dezavantajlarınıda kısaca özetlemek gerekirse;

Avantajlar;

- Ölçeklenebilir,
- Dağıtık Çalışabilir,
- Yüksek performanslı,
- Çevik,
- Her bir parçanın ayrı ayrı yüklenebilmesi,
- bileşenlerin sadece kendi mantıklarını bilmesi ve buna göre test yapılması açısından oldukça güzel bir mimari stildir. Bundan dolayı zaten dağıtık yapılarda sıkça tercih edilir.

Dezavantajlarına gelirsek;
 - Consistency(tutarlılık),
 - Atomicity (atomik),
 - Sistemin takip edilebilirliği, bir hata oluştuğunda yapılan işlemlerin geri alınması,
 - Tüm sistemin testi

gibi ana başlıklar bu stilin zorluklarıdır diyebiliriz. Mimarinin daha detaylı analizi için Onur Dayıbaşının [şuradaki makalesini](https://medium.com/architectural-patterns/event-driven-architecture-7d0a7fb57db8 "Event driven mimari için güzel bir türkçe içerik.") incelemenizi tavsiye ederim.

Tabi event driven mimarinin önde gelen konusu olan asenkron programlama ile ilgili detaylı bir bilgi almak isterseniz Tarık Güney'in konuyu gündelik yaşamdan anlattığı [şu makale](https://atarikguney.medium.com/asenkron-asynchronous-programlama-nedir-296230121f9d "Asenkron programlamanın mantığını aktarması adına gördüğüm en güzel makale") şimdiye kadar karşılaştığım en güzel anlatım şekliydi.

Temelde anlatılan konu uzun sürecek olan işlemlerin birbirilerini bloklamamasıdır. Ancak yinede kısa bir özet geçelim, klasik programlama yönteminde, request sunucuya geldiğinde sunucu o request'e karşılık gelen ilgili api fonksiyonunu tetikler ve uygulama server'da o fonksiyonun altında bir dizi işlemler gerçekleştirir. Bunlar arasında birbiri ile alakası olmayan ve paralelde yürütülebilecek olan işlemler olmasına rağmen siz uygulamayı buna uygun geliştirmezseniz uygulama bu şekilde hareket etmez. Birbiri ile ilişkişi olmayan işlemler birbirini beklemeye başlar.

İşte tam bu yüzden asenkron programlamaya ihtiyacımız var. Burada bunu uygulama geliştirme metodolojisi olarak ortaya koyan yaklaşımda Event Driven mimaridir. Bu mimari ile sadece fonksiyonun asenkron çalışmasını ve bunu fonksiyon bazlı değil, komple süreç bazlı yapmaya olanak verir. Peki bu ne demek? Aslında "nodejs" varsayılan olarak asenkron bir yapıda çalışıyor. Ancak bir programlama dilinin asenkron yapıda çalışması ile komple sistemin dağıtık bir mimaride asenkron çalışmasını sağlamak aynı şeyler değil. Bu kısımda monolith mimari ile dağıtık mimarilerin ayrıldığı nokta ölçeklendirilebilir yapılar sunması. Darboğaza giren bir servisinizin olduğunu düşünün. Bilinen ve her yerde anlatıldığı üzere, monolith bir mimaride uygulamanın daha fazla request karşılayabilmesini sağlamanın iki farklı yolu var. Dikey ve yatay ölçeklendirme. İşte tam bu kısımda da [şuradaki makaleyi](https://barisvelioglu.net/veritabanlar%C4%B1n%C4%B1n-evrimi-nosql-veritabanlar%C4%B1-neden-i%CC%87cat-edildi-sebebi-neydi-ki-7de176ed4486 "Ölçeklendirme konusunda detaylı bir kaynak") şiddetle tavsiye ediyorum.

Yukarıdaki uygulamamızda gerçekleştirdiğimiz örnekte ise bir message broker olan RabbitMq yardımıyla bu gerçekleştiriliyor. Crypt Rest API servisine gelen request'ler bir kuyruğa atılır ve bu kuyruğu dinleyen bir message broker sistemi kuyruktaki event'leri alıp işleyerek süreci tamamlayabilir, başka bir event tetikleyebilir yada işlem sonucu direkt client uygulama ile haberleşmesi gerekiyorsa günümüzde çokça kullanılan yollardan birisi olan websocket aracılığıyla işlem sonucunu client'a bildirebilir.
