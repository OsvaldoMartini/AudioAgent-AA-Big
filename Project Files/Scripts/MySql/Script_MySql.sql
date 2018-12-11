/*
# ---------------------------------------------------------------------- #
# Target DBMS:           MySql                                           #
# Project name:          Complaint                                       #
# Author:                Osvaldo F Martini                               #
# Created on:            2017-08-09                                      #
# ---------------------------------------------------------------------- #
*/
/*

show variables like 'collation%';

CREATE DATABASE wservices_portal CHARACTER SET utf8 COLLATE utf8_general_ci;

SHOW CREATE DATABASE wservices_portal; 

SHOW CREATE TABLE tablename; //Show Description of the table

*/

/*

DROP PROCEDURE IF EXISTS `drop_all_tables`;

DELIMITER $$
CREATE PROCEDURE `drop_all_tables`()
BEGIN
    DECLARE _done INT DEFAULT FALSE;
    DECLARE _tableName VARCHAR(255);
    DECLARE _cursor CURSOR FOR
        SELECT table_name 
        FROM information_schema.TABLES
        WHERE table_schema = SCHEMA();
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET _done = TRUE;

    SET FOREIGN_KEY_CHECKS = 0;

    OPEN _cursor;

    REPEAT FETCH _cursor INTO _tableName;

    IF NOT _done THEN
        SET @stmt_sql = CONCAT('DROP TABLE ', _tableName);
        PREPARE stmt1 FROM @stmt_sql;
        EXECUTE stmt1;
        DEALLOCATE PREPARE stmt1;
    END IF;

    UNTIL _done END REPEAT;

    CLOSE _cursor;
    SET FOREIGN_KEY_CHECKS = 1;
END$$

DELIMITER ;

call drop_all_tables(); 

DROP PROCEDURE IF EXISTS `drop_all_tables`;
*/


USE db_a418f0_martini;
/*
CREATE USER IF NOT EXISTS 'db_a418f0_martini'@'%' IDENTIFIED BY 'martini38';
GRANT SELECT,INSERT,UPDATE,DELETE,CREATE,DROP ON db_a418f0_martini.* TO 'db_a418f0_martini'@'%';
GRANT ALTER ROUTINE, CREATE ROUTINE, EXECUTE ON *.* TO 'db_a418f0_martini'@'%' ;
FLUSH PRIVILEGES;
*/

SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS `db_a418f0_martini`.`CommentAssigned`; 
DROP TABLE IF EXISTS `db_a418f0_martini`.`CustomerComment`; 
DROP TABLE IF EXISTS `db_a418f0_martini`.`ServiceTechnology`; 
DROP TABLE IF EXISTS `db_a418f0_martini`.`StatusComment`; 
DROP TABLE IF EXISTS `db_a418f0_martini`.`Employee`;
DROP TABLE IF EXISTS `db_a418f0_martini`.`Company`;
DROP procedure IF EXISTS `PROC_DELETE_STATUS_COMMENT`;
DROP procedure IF EXISTS `PROC_DELETE_SERVICE_TECHNOLOGY`;
DROP procedure IF EXISTS `PROC_DELETE_COMMENT_ASSIGNED`;
DROP procedure IF EXISTS `PROC_DELETE_EMPLOYEE`;
DROP procedure IF EXISTS `PROC_DELETE_CUSTOMER_COMMENT`;
DROP procedure IF EXISTS `PROC_INSERT_COMMENT_ASSIGNED`;
DROP procedure IF EXISTS `PROC_INSERT_CUSTOMER_COMMENT`;
DROP procedure IF EXISTS `PROC_INSERT_EMPLOYEE`;
DROP procedure IF EXISTS `PROC_UPDATE_EMPLOYEE`;
DROP procedure IF EXISTS `PROC_UPDATE_SERVICE_TECHNOLOGY`;
DROP procedure IF EXISTS `PROC_UPDATE_CUSTOMER_COMMENT`;
DROP procedure IF EXISTS `PROC_UPDATE_COMMENT_ASSIGNED`;
SET FOREIGN_KEY_CHECKS = 1; 




/****** Object:  Table Company    Script Date: 08/07/2017 09:07:02 ******/
CREATE TABLE Company(
	CompanyID int AUTO_INCREMENT NOT NULL,
	Name varchar(100) NOT NULL,
	Address varchar(250) NOT NULL,
	PostCode varchar(10) NOT NULL,
	State  varchar(50) NOT NULL,
	Country  varchar(50) NOT NULL,
	Email varchar(255) NOT NULL,
	WebSite varchar(255) NOT NULL,
	Phone varchar(20) NOT NULL,
	DateCreated datetime NULL,
CONSTRAINT PK_Employee PRIMARY KEY CLUSTERED 
(
	CompanyID ASC
)
)
;


/****** Object:  Table Users    Script Date: 08/07/2017 09:07:02 ******/
CREATE TABLE Employee(
	EmployeeID int AUTO_INCREMENT NOT NULL,
	CompanyID int NOT NULL,
	LastName varchar(30) NOT NULL,
	FirstName varchar(20) NOT NULL,
	UserName varchar(20) NOT NULL,
	Email varchar(255) NOT NULL,
	Password varchar(20) NOT NULL,
	RoleID int NOT NULL,
	DateCreated datetime NULL,
CONSTRAINT PK_Employee PRIMARY KEY CLUSTERED 
(
	EmployeeID ASC
)
)
;

/****** Object:  Table Status_Comments    Script Date: 08/07/2017 09:07:02 ******/

CREATE TABLE StatusComment(
	StatusID int AUTO_INCREMENT NOT NULL,
	CompanyID int NOT NULL,
	StatusName varchar(255) NULL,
 CONSTRAINT PK_StatusComment PRIMARY KEY CLUSTERED 
(
	StatusID ASC
));





/****** Object:  Table Services_Technolgy    Script Date: 08/07/2017 09:07:02 ******/

CREATE TABLE ServiceTechnology(
	ServiceID int AUTO_INCREMENT NOT NULL,
	CompanyID int NOT NULL,
	ServiceDept varchar(30) NOT NULL,
	ServiceName varchar(30) NOT NULL,
 CONSTRAINT PK_ServiceTechnology PRIMARY KEY CLUSTERED 
(
	ServiceID ASC
));



/****** Object:  Table Customers_Comments    Script Date: 08/07/2017 09:07:02 ******/

CREATE TABLE CustomerComment(
	CommentID int AUTO_INCREMENT NOT NULL,
	CompanyID int NOT NULL,
	Email varchar(255) NULL,
	LastName varchar(30) NULL,
	FirstName varchar(20) NULL,
	Mobile varchar(20) NULL,
	LevelComment int NOT NULL,
	ServiceID int NOT NULL,
	DateComment datetime NULL,
	Description varchar(500) NULL,
 CONSTRAINT PK_CustomerComment PRIMARY KEY CLUSTERED 
(
	CommentID ASC
));



/****** Object:  Table Comments_Assigned    Script Date: 08/07/2017 09:07:02 ******/
CREATE TABLE CommentAssigned(
	AssignedID int AUTO_INCREMENT NOT NULL,
	CompanyID int NOT NULL,
	EmployeeIDFrom int NULL,
	EmployeeIDTo int NULL,
	CommentID int NOT NULL,
	DateCreated datetime NULL,
	DateUpdated datetime NULL,
	Solution varchar(500) NULL,
    StatusID int NOT NULL,
 CONSTRAINT PK_CommentAssigned PRIMARY KEY CLUSTERED 
(
	AssignedID ASC
));




/*
# ---------------------------------------------------------------------- #
# Foreign keys                                                           #
# ---------------------------------------------------------------------- #
*/

ALTER TABLE Employee ADD CONSTRAINT FK_CompanyToEmployee 
    FOREIGN KEY (CompanyID) REFERENCES Company(CompanyID);

ALTER TABLE CommentAssigned ADD CONSTRAINT FK_CompanyToCommentAssigned 
    FOREIGN KEY (CompanyID) REFERENCES Company(CompanyID);
	
ALTER TABLE CustomerComment ADD CONSTRAINT FK_CompanyToCustomerComment 
    FOREIGN KEY (CompanyID) REFERENCES Company(CompanyID);
	
ALTER TABLE ServiceTechnology ADD CONSTRAINT FK_CompanyToServiceTechnology 
    FOREIGN KEY (CompanyID) REFERENCES Company(CompanyID);

ALTER TABLE StatusComment ADD CONSTRAINT FK_CompanyToStatusComment 
    FOREIGN KEY (CompanyID) REFERENCES Company(CompanyID);

/*
# ---------------------------------------------------------------------- #
# Foreign key EmployeeID                                                 #
# ---------------------------------------------------------------------- #
*/	
ALTER TABLE CommentAssigned ADD CONSTRAINT FK_CommentAssignEmployeeFrom 
    FOREIGN KEY (EmployeeIDFrom) REFERENCES Employee(EmployeeID);

ALTER TABLE CommentAssigned ADD CONSTRAINT FK_CommentAssignEmployeeTo 
    FOREIGN KEY (EmployeeIDTo) REFERENCES Employee(EmployeeID);

/*
# ---------------------------------------------------------------------- #
# Foreign key Others                                                     #
# ---------------------------------------------------------------------- #
*/
ALTER TABLE CommentAssigned ADD CONSTRAINT FK_CommentAssStatus 
    FOREIGN KEY (StatusID) REFERENCES StatusComment(StatusID);

ALTER TABLE CommentAssigned ADD CONSTRAINT FK_CommAssComm 
    FOREIGN KEY (CommentID) REFERENCES CustomerComment(CommentID);

ALTER TABLE CustomerComment ADD CONSTRAINT FK_CusmtCommServic 
    FOREIGN KEY (ServiceID) REFERENCES ServiceTechnology(ServiceID);



INSERT INTO `Company` (`Name`,`Address`,`PostCode`,`State`,`Country`,`Email`,`WebSite`,`Phone`,`DateCreated`) 
VALUES('Martini-Marcas e Patentes S/C Ltda','Rua Bom Pastor, 491','04231-002','São Paulo','Brasil','martini-marcas@uol.com.br','martini-marcas.com.br', '+551120689595', CURRENT_TIMESTAMP());

INSERT INTO `Company` (`Name`,`Address`,`PostCode`,`State`,`Country`,`Email`,`WebSite`,`Phone`,`DateCreated`) 
VALUES('Hillgate Travel','Stephenson House, 75 Hampstead Road','NW1 2PL','London','United Kingdom','https://www8.hillgatetravel.com/','Ceri.Edwards@hillgatetravel.com', '+44(0)2077538811', CURRENT_TIMESTAMP());

/* Copany Martini Marcas   */
INSERT INTO `Employee` (`CompanyID`,`LastName`,`FirstName`,`UserName`,`Email`,`Password`,`RoleID`,`DateCreated`) VALUES(1, 'Martini','Osvaldo','martini','martini.marcas@uol.com.br','martini', 1, CURRENT_TIMESTAMP());
INSERT INTO `Employee` (`CompanyID`,`LastName`,`FirstName`,`UserName`,`Email`,`Password`,`RoleID`,`DateCreated`) VALUES(1, 'Admin','Administrator','admin','osvaldo.martini@gmail.com','admin', 1, CURRENT_TIMESTAMP());

/* Copany Hillgate Travel   */
INSERT INTO `Employee` (`CompanyID`,`LastName`,`FirstName`,`UserName`,`Email`,`Password`,`RoleID`,`DateCreated`) VALUES(2, 'Martini','Osvaldo','omartini','osvaldo.martini@gmail.com','martini', 1, CURRENT_TIMESTAMP());
INSERT INTO `Employee` (`CompanyID`,`LastName`,`FirstName`,`UserName`,`Email`,`Password`,`RoleID`,`DateCreated`) VALUES(2, 'Smith','John','Supervisor','claudiabhz@gmail.com','123456', 2, CURRENT_TIMESTAMP());
INSERT INTO `Employee` (`CompanyID`,`LastName`,`FirstName`,`UserName`,`Email`,`Password`,`RoleID`,`DateCreated`) VALUES(2, 'Claudia','Rezende','claudia','claudiabhz@gmail.com','123456', 3, CURRENT_TIMESTAMP());
INSERT INTO `Employee` (`CompanyID`,`LastName`,`FirstName`,`UserName`,`Email`,`Password`,`RoleID`,`DateCreated`) VALUES(2, 'Rodrigo','Silver','Silver','martini.dev.architect@gmail.com','123456', 4, CURRENT_TIMESTAMP());

INSERT INTO `ServiceTechnology` (`CompanyID`, `ServiceDept`,`ServiceName`) VALUES(2,'SERVICE', 'Executive Leisure');
INSERT INTO `ServiceTechnology` (`CompanyID`, `ServiceDept`,`ServiceName`)  VALUES(2,'SERVICE', 'Passport & Visa');
INSERT INTO `ServiceTechnology` (`CompanyID`, `ServiceDept`,`ServiceName`)  VALUES(2,'SERVICE', 'Foreign Exchange');
INSERT INTO `ServiceTechnology` (`CompanyID`, `ServiceDept`,`ServiceName`)  VALUES(2,'SERVICE', 'VIP Service');
INSERT INTO `ServiceTechnology` (`CompanyID`, `ServiceDept`,`ServiceName`)  VALUES(2,'SERVICE', 'Concierge');
INSERT INTO `ServiceTechnology` (`CompanyID`, `ServiceDept`,`ServiceName`)  VALUES(2,'TECHNOLOGY', 'GATEWAY');
INSERT INTO `ServiceTechnology` (`CompanyID`, `ServiceDept`,`ServiceName`)  VALUES(2,'TECHNOLOGY', 'FREEWAY');
INSERT INTO `ServiceTechnology` (`CompanyID`, `ServiceDept`,`ServiceName`)  VALUES(2,'TECHNOLOGY', 'MIWAY ANALYTICS');


INSERT INTO `StatusComment` (`CompanyID`, `StatusName`) VALUES(2,'Open');
INSERT INTO `StatusComment` (`CompanyID`, `StatusName`) VALUES(2,'In Progress');
INSERT INTO `StatusComment` (`CompanyID`, `StatusName`) VALUES(2,'Investigating');
INSERT INTO `StatusComment` (`CompanyID`, `StatusName`) VALUES(2,'Re-Assigned');	
INSERT INTO `StatusComment` (`CompanyID`, `StatusName`) VALUES(2,'Closed');

	
	

/****** Object:  StoredProcedure PROC_UPDATE_SERVICE_TECHNOLOGY    Script Date: 08/22/2017 07:02:44 ******/

DELIMITER $$
CREATE PROCEDURE `PROC_UPDATE_SERVICE_TECHNOLOGY`(
in P_CompanyID int,
in P_ServiceID int,
in P_ServiceName varchar(30),
out P_Return_Message varchar(500)
)
BEGIN
declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
        set P_Return_Message = 'Operation Not Allowed!';
    end;
  
start transaction;
    
        UPDATE ServiceTechnology
                        SET ServiceName = P_ServiceName
                         Where 
						 CompanyID = P_CompanyID and
		    			 ServiceID = P_ServiceID;
         
    commit;

END$$

DELIMITER ;
	

/****** Object:  StoredProcedure PROC_UPDATE_EMPLOYEE    Script Date: 08/22/2017 07:02:44 ******/
DELIMITER $$
CREATE PROCEDURE `PROC_UPDATE_EMPLOYEE`(
in P_CompanyID int,
in P_EmployeeID int,
in P_LastName varchar(30),
in P_FirstName varchar(20),
in P_UserName varchar(20),
in P_Email varchar(255),
in P_Password varchar(20),
in P_RoleID int,
out P_Return_Message varchar(500) 
)
BEGIN

declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
        set P_Return_Message = 'Operation Not Allowed!';
    end;
  
start transaction;
    
         UPDATE Employee
                        SET LastName = P_LastName
                         ,FirstName = P_FirstName
                         ,UserName = P_UserName
                         ,Email = P_Email
                         ,Password = P_Password
                         ,RoleID = P_RoleID
                         Where 
						 CompanyID = P_CompanyID and
		    			 EmployeeID = P_EmployeeID;
                         
         
    commit;
END$$

DELIMITER ;




/****** Object:  StoredProcedure PROC_UPDATE_CUSTOMER_COMMENT    Script Date: 08/22/2017 07:02:40 ******/
DELIMITER $$
CREATE PROCEDURE `PROC_UPDATE_CUSTOMER_COMMENT`(
in P_CompanyID int,
in P_CommentID int,
in P_Email varchar(255) ,
in P_LastName varchar(30) ,
in P_FirstName varchar(20) ,
in P_Mobile varchar(20) ,
in P_LevelComment int ,
in P_ServiceID int ,
in P_Description varchar(500),
out P_Return_Message varchar(500)
)
BEGIN
declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
        set P_Return_Message = 'Operation Not Allowed!';
    end;
  
start transaction;

     UPDATE CustomerComment 
                    SET Email = IFNULL(P_Email , Email )
                         ,LastName = IFNULL(P_LastName , LastName )
                         ,FirstName = IFNULL(P_FirstName, FirstName ) 
                         ,Mobile = IFNULL(P_Mobile , Mobile  )
                         ,LevelComment = IFNULL(P_LevelComment ,LevelComment )
                         ,ServiceID = IFNULL(P_ServiceID , ServiceID)
                         ,Description = IFNULL(P_Description ,Description )
                       Where 
					    CompanyID = P_CompanyID and
		    		    CommentID = P_CommentID;
 
         
    commit;
END$$

DELIMITER ;





/****** Object:  StoredProcedure PROC_UPDATE_COMMENT_ASSIGNED    Script Date: 08/22/2017 07:02:35 ******/
DELIMITER $$
CREATE PROCEDURE `PROC_UPDATE_COMMENT_ASSIGNED` (
in P_CompanyID int,
in P_AssignedID int,
in P_EmployeeIDFrom int ,
in P_EmployeeIDTo int ,
in P_DateUpdated DateTime ,
in P_Solution varchar(500) ,
in P_StatusID int,
out P_Return_Message varchar(500) 
)
BEGIN
declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
        set P_Return_Message = 'Operation Not Allowed!';
    end;
  
start transaction;

    
		UPDATE CommentAssigned 
			SET EmployeeIDFrom = ifnull(P_EmployeeIDFrom, EmployeeIDFrom )
           ,EmployeeIDTo = ifnull(P_EmployeeIDTo, EmployeeIDTo )
           ,DateUpdated = ifnull(P_DateUpdated, DateUpdated )
           ,Solution = ifnull(P_Solution, Solution )
		   ,StatusID = ifnull(P_StatusID, StatusID) 
           where
            CompanyID = P_CompanyID and
		    AssignedID = P_AssignedID;
 
         
    commit;
END$$

DELIMITER ;

/****** Object:  StoredProcedure PROC_INSERT_EMPLOYEE    Script Date: 08/22/2017 07:02:32 ******/
DELIMITER $$
CREATE PROCEDURE `PROC_INSERT_EMPLOYEE`(
in P_CompanyID int,
in P_LastName varchar(20),
in P_FirstName varchar(30),
in P_UserName varchar(20),
in P_Email varchar(255),
in P_Password varchar(20),
in P_RoleID int,
out P_int_Identity int,
out P_Return_Message varchar(500)
)
BEGIN
declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
set P_Return_Message = 'Operation Not Allowed!';
    end;  
  
start transaction;
    
INSERT INTO Employee 
			(CompanyID
			,FirstName 
           ,LastName
           ,UserName 
           ,Email 
           ,Password
           ,RoleID
           ,DateCreated)
			VALUES (
			 P_CompanyID
			,P_FirstName 
			,P_LastName 
			,P_UserName 
			,P_Email
			,P_Password 
			,P_RoleID 
			,CURRENT_TIMESTAMP()
			);
 
         SET P_int_Identity = LAST_INSERT_ID();
    commit;

END$$

DELIMITER ;





/****** Object:  StoredProcedure PROC_INSERT_CUSTOMER_COMMENT    Script Date: 08/22/2017 07:02:27 ******/
DELIMITER $$
CREATE PROCEDURE `PROC_INSERT_CUSTOMER_COMMENT`(
in P_CompanyID int,
in P_Email varchar(255) ,
in P_LastName varchar(30) ,
in P_FirstName varchar(20) ,
in P_Mobile varchar(20),
in P_LevelComment int,
in P_ServiceID int,
in P_Description  varchar(500) ,
out P_int_Identity int,
out P_Return_Message varchar(500)
)
BEGIN
declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
		set P_Return_Message = 'Operation Not Allowed!';
    end;
	

	

start transaction;

    INSERT INTO CustomerComment 
			(
			 CompanyID
			,Email
			,LastName 
			,FirstName 
			,Mobile 
			,LevelComment
			 ,ServiceID 
			 ,DateComment 
			 ,Description) 
			VALUES (
			P_CompanyID
			,P_Email
			,P_LastName 
			,P_FirstName 
			,P_Mobile 
			,P_LevelComment 
			,P_ServiceID 
			,CURRENT_TIMESTAMP()
			,P_Description);
			
		    SET P_int_Identity = LAST_INSERT_ID();
         
    commit;

END$$

DELIMITER ;




/****** Object:  StoredProcedure PROC_INSERT_COMMENT_ASSIGNED    Script Date: 08/22/2017 07:02:22 ******/
DELIMITER $$
CREATE PROCEDURE `PROC_INSERT_COMMENT_ASSIGNED` (
in P_CompanyID int,
in P_EmployeeIDFrom int,
in P_EmployeeIDTo int,
in P_CommentID int,
in P_Solution varchar(500),
in P_StatusID int,
out P_int_Identity int ,
out P_Return_Message varchar(500)
)
BEGIN
declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
        set P_Return_Message = 'Operation Not Allowed!';
    end;

start transaction;

	INSERT INTO CommentAssigned 
			(
			CompanyID
			,EmployeeIDFrom
			,EmployeeIDTo
			,CommentID
            ,DateCreated
		    ,StatusID
		    ,Solution
		   )
			VALUES (
			P_CompanyID
			,P_EmployeeIDFrom
			,P_EmployeeIDTo
			,P_CommentID 
			,CURRENT_TIMESTAMP()
			,P_StatusID			
			,P_Solution
			);
			

   SET P_int_Identity = LAST_INSERT_ID();
commit;
END$$

DELIMITER ;



/****** Object:  StoredProcedure PROC_DELETE_CUSTOMER_COMMENT    Script Date: 09/18/2017 04:23:25 ******/
DELIMITER $$
CREATE PROCEDURE `PROC_DELETE_CUSTOMER_COMMENT`(
in P_CompanyID int,
in P_CommentID int,
out P_Return_Message varchar(500) 
)
BEGIN
declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
set P_Return_Message = 'Operation Not Allowed!';
    end;

start transaction;
	
DELETE FROM CustomerComment 
WHERE
	CompanyID = P_CompanyID and
    CommentID = P_CommentID;
commit;

END$$

DELIMITER ;



/****** Object:  StoredProcedure PROC_DELETE_EMPLOYEE    Script Date: 09/18/2017 04:44:44 ******/
DELIMITER $$
CREATE PROCEDURE `PROC_DELETE_EMPLOYEE`(
in P_CompanyID int,
in P_EmployeeID int,
out P_Return_Message varchar(500) 
)
BEGIN

declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
set P_Return_Message = 'Operation Not Allowed!';
    end;

start transaction;


	DELETE FROM Employee 
WHERE
    CompanyID = P_CompanyID and
    EmployeeID = P_EmployeeID;

commit;


END$$

DELIMITER ;





/****** Object:  StoredProcedure PROC_DELETE_COMMENT_ASSIGNED    Script Date: 09/18/2017 04:46:27 ******/
DELIMITER $$
CREATE PROCEDURE `PROC_DELETE_COMMENT_ASSIGNED`(
in P_CompanyID int,
in P_AssignedID int,
out P_Return_Message varchar(500)
)
BEGIN


declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
		set P_Return_Message = 'Operation Not Allowed!';
    end;

start transaction;


		
		DELETE FROM CommentAssigned 
		WHERE
			CompanyID = P_CompanyID and
			AssignedID = P_AssignedID;

commit;

END$$

DELIMITER ;





/****** Object:  StoredProcedure PROC_DELETE_SERVICE_TECHNOLOGY    Script Date: 09/18/2017 04:47:41 ******/
DELIMITER $$
CREATE PROCEDURE `PROC_DELETE_SERVICE_TECHNOLOGY`(
in P_CompanyID int,
in P_ServiceID int,
out P_Return_Message varchar(500)
)
BEGIN


declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
set P_Return_Message = 'Operation Not Allowed!';
    end;

start transaction;


		
		Delete from ServiceTechnology 
		where 
			CompanyID = P_CompanyID and
			ServiceID   = P_ServiceID;

commit;
END$$

DELIMITER ;






/****** Object:  StoredProcedure PROC_DELETE_STATUS_COMMENT    Script Date: 09/18/2017 04:48:52 ******/
DELIMITER $$
CREATE PROCEDURE `PROC_DELETE_STATUS_COMMENT` (
in P_CompanyID int,
in P_StatusID int,
out P_Return_Message varchar(500)
)
BEGIN

declare exit handler for SQLEXCEPTION
    begin
        ROLLBACK;
set P_Return_Message = 'Operation Not Allowed!';
    end;

start transaction;
	
		DELETE FROM StatusComment 
		WHERE			
			CompanyID = P_CompanyID and
			StatusID = P_StatusID;

commit;
END$$

DELIMITER ;


