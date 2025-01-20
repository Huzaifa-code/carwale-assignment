# Database Design :

Relational Database design for E-commerce Platform

### Tables :
1. customers 
2. products
3. orders
4. category

### 1. customers :

**Fieds** :
1. customer_id -> **PRIMARY_KEY**
2. email
3. name
4. phone_no
5. address


 

### 2. products

**Fields** :
1. product_id -> **PRIMARY_KEY**
2. category_id -> **FOREIGN_KEY** ON DELETE CASCADE
3. name 
4. description
5. price


**ON DELETE CASCADE :** if a row of the referenced table is deleted, then all matching rows in the referencing table are deleted.

### 3. orders

**Fields** :
1. order_id -> **PRIMARY_KEY**
2. customer_id -> **FOREIGN_KEY** ON DELETE CASCADE
3. product_id -> **FOREIGN_KEY** ON DELETE CASCADE
4. qty 
5. order_data_time
6. amount  ( = qty*price from product_id )

### 4. category 

**Fields** : 

1. category_id -> **PRIMARY_KEY**
2. name
3. description


## Creating Tables :

1. customers :

```sql
CREATE TABLE customers (
    customer_id INT PRIMARY KEY,
    email VARCHAR(255) NOT NULL UNIQUE,
    name VARCHAR(255) NOT NULL,
    phone_no VARCHAR(20),
    address TEXT
);
```

2. category

```sql
CREATE TABLE category (
    category_id INT AUTO_INCREMENT PRIMARY KEY,
    description VARCHAR(256),
    name VARCHAR(255) NOT NULL
);
```


3. products :

```sql
CREATE TABLE products (
    product_id INT AUTO_INCREMENT PRIMARY KEY,
    category_id INT NOT NULL,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    price DECIMAL(10, 2) NOT NULL,
    CONSTRAINT fk_category FOREIGN KEY (category_id) REFERENCES category(category_id) ON DELETE CASCADE
);
```



4. orders :

```sql
CREATE TABLE orders (
    order_id INT AUTO_INCREMENT PRIMARY KEY,
    customer_id INT,
    product_id INT,
    qty INT,
    order_date_time DATETIME,
    amount DECIMAL(10, 2),

    FOREIGN KEY (customer_id) REFERENCES customers(customer_id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE CASCADE
);
```

![](https://i.imgur.com/uW16HZ2.png)



Inserting Data :

customer table :

```sql
INSERT INTO customers (email, name, phone_no, address) VALUES
    ('huzaifa@gmail.com','huzaifa', '+911148174','Indore')
    ( 'anish@gmail.com', 'anish', '+91123384347','vashi navi mumabi vishwaroop IT PARK' ),
    ('john@gmail.com','john','+918734242847','Bhopal MP'  );
```

category table :

```sql
INSERT INTO category (name, description) VALUES 
('Electronics', 'Devices like phones, laptops, and accessories'),
('Clothing', 'Apparel for men and women'),
('Books', 'Printed and digital reading materials');
```

products table :
```sql
 INSERT INTO products (category_id, name, description, price) VALUES
    -> (1, 'Redmi Note 14', 'Latest model with high resolution camera', 18000),
    -> (1, 'OnePlus 13R', 'Smarter with OnePlus AI', 42998),
    -> (2, 'Denim Jeans', 'Stylish denim jeans for casual wear', 2000);
```

orders table :

```sql
INSERT INTO orders (customer_id, product_id, qty, order_date_time, amount) VALUES (1, 1, 2, '2025-01-20 10:00:00', 2 * 18000),
    -> (1, 2, 2, '2025-01-20 12:00:00', 2 * 42998),
    -> (2, 1, 2, '2025-01-20 12:10:00', 2 * 42998),
    -> (2, 3, 4, '2025-01-20 11:00:00', 4 * 2000);
```

## CRUD operations :

Create, Read, Update & Delete 

### Creating
Inserting Data

product :
```sql
INSERT INTO products (category_id, name, description, price) VALUES 
(1, 'xiaomi pad 7', 'Tablet by xiaomi with powerful hardware and latest software', 33000);
```

customer :
```sql
INSERT INTO customers (email, name, phone_no, address) VALUES 
('mike.brown@example.com', 'Mike Brown', '555-7890', '202 Birch St, Springfield');
```

orders :
```sql
INSERT INTO orders (customer_id, product_id, qty, order_date_time) VALUES 
(1, 2, 2, '2025-01-21 14:30:00');
```

### Read

![](https://i.imgur.com/rtM5P9o.png)

**Retrieving all products from the products table:**
```sql
SELECT * FROM products;
```

**Retrieving products based on a specific category (e.g., "Electronics"):**
```sql
SELECT * FROM products WHERE category_id = 1;
```

![](https://i.imgur.com/UguJyXm.png)

**Retrieving products within a specific price range (e.g., between RS.1,000 and RS.20,000):**
```sql
SELECT * FROM products WHERE price BETWEEN 1000 AND 20000;
```

![](https://i.imgur.com/L1ABuII.png)

**Retrieving all orders made by a specific customer:**
```sql
SELECT * FROM orders WHERE customer_id = 1;
```
![](https://i.imgur.com/qVtgzHJ.png)

**Retrieving all orders with total amount:**
```sql
SELECT order_id, customer_id, product_id, qty, order_date_time, amount 
FROM orders;
```

![](https://i.imgur.com/qC61975.png)

### Update :
updating Records :

**Updating a product's price in the products table:**
```sql
UPDATE products SET price=17999 WHERE product_id = 1;
```
![](https://i.imgur.com/AybxPRP.png)

**Updating a customer's address in the customers table:**
```sql
UPDATE customers SET address = '105 Maple St, Shelbyville' WHERE customer_id = 2;
```

**Updating the quantity of a product in an order:**
```sql
UPDATE orders SET qty = 3 WHERE order_id = 1;
```

### Deleting Records:

**Deleting a product from the products table (e.g., a product with product_id = 2):**

```sql
DELETE FROM products WHERE product_id = 2;
```

![](https://i.imgur.com/zzl3iAS.png)

**Deleting a customer from the customers table (e.g., a customer with customer_id = 3 ):**
```sql
DELETE FROM customers WHERE customer_id = 3;
```

**Deleting an order from the orders table (e.g., an order with order_id = 2):**
```sql
DELETE FROM orders WHERE order_id = 2;
```

### Advanced Queries and Joins:



**Retrieving Orders with Customer and Product Information**
```sql
SELECT 
    o.order_id,
    o.order_date_time,
    o.qty,
    o.amount,
    c.name AS customer_name,
    c.email AS customer_email,
    p.name AS product_name,
    p.price AS product_price
FROM 
    orders o
JOIN 
    customers c ON o.customer_id = c.customer_id
JOIN 
    products p ON o.product_id = p.product_id;
```

![](https://i.imgur.com/SMYPvA6.png)

**Getting Total Revenue for a Specific Time Period**

```sql
SELECT 
    DATE(order_date_time) AS order_date,
    SUM(amount) AS total_revenue
FROM 
    orders
WHERE 
    order_date_time BETWEEN '2025-01-01' AND '2025-01-31'
GROUP BY 
    DATE(order_date_time)
ORDER BY 
    order_date;
```

![](https://i.imgur.com/GvbJMWU.png)

**Finding Customers Who Have Made Multiple Purchases** :

```sql
SELECT 
    c.customer_id,
    c.name AS customer_name,
    c.email AS customer_email,
    COUNT(o.order_id) AS total_orders
FROM 
    customers c
JOIN 
    orders o ON c.customer_id = o.customer_id
GROUP BY 
    c.customer_id, c.name, c.email
HAVING 
    COUNT(o.order_id) > 1
ORDER BY 
    total_orders DESC;
```

![](https://i.imgur.com/pLp0lKH.png)


## Indexing and Optimization

MySQL creates a unique index for primary keys by default to enforce uniqueness and optimize lookups by primary key.

In **Customer** table :

There are 2 indexes -
1. customer_id
2. email

email was automatically created index in this table because in schema it is defined as unique

![](https://i.imgur.com/vuVNeoC.png)

Foreign key columns (category_id in products, customer_id and product_id in orders) - MySQL creates indexes automatically on these columns to: 

- Speed up join operations (e.g., JOIN products ON category.category_id = products.category_id).
- Facilitate cascading operations like ON DELETE CASCADE or ON UPDATE CASCADE by locating referenced rows quickly.