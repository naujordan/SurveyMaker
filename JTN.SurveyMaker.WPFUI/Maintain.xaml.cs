using JTN.SurveyMaker.BL;
using JTN.SurveyMaker.BL.Models;
using JTN.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        string APIAddress;

        public Maintain(ScreenMode screenMode, string apiAddress)
        {
            InitializeComponent();
            this.screenMode = screenMode;
            APIAddress = apiAddress;
            Reload();
            cboAttributes.DisplayMemberPath = "Text";
            cboAttributes.SelectedValuePath = "Id";
            lblAttribute.Content = "Enter a " + screenMode.ToString();
            this.Title = "Maintain " + screenMode.ToString();
            
        }

        private async void Reload()
        {
            cboAttributes.ItemsSource = null;
            var apiclient = new ApiClient(APIAddress);
            switch (screenMode)
            {
                case ScreenMode.Answer:
                    answers = (List<Answer>)await AnswerManager.Load();
                    //answers = apiclient.GetList<Answer>("Answer"); 
                    cboAttributes.ItemsSource = answers;
                    break;
                case ScreenMode.Question:
                    //questions = (List<Question>)await QuestionManager.Load();
                    questions = apiclient.GetList<Question>("Question");
                    cboAttributes.ItemsSource = questions;
                    break;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var apiclient = new ApiClient(APIAddress);
            switch (screenMode)
            {
                case ScreenMode.Answer:
                    Answer answer = new Answer {Text = txtDescription.Text };
                    Task.Run(async () =>
                    {
                        bool results = await AnswerManager.Insert(answer);
                    });
                    //var response = apiclient.Post<Answer>(answer, "Answer");
                    //string result = response.Content.ReadAsStringAsync().Result;
                    //result = result.Replace("\"", "");
                    //answer.Id = Guid.Parse(result);
                    answers.Add(answer);
                    Rebind(answers.Count - 1);
                    break;
                case ScreenMode.Question:
                    Question question = new Question { Text = txtDescription.Text };
                    //Task.Run(async () =>
                    //{
                    //    int results = await QuestionManager.Insert(question);
                    //});
                    var response = apiclient.Post<Question>(question, "Question");
                    string result = response.Content.ReadAsStringAsync().Result;
                    result = result.Replace("\"", "");
                    questions.Add(question);
                    Rebind(questions.Count - 1);
                    break;
                default:
                    break;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var apiclient = new ApiClient(APIAddress);
            switch (screenMode)
            {
                case ScreenMode.Answer:
                    Answer answer = answers[cboAttributes.SelectedIndex];
                    answer.Text = txtDescription.Text;
                    //Task.Run(async () =>
                    //{
                    //    int results = await AnswerManager.Update(answer);
                    //});
                    var response = apiclient.Put<Answer>(answer, "Answer", answer.Id);
                    string result = response.Content.ReadAsStringAsync().Result;
                    Rebind(cboAttributes.SelectedIndex);
                    break;
                case ScreenMode.Question:
                    Question question = questions[cboAttributes.SelectedIndex];
                    question.Text = txtDescription.Text;
                    //Task.Run(async () =>
                    //{
                    //    int results = await QuestionManager.Update(question);
                    //});
                    var response2 = apiclient.Put<Question>(question, "Question", question.Id);
                    string result2 = response2.Content.ReadAsStringAsync().Result;
                    Rebind(cboAttributes.SelectedIndex);
                    break;
                default:
                    break;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var apiclient = new ApiClient(APIAddress);
            switch (screenMode)
            {
                case ScreenMode.Answer:
                    Answer answer = answers[cboAttributes.SelectedIndex];
                    Task.Run(async () =>
                    {
                        int results = await AnswerManager.Delete(answer.Id);
                    });
                    //var response = apiclient.Delete("Answer", answer.Id);
                    //string result = response.Content.ReadAsStringAsync().Result;
                    //answers.Remove(answer);
                    Rebind(-1);
                    break;
                case ScreenMode.Question:
                    Question question = questions[cboAttributes.SelectedIndex];
                    //Task.Run(async () =>
                    //{
                    //    int results = await QuestionManager.Delete(question.Id);
                    //});
                    var response = apiclient.Delete("Question", question.Id);
                    string result = response.Content.ReadAsStringAsync().Result;
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
