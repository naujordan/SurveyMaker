ALTER TABLE [dbo].tblResponse
	ADD CONSTRAINT [fkAnswerIdResponseTable]
	FOREIGN KEY (AnswerId)
	REFERENCES [dbo].tblAnswer (Id)
