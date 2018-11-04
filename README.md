# Task-Managment-MVC-Project
MVC project that manages tasks 

1- Change the connection string in the webConfig to match yours 
2- Open Package Manager Console in Visual Studio run the following command : update-database

3- Run the following Stored Procedure in your SQL Server.
ALTER PROCEDURE [dbo].[AllTasksReport]
AS
SELECT t.ID , t.Description, ts.Status , usr.UserName as Creator, assigUsr.UserName as AssignedUsere,t.[File]  FROM  Task t
left join TaskStatus ts on t.TaskStatusID = ts.ID
left join [dbo].[User] usr on t.UserID = usr.ID
left join [dbo].[User] assigUsr on t.AssignedUserID = assigUsr.ID


4- Run the Project 