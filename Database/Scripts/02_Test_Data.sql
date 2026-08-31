-- ============================================================
-- EVERYBEATS – TEST DATA INSERTION
-- ============================================================

USE Everybeats;
GO

-- ============================================================
-- 1. INSERT USERS
-- ============================================================
INSERT INTO Users (first_name, last_name, id_number, email, cellnumber, password_hash, race, role)
VALUES
    ('Sarah', 'Smith', '8001015009088', 'sarah@email.com', '0821111111', 'hashed_password_1', 'White', 'producer'),
    ('John', 'Doe', '9002025109088', 'john@email.com', '0822222222', 'hashed_password_2', 'Black', 'producer'),
    ('Mike', 'Brown', '9503035209088', 'mike@email.com', '0823333333', 'hashed_password_3', 'Coloured', 'producer'),
    ('Lisa', 'Chen', '9604045309088', 'lisa@email.com', '0824444444', 'hashed_password_4', 'Asian', 'buyer'),
    ('David', 'Khumalo', '9705055409088', 'david@email.com', '0825555555', 'hashed_password_5', 'Black', 'buyer'),
    ('Admin', 'User', '9806065509088', 'admin@everybeats.com', '0826666666', 'hashed_password_6', 'Other', 'admin');
GO

-- ============================================================
-- 2. INSERT PRODUCERS
-- ============================================================
INSERT INTO Producer (userID, artist_name)
VALUES
    (1, 'Sarah Beats'),
    (2, 'John Tracks'),
    (3, 'Mike Melodies');
GO

-- ============================================================
-- 3. INSERT BANK DETAILS
-- ============================================================
INSERT INTO BankDetails (producer_id, account_holder, bank_name, account_number, branch_code)
VALUES
    (1, 'Sarah Smith', 'FNB', '1234567890', '250655'),
    (2, 'John Doe', 'Standard Bank', '0987654321', '051001'),
    (3, 'Mike Brown', 'Capitec', '1122334455', '470010');
GO

-- ============================================================
-- 4. INSERT GENRES
-- ============================================================
INSERT INTO Genre (name)
VALUES
    ('Hip-Hop'),
    ('Trap'),
    ('R&B'),
    ('Pop'),
    ('Electronic'),
    ('Jazz');
GO

-- ============================================================
-- 5. INSERT BEATS
-- ============================================================
INSERT INTO Beat (producer_id, genre_id, title, bmp, music_key, mp3_file, wav_file, stems_file, status)
VALUES
    (1, 1, 'Summer Vibes', 120, 'C#', '/beats/1/summer_vibes.mp3', '/beats/1/summer_vibes.wav', '/beats/1/summer_vibes_stems.zip', 'Available'),
    (1, 2, 'Night Beats', 140, 'Fm', '/beats/2/night_beats.mp3', '/beats/2/night_beats.wav', '/beats/2/night_beats_stems.zip', 'Available'),
    (2, 1, 'Old School Flow', 90, 'G', '/beats/3/old_school_flow.mp3', '/beats/3/old_school_flow.wav', NULL, 'Available'),
    (3, 3, 'Melodic Dream', 110, 'Am', '/beats/4/melodic_dream.mp3', '/beats/4/melodic_dream.wav', '/beats/4/melodic_dream_stems.zip', 'Available'),
    (2, 4, 'Pop Anthem', 128, 'Eb', '/beats/5/pop_anthem.mp3', '/beats/5/pop_anthem.wav', NULL, 'Available');
GO

-- ============================================================
-- 6. INSERT LICENSES
-- ============================================================
INSERT INTO License (beat_id, liscencse_type, terms, price)
VALUES
    (6, 'lease', 'Non-exclusive, 10,000 streams limit, 2-year term', 150.00),
    (6, 'exclusive', 'Full ownership, unlimited streams, perpetual', 500.00),
    (2, 'lease', 'Non-exclusive, 50,000 streams limit, 2-year term', 200.00),
    (2, 'exclusive', 'Full ownership, unlimited streams, perpetual', 600.00),
    (3, 'lease', 'Non-exclusive, 25,000 streams limit, 2-year term', 120.00),
    (4, 'lease', 'Non-exclusive, 100,000 streams limit, 2-year term', 250.00),
    (4, 'exclusive', 'Full ownership, unlimited streams, perpetual', 700.00),
    (5, 'lease', 'Non-exclusive, 15,000 streams limit, 2-year term', 100.00);
GO

-- ============================================================
-- 7. INSERT COLLABORATIONS
-- ============================================================
INSERT INTO Collaborations (beat_id, producer_id, split_percent)
VALUES
    (6, 1, 50.00),
    (6, 2, 30.00),
    (6, 3, 20.00);
GO

-- ============================================================
-- 8. INSERT ORDERS
-- ============================================================
INSERT INTO Orders (userID, total_amount, status)
VALUES
    (4, 500.00, 'completed'),
    (5, 200.00, 'completed');
GO

-- ============================================================
-- 9. INSERT ORDER ITEMS
-- ============================================================
INSERT INTO OrderItems (order_id, beat_id, license_id, price_paid)
VALUES
    (1, 6, 2, 500.00),  -- Lisa buys Exclusive license for Summer Vibes
    (2, 2, 3, 200.00);  -- David buys Lease license for Night Beats
GO

-- ============================================================
-- 10. INSERT AGREEMENTS
-- ============================================================
INSERT INTO Agreement (producer_id, userID, order_item_id, signed_pdf_url)
VALUES
    (1, 4, 1, '/agreements/agreement_1.pdf'),
    (1, 5, 2, '/agreements/agreement_2.pdf');
GO

-- ============================================================
-- 11. INSERT PAYOUTS
-- ============================================================
INSERT INTO Payouts (producer_id, amount, status, paid_at)
VALUES
    (1, 450.00, 'approved', '2026-08-30 14:00:00'),
    (2, 0.00, 'pending', NULL),
    (3, 0.00, 'pending', NULL);
GO

-- ============================================================
-- 12. INSERT SPLITS
-- ============================================================
INSERT INTO Splits (order_item_id, producer_id, payout_id, percentage, amount)
VALUES
    -- Splits for Summer Vibes (Order Item 1, Producer Payout = 450)
    (1, 1, 1, 50.00, 225.00),
    (1, 2, NULL, 30.00, 135.00),
    (1, 3, NULL, 20.00, 90.00),
    -- Splits for Night Beats (Order Item 2, Producer Payout = 180)
    (2, 1, NULL, 100.00, 180.00);
GO

-- ============================================================
-- 13. INSERT SHOPPING CART
-- ============================================================
INSERT INTO ShoppingCart (userID, beat_id, license_type)
VALUES
    (5, 3, 'lease'),
    (5, 4, 'exclusive');
GO