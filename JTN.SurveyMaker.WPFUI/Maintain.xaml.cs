using JTN.SurveyMaker.BL;
using JTN.SurveyMaker.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JTN.SurveyMaker.WPFUI
{
    public enum ScreenMode
    { 
        Answer = 0,
        Question = 1
    }
    /// <summary>
    /// Interaction logic for Maintain.xaml
    /// </summary>
    public partial class Maintain : Window
    {
        List<Answer> answers;
        List<Question> questions;
        ScreenMode screenMode;

        public Maintain(ScreenMode screenMode)
        {
            InitializeComponent();
            this.screenMode = screenMode;
            Reload();
            cboAttributes.DisplayMemberPath = "Text";
            cboAttributes.SelectedValuePath = "Id";
            lblAttribute.Content = "Enter a " + screenMode.ToString();
            this.Title = "Maintain " + screenMode.ToString();
        }

        private async void Reload()
        {
            cboAttributes.ItemsSource = null;
            switch (screenMode)
            {
                case ScreenMode.Answer:
                    answers = (List<Answer>)await AnswerManager.Load();
                    cboAttributes.ItemsSource = answers;
                    break;
                case ScreenMode.Question:
                    questions = (List<Question>)await QuestionManager.Load();
                    cboAttributes.ItemsSource = questions;
                    break;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (screenMode)
            {
                case ScreenMode.Answer:
                    Answer answer = new Answer { Text = txtDescription.Text };
                    Task.Run(async () =>
                    {
                        bool results = await AnswerManager.Insert(answer);
                    });
                    answers.Add(answer);
                    Rebind(answers.Count - 1);
                    break;
                case ScreenMode.Question:
                    Question question = new Question { Text = txtDescription.Text };
                    Task.Run(async () =>
                    {
                        int results = await QuestionManager.Insert(question);
                    });
                    questions.Add(question);
                    Rebind(questions.Count - 1);
                    break;
                default:
                    break;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            switch (screenMode)
            {
                case ScreenMode.Answer:
                    Answer answer = answers[cboAttributes.SelectedIndex];
                    answer.Text = txtDescription.Text;
                    Task.Run(async () =>
                    {
                        int results = await AnswerManager.Update(answer);
                    });
                    Rebind(cboAttributes.SelectedIndex);
                    break;
                case ScreenMode.Question:
                    Question question = questions[cboAttributes.SelectedIndex];
                    question.Text = txtDescription.Text;
                    Task.Run(async () =>
                    {
                        int results = await QuestionManager.Update(question);
                    });
                    Rebind(cboAttributes.SelectedIndex);
                    break;
                default:
                    break;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (screenMode)
            {
                case ScreenMode.Answer:
                    Answer answer = answers[cboAttributes.SelectedIndex];
                    Task.Run(async () =>
                    {
                        int results = await AnswerManager.Delete(answer.Id);
                    });
                    answers.Remove(answer);
                    Rebind(-1);
                    break;
                case ScreenMode.Question:
                    Question question = questions[cboAttributes.SelectedIndex];
                    Task.Run(async () =>
                    {
                        int results = await QuestionManager.Delete(question.Id);
                    });
                    questions.Remove(question);
                    Rebind(-1);
                    break;
                default:
                    break;
            }
        }

        private void Rebind(int index)
        {
            cboAttributes.ItemsSource = null;
            switch (screenMode)
            {
                case ScreenMode.Answer:
                    cboAttributes.ItemsSource = answers;
                    break;
                case ScreenMode.Question:
                    cboAttributes.ItemsSource = questions;
                    break;
            }
            cboAttributes.DisplayMemberPath = "Text";
            cboAttributes.SelectedValuePath = "Id";
            cboAttributes.SelectedIndex = index;
        }

        private void cboAttributes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtDescription.Text = string.Empty;
            if (cboAttributes.SelectedIndex > -1)
            {
                if (screenMode == ScreenMode.Question)
                {
                    txtDescription.Text = questions[cboAttributes.SelectedIndex].Text;
                }
                else
                {
                    txtDescription.Text = answers[cboAttributes.SelectedIndex].Text;
                }
            }
        }
    }
}
