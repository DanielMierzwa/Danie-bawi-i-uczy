# Dokumentacja Struktury i Kolumn Tabel Bazy Danych (`LearningDbContext`)

> **Dokumentacja Bazy Danych**  
> Opis encji (modeli) oraz struktury tabel bazy danych stworzonej na potrzeby aplikacji edukacyjnej.

---

## 📊 Podsumowanie tabel bazy danych

| Nazwa tabeli | Opis | Liczba kolumn | Klucz główny (PK) | Klucze obce (FK) |
| :--- | :--- | :---: | :--- | :--- |
| **Users** | Użytkownicy i uprawnienia | 5 | `Id` | — |
| **Subjects** | Słownik przedmiotów / kategorii | 2 | `Id` | — |
| **Notes** | Notatki (Markdown, Audio) | 7 | `Id` | `UserId`, `SubjectId` |
| **Materials** | Materiały (Quizy, Fiszki) | 6 | `Id` | `UserId`, `SubjectId` |
| **Questions** | Pytania do materiałów | 5 | `Id` | `MaterialId` |
| **AnswerOptions** | Warianty odpowiedzi do pytań | 4 | `Id` | `QuestionId` |
| **UserStatistics** | Statystyki aktywności i postępów | 6 | `Id` | `UserId` |
| **Achievements** | Słownik odznak i osiągnięć | 3 | `Id` | — |
| **UserAchievements** | Relacja N:N (Odblokowane odznaki) | 4 | `Id` | `UserId`, `AchievementId` |

---

## 🛠 Szczegółowy opis tabel

---

### 1. Tabela: `Users` (Użytkownicy)

Przechowuje dane uwierzytelniające oraz uprawnienia użytkowników systemu.

| Kolumna | Typ danych | Atrybuty / Właściwości | Opis |
| :--- | :--- | :--- | :--- |
| `Id` | `int` | `Primary Key`, `Identity` | Unikalny identyfikator użytkownika. |
| `Login` | `string` | `MaxLength: 100`, `Required` | Unikalna nazwa użytkownika. |
| `PasswordHash` | `string` | `Required` | Zaszyfrowane hasło użytkownika. |
| `Role` | `string` | `Required`, `Domyślnie: "User"` | Rola w systemie (`"Admin"` lub `"User"`). |
| `IsPremium` | `bool` | `Domyślnie: false` | Flaga określająca status konta premium. |

- **Powiązania nawigacyjne:** Kolekcje `Notes`, `Materials`, `UserStatistics` oraz `UserAchievements`.

---

### 2. Tabela: `Subjects` (Przedmioty)

Słownik przedmiotów szkolnych / kategorii (np. Matematyka, Fizyka, Chemia).

| Kolumna | Typ danych | Atrybuty / Właściwości | Opis |
| :--- | :--- | :--- | :--- |
| `Id` | `int` | `Primary Key`, `Identity` | Unikalny identyfikator przedmiotu. |
| `Name` | `string` | `MaxLength: 100`, `Required` | Nazwa przedmiotu. |

- **Powiązania nawigacyjne:** Kolekcje `Materials` oraz `Notes`.

---

### 3. Tabela: `Notes` (Notatki)

Notatki tworzone przez użytkowników, wspierające formatowanie Markdown oraz nagrania audio.

| Kolumna | Typ danych | Atrybuty / Właściwości | Opis |
| :--- | :--- | :--- | :--- |
| `Id` | `int` | `Primary Key`, `Identity` | Unikalny identyfikator notatki. |
| `Title` | `string` | `MaxLength: 200`, `Required` | Tytuł notatki. |
| `Content` | `string` | — | Treść zapisana w formacie `.md` (Markdown). |
| `AudioFilePath` | `string` | `Nullable` | Ścieżka do powiązanego pliku audio / nagrania. |
| `CreatedAt` | `DateTime` | `Domyślnie: DateTime.Now` | Data i czas utworzenia. |
| `UserId` | `int` | `Foreign Key -> Users.Id` | Klucz obcy autora notatki. |
| `SubjectId` | `int?` | `Foreign Key -> Subjects.Id`, `Nullable` | Klucz obcy przedmiotu. |

---

### 4. Tabela: `Materials` (Materiały / Quizy / Fiszki)

Główne jednostki edukacyjne (zestawy quizowe lub fiszki).

| Kolumna | Typ danych | Atrybuty / Właściwości | Opis |
| :--- | :--- | :--- | :--- |
| `Id` | `int` | `Primary Key`, `Identity` | Unikalny identyfikator materiału. |
| `Title` | `string` | `MaxLength: 200`, `Required` | Tytuł materiału. |
| `Type` | `string` | `Required` | Typ materiału (`"Quiz"` lub `"Flashcard"`). |
| `IsPublic` | `bool` | `Domyślnie: false` | Status widoczności (`false` = prywatny, `true` = publiczny). |
| `UserId` | `int` | `Foreign Key -> Users.Id` | Klucz obcy twórcy materiału. |
| `SubjectId` | `int` | `Foreign Key -> Subjects.Id` | Klucz obcy powiązanego przedmiotu. |

- **Powiązania nawigacyjne:** Kolekcja `Questions`.

---

### 5. Tabela: `Questions` (Pytania)

Pytania powiązane z materiałami (obsługujące różne tryby odpowiedzi).

| Kolumna | Typ danych | Atrybuty / Właściwości | Opis |
| :--- | :--- | :--- | :--- |
| `Id` | `int` | `Primary Key`, `Identity` | Unikalny identyfikator pytania. |
| `QuestionText` | `string` | `Required` | Treść pytania lub awers (przód) fiszki. |
| `QuestionType` | `string` | `Required`, `Domyślnie: "Closed"` | Typ pytania (`"Closed"`, `"Open"`, `"Flashcard"`). |
| `CorrectAnswerText` | `string` | `Nullable` | Poprawna odpowiedź tekstowa (dla pytań otwartych / rewersu fiszki). |
| `MaterialId` | `int` | `Foreign Key -> Materials.Id` | Klucz obcy materiału nadrzędnego. |

- **Powiązania nawigacyjne:** Kolekcja wariantów odpowiedzi (`AnswerOptions`).

---

### 6. Tabela: `AnswerOptions` (Opcje odpowiedzi)

Warianty odpowiedzi do pytań zamkniętych.

| Kolumna | Typ danych | Atrybuty / Właściwości | Opis |
| :--- | :--- | :--- | :--- |
| `Id` | `int` | `Primary Key`, `Identity` | Unikalny identyfikator opcji. |
| `OptionText` | `string` | `Required` | Treść wariantu odpowiedzi. |
| `IsCorrect` | `bool` | `Domyślnie: false` | Flaga wskazująca, czy odpowiedź jest poprawna. |
| `QuestionId` | `int` | `Foreign Key -> Questions.Id` | Klucz obcy powiązanego pytania. |

---

### 7. Tabela: `UserStatistics` (Statystyki użytkownika)

Agregowane dane o aktywności i postępach edukacyjnych użytkownika.

| Kolumna | Typ danych | Atrybuty / Właściwości | Opis |
| :--- | :--- | :--- | :--- |
| `Id` | `int` | `Primary Key`, `Identity` | Unikalny identyfikator wpisu statystyk. |
| `StudyTimeMinutes` | `int` | — | Łączny czas nauki w minutach. |
| `CompletedQuizzesCount` | `int` | — | Liczba ukończonych quizów. |
| `RepeatedFlashcardsCount` | `int` | — | Liczba powtórzonych fiszek. |
| `SuccessRate` | `double` | — | Ogólny wskaźnik skuteczności w procentach (%). |
| `UserId` | `int` | `Foreign Key -> Users.Id` | Klucz obcy użytkownika. |

---

### 8. Tabela: `Achievements` (Osiągnięcia)

Słownik odznak i osiągnięć, jakie użytkownik może zdobyć w aplikacji.

| Kolumna | Typ danych | Atrybuty / Właściwości | Opis |
| :--- | :--- | :--- | :--- |
| `Id` | `int` | `Primary Key`, `Identity` | Unikalny identyfikator osiągnięcia. |
| `Name` | `string` | `Required` | Nazwa osiągnięcia. |
| `Description` | `string` | — | Opis warunków odblokowania odznaki. |

- **Powiązania nawigacyjne:** Tabela łącząca `UserAchievements`.

---

### 9. Tabela Łącząca: `UserAchievements` (Zdobyte osiągnięcia)

Realizuje relację wiele-do-wielu (N:N) pomiędzy użytkownikami a osiągnięciami.

| Kolumna | Typ danych | Atrybuty / Właściwości | Opis |
| :--- | :--- | :--- | :--- |
| `Id` | `int` | `Primary Key`, `Identity` | Unikalny identyfikator powiązania. |
| `UnlockedAt` | `DateTime` | `Domyślnie: DateTime.Now` | Data i czas odblokowania odznaki. |
| `UserId` | `int` | `Foreign Key -> Users.Id` | Klucz obcy użytkownika. |
| `AchievementId` | `int` | `Foreign Key -> Achievements.Id` | Klucz obcy osiągnięcia. |
