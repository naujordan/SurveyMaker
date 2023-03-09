ALTER TABLE [dbo].tblResponse
	ADD CONSTRAINT [fkQuestionIdResponseTable]
	FOREIGN KEY (QuestionId)
	REFERENCES [dbo].[tblQuestion] (Id)
