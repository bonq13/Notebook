# Notatnik użytkowników – ASP.NET Core MVC + EF Core + SQLite


## Funkcje

- Pełny CRUD użytkowników
- Dynamiczne dodatkowe atrybuty (dowolna liczba, różne dla każdej osoby)
- Walidacja + ochrona CSRF
- Raport CSV z kolumnami: Tytuł (Pan/Pani) · Imię · Nazwisko · Data urodzenia · Wiek · Płeć
- Nazwa pliku raportu: `20251123_221045.csv` (data do sekund)
- Czysty, polski interfejs (Bootstrap 5)
- Baza SQLite – działa od razu po `git clone`

## Technologie

- .NET 8 MVC
- Entity Framework Core 8 + SQLite
- Razor Views + Partial Views
- jQuery Validation + dynamiczne pola JavaScript
- Bootstrap 5

## Jak uruchomić 

```bash
git clone https://github.com/TwójLogin/Notebook.git
cd Notebook
dotnet run