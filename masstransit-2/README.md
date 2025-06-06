# Task details

Programy powinny wykorzystywać własny deweloperski
serwer założony wcześniej na https://www.cloudamqp.com/
Każdy proces powinien na początku się
przedstawić, czyli wypisać na konsolę kim
jest, np. "nadawca", "odbiorca 2" itp.
W celu poprawienia czytelności, aby odróżnić
komendu wypisywane przez program a treść
odebranych/wysłanych wiadomości, można użyć
klasy ConsoleCol (dostępna na eNauczanie).
Utworzenie szyny korzystającej z RabbitMq:

    var bus = Bus.Factory.CreateUsingRabbitMq(sbc => {
        var host = sbc.Host(new Uri("rabbitmq://<host>/<virtual host>"),
            h => { h.Username("login");
            h.Password("password"); });
    });

W, K, A, B to osobne procesy

1. wydawca wiadomości (W) 10%

- publikuje co sekundę wiadomości typu Publ
  z kolejnymi numerami (1, 2, 3, ...)
- reaguje na polecenia kontrolera

2. kontroler generatora (K) 15%

- po naciśnięciu klawisza s wysyła polecenie uruchomienia
  generatora (polecenie Ustaw { dziala = true } )
- po naciśnięciu klawisza t wysyła polecenie zatrzymania
  generatora (polecenie Ustaw { dziala = false } )

3. dwoje abonentów (A i B) 20%

- odbierają wiadomości od W i wypisują na konsolę
  ich zawartość
- A odpowiada na wiadomości o numerach podzielnych przez 2
  wiadomościami typu OdpA z polem kto = "abonent A"
- B odpowiada na wiadomości o numerach podzielnych przez 3
  wiadomościami typu OdpB z polem kto = "abonent B"
- W wypisuje na konsolę treść odpowiedzi

4. wyjątki 25%

- obsługa odpowiedzi w wydawcy (W)
  rzuca wyjątek z prawdopodobienstwem 1/2
- abonenci są informowani, gdy obsługa ich odpowiedzi
  wywołała wyjątek (wypisują informację o tym fakcie
  na konsolę)
- wydawca w przypadku wyjątku próbuje ponownie obsłużyć
  wiadomość (maksymalnie 3 razy)

5. obserwatory wiadomości 20%

- wydawca po naciśnięciu klawisza s wypisuje
  statystyki odebranych wiadomości:
  - liczba prób obsłużenia komunikatów każdego typu
  - liczba pomyślnie obsłużonych komunikatów każdego typu
  - liczba opublikowanych komunikatów typu

6. szyfrowanie 10%

- komunikacja między K i W jest szyfrowana
  współdzielonym kluczem (numer indeksu powtórzony
  dostatecznie wiele razy);
- wektor inicjalizacyjny powinien być inny
  w każdej wiadomości
