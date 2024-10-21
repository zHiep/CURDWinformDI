SELECT * FROM dbo.Customer

--Update 
CREATE PROCEDURE spKH_update
    @Id BIGINT, 
    @Name NVARCHAR(100), 
    @Email VARCHAR(100), 
    @PhoneNumber VARCHAR(15)
AS
BEGIN
    UPDATE dbo.Customer
    SET Name = @Name,
        Email = @Email,
        PhoneNumber = @PhoneNumber
    WHERE Id = @Id;
END

--Insert
CREATE PROCEDURE spKH_insert
    @Id BIGINT,
    @Name NVARCHAR(100), 
    @Email VARCHAR(100), 
    @PhoneNumber VARCHAR(15) 
AS
BEGIN
    INSERT INTO dbo.Customer (Id,Name, Email, PhoneNumber)
    VALUES (@Id,@Name, @Email, @PhoneNumber);
END

--Delete
CREATE PROCEDURE spKH_delete
    @Id BIGINT
AS
BEGIN
    DELETE FROM dbo.Customer
    WHERE Id = @Id;
END

