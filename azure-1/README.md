# Task details

UWAGA !!! - Zadanie proszę zrealizować w oparciu o wspierane wersje bibliotek:

    Azure.Core
    Azure.Data.Tables
    Azure.Storage.Blobs
    Azure.Storage.Common

Proszę nie wykorzystywać przestarzałych już bibliotek, takich jak:

    Microsoft.WindowsAzure.Storage
    Microsoft.WindowsAzure.ServiceRuntime
    Microsoft.WindowsAzure.Diagnostics
    Microsoft.OData.Edm

pomimo, że są one podawane w instrukcji.

Proszę napisać i uruchomić w emulatorze
Serwer WCF z metodami:

- Create(login, hasło) 20%
  tworzy użytkownika
- Login(login, hasło) 20%
  sprawdza, czy użytkownik istnieje i ma dane hasło;
  jeżeli tak, tworzy sesję o nowym identyfikatorze
  (Guid.NewGuid()) i zwraca ten identyfikator
- Logout(login) 10%
  usuwa sesję dla danego użytkownika
- Put(nazwa, treść(string), id_sesji) 30%
  tworzy plik (BLOB) o podanej nazwe i podanej treści,
  o ile id_sesji jest poprawne (dla użytkownika, który
  zalogował się w podanej sesji)
- Get(nazwa, id_sesji) -> string 20%
  pobiera zawartość pliku o podanej nazwe,
  o ile id_sesji jest poprawne (dla użytkownika, który
  zalogował się w podanej sesji)
