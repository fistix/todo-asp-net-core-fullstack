CREATE TABLE [dbo].[Customers]
(
  Id UNIQUEIDENTIFIER NOT NULL,
  FirstName varchar(255) NULL,
  LastName varchar(255) NULL,
  Email varchar(255) NULL,
  StripeCustomerId VARCHAR(255) NULL, 
  StripeCustomerName VARCHAR(255) NULL, 
  LastPaymentDeduct BIGINT NULL, 
  LastPaymentDeductOn DATETIME NULL,
  PayPalCustomerId VARCHAR(255) NULL, 
  PayPalCustomerName VARCHAR(255) NULL, 
  --PayPalCustomerEmail VARCHAR(255) NULL, 
  PayPalSubscriptionId VARCHAR(255) NULL, 
  PayPalLastPaymentDeduct VARCHAR(255) NULL, 
  PayPalLastPaymentDeductOn DATETIME NULL,
  PRIMARY KEY CLUSTERED ([Id] ASC), 
     
);
