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

	DECLARE @EventAreaId INT
                DECLARE cur CURSOR FOR
                SELECT EventSeat.EventAreaId FROM Event INNER JOIN EventArea ON Event.Id=EventArea.EventId INNER JOIN EventSeat ON EventArea.Id=EventSeat.EventAreaId WHERE Event.Id=@Id
                OPEN cur
                FETCH NEXT FROM cur INTO @EventAreaId
                WHILE @@FETCH_STATUS = 0
                BEGIN
	                DELETE FROM EventSeat WHERE EventAreaId=@EventAreaId
	                FETCH NEXT FROM cur INTO @EventAreaId
                END
                CLOSE cur

                DELETE FROM EventArea WHERE EventArea.EventId=@Id

                --выборка из Area и инсерт в EventArea
                DECLARE @AreaIdForEventId INT, @DescriptionForEventArea NVARCHAR(200), @CoordX INT, @CoordY INT, @EventAreaId2 INT
                DECLARE cur2 CURSOR FOR
                SELECT Area.Id, Area.Description, Area.CoordX, Area.CoordY FROM Layout INNER JOIN Area ON Layout.Id=Area.LayoutId WHERE Layout.Id=@LayoutId
                OPEN cur2
                FETCH NEXT FROM cur2 INTO @AreaIdForEventId, @DescriptionForEventArea, @CoordX, @CoordY
                WHILE @@FETCH_STATUS = 0
                BEGIN
	                INSERT INTO dbo.EventArea(EventId, Description, CoordX, CoordY, Price) VALUES(@Id, @DescriptionForEventArea, @CoordX, @CoordY, 0)

		                --получаем EventArea.Id, куда только что вставили данные
		                DECLARE curs CURSOR FOR
		                SELECT MAX(Id) FROM EventArea
		                OPEN curs
		                FETCH NEXT FROM curs INTO @EventAreaId2
		                CLOSE curs
		                DEALLOCATE curs

		                --выборка из Seat и инсерт в EventSeat
		                DECLARE @Row INT, @Number INT
		                DECLARE curso CURSOR FOR
		                SELECT Seat.Row, Seat.Number FROM Layout INNER JOIN Area ON Layout.Id=Area.LayoutId INNER JOIN Seat ON Area.Id=Seat.AreaId WHERE Layout.Id=@LayoutId AND Area.Id=@AreaIdForEventId
		                OPEN curso
		                FETCH NEXT FROM curso INTO @Row, @Number
		                WHILE @@FETCH_STATUS = 0
		                BEGIN
		                INSERT INTO EventSeat VALUES(@EventAreaId2, @Row, @Number, 0)
			                FETCH NEXT FROM curso INTO @Row, @Number
		                END
		                CLOSE curso
		                DEALLOCATE curso

	                FETCH NEXT FROM cur2 INTO @AreaIdForEventId, @DescriptionForEventArea, @CoordX, @CoordY
                END
                CLOSE cur2
GO
