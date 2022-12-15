# CryptApp

uygulama hala geliþtirme aþamasýnda.

ancak traefik üzerinden temel fonksiyonlar yapýlabiliyor.

- jwt validation middleware ile jwt kontrolü,
- token almak için "http://localhost:9000/api/auth/signin" api'sini kullanabilirsiniz. Authentication servisi ilk ayaða kalkarken redis üzerinde varsayýlan api'key ile "ApplicationIdenifier" kayýtlý deðil ise ilk bu kaydý redise ekler. Aþaðýdaki body ile token alabilirsiniz;
```json
{
    "ApiKey": "00000000-0000-0000-1111-000000000000",
    "Password": "!38bad8_92f@T7"
}
```
- token valid ise "http://localhost:9000/api/crypt/encrypt" yada "http://localhost:9000/api/crypt/decrypt" api'leri kullanýma hazýr, (jwt ile authentice deðil ise 401 hatasý verir)
- vuejs client app yine traefik üzerinden çalýþýr vaziyette,

önemli not : sistem rabbitmq ile asenkron tasarlanmaktadýr ve henüz api'lere cevap döndüðünü gösterebileceðimiz client uygulamamýz tamamlanmadý.
önemli not : bir diðer tamamlanmasý gereken konu ise signalr ile client'a api üzerinden gelen ve event processor ile iþlenen request'in cevabýnýn döndürülmesi konusunu henüz çözemedim.

þimdilik bu kadar :)