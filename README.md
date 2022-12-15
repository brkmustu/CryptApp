# CryptApp

uygulama hala geli�tirme a�amas�nda.

ancak traefik �zerinden temel fonksiyonlar yap�labiliyor.

- jwt validation middleware ile jwt kontrol�,
- token almak i�in "http://localhost:9000/api/auth/signin" api'sini kullanabilirsiniz. Authentication servisi ilk aya�a kalkarken redis �zerinde varsay�lan api'key ile "ApplicationIdenifier" kay�tl� de�il ise ilk bu kayd� redise ekler. A�a��daki body ile token alabilirsiniz;
```json
{
    "ApiKey": "00000000-0000-0000-1111-000000000000",
    "Password": "!38bad8_92f@T7"
}
```
- token valid ise "http://localhost:9000/api/crypt/encrypt" yada "http://localhost:9000/api/crypt/decrypt" api'leri kullan�ma haz�r, (jwt ile authentice de�il ise 401 hatas� verir)
- vuejs client app yine traefik �zerinden �al���r vaziyette,

�nemli not : sistem rabbitmq ile asenkron tasarlanmaktad�r ve hen�z api'lere cevap d�nd���n� g�sterebilece�imiz client uygulamam�z tamamlanmad�.
�nemli not : bir di�er tamamlanmas� gereken konu ise signalr ile client'a api �zerinden gelen ve event processor ile i�lenen request'in cevab�n�n d�nd�r�lmesi konusunu hen�z ��zemedim.

�imdilik bu kadar :)