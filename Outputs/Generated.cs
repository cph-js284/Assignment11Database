
public class Product {
 public int Product_id {get; set; }
public string name {get; set; }
public int price {get; set; }
}


public class OrderLine {
 public int OrderLine_id {get; set; }
public Order order{get; set; }
public Product product{get; set; }
public int count {get; set; }
public int total {get; set; }
}


public class Order {
 public int Order_id {get; set; }
public string date {get; set; }
public int total {get; set; }
public Customer customer{get; set; }
public System.Collections.Generic.List<OrderLine> lines {get; set; }
}


public class Customer {
 public int Customer_id {get; set; }
public string name {get; set; }
public System.Collections.Generic.List<Order> orders {get; set; }
}

