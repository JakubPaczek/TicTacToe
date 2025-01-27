# TicTacToe

Jest to prosta implementacja klasycznej gry w Kółko-krzyżyk z graficznym interfejsem użytkownika (GUI) zbudowana przy użyciu WPF (Windows Presentation Foundation). Gra oferuje kilka trybów, w tym tryb dwuosobowy oraz tryb gry z botem. Bot korzysta z algorytmu minimax do podejmowania decyzji o ruchach.

## Funkcje

- **Tryb dwuosobowy**: Graj z innym graczem na tym samym komputerze.
- **Tryb jednoosobowy**: Graj przeciwko botowi o wybranej trudności.
- **Wykrywanie wyniku gry**: Gra automatycznie wykrywa, czy któryś z graczy wygrał lub czy zakończyła się remisem.
- **Architektura oparta na zdarzeniach**: Gra wykorzystuje zdarzenia i delegaty do obsługi zmian stanu gry, takich jak wygrana gracza czy zakończenie gry remisem.

## Jak grać

1. Rozpocznij grę, wybierając jeden z dwóch trybów: **gra dwuosobowa** lub **gra z botem**.
2. Klikaj na pola na planszy, aby wykonać swój ruch.
3. Gra automatycznie ogłosi wynik – wygrana gracza lub remis.

## Zastosowane zagadnienia

W projekcie zostały zastosowane różne techniki i podejścia:

- **Delegaty i zdarzenia**: Do obsługi zmian stanu gry, takich jak ogłoszenie wygranej gracza lub zakończenie gry.
- **WPF**: Wykorzystane do stworzenia interfejsu użytkownika, umożliwiającego wygodną interakcję z grą.
- **Testowanie z użyciem NUnit**: Zaimplementowano testy jednostkowe w celu zapewnienia poprawności działania logiki gry, w tym detekcji zwycięzcy oraz działania bota.

## Instrukcje uruchomienia

1. Sklonuj repozytorium na swój komputer.
2. Uruchom projekt w Visual Studio lub innym środowisku wspierającym WPF.
3. Gra uruchomi się automatycznie, umożliwiając wybór trybu gry.

Życzę powodzenia w grze! :)
