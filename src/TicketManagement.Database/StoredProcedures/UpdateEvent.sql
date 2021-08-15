CREATE PROCEDURE [dbo].[UpdateEvent]
	@Id int,
	@Name nvarchar(50),
	@Description nvarchar(100),
	@LayoutId int,
	@EventStart DateTime,
	@EventEnd DateTime,
	@Image nvarchar(MAX)

AS
	UPDATE Event 
	SET		Name = @Name,  
            Description = @Description,  
            LayoutId = @LayoutId,
			EventStart = @EventStart,
			EventEnd = @EventEnd,
			Image = @Image
	WHERE Id = @Id;
GO