# Task details

Klasa (klasa.dll)
guid interfejsu: D2767A1D-7FDB-4AA1-9A69-8399F02C64EF
guid klasy: 82C2867A-8E32-4BB7-922A-18398DDA7F2C
ProgID klasy: KSR20.COM3Klasa.1
interfejs:
interface IKlasa {
uint Test(string s);
}

KlasaC (klasac.dll)<br>
guid interfejsu: D4A18C38-507D-458F-B47F-0DF89BDEC7E4<br>
guid klasy: 67417A67-096F-45EE-8063-4D206F5985B5<br>
guid biblioteki typów: 77833C46-9178-4A0D-80B1-93202FE99A27<br>

Klasa2<br>
guid interfejsu: F59DA79E-29BB-476C-BFF4-2E9C0ADFDD4D<br>
guid klasy: F08FB011-E87D-472E-9886-659C2559FB10<br>
ProgID klasy: KSR20.COM3Klasa.2<br>

KlasaC2 (klasac2.dll)<br>
guid interfejsu: 442A7716-E106-4279-9BC0-9C1B4ED1779B<br>
DEFINE_GUID(<<name>>, 0x442a7716, 0xe106, 0x4279, 0x9b, 0xc0, 0x9c, 0x1b, 0x4e, 0xd1, 0x77, 0x9b);<br>
guid klasy: 3F7A6746-5F7C-4A1C-BA61-E2C5C79CC096<br>
DEFINE_GUID(<<name>>, 0x67417a67, 0x96f, 0x45ee, 0x80, 0x63, 0x4d, 0x20, 0x6f, 0x59, 0x85, 0xb5);<br>
guid biblioteki typów: 36021344-93CA-4F02-B0AB-3A48AD734813<br>

Zadania:

1. rejestracja gotowego COMa napisanego w C# 10%
   testowanie: test.exe
   pliki:

   - klasa.dll
   - rej1.reg
   - unrej1.reg

2. napisanie klienta w C#, x86 (późne wiązanie) 10%
   do COMa z pkt. 1
   Sprawdzić czy do metody Test przekazujemy string jako parametr
   pliki:

   - klient2.cs (do własnej implementacji, nie dodany archiwum)

3. napisanie klienta w C++ do COMa z pkt. 1. 20%
   (CLSCTX_ALL)
   pliki:

   - klient3.cpp
   - klasa.tlb

4. napisanie klienta w C# NET.Framework do COMa w C++ (klasac.dll) 20%
   klasę należy samodzielnie zarejestrować in-process
   testowanie: test.exe c
   pliki:

   - klient4.cs (do własnej implementacji, powinien zawierać metodę o sygnaturze: 'uint Test(string napis);' wołaną przez test.exe)
   - klasac.dll
   - rej4.reg
   - unrej4.reg

5. implementacja i rejestracja własnej klasy COM, C#, x86 (Klasa2) 20%
   assembly musi być podpisane i zarejestrowane (regasm i gacutil)
   sprawdzić czy zaznaczona jest opcja Make assembly COM-Visible w Assembly Information
   vtestowanie: test.exe 2
   pliki:

   - klasa2.cs (do własnej implementacji)
   - rej5.reg (w przypadku rejestracji ręcznej)
   - unrej5.reg

6. implementacja COMa w C++ (klasac2.dll) 10%
   klasę należy samodzielnie zarejestrować in-process
   pliki:

   - IKlasa2.h
   - klasa2.h
   - fabryka2.h
   - dllmain2.cpp
   - fabryka2.cpp
   - guid_k2.cpp
   - klasa2.cpp
   - IKlasa2.idl
   - klasa2.def
   - rej6.reg
   - unrej6.reg

7. implementacja aplikacji WPF wykorzystującej kontrolki ActiveX 10%
   - wyświetlanie pliku PDF
   - odtwarzanie pliku WAV
   - otwieranie strony WWW

test.exe - test rejestracji COMa w C# (pkt. 1)<br>
test.exe c - test rejestracji COMa w C++ (pkt. 4)<br>
test.exe 2 - test rejestracji COMa w C# (pkt. 5)<br>
test.exe c2 - test rejestracji COMa w C++ (pkt. 6)<br>
