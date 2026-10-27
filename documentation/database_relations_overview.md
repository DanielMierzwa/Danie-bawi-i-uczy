# Schemat Relacji i Zasad Kaskadowych (`DeleteBehavior`)

> **Dokumentacja Bazy Danych**  
> Zbiór powiązań strukturalnych oraz zachowań bazy danych w momencie usuwania rekordów powiązanych (skonfigurowane w `LearningDbContext`).

---

## 📊 Podsumowanie relacji (Tabela podglądowa)

| Encja źródłowa | Encja docelowa | Typ relacji | Klucz obcy | Zachowanie (`DeleteBehavior`) |
| :--- | :--- | :---: | :--- | :---: |
| **User** | **Notes** | 1 : N | `Note.UserId` $\rightarrow$ `User.Id` | `Cascade` |
| **Subject** | **Notes** | 1 : N (opcjonalna) | `Note.SubjectId` $\rightarrow$ `Subject.Id` | `SetNull` |
| **User** | **Materials** | 1 : N | `Material.UserId` $\rightarrow$ `User.Id` | `Cascade` |
| **Subject** | **Materials** | 1 : N | `Material.SubjectId` $\rightarrow$ `Subject.Id` | `Cascade` |
| **Material** | **Questions** | 1 : N | `Question.MaterialId` $\rightarrow$ `Material.Id` | `Cascade` |
| **Question** | **AnswerOptions** | 1 : N | `AnswerOption.QuestionId` $\rightarrow$ `Question.Id` | `Cascade` |
| **User** | **UserStatistics** | 1 : N | `UserStatistic.UserId` $\rightarrow$ `User.Id` | `Cascade` |
| **User** / **Achievement** | **UserAchievement** | N : N | `UserAchievement.UserId`<br>`UserAchievement.AchievementId` | `Cascade` |

---

## 🛠 Szczegółowy opis relacji

### 1. `User` ➔ `Notes`
- **Typ relacji:** 1 : N
- **Klucz obcy:** `Note.UserId` $\rightarrow$ `User.Id`
- **DeleteBehavior:** `Cascade`
- **Opis:** Usunięcie konta użytkownika powoduje automatyczne usunięcie wszystkich powiązanych z nim notatek (wraz z nagraniami audio).

### 2. `Subject` ➔ `Notes`
- **Typ relacji:** 1 : N (Opcjonalna)
- **Klucz obcy:** `Note.SubjectId` $\rightarrow$ `Subject.Id` *(typ nullable: `int?`)*
- **DeleteBehavior:** `SetNull`
- **Opis:** Usunięcie przedmiotu **nie usuwa** notatek użytkownika – pole `SubjectId` w notatce zostaje automatycznie ustawione na wartość `NULL`.

> [!NOTE]
> Zachowanie `SetNull` pozwala na zachowanie notatek użytkownika nawet po usunięciu przedmiotu, do którego były pierwotnie przypisane.

### 3. `User` ➔ `Materials`
- **Typ relacji:** 1 : N
- **Klucz obcy:** `Material.UserId` $\rightarrow$ `User.Id`
- **DeleteBehavior:** `Cascade`
- **Opis:** Usunięcie użytkownika kasuje stworzone przez niego materiały (quizy i fiszki).

### 4. `Subject` ➔ `Materials`
- **Typ relacji:** 1 : N
- **Klucz obcy:** `Material.SubjectId` $\rightarrow$ `Subject.Id`
- **DeleteBehavior:** `Cascade`
- **Opis:** Usunięcie przedmiotu usuwa powiązane z nim materiały edukacyjne.

### 5. `Material` ➔ `Questions`
- **Typ relacji:** 1 : N
- **Klucz obcy:** `Question.MaterialId` $\rightarrow$ `Material.Id`
- **DeleteBehavior:** `Cascade`
- **Opis:** Usunięcie materiału (np. zestawu pytań) automatycznie usuwa wszystkie należące do niego pytania.

### 6. `Question` ➔ `AnswerOptions`
- **Typ relacji:** 1 : N
- **Klucz obcy:** `AnswerOption.QuestionId` $\rightarrow$ `Question.Id`
- **DeleteBehavior:** `Cascade`
- **Opis:** Usunięcie pytania usuwa powiązane z nim warianty odpowiedzi.

### 7. `User` ➔ `UserStatistics`
- **Typ relacji:** 1 : N
- **Klucz obcy:** `UserStatistic.UserId` $\rightarrow$ `User.Id`
- **DeleteBehavior:** `Cascade`
- **Opis:** Usunięcie użytkownika powoduje usunięcie jego statystyk (czas nauki, skuteczność, wyniki).

### 8. `User` 🟌 `Achievement`
- **Typ relacji:** N : N *(poprzez tabelę łączącą `UserAchievement`)*
- **Klucze obce:**
  - `UserAchievement.UserId` $\rightarrow$ `User.Id` (`Cascade`)
  - `UserAchievement.AchievementId` $\rightarrow$ `Achievement.Id` (`Cascade`)
- **Opis:** Tabela łącząca zarządza odblokowanymi odznakami. Usunięcie użytkownika lub definicji osiągnięcia usuwa odpowiednie powiązania w tabeli łączącej.
