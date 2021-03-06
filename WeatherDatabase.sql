CREATE DATABASE Weather

Drop database Weather
CREATE TABLE Users (
Username VARCHAR (50)NOT NULL PRIMARY KEY,
UserType VARCHAR (50)NOT NULL,
Password VARCHAR (50)NOT NULL,
);

CREATE TABLE Weather (
ID INT NOT NULL IDENTITY(100,1) PRIMARY KEY,
CityName VARCHAR(50) NOT NULL,
Date Date NOT NULL,
MinTemp INT NOT NULL,
MaxTemp INT NOT NULL,
Precipitation INT NOT NULL,
Humidity INT NOT NULL,
WindSpeed INT NOT NULL,
);


CREATE TABLE FavoriteCities (
ID INT NOT NULL IDENTITY(100,1) PRIMARY KEY,
CityName VARCHAR(50) NOT NULL,
Date Date NOT NULL,
MinTemp INT NOT NULL,
MaxTemp INT NOT NULL,
Precipitation INT NOT NULL,
Humidity INT NOT NULL,
WindSpeed INT NOT NULL,
Username VARCHAR (50) NOT NULL,
FOREIGN KEY (Username) REFERENCES Users (Username)
);

INSERT INTO Users VALUES
('cedy','Admin','123'),
('Wishiya','General User','123');

INSERT INTO Weather VALUES
('Durban','2020-07-27 10:34:09 AM',30,45,56,67,78);

INSERT INTO FavoriteCities VALUES
('Durban','2020-07-27 10:34:09 AM',30,45,56,67,78,'Esther'),
('Durban','2020-07-27 10:34:09 AM',30,45,56,67,78,'Cedric');

Select*from Users
Select*from Weather
select * from FavoriteCities

drop table FavoriteCities