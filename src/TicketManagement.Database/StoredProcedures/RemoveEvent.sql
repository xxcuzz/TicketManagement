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