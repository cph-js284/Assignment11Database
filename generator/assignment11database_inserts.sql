start transaction;

insert into Product_tbl (name, price) values ('product1', 100);
insert into Product_tbl (name, price) values ('product2', 200);
insert into Product_tbl (name, price) values ('product3', 300);
insert into Product_tbl (name, price) values ('product4', 400);
insert into Product_tbl (name, price) values ('product5', 500);
insert into Product_tbl (name, price) values ('product6', 600);
insert into Product_tbl (name, price) values ('product7', 700);

insert into Customer_tbl (name, order_id) values ('cust1', 1);
insert into Customer_tbl (name, order_id) values ('cust2', 2);
insert into Customer_tbl (name, order_id) values ('cust3', 3);

insert into Order_tbl (date, total, customer_id, OrderLine_id) values ('1-1-2019', 2600, 1, 1);
insert into Order_tbl (date, total, customer_id, OrderLine_id) values ('2-1-2019', 15000, 2, 2);
insert into Order_tbl (date, total, customer_id, OrderLine_id) values ('3-1-2019', 6100, 3, 3);

insert into OrderLine_tbl (order_id, product_id, count, total) values (1, 5, 4, 2000);
insert into OrderLine_tbl (order_id, product_id, count, total) values (1, 2, 1, 200);
insert into OrderLine_tbl (order_id, product_id, count, total) values (1, 1, 4, 400);

insert into OrderLine_tbl (order_id, product_id, count, total) values (2, 5, 4, 2000);
insert into OrderLine_tbl (order_id, product_id, count, total) values (2, 20, 3, 6000);
insert into OrderLine_tbl (order_id, product_id, count, total) values (2, 10, 7, 7000);

insert into OrderLine_tbl (order_id, product_id, count, total) values (3, 5, 4, 2000);
insert into OrderLine_tbl (order_id, product_id, count, total) values (3, 1, 1, 100);
insert into OrderLine_tbl (order_id, product_id, count, total) values (3, 10,4, 4000);

commit;
