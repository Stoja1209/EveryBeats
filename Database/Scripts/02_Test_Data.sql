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
    (1, 1, 'Summer Vibes', 120, 'C#', '/beats/1/summer_vibes.mp3', '/beats/1/summer_vibes.wav', '/beats/1/summer_vibes_stems.zip', 1),
    (1, 2, 'Night Beats', 140, 'Fm', '/beats/2/night_beats.mp3', '/beats/2/night_beats.wav', '/beats/2/night_beats_stems.zip', 1),
    (2, 1, 'Old School Flow', 90, 'G', '/beats/3/old_school_flow.mp3', '/beats/3/old_school_flow.wav', NULL, 1),
    (3, 3, 'Melodic Dream', 110, 'Am', '/beats/4/melodic_dream.mp3', '/beats/4/melodic_dream.wav', '/beats/4/melodic_dream_stems.zip', 1),
    (2, 4, 'Pop Anthem', 128, 'Eb', '/beats/5/pop_anthem.mp3', '/beats/5/pop_anthem.wav', NULL, 1),
	(1, 1, 'Midnight Hustle', 130, 'Am', '/beats/6/midnight_hustle.mp3', '/beats/6/midnight_hustle.wav', '/beats/6/midnight_hustle_stems.zip', 1),
    (1, 2, 'Street Anthem', 145, 'Cm', '/beats/7/street_anthem.mp3', '/beats/7/street_anthem.wav', NULL, 1),
    (1, 3, 'Smooth Nights', 90, 'Dm', '/beats/8/smooth_nights.mp3', '/beats/8/smooth_nights.wav', '/beats/8/smooth_nights_stems.zip', 1),
    (2, 1, 'Boom Bap Classic', 95, 'Gm', '/beats/9/boom_bap_classic.mp3', '/beats/9/boom_bap_classic.wav', NULL, 1),
    (2, 4, 'Summer Party', 125, 'C', '/beats/10/summer_party.mp3', '/beats/10/summer_party.wav', '/beats/10/summer_party_stems.zip', 1),
    (2, 5, 'Electric Wave', 128, 'F#m', '/beats/11/electric_wave.mp3', '/beats/11/electric_wave.wav', NULL, 1),
    (3, 3, 'Love Story', 85, 'A', '/beats/12/love_story.mp3', '/beats/12/love_story.wav', '/beats/12/love_story_stems.zip', 1),
    (3, 6, 'Jazz Lounge', 100, 'Bb', '/beats/13/jazz_lounge.mp3', '/beats/13/jazz_lounge.wav', NULL, 1),
    (3, 1, 'Trap Nation', 150, 'Em', '/beats/14/trap_nation.mp3', '/beats/14/trap_nation.wav', '/beats/14/trap_nation_stems.zip', 1),
    (1, 4, 'Pop Sensation', 120, 'Db', '/beats/15/pop_sensation.mp3', '/beats/15/pop_sensation.wav', NULL, 1),
    (2, 2, 'Dark Alley', 140, 'Bm', '/beats/16/dark_alley.mp3', '/beats/16/dark_alley.wav', '/beats/16/dark_alley_stems.zip', 1),
    (3, 5, 'Neon Lights', 122, 'Ab', '/beats/17/neon_lights.mp3', '/beats/17/neon_lights.wav', NULL, 1),
    (1, 6, 'Coffee Shop', 80, 'F', '/beats/18/coffee_shop.mp3', '/beats/18/coffee_shop.wav', '/beats/18/coffee_shop_stems.zip', 1),
    (2, 3, 'Slow Jam', 75, 'G', '/beats/19/slow_jam.mp3', '/beats/19/slow_jam.wav', NULL, 1),
    (3, 1, 'Hip Hop Legacy', 92, 'D', '/beats/20/hip_hop_legacy.mp3', '/beats/20/hip_hop_legacy.wav', '/beats/20/hip_hop_legacy_stems.zip', 1);
GO

-- ============================================================
-- 6. INSERT LICENSES
-- ============================================================
INSERT INTO License (beat_id, liscencse_type, terms, price)
VALUES
    (6, 'lease', 'Non-exclusive, 10,000 streams limit, 2-year term', 150.00,1),
    (6, 'exclusive', 'Full ownership, unlimited streams, perpetual', 500.00,1),
    (2, 'lease', 'Non-exclusive, 50,000 streams limit, 2-year term', 200.00,1),
    (2, 'exclusive', 'Full ownership, unlimited streams, perpetual', 600.00,1),
    (3, 'lease', 'Non-exclusive, 25,000 streams limit, 2-year term', 120.00,1),
    (4, 'lease', 'Non-exclusive, 100,000 streams limit, 2-year term', 250.00,1),
    (4, 'exclusive', 'Full ownership, unlimited streams, perpetual', 700.00,1),
    (5, 'lease', 'Non-exclusive, 15,000 streams limit, 2-year term', 100.00,1),
	(6, 'lease', 'Non-exclusive, 10,000 streams limit, 2-year term', 120.00, 1),
    (6, 'exclusive', 'Full ownership, unlimited streams, perpetual', 450.00, 1),
    -- Beat 7: Street Anthem
    (7, 'lease', 'Non-exclusive, 25,000 streams limit, 2-year term', 150.00, 1),
    -- Beat 8: Smooth Nights
    (8, 'lease', 'Non-exclusive, 50,000 streams limit, 2-year term', 180.00, 1),
    (8, 'exclusive', 'Full ownership, unlimited streams, perpetual', 600.00, 1),
    -- Beat 9: Boom Bap Classic
    (9, 'lease', 'Non-exclusive, 15,000 streams limit, 2-year term', 100.00, 1),
    -- Beat 10: Summer Party
    (10, 'lease', 'Non-exclusive, 30,000 streams limit, 2-year term', 160.00, 1),
    -- Beat 11: Electric Wave
    (11, 'lease', 'Non-exclusive, 20,000 streams limit, 2-year term', 130.00, 1),
    (11, 'exclusive', 'Full ownership, unlimited streams, perpetual', 500.00, 1),
    -- Beat 12: Love Story
    (12, 'lease', 'Non-exclusive, 40,000 streams limit, 2-year term', 170.00, 1),
    -- Beat 13: Jazz Lounge
    (13, 'lease', 'Non-exclusive, 10,000 streams limit, 2-year term', 110.00, 1),
    -- Beat 14: Trap Nation
    (14, 'lease', 'Non-exclusive, 60,000 streams limit, 2-year term', 200.00, 1),
    (14, 'exclusive', 'Full ownership, unlimited streams, perpetual', 700.00, 1),
    -- Beat 15: Pop Sensation
    (15, 'lease', 'Non-exclusive, 35,000 streams limit, 2-year term', 175.00, 1),
    -- Beat 16: Dark Alley
    (16, 'lease', 'Non-exclusive, 45,000 streams limit, 2-year term', 185.00, 1),
    -- Beat 17: Neon Lights
    (17, 'lease', 'Non-exclusive, 55,000 streams limit, 2-year term', 190.00, 1),
    -- Beat 18: Coffee Shop
    (18, 'lease', 'Non-exclusive, 12,000 streams limit, 2-year term', 105.00, 1),
    -- Beat 19: Slow Jam
    (19, 'lease', 'Non-exclusive, 18,000 streams limit, 2-year term', 115.00, 1),
    -- Beat 20: Hip Hop Legacy
    (20, 'lease', 'Non-exclusive, 22,000 streams limit, 2-year term', 140.00, 1);
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
USE Everybeats;
GO

