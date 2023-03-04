select FirstName + ' ' + LastName as FullName, age, od.orderid, datecreated, MethodOfPurchase as PurchaseMethod, ProductNumber, ProductOrigin
from OrderDetails od
inner join orders o on o.OrderID = od.OrderID
inner join Customers c on c.PersonID = o.PersonID
where ProductID = 1112222333

