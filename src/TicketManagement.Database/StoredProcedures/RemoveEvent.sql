CREATE PROCEDURE [dbo].[RemoveEvent]
	@Id int
AS
                --получаем EventAreaId из EventSeat по Event.Id и удаляем записи по Event.Id из EventSeat
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

                DELETE FROM Event WHERE Event.Id=@Id

                
                DECLARE @maxSeat INT
                SELECT @maxSeat=max([Id]) FROM EventSeat
                IF @maxSeat IS NULL   --check when max is returned as null
                    SET @maxSeat = 0
                DBCC CHECKIDENT ('EventSeat', RESEED, @maxSeat)

                DECLARE @maxArea INT
                SELECT @maxArea=max([Id]) FROM EventArea
                IF @maxArea IS NULL   --check when max is returned as null
                    SET @maxArea = 0
                DBCC CHECKIDENT ('EventArea', RESEED, @maxArea)

                DECLARE @maxEvent INT
                SELECT @maxEvent=max([Id]) FROM Event
                IF @maxEvent IS NULL   --check when max is returned as null
                    SET @maxEvent = 0
                DBCC CHECKIDENT ('Event', RESEED, @maxEvent)