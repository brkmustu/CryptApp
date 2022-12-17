# Backend Yapısı

Uygulamayı geliştirirken elimden geldiğince temiz mimari (clean architect) ve alan odaklı tasarım (domain driven design) konularının ilkelerini ön planda tutmaya özen gösterdim.

Burada "Backend" yapısından bahsedeceğiz.

backend'i oluşturan 3 ana başlığımız var.

  - "auth": kimlik doğrulama servisi => 
  - "crypt": şifreleme servisleri
    * "Encrypt": şifreleme servisi
    * "Decrypt": şifrelenmiş metni çözümleyen servis
  - mesaj işleyici yada diğer bir deyişle mesaj komisyoncusu (message broker, türkçeleştirince ilk etapta kulağa biraz garip geliyor:))
    * "EncryptConsumer": Şifreleme isteklerinin iletildiği kuyruğu dinleyerek gelen istekler için şifreleme işlemini gerçekleştirip, client uygulamasına sonucu SignalR yardımıyla ileten tükecitidir. Namı diğer event processor.
    * "DecryptConsumer": Şifre çözümleme isteklerinin iletildiği kuyruğu dinleyerek gelen istekler için şifrelenmiş metni çözümleyip sonucu client uygulamasına SignalR yardımıyla ileten tüketicidir.

## Kimlik Doğrulama Servisi

"ApiKey" ile sistemde kayıtlı bir uygulama tanım kaydı (ApplicationIdentifier) varsa istekte "ApiKey" ile birlikte gönderilen "Password" bilgileri eşleşiyorsa bir token üretip cevap olarak döndürür.

Token almak için url > "http://localhost:9000/api/auth/signin"
Örnek request body;

```json
{
    "ApiKey": "UdqoDma93upHRNm2rn0u",
    "Password": "123456"
}
```

## Şifreleme Servisleri

### Şifreleme Servisi (Encrypt)

ilgili istek içinde gönderilen "Context" string değerini şifrelenmesini sağlayacak ilgili mesaj kuyruğuna request'i ileten servistir.

Şifreleme için url > "http://localhost:9000/api/crypt/encrypt"

```json
{
    "Context": "düz metin"
}
```

### Şifreleme Servisi (Decrypt)

ilgili istek içinde gönderilen "Context" şifrelenmiş metnin çözümlenmesini sağlayacak ilgili mesaj kuyruğuna request'i ileten servistir.

Şifrelenmiş metini çözmek için url > "http://localhost:9000/api/crypt/decrypt"

```json
{
    "Context": "metnin şifrelenmiş hali"
}
```