CREATE TABLE [dbo].[Customers]
(
  Id UNIQUEIDENTIFIER NOT NULL,
  FirstName varchar(255) NULL,
  LastName varchar(255) NULL,
  Email varchar(255) NULL

  PRIMARY KEY CLUSTERED ([Id] ASC), 
    [StripeCustomerId] VARCHAR(255) NULL, 
    [StripeCustomerName] VARCHAR(255) NULL    
);
