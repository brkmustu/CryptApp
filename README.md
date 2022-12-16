# CryptApp

uygulama hala geliştirme aşamasında.

ancak traefik üzerinden temel fonksiyonlar yapılabiliyor.

- jwt validation middleware ile jwt kontrolü,
- token almak için "http://localhost:9000/api/auth/signin" api'sini kullanabilirsiniz. Authentication servisi ilk ayağa kalkarken redis üzerinde varsayılan api'key ile "ApplicationIdenifier" kayıtlı değil ise ilk bu kaydı redise ekler. Aşağıdaki body ile token alabilirsiniz;
```json
{
    "ApiKey": "UdqoDma93upHRNm2rn0u",
    "Password": "123456"
}
```
- token valid ise "http://localhost:9000/api/crypt/encrypt" yada "http://localhost:9000/api/crypt/decrypt" api'leri kullanıma hazır, (jwt ile authentice değil ise 401 hatası verir)
- vuejs client app yine traefik üzerinden çalışır vaziyette,

önemli not : sistem rabbitmq ile asenkron tasarlanmaktadır ve henüz api'lere cevap döndüğünü gösterebileceğimiz client uygulamamız tamamlanmadı.\
önemli not : bir diğer tamamlanması gereken konu ise signalr ile client'a api üzerinden gelen ve event processor ile işlenen request'in cevabının döndürülmesi konusunu henüz çözemedim.

şimdilik bu kadar :)
