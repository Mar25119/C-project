Онлайн-кинотеатр

Цель
Создать систему управления онлайн-кинотеатром, позволяющую управлять фильмами, сериалами, пользователями, отзывами и подписками через консольный интерфейс.

Функционал
- Регистрация и вход
- Просмотр контента
- Добавление отзывов
- Подписка
- Поиск по параметрам

Архитектура проекта

Проект реализован так что каждый уровень отвечает за свою задачу:

**[Пользователь]
      ↓
[Program.cs] – консольный интерфейс
      ↓
[UserService, ContentService и т.д.] – бизнес-логика
      ↓
[UserRepository, ContentRepository и т.д.] – работа с БД
      ↓
[OnlineCinema.db] – физическая база данных**

1. Presentation Layer 
- Файл: `Program.cs`
- Отвечает за взаимодействие с пользователем
- Не содержит логики работы с БД

2. Business Logic Layer 
- Папка: `Services/`
- Содержит классы:
  - `UserService`
  - `ContentService`
  - `ReviewService`
  - `SubscriptionService`
- Обрабатывает данные, применяет бизнес-правила

3. Data Access Layer 
- Папка: `Database/`
- Содержит репозитории:
  - `UserRepository`
  - `ContentRepository`
  - `ReviewRepository`
  - `SubscriptionRepository`
- Работает с БД напрямую

4. Models
- Папка: `Models/`
- Содержит 8 классов:
  - `Content`, `Movie`, `TvSeries`
  - `User`, `PremiumUser`
  - `Review`, `Genre`, `Subscription`

  
  _________________________________________________________________________________________
  _________________________________________________________________________________________

  
Диаграммы

Диаграмма классов
![uml1](https://github.com/user-attachments/assets/0b2f14bd-157f-4fe7-a714-2e81f4e63965)



_________________________________________________________________________________________
Диаграмма вариантов использования
![uml2](https://github.com/user-attachments/assets/1c217ba5-7cf4-4475-934a-b6700054d16e)



_________________________________________________________________________________________
Диаграмма последовательностей
![uml3](https://github.com/user-attachments/assets/7e1f48cf-bcba-4da4-8f48-d1ea222d1b2d)



_________________________________________________________________________________________
_________________________________________________________________________________________


 База данных
 
 CREATE TABLE IF NOT EXISTS Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Email TEXT UNIQUE NOT NULL,
    PasswordHash TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Content (
    Id INTEGER PRIMARY KEY,
    Title TEXT NOT NULL,
    Year INTEGER,
    Description TEXT
);

CREATE TABLE IF NOT EXISTS Movies (
    ContentId INTEGER PRIMARY KEY,
    Duration INTEGER,
    Director TEXT,
    FOREIGN KEY(ContentId) REFERENCES Content(Id)
);

CREATE TABLE IF NOT EXISTS Series (
    ContentId INTEGER PRIMARY KEY,
    Seasons INTEGER,
    Episodes INTEGER,
    FOREIGN KEY(ContentId) REFERENCES Content(Id)
);

CREATE TABLE IF NOT EXISTS Reviews (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER,
    ContentId INTEGER,
    Text TEXT,
    Rating INTEGER CHECK(Rating BETWEEN 1 AND 10),
    FOREIGN KEY(UserId) REFERENCES Users(Id),
    FOREIGN KEY(ContentId) REFERENCES Content(Id)
);

CREATE TABLE IF NOT EXISTS Subscriptions (
    UserId INTEGER PRIMARY KEY,
    Type TEXT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME,
    FOREIGN KEY(UserId) REFERENCES Users(Id)
);

CREATE TABLE IF NOT EXISTS Genres (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT UNIQUE NOT NULL
);

CREATE TABLE IF NOT EXISTS MovieGenres (
    MovieId INTEGER,
    GenreId INTEGER,
    PRIMARY KEY(MovieId, GenreId),
    FOREIGN KEY(MovieId) REFERENCES Movies(ContentId),
    FOREIGN KEY(GenreId) REFERENCES Genres(Id)
);


SQL-скрипты находятся в папке `database/`.

 Файлы:
- `create_tables.sql` – создание таблиц и связей
- `insert_sample_data.sql` – начальное заполнение данными

 СУБД
Проект использует SQLite для удобства и простоты разработки.


_________________________________________________________________________________________
_________________________________________________________________________________________

Консольный интерфейс

Консольный интерфейс реализован в файле src/OnlineCinemaApp/Program.cs.

![image](https://github.com/user-attachments/assets/dd0a13e9-71d8-4b43-b604-56a8654a39c7)

![image](https://github.com/user-attachments/assets/bdce4cd1-34ca-4b87-965c-568f1187ff6f)

![image](https://github.com/user-attachments/assets/3c1aff03-962c-4cd6-b416-af0d5804245f)

![image](https://github.com/user-attachments/assets/f03d684a-e49c-40c5-8a06-324775702321)

![image](https://github.com/user-attachments/assets/7e045ced-98de-455f-a9dc-c84b3cc3b9c9)

![image](https://github.com/user-attachments/assets/181e2c69-dd5f-4182-aa13-ea479b4ea522)

![image](https://github.com/user-attachments/assets/41bee817-c79a-4c00-a021-414f8f22eb0e)

![image](https://github.com/user-attachments/assets/3ea65cb4-0d1c-4157-96a7-24fcbb7f8dff)


_________________________________________________________________________________________
_________________________________________________________________________________________


Слой работы с БД

Реализованы классы:
- `DbContext` – управление подключением
- `UserRepository` – регистрация и вход
- `MovieRepository` – просмотр фильмов

_________________________________________________________________________________________
_________________________________________________________________________________________


Бизнес-логика и ООП

Классы:
1. `Content` – абстрактный класс для контента
2. `Movie` – наследник `Content`
3. `TvSeries` – наследник `Content`
4. `User` – базовый пользователь
5. `PremiumUser` – наследник `User`
6. `Review` – отзывы
7. `Subscription` – подписки
8. `Genre` – жанры

Использованные механизмы ООП:
- Наследование: `Movie : Content`, `PremiumUser : User`
- Полиморфизм: переопределение метода `DisplayInfo()`
- Инкапсуляция: закрытие внутренних данных через свойства и приватные поля

_________________________________________________________________________________________
_________________________________________________________________________________________

ООП: Наследование, инкапсуляция, абстракция, полиморфизм

1. Наследование
2. 
Где реализовано:

Movie : Content

TvSeries : Content

PremiumUser : User


Пример:
_________________________________________________________________________________________
public abstract class Content { /* ... */ }

public class Movie : Content { /* наследует свойства и методы Content */ }
public class TvSeries : Content { /* наследует свойства и методы Content */ }

public class User { /* ... */ }

public class PremiumUser : User { /* расширяет функционал User */ }
_________________________________________________________________________________________

Зачем:
Объединяет общие свойства и методы у фильмов и сериалов.

Позволяет создать общий список List<Content> для работы с разными типами контента.

Расширяет базовый класс User дополнительным функционалом в PremiumUser.


2. Инкапсуляция
Где реализована:
В каждом классе: приватные поля + публичные свойства и методы.
Пример:
_________________________________________________________________________________________
public class Movie : Content
{
    private string director;
    public string Director
    {
        get => director;
        set => director = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Duration { get; set; }
}
_________________________________________________________________________________________

Зачем:
Скрытие внутренней реализации.

Контроль доступа к данным (например, запрет null).

Защита целостности объекта.


3. Абстракция 
Где реализована:
В абстрактном классе Content, который содержит только общую структуру контента.
Пример:
_________________________________________________________________________________________

public abstract class Content
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }

    public abstract void DisplayInfo();
}
_________________________________________________________________________________________

Зачем:
Скрытие деталей реализации от пользователя.

Выделение общих черт между различными типами данных.

Предоставление интерфейса взаимодействия без знания внутреннего устройства.


4. Полиморфизм 
Где реализован:

Метод DisplayInfo() переопределяется в Movie и TvSeries.

Пример:
_________________________________________________________________________________________
public override void DisplayInfo()
{
    Console.WriteLine($"Фильм: {Title} ({Year}), реж. {Director}, {Duration} мин.");
}

public override void DisplayInfo()
{
    Console.WriteLine($"Сериал: {Title} ({Year}), {Seasons} сезонов, {Episodes} эпизодов");
}
_________________________________________________________________________________________

Зачем:
Возможность работать с разными типами через один интерфейс.

Упрощает код и делает его гибким к изменениям.




