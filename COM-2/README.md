# Task details

UWAGA:

- po wypakowaniu plików należy kliknąć prawym przyciskiem myszy na plikach exe i dll, wybrać właściwości i kliknąć "Odblokuj"
- wszystkie dll-ki są 32-bitowe (co można sprawdzić za pomocą sigcheck64.exe)
- podczas rejestracji należy wziąć pod uwagę czy system operacyjny jest 32 czy 64-bitowy
- klasy proszę rejestrować tylko dla bieżącego użytkownika
  (na stanowiskach labortoryjnych nie ma uprawnień do rejestrowania dla wszystkich)

Klasa:<br>
guid klasy = 23E86DCB-EE9F-44DE-B37B-D30BABD1B58C<br>
CLSID_Klasa = { 0x23e86dcb, 0xee9f, 0x44de, 0xb3, 0x7b, 0xd3, 0xb, 0xab, 0xd1, 0xb5, 0x8c };<br>
guid interfejsu = 09726F06-F81E-4F8A-99E2-21CC845D665A<br>
IID_IKlasa = { 0x9726f06, 0xf81e, 0x4f8a, 0x99, 0xe2, 0x21, 0xcc, 0x84, 0x5d, 0x66, 0x5a };<br>
guid proxy = 1434095E-4E29-4AD1-8784-0652813BD9D7}<br>
PROXY_CLSID_IS = { 0x1434095e, 0x4e29, 0x4ad1, { 0x87, 0x84, 0x6, 0x52, 0x81, 0x3b, 0xd9, 0xd7 } };<br>
ProgID = KSR20.Klasa.1<br>

Klasa2:<br>
guid klasy = 6C211F4F-B068-4F9C-B5B7-7E9A9423A64A<br>
CLSID_Klasa2 = { 0x6c211f4f, 0xb068, 0x4f9c, 0xb5, 0xb7, 0x7e, 0x9a, 0x94, 0x23, 0xa6, 0x4a };<br>
guid interfejsu = AFEAC528-6033-4E9B-85D7-36EBE56A4A81<br>
GUID IID_IKlasa2 = { 0xafeac528, 0x6033, 0x4e9b, 0x85, 0xd7, 0x36, 0xeb, 0xe5, 0x6a, 0x4a, 0x81 };<br>
guid proxy = 08B8D49E-E421-4A23-BFB8-184439F98DC6<br>
PROXY_CLSID_IS = { 0x8b8d49e, 0xe421, 0x4a23, { 0xbf, 0xb8, 0x18, 0x44, 0x39, 0xf9, 0x8d, 0xc6 } };<br>

zadania:

1. rejestracja serwera i proxy Klasy (pliki \*.reg) 10% + 10%
   testowanie: test.exe
   pliki:
   - server.exe
   - proxyStub.dll
   - rej.reg/rej64.reg + unrej.reg/unrej64.reg
   - rejproxy.reg/rejproxy64.reg + unrejproxy64.reg
2. nadanie ProgID Klasie (KSR20.Klasa.1) 10%
   testowanie: test.exe p
   pliki:
   - setprogid.reg
   - unsetprogid.reg
3. klient Klasy 10%
   pliki:
   - client.cpp
   - guid_k1.cpp
   - Iklasa.h
4. proxyStub dla IKlasa2
   - implementacja i rejestracja proxy 20%
     testowanie: test.exe 2
     pliki:
   - Klasa2.idl
   - proxyStub2.def
   - rejproxy2.reg / rejproxy2_64.reg
   - unrejproxy2.reg / unrejproxy2_64.reg
   - server2.exe
   - proxyStub2.dll (pozwala sprawdzić poprawność rejestreacji proxy)
5. serwer dla Klasy2
   - implementacja klasy 15%
   - implementacja rejestracja serwera 15%
     testowanie: test.exe 2
     pliki:
   - IKlasa2.h
   - Klasa2.h
   - Klasa2.cpp
   - fabryka2.cpp
   - guid_k2.cpp
   - server2.cpp
   - rej2.reg / rej2_64.reg
   - unrej2.reg / unrej2_64.reg
6. klient do Klasy2 10%
   pliki:
   - client2.cpp
   - guid_k2.cpp
   - Iklasa2.h

Uwagi:

1. rejestrujemy tylko dla bieżącego użytkownika
2. pliki wykonywalne należy odblokować
   (prawym przyciskiem myszy -> właściwości -> odblokuj)
3. zalecany jest ssytem 64-bitowy, dll-ki są 32-bitowe
4. serwer proszę umieścić w apartamencie wielowątkowym
5. proxy do Klasy2 proszę skompilować w trybie Release
