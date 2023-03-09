ALTER TABLE [dbo].[tblActivation]
	ADD CONSTRAINT [fkQuestionIdActivationTable]
	FOREIGN KEY (QuestionId)
	REFERENCES [dbo].[tblQuestion] (Id)
