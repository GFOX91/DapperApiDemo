/*
Post-Deployment Script						
--------------------------------------------------------------------------------------
Populates the users DB with Dummy Data if none already exists			
--------------------------------------------------------------------------------------
*/

if not exists (select 1 from dbo.[user])
begin
	insert into dbo.[user] (FirstName, LastName)
	values ('Greg', 'Fox'),
	('Lucy', 'Ellis'),
	('Samwise', 'Ceaser'),
	('John', 'Smith'),
	('Mary', 'Jones');
end 
