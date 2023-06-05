

INSERT INTO [dbo].[Sites] 
    ([Id],
    [PhoneNumber],
    [JobTime],
    [TelegrammLink],
    [InstagramLink],
    [Address_Id],
    [CreatedAt],
    [UpdatedAt])
VALUES 
   ('8865873b-b03e-428c-3f1a-08db62811147',
    '998912345678',
    '9:00 AM - 7:00 PM',
    'https://t.me/example',
    'https://www.instagram.com/example',
    '8865873b-b03e-428c-3f1a-08db62811141',
    GETDATE(),
    GETDATE());