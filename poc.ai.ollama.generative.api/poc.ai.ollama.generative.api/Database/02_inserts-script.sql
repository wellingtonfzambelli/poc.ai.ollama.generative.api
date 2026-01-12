PRAGMA foreign_keys = OFF;

DELETE FROM Payments;
DELETE FROM OrderItems;
DELETE FROM Orders;
DELETE FROM Products;
DELETE FROM Users;

DELETE FROM sqlite_sequence;

PRAGMA foreign_keys = ON;

-- =========================
-- USERS (10)
-- =========================
INSERT INTO Users (Name, Email, CreatedAt) VALUES
('John Smith', 'john.smith2@email.com', '2024-01-05'),
('Emily Johnson', 'emily.johnson@email.com', '2024-01-07'),
('Michael Brown', 'michael.brown@email.com', '2024-01-10'),
('Sarah Davis', 'sarah.davis@email.com', '2024-01-12'),
('David Wilson', 'david.wilson@email.com', '2024-01-15'),
('Laura Miller', 'laura.miller@email.com', '2024-01-18'),
('Daniel Anderson', 'daniel.anderson@email.com', '2024-01-20'),
('Sophia Martinez', 'sophia.martinez@email.com', '2024-01-22'),
('James Taylor', 'james.taylor@email.com', '2024-01-25'),
('Olivia Thomas', 'olivia.thomas@email.com', '2024-01-28');

-- =========================
-- PRODUCTS (10)
-- =========================
INSERT INTO Products (Name, Price, Stock, CreatedAt) VALUES
('Laptop Pro 15', 4800.00, 15, '2024-01-01'),
('Wireless Mouse', 120.00, 200, '2024-01-01'),
('Mechanical Keyboard', 350.00, 120, '2024-01-01'),
('27-inch Monitor', 1400.00, 60, '2024-01-01'),
('Noise Cancelling Headset', 650.00, 80, '2024-01-01'),
('HD Webcam', 320.00, 90, '2024-01-01'),
('External SSD 1TB', 750.00, 50, '2024-01-01'),
('Office Chair', 980.00, 40, '2024-01-01'),
('Standing Desk', 1350.00, 25, '2024-01-01'),
('USB-C Hub', 150.00, 300, '2024-01-01');

-- =========================
-- ORDERS (10)
-- =========================
INSERT INTO Orders (UserId, OrderDate, Status, TotalAmount) VALUES
(1, '2024-02-01', 'Paid', 5150.00),
(2, '2024-02-02', 'Paid', 350.00),
(3, '2024-02-03', 'Paid', 1400.00),
(4, '2024-02-04', 'Cancelled', 650.00),
(5, '2024-02-05', 'Paid', 750.00),
(6, '2024-02-06', 'Paid', 120.00),
(7, '2024-02-07', 'Paid', 980.00),
(8, '2024-02-08', 'Pending', 1350.00),
(9, '2024-02-09', 'Paid', 150.00),
(10, '2024-02-10', 'Paid', 4800.00);

-- =========================
-- ORDER ITEMS (at least 10)
-- =========================
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice, TotalPrice) VALUES
(1, 1, 1, 4800.00, 4800.00),
(1, 2, 1, 120.00, 120.00),
(1, 10, 1, 150.00, 150.00),

(2, 3, 1, 350.00, 350.00),

(3, 4, 1, 1400.00, 1400.00),

(4, 5, 1, 650.00, 650.00),

(5, 7, 1, 750.00, 750.00),

(6, 2, 1, 120.00, 120.00),

(7, 8, 1, 980.00, 980.00),

(8, 9, 1, 1350.00, 1350.00),

(9, 10, 1, 150.00, 150.00),

(10, 1, 1, 4800.00, 4800.00);

-- =========================
-- PAYMENTS (Paid orders only)
-- =========================
INSERT INTO Payments (OrderId, PaymentMethod, Amount, PaidAt, Status) VALUES
(1, 'CreditCard', 5150.00, '2024-02-01', 'Paid'),
(2, 'Pix', 350.00, '2024-02-02', 'Paid'),
(3, 'DebitCard', 1400.00, '2024-02-03', 'Paid'),
(5, 'CreditCard', 750.00, '2024-02-05', 'Paid'),
(6, 'Pix', 120.00, '2024-02-06', 'Paid'),
(7, 'CreditCard', 980.00, '2024-02-07', 'Paid'),
(9, 'Pix', 150.00, '2024-02-09', 'Paid'),
(10, 'CreditCard', 4800.00, '2024-02-10', 'Paid');
