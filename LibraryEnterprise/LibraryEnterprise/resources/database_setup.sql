USE library_database;

DROP TABLE IF EXISTS requests;
DROP TABLE IF EXISTS checkouts;
DROP TABLE IF EXISTS books;
DROP TABLE IF EXISTS genres;
DROP TABLE IF EXISTS employees;
DROP TABLE IF EXISTS patrons;
DROP TABLE IF EXISTS lastaccessed;

CREATE TABLE lastaccessed (
	lastaccessed DATETIME,
	singleton INT
)
INSERT INTO lastaccessed VALUES(CURRENT_TIMESTAMP,0);

CREATE TABLE genres (
	genre_id INT NOT NULL IDENTITY(0,1),
	genre_name VARCHAR(50) NOT NULL UNIQUE,
	PRIMARY KEY (genre_id)
)

CREATE TABLE employees (
	employee_id INT NOT NULL IDENTITY(0,1),
	first_name VARCHAR(50) NOT NULL,
	last_name VARCHAR(50) NOT NULL,
	email VARCHAR(50) NOT NULL,
	password VARCHAR(50) NOT NULL,
	phone VARCHAR(20) NOT NULL,
	address VARCHAR(50) NOT NULL,
	role VARCHAR(20) NOT NULL
	PRIMARY KEY (employee_id)
)

CREATE TABLE patrons (
	patron_id INT NOT NULL IDENTITY(0,1),
	first_name VARCHAR(50) NOT NULL,
	last_name VARCHAR(50) NOT NULL,
	email VARCHAR(50) NOT NULL,
	password VARCHAR(50) NOT NULL,
	phone VARCHAR(20) NOT NULL,
	address VARCHAR(50) NOT NULL,
	account_balance DECIMAL(18,2) NOT NULL
	PRIMARY KEY (patron_id)
)

CREATE TABLE books (
	book_id INT NOT NULL IDENTITY(0,1),
	isbn VARCHAR(20) NOT NULL,
	author VARCHAR(50) NOT NULL,
	title VARCHAR(50) NOT NULL,
	genre_id INT NOT NULL,
	language VARCHAR(50) NOT NULL,
	year INT NOT NULL,
	quantity INT NOT NULL,
	PRIMARY KEY (book_id),
	FOREIGN KEY (genre_id) REFERENCES genres(genre_id)
)

CREATE TABLE checkouts (
	checkout_id INT NOT NULL IDENTITY(0,1),
	patron_id INT NOT NULL,
	book_id INT NOT NULL,
	date_out DATETIME NOT NULL,
	date_in DATETIME,
	PRIMARY KEY (checkout_id),
	FOREIGN KEY (patron_id) REFERENCES patrons(patron_id),
	FOREIGN KEY (book_id) REFERENCES books(book_id),
)

CREATE TABLE requests 
(
	request_id INT NOT NULL IDENTITY(0,1),
	patron_id INT NOT NULL,
	isbn VARCHAR(20) NOT NULL,
	author VARCHAR(50) NOT NULL,
	title VARCHAR(50) NOT NULL,
	genre_id INT NOT NULL,
	language VARCHAR(50) NOT NULL,
	year INT NOT NULL,
	FOREIGN KEY (patron_id) REFERENCES patrons(patron_id),
	FOREIGN KEY (genre_id) REFERENCES genres(genre_id)
)

INSERT INTO genres VALUES
('Action and Adventure'),
('Alternate History'), 
('Anthology'),
('Art'),
('Autobiography'),
('Biography'),
('Book Review'),
('Chick Lit'),
('Children'),
('Comic Book'),
('Coming of Age'),
('Cookbook'),
('Crime'),
('Diary'),
('Dictionary'),
('Drama'),
('Encyclopedia'),
('Fairytale'),
('Fantasy'),
('Health'),
('History'),
('Graphic Novel'),
('Guide'),
('Journal'),
('Historical Fiction'),
('Horror'),
('Math'),
('Memoir'),
('Mystery'),
('Paranormal'),
('Prayer'),
('Picture Book'), 
('Poetry'),
('Political Thriller'),
('Religion, Spirituality, and New Age'),
('Review'),
('Romance'),
('Satire'),
('Science'),
('Science Fiction'),
('Self Help'),
('Short Story'),
('Suspense'),
('Textbook'),
('Travel'),
('Thriller'),
('True Crime'),
('Young Adult');

INSERT INTO employees VALUES
('Shawn', 'Pudjo', 'spudjo@library.ca', 'admin', '(647)666-6666', '123 Fake Street', 'Owner'),
('Scott', 'Ritchie', 'sritchie@library.ca', 'admin', '(647)111-1111', '123 Real Drive', 'Owner'),
('Steven', 'Carino', 'scarino@library.ca', 'admin', '(647)222-2222', '125 Fake Street', 'Owner');

INSERT INTO patrons VALUES
('Sach', 'Dhanota', 'sdha@person.ca', '12345', '(647)565-1212', '123 Stargazer Road', 0),
('Casey', 'Byrne', 'cbyrne@wow.ca', '54321', '(647)123-4567', '123 Moongazer Drive', 500.25);

INSERT INTO books VALUES
( '978067697063', 'Salman Rushdie', 'The Satanic Verses', 18, 'English', '1988', 5),
( '9780393609097', 'Neil Gaiman', 'Norse Mythology', 18, 'English', '2017', 3),
( '9780062316097', 'Yuval Harari', 'Sapiens', 44, 'Hebrew', '2011', 3),
( '9780500251942', 'John Haywood', 'Viking: The Norse Warriors Manual ', 44, 'English', '2013', 2);

INSERT INTO checkouts VALUES
(0, 1, '2019-07-12T09:20:12', '2019-07-31T15:02:44'),
(0, 2, '2019-07-12T09:20:12', NULL),
(1, 0, '2019-07-25T11:42:19', NULL);

INSERT INTO requests VALUES
(1, '9780765311788', 'Brandon Sanderson', 'Mistborn: The Final Empire', 18, 'English', '2006'),
(1, '9780765316882', 'Brandon Sanderson', 'Mistborn: The Well of Ascension', 18, 'English', '2007'),
(1, '9780765316899', 'Brandon Sanderson', 'Mistborn: The Hero of Ages', 18, 'English', '2008');