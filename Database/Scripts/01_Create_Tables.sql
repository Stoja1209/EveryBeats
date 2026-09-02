CREATE DATABASE Everybeats;
-- ============================================================
-- USERS TABLE
-- ============================================================
USE Everybeats;
GO
CREATE TABLE Users(
	userID INT IDENTITY(1,1),
	first_name VARCHAR(50) NOT NULL,
	last_name VARCHAR(50) NOT NULL,
	id_number CHAR(13) NOT NULL UNIQUE,
	email VARCHAR(150) NOT NULL UNIQUE,
	cellnumber VARCHAR(20) NOT NULL UNIQUE,
	password_hash VARCHAR(255) NOT NULL,
	race VARCHAR(20) NOT NULL,
	role VARCHAR(20) NOT NULL DEFAULT 'buyer',
	CONSTRAINT CK_Users_Role CHECK (role IN ('buyer', 'producer', 'both', 'admin')),
	created_at DATETIME DEFAULT SYSDATETIME(),
	is_active BIT DEFAULT 1
    PRIMARY KEY(userID)
);

-- ============================================================
-- PRODUCER TABLE
-- ============================================================

CREATE TABLE Producer(
	producer_id INT IDENTITY(1,1),
	userID INT NOT NULL UNIQUE,
	artist_name  VARCHAR(100) NOT NULL,
	PRIMARY KEY(producer_id),
	FOREIGN KEY(userID) REFERENCES Users(userID)
);

-- ============================================================
-- BANKDETAILS TABLE
-- ============================================================

CREATE TABLE BankDetails(
	bank_detail_id INT IDENTITY(1,1),
	producer_id INT NOT NULL UNIQUE,
	account_holder VARCHAR(100) NOT NULL,
	bank_name VARCHAR(100) NOT NULL,
	account_number VARCHAR(30) NOT NULL,
	branch_code VARCHAR(20) NOT NULL,
	PRIMARY KEY(bank_detail_id),
	FOREIGN KEY(producer_id) REFERENCES Producer(producer_id)
);

-- ============================================================
-- GENRE TABLE
-- ============================================================

CREATE TABLE Genre (
    genre_id INT IDENTITY(1,1),
    name VARCHAR(50) NOT NULL UNIQUE,
	PRIMARY KEY(genre_id)
);

-- ============================================================
-- BEAT TABLE
-- ============================================================

CREATE TABLE Beat(
	beat_id INT IDENTITY(1,1),
	producer_id INT NOT NULL,
	genre_id INT NOT NULL,
	title VARCHAR(200) NOT NULL,
	bmp INT,
	music_key VARCHAR(10),
	mp3_file VARCHAR(max) NOT NULL,
	wav_file VARCHAR(max) NOT NULL,
	stems_file VARCHAR(max),
	status VARCHAR NOT NULL DEFAULT 'Available',
	CONSTRAINT CK_Beat_Status CHECK (status IN ('Available', 'Sold', 'Draft')),
	created_at DATETIME DEFAULT SYSDATETIME(),
	PRIMARY KEY(beat_id),
	FOREIGN KEY(producer_id) REFERENCES Producer(producer_id),
	FOREIGN KEY(genre_id) REFERENCES Genre(genre_id)
);
-- ============================================================
-- LICENSE TABLE
-- ============================================================

CREATE TABLE License(
	license_id INT IDENTITY(1,1),
	beat_id INT NOT NULL,
	liscencse_type VARCHAR(20) NOT NULL,
	terms VARCHAR(MAX) NOT NULL,
	price DECIMAL(10,2) NOT NULL,
	created_at DATETIME DEFAULT SYSDATETIME(),
	update_at DATETIME DEFAULT SYSDATETIME(),
	CONSTRAINT Ltype CHECK(liscencse_type IN('lease','exclusive')),
	PRIMARY KEY(license_id),
	FOREIGN KEY (beat_id) REFERENCES Beat(beat_id)
);

-- ============================================================
-- COLLABORATIONS TABLE
-- ============================================================

CREATE TABLE Collaborations (
    collaboration_id  INT IDENTITY(1,1) NOT NULL,
    beat_id INT NOT NULL,
    producer_id INT NOT NULL,
    split_percent DECIMAL(5,2) NOT NULL,
    PRIMARY KEY (collaboration_id, beat_id, producer_id),
    FOREIGN KEY (beat_id) REFERENCES Beat(beat_id),
    FOREIGN KEY (producer_id) REFERENCES Producer(producer_id)
);

-- ============================================================
-- ORDERS TABLE
-- ============================================================

CREATE TABLE Orders(
	order_id INT IDENTITY(1,1) PRIMARY KEY,
	userID INT NOT NULL,
	total_amount DECIMAL(10,2) NOT NULL,
	OrderDate DATETIME DEFAULT SYSDATETIME(),
	status VARCHAR(20) NOT NULL DEFAULT 'completed', 
	CONSTRAINT OD_STATUS CHECK(status IN ('completed','pending', 'failed')),
    FOREIGN KEY (userID) REFERENCES Users(userID)
);

-- ============================================================
-- ORDERITEMS TABLE
-- ============================================================

CREATE TABLE OrderItems (
    order_item_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    beat_id INT NOT NULL,
    license_id INT NOT NULL,
    price_paid DECIMAL(10,2) NOT NULL,
	Order_item_date DATETIME DEFAULT SYSDATETIME(),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    FOREIGN KEY (beat_id) REFERENCES Beat(beat_id),
    FOREIGN KEY (license_id) REFERENCES License(license_id)
);

-- ============================================================
-- AGREEMENT TABLE
-- ============================================================

CREATE TABLE Agreement(
    agreement_id INT IDENTITY(1,1) PRIMARY KEY,
	producer_id INT NOT NULL,
	userID INT NOT NULL,
    order_item_id INT NOT NULL UNIQUE,
    signed_pdf_url VARCHAR(max),
    signed_at DATETIME DEFAULT SYSDATETIME(),
	FOREIGN KEY (producer_id) REFERENCES Producer(producer_id),
	FOREIGN KEY (userID) REFERENCES Users(userID),
    FOREIGN KEY (order_item_id) REFERENCES OrderItems(order_item_id)
);

-- ============================================================
-- PAYOUTS TABLE
-- ============================================================

CREATE TABLE Payouts (
    payout_id INT IDENTITY(1,1) PRIMARY KEY,
    producer_id INT NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    status VARCHAR(20) NOT NULL DEFAULT 'pending',
	CONSTRAINT ST_PAYOUT CHECK(status IN('pending','approved')),
    paid_at DATETIME , 
    FOREIGN KEY (producer_id) REFERENCES Producer(producer_id)
);

-- ============================================================
-- SPLITS TABLE
-- ============================================================

CREATE TABLE Splits (
    split_id INT IDENTITY(1,1) PRIMARY KEY,
    order_item_id INT NOT NULL,
    producer_id  INT NOT NULL,
    payout_id INT,
    percentage DECIMAL(5,2) NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (order_item_id) REFERENCES OrderItems(order_item_id),
    FOREIGN KEY (producer_id) REFERENCES Producer(producer_id),
    FOREIGN KEY (payout_id) REFERENCES Payouts(payout_id)
);

-- ============================================================
-- SHOPPING CART TABLE
-- ============================================================
CREATE TABLE ShoppingCart (
    cart_id INT IDENTITY(1,1) PRIMARY KEY,
    userID INT NOT NULL,
    beat_id INT NOT NULL,
    license_type VARCHAR(20) NOT NULL,
    date_added DATETIME DEFAULT SYSDATETIME(),
    FOREIGN KEY (userID) REFERENCES Users(userID),
    FOREIGN KEY (beat_id) REFERENCES Beat(beat_id)
);

UPDATE Users SET role = 'admin' WHERE email = 'everybeatadmin@gmail.com';
