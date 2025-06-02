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

    var bus = Bus.Factory.CreateUsingRabbitMq(sbc => {<br>
        var host = sbc.Host(new Uri("rabbitmq://<host>/<virtual host>"),<br>
            h => { h.Username("login");<br>
            h.Password("password"); });<br>
    });

1. wydawca (W) 20%
   publikuje 10 wiadomości ze zmieniającą się treścią

2. odbiorca (A) 15%
   wypisuje na konsolę treść odebranych wiadomości

3. nagłówki 10%
   ustawienie w nadawcy i wypisanie w odbiorcy dwóch różnych nagłówków (w każdej wiadomości)

4. odbiorca (B) 15%
   wypisuje na konsolę treść odebranych wiadomości; wyświetla także liczbę odebranych wiadomości (licznik powinien
   być polem klasy obsługującej komunikaty i nie może być statyczny)

5. odbiorca (C) i drugi typ wiadomości 20%
   wydawca publikuje dodatkowo wiadomości drugiego typu; odbiorca C wyświetla ich treść na konsolę; wiadomości są interfejsami

6. wersjonowanie 20%
   wydawca publikuje trzy typy wiadomości; trzeci typ dziedziczy po pierwszych
   dwóch (z pkt. 1 i 5.); wiadomości te odbiera odbiorca B
