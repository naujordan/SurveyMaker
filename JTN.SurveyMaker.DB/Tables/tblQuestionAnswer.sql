﻿CREATE TABLE [dbo].[tblQuestionAnswer]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[AnswerId] UNIQUEIDENTIFIER NOT NULL,
	[QuestionId] UNIQUEIDENTIFIER NOT NULL,
	[isCorrect] BIT NOT NULL
)