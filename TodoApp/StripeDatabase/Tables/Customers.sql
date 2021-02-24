CREATE TABLE [dbo].[Customers]
(
  Id UNIQUEIDENTIFIER NOT NULL,
  FirstName varchar(255) NULL,
  LastName varchar(255) NULL,
  Email varchar(255) NULL,
  StripeCustomerId VARCHAR(255) NULL, 
  StripeCustomerName VARCHAR(255) NULL, 
  Amount BIGINT NULL, 
  LastPaymentDeduct DATETIME NULL,
  

  PRIMARY KEY CLUSTERED ([Id] ASC), 
     
);
