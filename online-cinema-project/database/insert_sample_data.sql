-- database/insert_sample_data.sql

INSERT INTO Users (Name, Email, PasswordHash) VALUES ('Иван Иванов', 'ivan@example.com', 'hash1');
INSERT INTO Users (Name, Email, PasswordHash) VALUES ('Мария Петрова', 'maria@example.com', 'hash2');

INSERT INTO Content (Title, Year, Description) VALUES ('Терминатор', 1984, 'Классический боевик');
INSERT INTO Content (Title, Year, Description) VALUES ('Игра престолов', 2011, 'Фэнтези от HBO');

INSERT INTO Movies (ContentId, Duration, Director) VALUES (1, 107, 'Джеймс Кэмерон');
INSERT INTO TvSeries (ContentId, Seasons, Episodes) VALUES (2, 8, 73);

INSERT INTO Genres (Name) VALUES ('Боевик'), ('Фантастика'), ('Фэнтези');

INSERT INTO MovieGenres (MovieId, GenreId) VALUES (1, 1), (1, 2);

INSERT INTO Subscriptions (UserId, Type, StartDate, EndDate) 
VALUES (1, 'Premium', '2025-05-01', '2025-06-01');

INSERT INTO Reviews (UserId, ContentId, Text, Rating) VALUES (1, 1, 'Отличный фильм!', 9);
INSERT INTO WatchHistory (UserId, ContentId, Progress) VALUES (1, 1, 100);