# Task details

Programy powinny wykorzystywać własny deweloperski serwer założony wcześniej na https://www.cloudamqp.com/
Każdy proces powinien na początku się przedstawić, czyli wypisać na konsolę, kim jest, np. "nadawca", "odbiorca 2" itp.
W celu poprawienia czytelności, aby odróżnić komendy wypisywane przez program od treści odebranych/wysłanych wiadomości, można użyć klasy ConsoleCol (dostępna na eNauczanie).

Utworzenie szyny korzystającej z RabbitMQ:

    var bus = Bus.Factory.CreateUsingRabbitMq(sbc => {
        var host = sbc.Host(new Uri("rabbitmq://<host>/<virtual host>"),
            h => {<br>
                h.Username("login");<br>
                h.Password("password");<br>
            });<br>
    });<br>

Procesy: sklep, klient A, klient B, magazyn

Wiadomości (można dodawać dodatkowe pola):

    StartZamówienia { int ilosc; } — klient → sklep

    PytanieoPotwierdzenie { int ilosc; } — sklep → klient

    Potwierdzenie — klient → sklep

    BrakPotwierdzenia — klient → sklep

    PytanieoWolne { int ilosc; } — sklep → magazyn

    OdpowiedzWolne — magazyn → sklep

    OdpowiedzWolneNegatywna — magazyn → sklep

    AkceptacjaZamówienia { int ilosc; } — sklep → klient

    OdrzucenieZamówienia { int ilosc; } — sklep → klient

Proces: Sklep (40% + 10% za timeout)

    Przechowuje i przetwarza sagi opisujące proces zamówienia

    Wysyła do magazynu pytanie o potwierdzenie zamówienia (wraz z ilością)

    Wysyła do klienta pytanie o potwierdzenie zamówienia (wraz z ilością)

    Odbiera potwierdzenia od magazynu

    Odbiera potwierdzenia od klienta

    Kolejność odbierania potwierdzeń nie powinna mieć znaczenia

    Jeżeli obie strony potwierdzą, transakcja kończy się sukcesem i wysyłane jest do klienta potwierdzenie

    W przeciwnym wypadku wysyłany jest do klienta brak potwierdzenia

    Timeout: zamówienie niepotwierdzone w ciągu 10 sekund jest anulowane

Procesy: Klient A i Klient B (20%)

    Wysyłają zamówienie; zamówienie zawiera ilość (liczba pobierana z klawiatury)

    Odbierają pytania o potwierdzenie od sklepu i potwierdzają lub nie (brak potwierdzenia również sygnalizowany wiadomością) w zależności od naciśniętego klawisza

    Odbierają wiadomości potwierdzające (lub niepotwierdzające) zakup od sklepu

Proces: Magazyn (30%)

    Przechowuje stan magazynu (liczba całkowita, początkowo 0); stan dzieli się na jednostki wolne i zarezerwowane

    Wyświetla na konsoli stan magazynu (wolne i zarezerwowane osobno)

    Wpisanie liczby zwiększa stan magazynu (wolne) o podaną wartość

    Odbiera pytania o potwierdzenie od sklepu i potwierdza lub nie (brak potwierdzenia również sygnalizowany wiadomością)

    Odpowiedź jest twierdząca, jeżeli liczba wolnych jednostek jest co najmniej taka jak ilość w zamówieniu

    W przeciwnym wypadku odpowiedź jest negatywna

    Potwierdzenie pytania rezerwuje ilość jednostek (przenosi je z wolnych na zarezerwowane)

    Akceptacja zamówienia usuwa zarezerwowane jednostki

    Odrzucenie zamówienia przenosi zarezerwowane jednostki do wolnych
