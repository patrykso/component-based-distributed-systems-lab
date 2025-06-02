# Task details

Proszę napisać i uruchomić w emulatorze
Azure serwis WCF z metodami:

- Koduj(string nazwa, string tresc) 10%
- tworzy bloba o nazwie nazwa i treści tresc
  i informuje workera, poprzez kolejkę,
  o nowym blobie do zakodowania
- worker 40%
  w nieskończoność:
- odbiera informacje od metody Koduj
  (poprzez kolejkę)
- koduje bloby algorytmem ROT13
- kodowanie z prawdopodobieństwem 1/3
  rzuca wyjątek, którego worker nie łapie
- w przypadku wyjątku worker próbuje
  ponowić kodowanie
- zakodowane bloby zapisuje w drugim kontenerze
- string Pobierz(string nazwa) 10%
  pobiera treść zakodowanego bloba o nazwie nazwa
  z drugiego kontenera
- kolejka 20%
- bloby 10%
- bloby kodowane 10%

ROT13 należy zaimplementować samodzielnie.
https://pl.wikipedia.org/wiki/ROT13
Polskie znaki można zignorować.

UWAGA !!! - Zadanie proszę zrealizować w oparciu o wspierane wersje bibliotek:
Azure.Core<br>
Azure.Data.Tables<br>
Azure.Storage.Common<br>
Azure.Storage.Blobs<br>
Azure.Storage.Queues<br>
Proszę nie wykorzystywać przestarzałych już bibliotek, takich jak:<br>
Microsoft.WindowsAzure.Storage<br>
Microsoft.WindowsAzure.ServiceRuntime<br>
Microsoft.WindowsAzure.Diagnostics<br>
Microsoft.OData.Edm<br>
pomimo, że są one podawane w instrukcji.<br>
Wyjątek stanowi klasa abstrakcyjna RoleEntryPoint z pakietu Microsoft.WindowsAzure.ServiceRuntime
