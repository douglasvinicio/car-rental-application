CREATE TABLE [dbo].[Returns] (
    [ReturnId]   INT           IDENTITY (1, 1) NOT NULL,
    [CarId]     INT NOT NULL,
    [CustId]     INT           NOT NULL,
    [ReturnDate] DATETIME      NOT NULL,
    [Fine]       FLOAT (53)    NULL,
    PRIMARY KEY CLUSTERED ([ReturnId] ASC)
);

