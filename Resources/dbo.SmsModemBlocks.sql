CREATE TABLE [dbo].[SmsModemBlocks] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Port]                 NVARCHAR (MAX) NULL,
    [Operator]             NVARCHAR (MAX) NULL,
    [TelNumber]            NVARCHAR (MAX) NULL,
    [SignalLevel]          INT            NULL,
    [MacAddress]           NVARCHAR (MAX) NULL,
    [ConnectionCheckDelay] INT            NOT NULL,
    [LogLevel]             INT            NOT NULL,
    CONSTRAINT [PK_dbo.SmsModemBlocks] PRIMARY KEY CLUSTERED ([Id] ASC)
);

