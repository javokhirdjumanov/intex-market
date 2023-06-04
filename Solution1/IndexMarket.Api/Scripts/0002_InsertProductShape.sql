USE [IntexMarket]
GO

/****** Object: Table [dbo].[ProductShapes] Script Date: 6/4/2023 2:10:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductShapes] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR (100)   NOT NULL
);


INSERT INTO ProductShapes (Id, Name)
VALUES ('4110b601-cdd5-44e7-1817-08db6457b821', 'Rectangle');

select * from ProductShapes;