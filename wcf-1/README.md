# Task details

Program test.exe udostępnia serwisy, z których należy
skorzystać w zadaniach 1 i 5 oraz testuje zadania 2 i 4.

Zadania:

1. skorzystanie z serwisu IZadanie1 (metoda Test) 15%
   adres: net.pipe://localhost/ksr-wcf1-test
   binding: NetNamedPipeBinding
   plik: KSR_WCF1.dll
   interfejs:
   KSR_WCF1.IZadanie1 {
   string Test(string arg);
   void RzucWyjatek(bool czy_rzucic);
   }
   KSR_WCF1.Wyjatek {
   string opis;
   }

2. wystawienie serwisu (własny serwer self-hosting) 20%
   adres: localhost/ksr-wcf1-zad2
   binding: NetNamedPipeBinding
   plik: KSR_WCF1.dll
   interfejs:
   KSR_WCF1.IZadanie2 {
   string Test(string arg);
   }

3. udostępnienie metadanych we własnym serwisie 10%
   Zrobić print screen podczas wykrywania serwisu dodając odwołanie do usługi.
   W oknie Usługi powinien być widoczny udostępniony interfejs a poniżej tekst, że znaleziono usługi.

4. udostępnienie serwisu z pkt. 2 również pod 15%
   adresem 127.0.0.1:55765
   z bindingiem NetTcpBinding

5. wywołanie metody RzucWyjatek (IZadanie1) 20%
   z parametrem true i wykonanie polecenia zawartego
   w treści wyjątku we własnym kliencie (z pkt. 1)

6. i 7. dodanie do własnego serwera usługi z metodą 20%
   RzucWyjatek7 rzucającą wyjątek Wyjatek7
   i złapanie wyjątku we własnymy kliencie
   i wypisanie jego zawartości
   interfejs:
   IZadanie7 {
   void RzucWyjatek7(string a, int b);
   }
   Wyjatek7 {
   string opis;
   string a;
   int b;
   }
