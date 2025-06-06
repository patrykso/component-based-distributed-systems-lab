# Task details

UWAGA:

- po wypakowaniu plików należy kliknąć prawym przyciskiem myszy na plikach exe i dll, wybrać właściwości i kliknąć "Odblokuj"
- wszystkie dll-ki są 32-bitowe
- podczas rejestracji należy wziąć pod uwagę czy system operacyjny jest 32 czy 64-bitowy
- klasy proszę rejestrować tylko dla bieżącego użytkownika
  (nie ma uprawnień do rejestrowania dla wszystkich)

Klasa:<br>
guid interfejsu = D4A18C38-507D-458F-B47F-0DF89BDEC7E4<br>
GUID IID_IKlasa = { 0xd4a18c38, 0x507d, 0x458f, 0xb4, 0x7f, 0xd, 0xf8, 0x9b, 0xde, 0xc7, 0xe4 };<br>
guid klasy = 67417A67-096F-45EE-8063-4D206F5985B5<br>
CLSID CLSID_Klasa = { 0x67417a67, 0x96f, 0x45ee, 0x80, 0x63, 0x4d, 0x20, 0x6f, 0x59, 0x85, 0xb5 };<br>

Klasa2:<br>
guid interfejsu = 71E9F91F-9306-47B0-A6D0-8C8CBBC388F4<br>
GUID IID_IKlasa2 = { 0x71e9f91f, 0x9306, 0x47b0, 0xa6, 0xd0, 0x8c, 0x8c, 0xbb, 0xc3, 0x88, 0xf4 };<br>
guid klasy = 4F247A8D-0B03-445F-8C85-9AABC29C46C8<br>
CLSID CLSID_Klasa2 = { 0x4f247a8d, 0xb03, 0x445f, 0x8c, 0x85, 0x9a, 0xab, 0xc2, 0x9c, 0x46, 0xc8 };<br>

Klasa3:<br>
guid interfejsu = 6C18712A-4ECF-4ADB-9A91-79F13B17F409<br>
DEFINE_GUID(IID_IKlasa3, 0x6c18712a, 0x4ecf, 0x4adb, 0x9a, 0x91, 0x79, 0xf1, 0x3b, 0x17, 0xf4, 0x9);<br>
guid klasy = D41B2BB6-8973-4DBD-8A92-B1A81D3F4429<br>
DEFINE_GUID(CLSID_Klasa3, 0xd41b2bb6, 0x8973, 0x4dbd, 0x8a, 0x92, 0xb1, 0xa8, 0x1d, 0x3f, 0x44, 0x29);<br>

Zadania:

1. zarejestrowanie/wyrejestrowanie gotowej (20%)
   klasy o nazwie Klasa z pliku serwerInProc.dll
   serwerInProc.dll należy zarejestrować ręcznie
   (nie obsługuje regsvr32)
   testowanie: klient.exe
   info przed rejestracją i po wyrejestrowaniu: "klasa nie dziala (instancja nie pobrana)"
   info po rejestracji: "klasa stowrzona poprawnie (instancja pobrana)"
   pliki (\*.reg należy wybrać odpowiednie w zaleźności od systemu):

   - serwerInProc.dll
   - klient.exe
   - rej.reg / rej64.reg
   - unrej.reg / unrej64.reg
     zaliczenie: printScreen:
   - info po rejestracji i po wyrejestrowaniu
   - gałęzi odnalezionej w regedit z dodanym wpisem
   - pliki: \*.reg

2. implementacja klienta korzystającego (20%)
   z Klasy (interfejs jest opisany w IKlasa.h)
   pliki:

   - klient.cpp
   - guid.cpp
   - IKlasa.h
     zaliczenie: printScreen:
   - info z działającego klienta:
     "klasa stowrzona poprawnie (instancja pobrana), indeks: XXXXXX"
     XXXXXX - podczas implementacji zamienić na własny nr. indeksu
     pliki: _.cpp, _.h + oczyszczony projekt

3. implementacja klienta do gotowej Klasy2 (10%)
   korzystającego z metody registration-free;
   Klasa2 jest zaimplementowana w pliku serwerInProc2.dll
   i zawiera już poprawny manifest (włączony)
   pliki:

   - serwerInProc2.dll
   - klient2.cpp
   - klient2.exe.manifest
   - guid2.cpp
   - Iklasa2.h
     zaliczenie: printScreen:
   - info z działającego klienta:
     "klasa stowrzona poprawnie (instancja pobrana), indeks: XXXXXX, manifest"
     XXXXXX - podczas implementacji zamienić na własny nr. indeksu
     pliki: _.cpp, _.h + oczyszczony projekt

4. implementacja i rejestracja własnej Klasy3 (30% + 10%)
   testowanie: klient3.exe
   pliki (\*.reg należy wybrać odpowiednie w zaleźności od systemu):

   - dllmain3.cpp
   - fabryka3.cpp
   - fabryka3.h
   - iklasa3.h
   - klasa3.cpp
   - klasa3.def
   - klasa3.h
   - klient3.exe
   - rej_3.reg
   - rej64_3.reg
   - unrej_3.reg
   - unrej64_3.reg
   - guid3.cpp
     zaliczenie: printScreen:
   - info z działającego klienta:
     "klasa3 stowrzona poprawnie (instancja pobrana)"
     pliki: _.cpp, _.h, _.def, _.reg + oczyszczony projekt

5. implementacja klienta do Klasy3 (10%)
   pliki:
   - klient3.cpp
   - guid3.cpp
     zaliczenie: printScreen:
   - info z działającego klienta:
     "klasa3 stowrzona poprawnie (instancja pobrana), indeks: XXXXXX"
     XXXXXX - podczas implementacji zamienić na własny nr. indeksu
     pliki: _.cpp, _.h + oczyszczony projekt
