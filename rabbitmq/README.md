# Task details

Programy mogą wykorzystywać własny deweloperski
serwer założony wcześniej na https://www.cloudamqp.com/,
lokalnie zainstalowany serwer albo uruchomiony
lokalnie w postaci kontenera Docker w wersji 4.X.X.
Zadanie MUSI być zrealizowane w oparciu o:

1. .NET >= "8.0"
2. RabbitMQ.Client Version >="7.1.2"
3. RabbitMQ Version >= "4.0.9"
   Każdy proces powinien na początku się
   przedstawić, czyli wypisać na konsolę kim
   jest, np. "nadawca", "odbiorca 2" itp.
   W celu poprawienia czytelności, aby odróżnić
   komendu wypisywane przez program a treść
   odebranych/wysłanych wiadomości, można użyć
   klasy ConsoleCol (dostępna na eNauczanie).

4. nadawca 20%
   wysłanie 10 wiadomości
   ze zmieniającą się treścią

5. odbiorca 15%
   wypisanie na konsolę treści
   wiadomości odebranych od nadawcy

6. nagłówki 10%
   ustawienie w nadawcy i wypisanie
   w odbiorcy dwóch różnych nagłówków
   w każdej wiadomości

7. drugi odbiorca 10%
   konkuruje z pierwszym o wiadomości
   (nadawca wysyła 10 wiadomości,
   pierwszy odbiera np. 4, drugi 6)

8. potwierdzenia 15%
   odbiorcy potwierdzają każdą wiadomość
   po 2 sekundach; nie powinni
   otrzymać kolejnej wiadomości, póki nie
   potwierdzą poprzedniej (najpierw uruchamiamy nadawcę,
   który wysyła wszystkie wiadomości,
   później jednego odbiorcę,
   który pobiera początkowo tylko jedną wiadomość,
   dopiero po kilku sekundach uruchamiamy drugiego odbiorcę,
   który powinien pobrać kolejną wiadomość z kolejki)

9. odpowiedzi 10%
   drugi odbiorca odpowiada na
   wiadomości; nadawca wypisuje na
   konsolę treść odpowiedzi

10. publish/subscribe 20%
    1 wydawca, 2 abonentów
    wydawca wysyła 10 wiadomości na
    kanałach abc.def i abc.xyz (na przemian)
    pierwszy abonent odbiera z kanałów
    zaczynających się na abc, drugi odbiera
    z kanałów kończących się na xyz
    (wiadomości z kanału abc.xyz powinny
    docierać do obu abonentów).
