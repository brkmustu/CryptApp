# CryptApp

Herkese merhabalar.

Uygulamanın temel amacı client ile backend arasında apigateway üzerinden asenkron haberleşme sağlayan event driven bir mimari ortaya koymak. Uygulama ile aşağıdaki kullanım durumlarını gerçekleştirmek üzerine odaklanılmıştır.

Kullanım Durumu 1: Client HTTP request ile kendisine verilen API Key ve API Pass ile bir Rest Web API servisine istekte bulunur ve kendisine bir Token döndürülür.
Kullanım Durumu 2: Client, Kullanım Durumu 1'de alınan Token ile belirli süre boyunca Crypto isteğinde bulunabilir. İstek sonucunda da parametre ile gönderilen data Encrypt/Decrypt edilerek Client'a döndürülür.

>Uygulamayı yerel bilgisayarınızda ayağa kaldırmak için docker'ın yüklü ve çalışıyor olması yeterli. powershell penceresinden ".\up-re-create.ps1" komutu ile uygulamayı ayağa kaldırabilirsiniz.

Uygulamada aşağıdaki teknolojiler kullanışmıştır;

- apigateway olarak "traefik" kullanılmıştır.
- client uygulama "vuejs" ile geliştirilmiştir.
- rest api servisleri asp.net 6 ile geliştirilmiştir.
- jwt validasyon için "traefik"'in "middleware" yapısından faydalanılmıştır ve sırf jwt token validasyonunu gerçekleştirmesi için ayrı bir api servisi geliştirilmiştir.
- dağıtık cache mimarisi için "redis" kullanılmıştır.

Git repository'i indirip docker üzerinden uygulamayı ayağa kaldırdıktan sonra "http://localhost:9000" üzerinden client uygulamasına erişebilirsiniz. Uygulama ilk ayağa kalkarken bir defaya mahsus redis'e aşağıdaki bilgileri otomatik kaydetmektedir

>ApiKey:"UdqoDma93upHRNm2rn0u"
>Password:"123456"

Eğer postman gibi bir rest api test aracı yardımıyla api'leri test etmek isterseniz aşağıdaki adresleri kullanabilirsiniz.

Token almak için > "http://localhost:9000/api/auth/signin"
Örnek request body;

```json
{
    "ApiKey": "UdqoDma93upHRNm2rn0u",
    "Password": "123456"
}
```

Authentication sürecini anlatan diyagramımız;

![Jwt Authentication Sürecimiz!](https://github.com/brkmustu/CryptApp/blob/main/docs/jwt-auth-sureci.png "Jwt Authentication Sürecimiz")


Şifreleme için > "http://localhost:9000/api/crypt/encrypt"

```json
{
    "Context": "düz metin"
}
```

Şifrelenmiş metini çözmek için > "http://localhost:9000/api/crypt/decrypt"

```json
{
    "Context": "metnin şifrelenmiş hali"
}
```

Şifreleme uygulamasının sürecini anlatan diyagramımız;

![Crypt Service Sürecimiz!](https://github.com/brkmustu/CryptApp/blob/main/docs/crypt-service-sureci.png "Crypt Service Sürecimiz")

## Büyük Resim :)

Şimdiye kadar ne geliştirdiğimizi ve nasıl kullanılabileceğini aktardık. Fakat asıl önemli kısım işin mantığını anlatılması en önemli noktalardandır. O halde biz niye böyle karmaşıklığı yüksek mimarilere ihtiyaç duyuyoruz?