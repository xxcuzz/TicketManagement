delete from EventSeat
delete from Seat
delete from EventArea
delete from Area
delete from Event
delete from Venue
delete from Layout
DBCC CHECKIDENT('Layout', RESEED, 0)
DBCC CHECKIDENT('EventSeat', RESEED, 0)
DBCC CHECKIDENT('Seat', RESEED, 0)
DBCC CHECKIDENT('EventArea', RESEED, 0)
DBCC CHECKIDENT('Venue', RESEED, 0)
DBCC CHECKIDENT('Event', RESEED, 0)
DBCC CHECKIDENT('Area', RESEED, 0)

/*
--- Venue
insert into dbo.Venue
values ('First venue', 'First venue address', '123 45 678 90 12')

--- Layout
insert into dbo.Layout
values (@@IDENTITY, 'First layout'),
(@@IDENTITY, 'Second layout')

--- Area
insert into dbo.Area
values (1, 'First area of first layout', 1, 1),
(1, 'Second area of first layout', 1, 1),
(1, 'First area of second layout', 4, 4)

--- Seat
insert into dbo.Seat
values (1, 1, 1),
(1, 1, 2),
(1, 1, 3),
(1, 2, 2),
(1, 2, 1)
*/