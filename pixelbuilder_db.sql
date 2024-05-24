/* check wether the database exists; if so, drop it */
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
                   WHERE name = 'pixelbuilder_db')

BEGIN






print '' print ''
print '/////// NOW CREATING :: DATABASE ///////'
print ''






DROP DATABASE pixelbuilder_db
print '' print '*** dropping database [PIXELBUILDER_DB]'
END
GO

print '' print '*** creating database [PIXELBUILDER_DB]'
GO
CREATE DATABASE pixelbuilder_db
GO

print '' print '*** using [PIXELBUILDER_DB]'
GO
USE [pixelbuilder_db]
GO






print '' print ''
print '/////// NOW CREATING :: TABLES AND TEST DATA - IMPLEMENTED ///////'
print ''






print '' print '*** creating [USER] table'
GO
CREATE TABLE [dbo].[User] 
(
    [UserID]        [int] IDENTITY(10000,1)     NOT NULL,
    [Username]      [nvarchar](25)              NOT NULL,
    [DisplayName]   [nvarchar](25)              NOT NULL DEFAULT 'New User',
    [ShortBio]      [nvarchar](140)             NOT NULL DEFAULT 'Welcome to my page!',
    [Email]         [nvarchar](100)             NOT NULL,
    [PasswordHash]  [nvarchar](100)             NOT NULL DEFAULT
        '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
    [CreatedOn]     [datetime]                  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    [UpdatedOn]     [datetime]                  NULL,
    [Active]        [bit]                       NOT NULL DEFAULT '1',
        /* constraints */
    CONSTRAINT      [pk_User]                        PRIMARY KEY([UserID]),
    CONSTRAINT      [ak_User_Username]               UNIQUE([Username]),
    CONSTRAINT      [ak_User_Email]                  UNIQUE([Email])
)
GO

/* User Test Records */
print '' print '*** inserting test records into [USER]'
GO

/* dont use the * value for this - jim will kill you */
INSERT INTO [dbo].[User]
        ([Username], [DisplayName], [Email])
    VALUES
        ( 'cuboied', 'avid robot enthusiast','mads@pixelbuilder.com'),
        ( 'memo', 'shrimpLiker','memo@pixelbuilder.com'),
        ( 'doodleBug', 'doodleBug' ,'drew@pixelbuilder.com'),
        ( 'stinkypete20', '[SNIFFS YOU] >:3' ,'pete@pixelbuilder.com'),
        ( 'VerySmart', 'MIT alum','ramiro@pixelbuilder.com'),
        ( 'tippy', 'put me down','mitch@dog.pet'),
        ( 'TunnieTyne', '*knocks drink over* oops','tungsten@kitty.cat'),
        ( 'WHEWnoBird', 'Whew! Whew-Whew!','uno@cockatiel.bird'),
        ( 'smartestOfTheBunch', 'millets overrated tbh','professor@parakeet.bird'),
        ( 'DancingKing', '#1 Dancer in Iowa','cloudy@parakeet.bird'),
        ( 'lilyellow', 'Banana Man 🍌','peep@parakeet.bird'),
        ( 'WallyWalnut', 'lemon&lime','wallace@parakeet.bird'),
        ( 'oldgameSprites', 'Earthbound4Ever','ness@pixelbuilder.com')
GO

print '' print '*** creating [ROLE] table'
GO
CREATE TABLE [dbo].[Role] 
(
    [RoleID]        [nvarchar](50)              NOT NULL,
    [Description]   [nvarchar](250)             NULL,
    /* constraints */
    CONSTRAINT      [pk_Role]                 PRIMARY KEY ([RoleID])
)
GO

print '' print '*** inserting test records into [ROLE]'
GO
INSERT INTO [dbo].[Role]
        ([RoleID], [Description])
    VALUES
        ('User', 'Just your run of the mill general user.'),
        ('Moderator','User that can delete art and suspend other users.'),
        ('Admin','God.')
GO

/* UserRole join table to join User and Role */

print '' print '*** creating [USERROLE] table'
GO
CREATE TABLE [dbo].[UserRole] 
(
    [UserID]        [int]                       NOT NULL,
    [RoleID]        [nvarchar](50)              NOT NULL DEFAULT 'User',             
    /* constraints */
    CONSTRAINT      [fk_UserRole_UserID]        FOREIGN KEY ([UserID])          REFERENCES [dbo].[User]([UserID]),
    CONSTRAINT      [fk_UserRole_RoleID]        FOREIGN KEY ([RoleID])          REFERENCES [dbo].[Role]([RoleID]),
    
    /* primary composite key */
    CONSTRAINT      [pk_UserRole]               PRIMARY KEY ([UserID], [RoleID])
)
GO


print '' print '*** inserting test records into [USERROLE]'
GO
INSERT INTO [dbo].[UserRole]
        ([UserID], [RoleID])
    VALUES
        (10000, 'Admin'),
        (10001, 'Moderator')
GO

/* Canvas */
print '' print '*** creating [ART] table'
GO
CREATE TABLE [dbo].[Art]
(
    [ArtID]          [int] IDENTITY(1000000,1)   NOT NULL,
    [UserID]         [int]                       NOT NULL,
    [ArtName]        [nvarchar](25)              NOT NULL,
    [Description]    [nvarchar](250)             NOT NULL DEFAULT 'Look what I made!',
    [PostedOn]       [datetime]                  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    [UpdatedOn]      [datetime]                  NULL,
    [Hidden]         [bit]                       NOT NULL DEFAULT '1',

    CONSTRAINT      [fk_Art_UserID]           FOREIGN KEY([UserID])       REFERENCES [dbo].[User]([UserID]),

    CONSTRAINT      [pk_ArtID]                PRIMARY KEY ([ArtID]),
    CONSTRAINT      [ak_Art_UserImage]        UNIQUE([UserID],[ArtName])
)

print '' print '*** inserting test records into [ART]'
GO
INSERT INTO [dbo].[Art]
        ([UserID], [ArtName])
    VALUES
        ('10011', 'defaultIcon'), /* 00 */
        ('10000', 'crab'), /* 01 */
        ('10000', 'teardrop'), /* 02 */
        ('10001', 'fishy'), /* 03 */
        ('10001', 'bird'), /* 04 */
        ('10002', 'tongue'), /* 05 */
        ('10002', 'ghost'), /* 06 */
        ('10003', 'evil bird'), /* 07 */
        ('10006', 'heart'), /* 08 */
        ('10007', 'orange'), /* 09 */
        ('10010', 'star'), /* 10 */
        ('10012', 'crow'), /* 11 */
        ('10012', 'dog'), /* 12 */
        ('10012', 'duck'), /* 13 */
        ('10012', 'now i LIKE this guy'), /* 14 */
        ('10012', 'painting'), /* 15 */
        ('10012', 'shapes'), /* 16 */
        ('10012', 'snake'), /* 17 */
        ('10012', 'spider'), /* 18 */
        ('10012', 'atoms') /* 19 */
GO

/* userfavorite */
print '' print '*** creating [USERFAVORITE] table'
GO
CREATE TABLE [dbo].[UserFavorite]
(
    [UserID]        [int]       NOT NULL,
    [ArtID]         [int]       NOT NULL,

    CONSTRAINT      [fk_UserFavorite_UserID]    FOREIGN KEY([UserId])       REFERENCES [dbo].[User]([UserID]),
    CONSTRAINT      [fk_UserFavorite_ArtID]     FOREIGN KEY([ArtID])        REFERENCES [dbo].[Art]([ArtID]),

    CONSTRAINT      [pk_UserFavorite]           PRIMARY KEY([UserID],[ArtID])    
)

print '' print '*** inserting test records into [USERFAVORITE]'
GO
INSERT INTO [dbo].[UserFavorite]
        ([UserID], [ArtID])
    VALUES
        (10001, 1000000) /* 'memo' bookmarked 'heart'!*/
GO

/* USERFOLLOWING */
print '' print '*** creating [USERFOLLOWING] table'
GO
CREATE TABLE [dbo].[UserFollowing]
(
    [UserID]        [int]       NOT NULL,
    [FollowingID]   [int]       NOT NULL,
    [FollowedOn]    [datetime]  NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT      [fk_UserFollowing_UserID]       FOREIGN KEY([UserID])          REFERENCES [dbo].[User]([UserID]),
    CONSTRAINT      [fk_UserFollowing_FollowingID]  FOREIGN KEY([FollowingID])     REFERENCES [dbo].[User]([UserID]),

    CONSTRAINT      [pk_UserFollowing]           PRIMARY KEY([UserID],[FollowingID])  
)
GO

print '' print '*** inserting test records into [USERFOLLOWING]'
GO
INSERT INTO [dbo].[UserFollowing]
        ([UserID], [FollowingID])
    VALUES
        (10000, 10001), /* cuboied following memo */
        (10001, 10000), /* memo following cuboied */
        (10002, 10001)  /* johnwicklover1994 following memo */
GO

print '' print '*** creating [USERICON] table'
GO
CREATE TABLE [dbo].[UserIcon]
(
    [UserID]        [int]       NOT NULL,
    [ArtID]         [int]       NOT NULL DEFAULT '1000000'

    CONSTRAINT      [fk_UserIcon_UserID]        FOREIGN KEY([UserID])       REFERENCES [dbo].[User]([UserID]),
    CONSTRAINT      [fk_UserIcon_ArtID]         FOREIGN KEY([ArtID])        REFERENCES [dbo].[Art]([ArtID])
)

print '' print '*** inserting test records into [USERICON]'
GO
INSERT INTO [dbo].[UserIcon]
        ([UserID])
    VALUES
        ('10000'),
        ('10001'),
        ('10002')
GO




print '' print ''
print '/////// NOW CREATING :: TABLES AND TEST DATA - NOT FULL STACK ///////'
print ''






/* Tag table for posts */
print '' print '*** creating [TAG] table'
GO
CREATE TABLE [dbo].[Tag]
(
    [TagName]   [nvarchar](50)                 NOT NULL,

    /* constraints*/
    CONSTRAINT      [pk_Tag]                PRIMARY KEY ([TagName])
)
GO


print '' print '*** inserting test records into [TAG]'
GO
INSERT INTO [dbo].[Tag]
        ([TagName])
    VALUES
        ('Pokemon'),
        ('Animal'),
        ('CubeAnimalCrossing'),
        ('Puppy'),
        ('WIP'),
        ('Shapes'),
        ('Hearts')
GO

/* brand */
print '' print '*** creating [BRAND] table'
GO
CREATE TABLE [dbo].[Brand]
(
    [BrandID]       [int] IDENTITY(1000,1)      NOT NULL,
    [BrandName]     [nvarchar](50)              NOT NULL,

    CONSTRAINT      [pk_Brand]                  PRIMARY KEY([BrandID])
)

print '' print '*** inserting test records into [BRAND]'
GO
INSERT INTO [dbo].[Brand]
        ([BrandName])
    VALUES
        ('Perler'), /* 1000 */
        ('Artkal'), /* 1001 */
        ('Transparent') /* 1002 */
GO

/* colorgroup */
print '' print '*** creating [COLORGROUP] table'
GO
CREATE TABLE [dbo].[ColorGroup]
(
    [ColorGroupID]      [int] IDENTITY(10,1)    NOT NULL,
    [ColorGroupName]    [nvarchar](15)          NOT NULL,

    /* constraints */
    CONSTRAINT      [pk_ColorGroup]             PRIMARY KEY([ColorGroupID])
)

print '' print '*** inserting test records into [COLORGROUP]'
GO
INSERT INTO [dbo].[ColorGroup]
        ([ColorGroupName])
    VALUES
        ('Red & Pink'), /* 10 */
        ('Purple'), /* 11 */
        ('Blue'), /* 12 */
        ('Green'), /* 13 */
        ('Yellow'), /* 14 */
        ('Orange'), /* 15 */
        ('White'), /* 16 */
        ('Grey'), /* 17 */
        ('Black'), /* 18 */
        ('Brown & Tan') /* 19 */
GO

/* bead */
print '' print '*** creating [BEAD] table'
GO
CREATE TABLE [dbo].[Bead]
(
    [BeadID]        [nvarchar](25)      NOT NULL,
    [ColorName]     [nvarchar](100)     NOT NULL,
    [ColorGroupID]  [int]               NOT NULL,
    [BrandID]       [int]               NOT NULL,
    [HexValue]      [nvarchar](8)       NOT NULL,

    /* constraints */
    CONSTRAINT      [pk_Bead]                   PRIMARY KEY ([BeadID]),
    CONSTRAINT      [fk_Bead_ColorGroupID]      FOREIGN KEY ([ColorGroupID])    REFERENCES [dbo].[ColorGroup]([ColorGroupID]),
    CONSTRAINT      [fk_Bead_BrandID]           FOREIGN KEY ([BrandID])      REFERENCES [dbo].[Brand]([BrandID])
)
GO


print '' print '*** inserting test records into [BEAD]'
GO
INSERT INTO [dbo].[Bead]
        ([BeadID], [ColorName], [ColorGroupID], [BrandID], [HexValue])
    VALUES
        ('80-15251','Thistle', '11', '1000','9998AF'),
        ('80-15259','Slime', '13', '1000','C4CE1E'),
        ('80-15258','Mulberry', '11', '1000','6D3B68'),
        ('S09','Pistachio', '13', '1001','90DD8A'),
        ('P60', 'Plum', '11', '1000', 'B25FAA'),
        ('P05', 'Red', '10', '1000', 'C43A44'),
        ('S24', 'True Blue', '12', '1001', '0078C2'),
        ('S45', 'Medium Turquoise', '12', '1001', '00A79E'),
        ('S65', 'Canary', '14', '1001', 'F4F141'),
        ('S48', 'Corn', '14', '1001', 'FFC856'),
        ('S66', 'Blaze Orange', '15', '1001', 'FF644F'),
        ('P48', 'Neon Orange', '15', '1000', 'FF8D2E'),
        ('80-19001', 'White', '16', '1000', 'F7F7F2'),
        ('80-19018', 'Black', '18', '1000',	'343234'),
        ('80-15207', 'Charcoal', '17', '1000', '545F5F'),
        ('80-15206', 'Pewter', '17', '1000', '93A19F'),
        ('80-19092'	, 'Dark Grey', '17', '1000', '474545'),
        ('80-19017', 'Grey', '17','1000','989999'),
        ('80-15181', 'Light Grey',	'17', '1000', 'D3D3CB'),
        ('80-19019', 'Clear', '17', '1000',	'9EA7AE50'),
        ('80-19012', 'Brown', '19', '1000',	'523529'),
        ('80-19021', 'Light Brown',	'19', '1000', '9E714B'),
        ('80-19085', 'Gold Metallic', '19', '1000',	'B57F45'),
        ('80-19035', 'Tan', '19', '1000','CFA889'),
        ('80-15205', 'Fawn', '19', '1000','D7B087'),
        ('80-15208', 'Toasted Marshmallow',	'16', '1000', 'F1E5D8'),
        ('80-19060', 'Plum'	,'11', '1000', 'B25FAA'),
        ('80-15210', 'Orchid', '11', '1000', 'B56C99'),
        ('80-19007', 'Purple', '11', '1000', '6F5493'),
        ('80-19054', 'Pastel Lavender', '11', '1000', '9582BB'),
        ('80-15182', 'Lavender', '11', '1000', 'B4A6D3'),
        ('80-19070', 'Periwinkle', '12', '1000', '6C88BF'),
        ('80-19093', 'Blueberry Creme', '12', '1000', '87A7E1'),
        ('80-15201', 'Midnight', '12', '1000', '162846'),
        ('80-19008', 'Dark Blue', '12', '1000', '2B307C'),
        ('80-15200', 'Cobalt', '12', '1000', '0066B3'),
        ('80-19009', 'Light Blue', '12', '1000', '278ACB'),
        ('80-19062', 'Turquoise', '12',	'1000',	'008FCC'),
        ('80-19052', 'Clear Blue', '12', '1000', '7CD2F250'),
        ('80-15218', 'Teal', '12', '1000', '368D97'),
        ('80-19091', 'Parrot Green', '13', '1000', '00968A'),
        ('80-15217', 'Lagoon', '12', '1000', '00ABB2'),
        ('80-19011', 'Light Green', '13', '1000', '38C7AF'),
        ('80-15216', 'Sky', '12', '1000', '54CDE3'),
        ('80-19058', 'Toothpaste', '13', '1000', 'B0E8D5'),
        ('80-15202', 'Robins Egg', '12', '1000', 'B4D9DF'),
        ('80-15179', 'Evergreen', '13', '1000', '3C614F'),
        ('80-19010', 'Dark Green', '13', '1000', '108355'),
        ('80-15199', 'Shamrock', '13', '1000', '009654'),
        ('80-19080', 'Green', '13', '1000', '54B160'),
        ('80-19053', 'Pastel Green', '13', '1000', '73D594'),
        ('80-19061', 'Kiwi Lime', '13', '1000', '77CA4A'),
        ('80-15220', 'Olive', '13', '1000',	'72763E'),
        ('80-15219', 'Fern', '13', '1000', '7B9730'),
        ('80-19097', 'Prickly Pear', '14', '1000', 'CBD735'),
        ('80-15214', 'Sherbert', '13', '1000', 'E1EE7D'),
        ('80-19003', 'Yellow', '14', '1000', 'F9D737'),
        ('80-19056', 'Pastel Yellow', '14', '1000', 'FAEE8D'),
        ('80-19002', 'Cream', '14', '1000', 'EDE7BA'),
        ('80-19020', 'Rust', '10', '1000', 'A04E3F'),
        ('80-19004', 'Orange', '15', '1000', 'FF803E'),
        ('80-15212', 'Spice', '10', '1000', 'E35C44'),
        ('80-19090', 'Butterscotch', '15', '1000', 'E19A52'),
        ('80-15213', 'Apricot', '15', '1000', 'FFA967'),
        ('80-19057', 'Cheddar', '14', '1000', 'FFB64E'),
        ('80-19096', 'Cranapple', '10', '1000', '88404F'),
        ('80-15961', 'Cherry', '10', '1000', 'AD3345'),
        ('80-19005', 'Red', '10', '1000', 'C43A44'),
        ('80-15211', 'Tomato', '10', '1000', 'EA4241'),
        ('80-19059', 'Hot Coral', '10', '1000', 'FF6158'),
        ('80-15204', 'Salmon', '10', '1000', 'FF777F'),
        ('80-19063', 'Blush', '10', '1000', 'FF9E8D'),
        ('80-19098', 'Sand', '19', '1000', 'EAC49F'),
        ('80-19033', 'Peach', '10', '1000', 'FCC6B8'),
        ('80-19088', 'Raspberry', '10', '1000', 'AD3C6C'),
        ('80-19038', 'Magenta', '10', '1000', 'F34676'),
        ('80-19083', 'Pink', '10', '1000', 'E65794'),
        ('80-19006', 'Bubblegum', '10', '1000', 'E16D9D'),
        ('80-19079', 'Light Pink', '10', '1000', 'F5C0D5'),
        ('TRANS', 'Transparent', '16', '1002', 'FFFFFF00')
GO


/* canvas */
print '' print '*** creating [CANVAS] table'
GO
CREATE TABLE [dbo].[Canvas]
(
    [CanvasID]      [int] IDENTITY(1000000,1)       NOT NULL,
    [ArtID]         [int]                           NOT NULL,
    [Row]           [int]                           NOT NULL,
    [Column]        [int]                           NOT NULL,
    [BeadID]        [nvarchar](25)                  NOT NULL DEFAULT 'TRANS',

    CONSTRAINT      [fk_Canvas_BeadID]                  FOREIGN KEY([BeadID])       REFERENCES [dbo].[Bead]([BeadID]),
    CONSTRAINT      [pk_CanvasID]                       PRIMARY KEY ([CanvasID]),
    CONSTRAINT      [ak_ArtIDRowColumn]                 UNIQUE ([ArtID], [Row], [Column])
)
GO

print '' print '*** inserting test records into [CANVAS]'
GO
INSERT INTO [dbo].[Canvas]
        ([ArtID],[Row], [Column], [BeadID])
    VALUES
        (1000019, 48,24,'P60'),
        (1000019, 48,25,'P60'),
        (1000019, 49,25,'P60'),
        (1000019, 48,26,'P60'),
        (1000019, 47,23,'P60'),
        (1000019, 47,24,'P60'),
        (1000019, 47,25,'P60'),
        (1000019, 47,26,'P60'),
        (1000019, 47,27,'P60'),
        (1000019, 46,24,'P60'),
        (1000019, 46,26,'P60')
GO

/* tagcanvas */
print '' print '*** creating [TAGART] table'
GO
CREATE TABLE [dbo].[TagArt]
(
    [ArtID]       [int]               NOT NULL,
    [TagName]     [nvarchar](50)      NOT NULL,

    CONSTRAINT  [fk_TagArt_ArtID]     FOREIGN KEY([ArtID])       REFERENCES [dbo].[Art]([ArtID]),
    CONSTRAINT  [fk_TagArt_TagName]      FOREIGN KEY([TagName])        REFERENCES [dbo].[Tag]([TagName]),

    CONSTRAINT  [pk_TagArt]              PRIMARY KEY([ArtID],[TagName])
)

print '' print '*** inserting test records into [TAGCANVAS]'
GO
INSERT INTO [dbo].[TagArt]
        ([ArtID], [TagName])
    VALUES
        (1000019, 'Shapes'),
        (1000019, 'Hearts')
GO





/* login-related stored procedures */
/* sp = stored procedure */
/* GO keyword is especially important for stored procedures */
print '' print ''
print '/////// NOW CREATING :: STORED PROCEDURES - IMPLEMENTED ///////'
print ''

print '---USER CREATE / INSERT'


        print '' print '*** creating [SP_INSERT_USER]'
        GO
        CREATE PROCEDURE [dbo].[sp_insert_user]
        (
            @Username       [nvarchar](25),            
            @DisplayName    [nvarchar](25),
            @Email          [nvarchar](100)
        )
        AS
            BEGIN
                INSERT INTO [dbo].[User]
                    ([Username], [DisplayName], [Email])
                VALUES
                    (@Username, @DisplayName, @Email)
            END
        GO

        print ''

print ''
print '*** creating [SP_FAVORITE_ART]'
GO 
CREATE PROCEDURE [dbo].[sp_favorite_art]
(
	@UserID [int], 
	@ArtID  [int]
)
AS
	BEGIN 
		INSERT INTO [UserFavorite]
		([UserID], [ArtID])
		VALUES
		(@UserID, @ArtID)
	END
GO

print ''
print '*** creating [SP_INSERT_FOLLOW]'
GO 
CREATE PROCEDURE [dbo].[sp_insert_follow]
(
	@UserID [int], 
	@FollowingID  [int]
)
AS
	BEGIN 
		INSERT INTO [UserFollowing]
		([UserID], [FollowingID])
		VALUES
		(@UserID, @FollowingID)
	END
GO


print ''
print '---USER READ / SELECT'

        print '' print '*** creating [SP_ALL_USERS]'
        GO
        CREATE PROCEDURE [dbo].[sp_all_users]
        AS
            BEGIN
                SELECT [UserID], [Username], [DisplayName], [ShortBio], [Email], [CreatedOn], [Active]
                FROM [User]
            END
        GO


        print '' print '*** creating [SP_AUTHENTICATE_USER]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_authenticate_user]
        (
            @Email          [nvarchar](100),
            @PasswordHash   [nvarchar](100)
        )
        AS 
            BEGIN
                SELECT  COUNT([UserID]) AS 'Authenticated'
                FROM    [User]
                WHERE   @Email = [Email]
                AND     @PasswordHash = [PasswordHash]
                AND     [Active] = 1
            END
        GO


        print '' print '*** creating [SP_SELECT_USER_BY_EMAIL]'
        GO
        CREATE PROCEDURE [dbo].[sp_select_User_by_email]
        (
            @Email          [nvarchar](100)
        )
        AS
            BEGIN
                SELECT  [UserID], [Username], [DisplayName], [ShortBio], [Email], [Active]
                FROM    [User]
                WHERE   @Email = [Email]
            END
        GO

        print '' print '*** creating [SP_SELECT_USER_BY_USERID]'
        GO
        CREATE PROCEDURE [dbo].[sp_select_User_by_userID]
        (
            @UserID         [int]
        )
        AS
            BEGIN
                SELECT  [UserID], [Username], [DisplayName], [ShortBio], [Email], [Active]
                FROM    [User]
                WHERE   @UserID = [UserID]
            END
        GO


        print '' print '*** creating [SP_SELECT_ROLE_BY_USERID]'
        GO
        CREATE PROCEDURE [dbo].[sp_select_role_by_UserID]
        (
            @UserID          [int]
        )
        AS
            BEGIN
                SELECT  [RoleID]
                FROM    [UserRole]
                WHERE   @UserID = [UserID]
            END
        GO

        print '' print '*** creating [SP_SELECT_USERICON_BY_USERID]'
        GO
        CREATE PROCEDURE [dbo].[sp_select_usericon_by_userid]
        (
            @UserID     [int]
        )
        AS
            BEGIN
                SELECT [User].[UserID], [Art].[ArtID], [Art].[ArtName]
                FROM [UserIcon]
                JOIN [User]
                ON [User].[UserID] = [UserIcon].[UserID]
                JOIN [Art]
                ON [Art].[ArtID] = [UserIcon].[ArtID]
                WHERE [User].[UserID] = @UserID
            END
        GO

        print '' print '*** creating [SP_SELECT_ALL_ROLES]'
        GO
        CREATE PROCEDURE [dbo].[sp_select_all_roles]
        AS
            BEGIN
                SELECT [RoleID]
                FROM [Role]
            END
        GO

        print '*** creating [SP_SELECT_ALL_USER_FAVORITES]'
        GO 
        CREATE PROCEDURE [dbo].[sp_select_all_user_favorites]
        ( 
            @UserID		[int]
        )
        AS
            BEGIN
                SELECT [Art].[ArtID], [User].[Username], [Art].[ArtName], [Art].[Description], [Art].[PostedOn], [Art].[UpdatedOn], [User].[UserID]
                FROM [Art]
                JOIN [UserFavorite]
                ON [Art].[ArtID] = [UserFavorite].[ArtID]
                JOIN [User]
                ON [UserFavorite].[UserID] = [User].[UserID]
                WHERE @UserID = [User].[UserID]
            END 
        GO

        print '*** creating [SP_SELECT_ALL_USER_FOLLOWERS]'
        GO 
        CREATE PROCEDURE [dbo].[sp_select_all_user_followers]
        ( 
            @UserID		[int]
        )
        AS
            BEGIN
                SELECT [User].[UserID], [Username], [DisplayName], [ShortBio], [Email], [CreatedOn], [Active]
                FROM [User]
                JOIN [UserFollowing]
                ON [User].[UserID] = [UserFollowing].[UserID]
                WHERE @UserID = [User].[UserID]
            END 
        GO

        print '*** creating [SP_SELECT_ALL_USERS_FOLLOWING]'
        GO 
        CREATE PROCEDURE [dbo].[sp_select_all_users_following]
        ( 
            @FollowingID		[int]
        )
        AS
            BEGIN
                SELECT [User].[UserID], [Username], [DisplayName], [ShortBio], [Email], [CreatedOn], [Active]
                FROM [User]
                JOIN [UserFollowing]
                ON [User].[UserID] = [UserFollowing].[UserID]
                WHERE @FollowingID = [UserFollowing].[FollowingID]
            END 
        GO

        print '' print '*** creating [SP_AUTHENTICATE_FOLLOW]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_authenticate_follow]
        (
            @FollowingID     [int], /* this is the active user */
            @UserID          [int] /* testing to see if they're following a specific user */
            
        )
        AS 
            BEGIN
                SELECT  COUNT([FollowingID]) AS 'Following'
                FROM    [UserFollowing]
                WHERE   @FollowingID = [FollowingID]
                AND     @UserID = [UserID]
            END
        GO


        
print ''
print '---ART CREATE'

 print '' print '*** creating [SP_INSERT_ART]'
        GO
        CREATE PROCEDURE [dbo].[sp_insert_art]
        (
            @UserID         [int],
            @ArtName        [nvarchar](25),
            @Description    [nvarchar](250)
        )
        AS
            BEGIN
                INSERT INTO [dbo].[Art]
                    ([UserID], [ArtName], [Description])
                VALUES
                    (@UserID, @ArtName, @Description)
            END
        GO


print ''
print '---ART READ / SELECT'
        print '' print '*** creating [SP_ALL_ART]'
        GO
        CREATE PROCEDURE [dbo].[sp_all_art]
        AS
            BEGIN
                SELECT [Art].[ArtID], [User].[Username], [Art].[ArtName], [Art].[Description], [Art].[PostedOn], [Art].[UpdatedOn], [User].[UserID]
                FROM [Art]
                JOIN [User]
                ON [User].[UserID] = [Art].[UserId]
            END
        GO

        print '' print '*** creating [SP_ALL_HIDDEN_ART]'
        GO
        CREATE PROCEDURE [dbo].[sp_all_hidden_art]
        AS
            BEGIN
                SELECT [Art].[ArtID], [User].[Username], [Art].[ArtName], [Art].[Description], [Art].[PostedOn], [Art].[UpdatedOn]
                FROM [Art]
                JOIN [User]
                ON [User].[UserID] = [Art].[UserId]
                WHERE [Art].[Hidden] = '0'
            END
        GO

        print '' print '*** creating [SP_ALL_VISIBLE_ART]'
        GO
        CREATE PROCEDURE [dbo].[sp_all_visible_art]
        AS
            BEGIN
                SELECT [Art].[ArtID], [User].[Username], [Art].[ArtName], [Art].[Description], [Art].[PostedOn], [Art].[UpdatedOn]
                FROM [Art]
                JOIN [User]
                ON [User].[UserID] = [Art].[UserId]
                WHERE [Art].[Hidden] = '1'
            END
        GO

        print '' print '*** creating [SP_SELECT_ART_BY_USERID]'
        GO
        CREATE PROCEDURE [dbo].[sp_select_art_by_userID]
        (
            @UserID     [int]
        )
        AS
            BEGIN
                SELECT [ArtID], [User].[Username], [ArtName], [Description], [PostedOn]
                FROM [Art]
                JOIN [User]
                ON [User].[UserID] = [Art].[UserID]
                WHERE @UserID = [Art].[UserID]
            END
        GO

        print '' print '*** creating [SP_SELECT_ART_BY_ARTID]'
        GO
        CREATE PROCEDURE [sp_select_art_by_id]
        (
            @ArtID	[int]
        )
        AS
            BEGIN
            SELECT	[Art].[ArtID], [User].[Username], [Art].[ArtName], [Art].[Description], [Art].[PostedOn], [Art].[UpdatedOn]
            FROM	[Art] 
            JOIN    [User] 
            ON      [User].[UserID] = [Art].[UserID]
            WHERE	[ArtID] = @ArtID
        END
        GO

        print '' print '*** creating [SP_SELECT_CANVAS_BY_ARTID]'
        GO
        CREATE PROCEDURE [sp_select_canvas_by_artID]
        (
            @ArtID      [int]
        )
        AS
            BEGIN
                SELECT [Canvas].[ArtID], [Row], [Column], [Canvas].[BeadID]
                FROM   [Canvas]
                JOIN   [Bead]
                ON     [Canvas].[BeadID] = [Bead].[BeadID]
                JOIN   [Art]
                ON     [Art].[ArtID] = [Canvas].[ArtID]
                WHERE  @ArtID = [Art].[ArtID]
            END
        GO

        print '' print '*** creating [SP_AUTHENTICATE_FAVORITE]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_authenticate_favorite]
        (
            @UserID          [int],
            @ArtID           [int]
        )
        AS 
            BEGIN
                SELECT  COUNT([UserID]) AS 'Favorited'
                FROM    [UserFavorite]
                WHERE   @UserID = [UserID]
                AND     @ArtID = [ArtID]
            END
        GO




print ''
print '---USER UPDATE'

        print '' print '*** creating [SP_UPDATE_PASSWORDHASH]'
        GO
        CREATE PROCEDURE [dbo].[sp_update_passwordHash]
        (
            @UserID                 [int],
            @PasswordHash           [nvarchar](100),
            @OldPasswordHash        [nvarchar](100)
        )
        AS
            BEGIN
                UPDATE  [User]
                SET     [PasswordHash] = @PasswordHash, [UpdatedOn] = CURRENT_TIMESTAMP
                WHERE   @UserID = [UserID]
                AND     @OldPasswordHash = [PasswordHash]

                RETURN @@ROWCOUNT
            END
        GO


        print '' print '*** creating [SP_UPDATE_USER_BIO]'
        GO
        CREATE PROCEDURE [dbo].[sp_update_user_bio]
        (
            @UserID     [int],
            @ShortBio   [nvarchar](140)
        )
        AS
            BEGIN
                UPDATE  [User]
                SET     [ShortBio] = @ShortBio, [UpdatedOn] = CURRENT_TIMESTAMP
                WHERE   @UserID = [UserID]
                RETURN @@ROWCOUNT
            END
        GO

        
        print '' print '*** creating [SP_UPDATE_USER_ACTIVE]'
        GO
        CREATE PROCEDURE [dbo].[sp_update_user_active]
        (
            @UserID     [int],
            @Active     [bit]
        )
        AS
            BEGIN
                UPDATE [User]
                SET @Active = [Active], [UpdatedOn] = CURRENT_TIMESTAMP
                WHERE @UserID = [UserID]
            END
        GO


print ''
print '---ART UPDATE'

        print '' print '*** creating [SP_UPDATE_ART_DESCRIPTION]'
        GO
        CREATE PROCEDURE [dbo].[sp_update_art_description]
        (
            @ArtID         [int],
            @Description   [nvarchar](250)
        )
        AS
            BEGIN
                UPDATE  [Art]
                SET     [Description] = @Description, [UpdatedOn] = CURRENT_TIMESTAMP
                WHERE   @ArtID = [ArtID]

                RETURN @@ROWCOUNT

            END
        GO



print ''
print '---USER DELETE / DEACTIVATE'
 
    print ''
    print '*** creating [SP_DELETE_FAVORITE]'
    GO 
    CREATE PROCEDURE [dbo].[sp_delete_favorite]
    (
        @UserID [int],
        @ArtID [int]
    )
    AS 
        BEGIN 
            DELETE FROM [UserFavorite]
            WHERE @ArtID = [ArtID]
            AND @UserID = [UserID]
    END 
    GO

    print ''
    print '*** creating [SP_DELETE_FOLLOW]'
    GO 
    CREATE PROCEDURE [dbo].[sp_delete_follow]
    (
        @UserID [int],
        @FollowingID [int]
    )
    AS 
        BEGIN 
            DELETE FROM [UserFollowing]
            WHERE @FollowingID = [FollowingID]
            AND @UserID = [UserID]
    END 
    GO



print ''
print '---ART DELETE / DEACTIVATE'

        print '' print '*** creating [SP_DELETE_ART_FROM_USER]'
        GO
        CREATE PROCEDURE [dbo].[sp_delete_art_from_user]
        (
            @UserID     [int],
            @ArtID      [int]
        )
        AS
            BEGIN
                DELETE FROM [Art]
                WHERE @UserID = [UserID]
                AND @ArtID = [ArtID]

                RETURN @@ROWCOUNT
            END
        GO




print '' print ''
print '/////// NOW CREATING : STORED PROCEDURES - NOT FULL STACK ///////'
print '' print ''






print '' print '*** creating [SP_SELECT_USER_ON_ACTIVE]'
GO
CREATE PROCEDURE [dbo].[sp_select_user_on_active]
(
    @Active         [bit]
)
AS
    BEGIN
        SELECT [UserID], [Username], [ShortBio], [Email], [Active]
        FROM [User]
        WHERE [Active] = @Active
    END
GO



print '' print '*** creating [SP_UPDATE_USER_ROLE]'
GO
CREATE PROCEDURE [dbo].[sp_update_user_role]
(
    @UserID     [int],
    @RoleID     [nvarchar](50)
)
AS
    BEGIN
        UPDATE [UserRole]
        SET @RoleID = [RoleID]
        WHERE @UserID = [UserID]
    END
GO


print '' print '*** creating [SP_SELECT_ALL_BEADS]'
GO
CREATE PROCEDURE [dbo].[sp_select_all_beads]
AS
    BEGIN
        SELECT [BeadID], [ColorName], [ColorGroupID], [Brand].[BrandID], [HexValue]
        FROM [Bead]
        JOIN [Brand]
        ON [Brand].[BrandID] = [Bead].[BrandID]
    END
GO

print '' print '*** creating [SP_SELECT_USERID_BY_ROLE]'
GO
CREATE PROCEDURE [dbo].[sp_select_userID_by_role]
(
    @RoleID          [nvarchar](50)
)
AS
    BEGIN
        SELECT  [UserID]
        FROM    [UserRole]
        WHERE   @RoleID = [RoleID]
    END
GO

print '' print '*** Creating sp_insert_user_role'
GO
CREATE PROCEDURE [sp_insert_user_role]
(
	@UserID			    [int],
	@RoleID				[nvarchar](50)
)
AS
BEGIN
INSERT INTO [dbo].[UserRole]
	([UserID], [RoleID])
	VALUES
	(@UserID, @RoleID)
END
GO

print '' print '*** Creating sp_delete_user_role'
GO
CREATE PROCEDURE [dbo].[sp_delete_user_role]
(
	@UserID 			[int],
	@RoleID				[nvarchar](50)
)
AS
BEGIN
	DELETE FROM [dbo].[UserRole]
	WHERE [UserID] =	@UserID
	  AND [RoleID] = 	@RoleID
END
GO

print '' print '*** Creating sp_select_all_colorgroups'
GO
CREATE PROCEDURE [dbo].[sp_select_all_colorgroups]
AS
    BEGIN
        SELECT [ColorGroupName]
        FROM [ColorGroup]
    END
GO

print '' print '*** Creating sp_select_beads_by_colorgroupID'
CREATE PROCEDURE [dbo].[sp_select_beads_by_colorgroupID]
(
    @ColorGroupID          [int]
)
AS
    BEGIN
        SELECT  [BeadID], [ColorName], [BrandID], [HexValue]
        FROM    [Bead]
        JOIN    [ColorGroup]
        ON      [ColorGroup].[ColorGroupID] = [Bead].[ColorGroupID]
        WHERE   @ColorGroupID = [ColorGroupID]
    END
GO

print '' print ''
print '' print ''
print '' print ''
print '/////// DATABASE CONSTRUCTION COMPLETE ///////'
print '' print ''
print '' print ''
print '' print ''

