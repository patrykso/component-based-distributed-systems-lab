# Task details

1. wywołanie asynchroniczne metody 20%
   IZadanie1.DlugieObliczenia
   w trakcie jej wykonania, w przeciągu 3 sekund należy
   wywołać metodę Szybciej z parametrami (x, 3x^2 - 2x)
   dla x = 0...20; metodę Szybciej proszę wywołać
   synchronicznie, inaczej zadanie może być nie zaliczone
   adres (metadane): net.pipe://localhost/ksr-wcf2-metadane

2. Wołanie zwrotne 15%
   należy skorzystać z metody IZadanie2.PodajZadania
   funkcja ta woła zwrotnie funkcję
   IZadanie2Enum.Zadanie z nazwą podzadania, liczbą punktów za nie
   i zmienną logiczną opisującą, czy podzadanie jest zaliczone
   Proszę wypisać te informacje (nazwa podzadania, liczbą punktów i czy zaliczone)
   w kliencie.
   adres (metadane): net.pipe://localhost/ksr-wcf2-metadane

3. Wołanie zwrotne 15%
   należy udostępnić serwis IZadanie3
   pod adresem
   net.pipe://localhost/ksr-wcf2-zad3
   (binding NetNamedPipeBinding)
   obsługa metody TestujZwrotny powinna wywołać w przeciągu 4 sekund
   metodę interfejsu zwrotnego WolanieZwrotne(x, x^3 - x^2) dla x=0..30
   Uwaga: proszę uruchomić swój serwer bez debuggera

4. Kontrola czasu życia 10%
   należy udostępnić serwis IZadanie4 z czasem życia PerSession
   pod adresem
   net.pipe://localhost/ksr-wcf2-zad4
   (binding NetNamedPipeBinding)
   funkcja Ustaw powinna ustawiać wewnętrzny licznik na otrzymany
   argument,
   funkcja Dodaj powinna dodawać swój parametr to wewnętrznego
   licznika i zwracać wartość tego licznika (po dodaniu)

Uruchamiając test.exe można podać w pierwszym i drugim argumencie
adresy serwisów do zadania 5 i 6 inaczej otrzymamy błąd (405) niedozwolona metoda.

5. Uruchomienie własnego hostowanego serwera WCF 15%
   implementującego interfejs IZadanie5
   z bindingiem basicHttpBinding

6. należy dodatkowo udostępnić serwis IZadanie6 w swoim 15%
   serwerze z bindingiem wsDualHttpBinding
   metoda Dodaj(int a, int b) powinna wołać zwrotnie
   metodę Wynik z parametrem a + b

7. Skorzystanie z obu endpointów ze swojego serwera 10%
   w swoim kliencie
