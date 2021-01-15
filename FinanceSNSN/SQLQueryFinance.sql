create database dbfinance

drop database dbfinance

create table RegisterBank
(
	Name varchar(30) not null,
	email varchar(30) not null unique,
	Phone_No varchar(10) not null unique,
	username varchar(20) primary key,
	Password nvarchar(20) not null,
	Address text not null,
	Card_Type bit not null,
	Fees_Paid bit,
	Bank_Name varchar(30) not null,
	Account_Number int unique not null,
	IFSC_Code varchar(11) not null,
	document varchar(max)
)

insert into RegisterBank values
('XYZ', 'xyz@gmail.com', '8787512497', 'xyz', 'xyz123', '711-2880 Nulla St. Mankato Mississippi 96522 (257) 563-7401', 0, 0, 'Kotak Bank', 1021257854, 'KM00124598', 'C:\Users\Navdeep\Documents\GET_886.Case Study - Finance.pdf'),
('ABC', 'abc@gmail.com', '7541294124', 'abc', 'abc987', 'P.O. Box 283 8562 Fusce Rd. Frederick Nebraska 20620 (372) 587-2335', 1, 1, 'State Bank of India', 2051373512, 'SBIN001245', 'C:\Users\Navdeep\Documents\GET_886.Case Study - Finance.pdf')

select * from RegisterBank

drop table RegisterBank

create proc sp_GetAllDetails
(@username)
as
begin
	select * from RegisterBank r join EMICard c
	on r.username=c.username
end

create table Admin
(
	username varchar(20) primary key,
	Password nvarchar(20) not null,
	Phone varchar(10) not null,
	Email varchar(30) not null
)

insert into Admin values
('aaa', 'aaa123','7235391272','aaa@gmail.com')

select * from Admin

drop table Admin

create table Products
(
	Product_ID int primary key,
	Name varchar(20) not null,
	Cost money not null,
	imagePath varchar(50),
	Details varchar(max) not null
)

insert into Products values
('101', 'Camera', 25999, 'assets/images/camera.jpg', 'DSLR'),
('102', 'Headphone', 2399, 'assets/images/headphone.jpg', 'Boat bluetooth headset'),
('103', 'Phone', 17999, 'assets/images/phone.jpg', 'Sumsung M31'),
('104', 'Shoes', 1230, 'assets/images/shoes.jpg', 'Nike Shoes'),
('105', 'Watch', 9799, 'assets/images/watch.jpg', 'Apple smart watch')
 
select * from Products
 
drop table Products

create table Cart
(
	Cart_ID int,
	Card_Number int not null references EMICard(Card_Number),
	Product_ID int not null references Products(Product_ID),
	Quantity int not null,
	primary key(Cart_ID, Product_ID)
)

insert into Cart values
(1, 112154, 101, 1),
(1, 112154, 105, 2)

select * from Cart

drop table Cart

create table EMICard
(
	Card_Number int primary key,
	username varchar(20) not null references RegisterBank(username),
	Card_Type bit references Card(Card_Type),
	/* Credit_Used money, */
	valid date not null,
	Active bit not null default 0,
	admin_username varchar(20) references Admin(username)
)
 
insert into EMICard values
('0000112154', 'abc', 1, '12-26-2034', 1, 'aaa'),
('0001225481', 'xyz', 0, '12-26-2034', 0, 'aaa')
 
select * from EMICard

drop table EMICard

update EMICard set Active=0
where username='xyz'
 
create table Card
(
	Card_Type bit primary key,
	Total_Credit money not null,
)

insert into Card values
(0, 50000),
(1, 80000)

select * from Card

drop table Card

create table Transactions
(
	Transaction_ID int primary key identity(1000000001,1), 
	Product_ID int references Products(Product_ID),
	Date date not null,
	Amount_Paid money not null,
	Card_Number int references EMICard(Card_Number),
	EMI_Tenure int default 0
)

insert into Transactions values
/* ('0048571249', '102', '12-27-2020', 39999, '0000112154'), */
('101', '12-28-2020', 8666.33, '0000112154',3),
('102', '12-30-2020', 799.66, '0000112154',3),
('101', '1-5-2021', 8666.33, '0000112154',3)

select * from Transactions

drop table Transactions

/* Store Procedures */

create proc sp_GetAllRegister
as
begin
	select * from RegisterBank join EMICard
	on RegisterBank.username = EMICard.username
	where Active=0
end

sp_GetAllRegister 
 

create proc sp_check(@username varchar(20))
as
begin
	declare @check int
	set @check = (select count(username) from EMICard where username = @username)
	select @check
end
 
exec sp_check 'abc'


create proc sp_InsertEMICard(@username varchar(20))
as
begin
	declare @cardtype bit
	set @cardtype= (select Card_Type from RegisterBank where username=@username)
 
	insert into EMICard values( CAST((FLOOR(RAND()*(2147483647-1000000001+1)+1000000001))as int), @username, @cardtype,DateAdd(yy,5,GetDate()),1,'aaa')
end 
 
exec sp_InsertEMICard 'abc'
 

alter proc Delete_EMIDetails(@username varchar(20))
as
begin 
	delete Transactions from Transactions join EMICard on Transactions.Card_Number=EMICard.Card_Number
	where EMICard.username = @username

	delete EMICard from EMICard where EMICard.username = @username

	delete RegisterBank from RegisterBank where RegisterBank.username = @username

	delete Cart from Cart join EMICard on Cart.Card_Number=EMICard.Card_Number
	where EMICard.username = @username
end

exec Delete_EMIDetails 'abc'


create proc sp_AdminLoginCheck
(@username varchar(20), @password nvarchar(20))
as
begin
	declare @login int
	set @login=(select count(*) from Admin where username=@username and Password=@password)
	select @login
end

sp_AdminLoginCheck 'aaa', 'aaa123'


create proc sp_UserLoginCheck
(@username varchar(20), @password nvarchar(20))
as
begin
	declare @login int
	set @login=(select count(*) from RegisterBank where username=@username and Password=@password)
	select @login
end

sp_UserLoginCheck 'abc', 'abc987'


create proc sp_EmailCheck(@Email varchar(30))
as
begin
	select username from RegisterBank where email = @Email
end

exec sp_EmailCheck 'pqr@gmail.com'


create proc sp_updatepassword(@username varchar(20), @password nvarchar(20))
as
begin
	update RegisterBank set Password = @password where username=@username
end

exec sp_updatepassword 'abc123', 'Abcd1234'


sp_UserLoginCheck 'abc', 'abc123'

create proc sp_UserWithEmail
(@email varchar(30))
as
begin
	select username from RegisterBank where email=@email
end

sp_UserWithEmail 'abc@gmail.com'


create proc sp_UserTransactions
(@username varchar(20))
as
begin
	select p.Name, t.Date, t.Amount_Paid from Transactions t join EMICard c
	on t.Card_Number = c.Card_Number
	join Products p on t.Product_ID = p.Product_ID
	where username = @username
end

sp_UserTransactions 'Navdeep'


alter proc sp_CreditDetails
(@cardNumber int)
as
begin
	declare @tc money
	if ((select Card_Type from EMICard where Card_Number = @cardNumber)=1)
		begin
			set @tc=80000
		end
	else
		begin
			set @tc=50000
		end

	declare @amountPaid money
	set @amountPaid = (select Sum(t.Amount_Paid) from Transactions t join EMICard c on t.Card_Number = c.Card_Number join Products p on 
						t.Product_ID = p.Product_ID where c.Card_Number = @cardNumber)

	if(@amountPaid is null)
		begin
			set @amountPaid=0
		end

	declare @totalCost money
	set @totalCost = (select Sum(Products.Cost) from Products where Products.Product_ID in(select Distinct p.Product_ID from Transactions t join 
						EMICard c on t.Card_Number = c.Card_Number join Products p on t.Product_ID = p.Product_ID where c.Card_Number = @cardNumber))

	if(@totalCost is null)
		begin
			set @totalCost=0
		end

	select @tc as 'Total Credit', (@totalCost-@amountPaid) as 'Credit Used', @tc-(@totalCost-@amountPaid) as 'Reamining Credit'
end

sp_CreditDetails 1225481

drop proc sp_CreditDetails


create proc sp_UserProducts  /* Displaying user products on dashboard */
(@username varchar(20))
as
begin
	select p.Product_ID, p.Name, Sum(t.Amount_Paid) as 'Amount Paid', p.Details, p.imagePath, p.Cost, (p.Cost-Sum(t.Amount_Paid)) as Balance, 
	t.EMI_Tenure as 'EMI_Tenure' from Transactions t join EMICard c on t.Card_Number = c.Card_Number
	join Products p on t.Product_ID = p.Product_ID
	where username = @username
	group by p.Product_ID, p.Name, p.Cost, p.Details, p.imagePath, t.EMI_Tenure
end

sp_UserProducts 'Navdeep'


create proc sp_LastTransactionDate
(@username varchar(20), @productId int)
as
begin
	select Month(t.Date) from Transactions t join EMICard c
	on t.Card_Number = c.Card_Number
	join Products p on t.Product_ID = p.Product_ID
	where username = @username and t.Product_ID = @productId
	group by p.Product_ID, t.Date
	order by t.Date desc
end

sp_LastTransactionDate 'Navdeep', 101


alter proc sp_UserProductWithId
(@username varchar(20), @productId int)
as
begin
	select p.Name, Sum(t.Amount_Paid) as 'Amount Paid', p.Details, p.imagePath, p.Cost, Round(p.Cost-Sum(t.Amount_Paid), 0) as Balance, (p.Cost/t.EMI_Tenure) as 'Current_Payment', t.EMI_Tenure as 'EMI_Tenure' from Transactions t join EMICard c
	on t.Card_Number = c.Card_Number
	join Products p on t.Product_ID = p.Product_ID
	where username = @username and t.Product_ID = @productId
	group by p.Product_ID, p.Name, p.Cost, p.Details, p.imagePath, t.EMI_Tenure
end

sp_UserProductWithId 'Navdeep', 101


create proc sp_CartProducts
(@cartId int)
as
begin
	select p.Product_ID, p.Name, p.Cost, p.imagePath, p.Details, c.Quantity, p.Cost*c.Quantity as 'Product Total' 
	from Products p join Cart c on p.Product_ID = c.Product_ID
	where p.Product_ID in(select Product_ID from Cart where Cart_ID = @cartId)
end

sp_CartProducts 1


create proc sp_TotalCostWithout
(@cartId int)
as
begin
	select Sum(p.Cost*c.Quantity) from Products p join Cart c on p.Product_ID = c.Product_ID where p.Product_ID in(select Product_ID from Cart where Cart_ID = @cartId) group by Cart_ID
end

sp_TotalCostWithout 1


alter proc sp_GetCardNumberWithUsername
(@username varchar(20))
as
begin
	select Card_Number from EMICard
	where username = @username
end

sp_GetCardNumberWithUsername 'Navdeep'

