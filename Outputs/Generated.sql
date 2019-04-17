DROP DATABASE IF EXISTS MicroShop;
CREATE DATABASE MicroShop;
USE MicroShop;

CREATE TABLE Product_tbl(
Product_id INT NOT NULL AUTO_INCREMENT
,PRIMARY KEY(Product_id)
,name VARCHAR(100)
,price INT
);


CREATE TABLE OrderLine_tbl(
OrderLine_id INT NOT NULL AUTO_INCREMENT
,PRIMARY KEY(OrderLine_id)
,order_id INT
,product_id INT
,count INT
,total INT
);


CREATE TABLE Order_tbl(
Order_id INT NOT NULL AUTO_INCREMENT
,PRIMARY KEY(Order_id)
,date VARCHAR(100)
,total INT
,customer_id INT
,OrderLine_id INT
);


CREATE TABLE Customer_tbl(
Customer_id INT NOT NULL AUTO_INCREMENT
,PRIMARY KEY(Customer_id)
,name VARCHAR(100)
,Order_id INT
);

