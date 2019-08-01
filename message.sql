/*
Navicat SQL Server Data Transfer

Source Server         : UNIPC-815
Source Server Version : 100000
Source Host           : UNIPC-815\SQLEXPRESS:1433
Source Database       : test
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 100000
File Encoding         : 65001

Date: 2019-08-02 01:48:00
*/


-- ----------------------------
-- Table structure for [dbo].[message]
-- ----------------------------
DROP TABLE [dbo].[message]
GO
CREATE TABLE [dbo].[message] (
[message_id] int NOT NULL IDENTITY(1,1) ,
[message_title] ntext NULL ,
[message_content] ntext NULL ,
[message_created_date] datetime NULL ,
[message_edited_date] datetime NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[message]', RESEED, 4)
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'message', 
'COLUMN', N'message_id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'資料庫流水號'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'message'
, @level2type = 'COLUMN', @level2name = N'message_id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'資料庫流水號'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'message'
, @level2type = 'COLUMN', @level2name = N'message_id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'message', 
'COLUMN', N'message_title')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'留言主題'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'message'
, @level2type = 'COLUMN', @level2name = N'message_title'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'留言主題'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'message'
, @level2type = 'COLUMN', @level2name = N'message_title'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'message', 
'COLUMN', N'message_content')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'留言內容'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'message'
, @level2type = 'COLUMN', @level2name = N'message_content'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'留言內容'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'message'
, @level2type = 'COLUMN', @level2name = N'message_content'
GO

-- ----------------------------
-- Records of message
-- ----------------------------
SET IDENTITY_INSERT [dbo].[message] ON
GO
SET IDENTITY_INSERT [dbo].[message] OFF
GO

-- ----------------------------
-- Indexes structure for table message
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [dbo].[message]
-- ----------------------------
ALTER TABLE [dbo].[message] ADD PRIMARY KEY ([message_id])
GO
