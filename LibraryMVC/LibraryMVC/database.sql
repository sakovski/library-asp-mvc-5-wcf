CREATE PROCEDURE dbo.LoanBook  
  @BookId int,  
  @LibraryUserId int,
  @ReturnDate datetime
AS
BEGIN TRANSACTION
INSERT INTO dbo.Loan VALUES (GETDATE(), @ReturnDate, 0, @LibraryUserId, @BookId);
IF @@ERROR <> 0
 BEGIN
    ROLLBACK
    RETURN
 END

SET @new_identity = SCOPE_IDENTITY();
UPDATE dbo.Book SET LoanId = @new_identity WHERE id = @BookId;
IF @@ERROR <> 0
 BEGIN
    ROLLBACK
    RETURN
 END

COMMIT
GO