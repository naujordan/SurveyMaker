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
    /// <summary>
    /// Interaction logic for SurveyMaker.xaml
    /// </summary>
    public partial class SurveyMaker : Window
    {
        ucAnswer[] ucAnswers = new ucAnswer[4];
        List<Question> questions;
        Guid selectedQuestion;
        string APIAddress = "https://localhost:7102/";
        public SurveyMaker()
        {
            InitializeComponent();
            LoadQuestions();
        }

        private async void LoadQuestions()
        {
            cboQuestion.ItemsSource = null;
            var apiclient = new ApiClient(APIAddress);
            questions = apiclient.GetList<Question>("Question");
            //questions = (List<Question>)await QuestionManager.Load();
            cboQuestion.ItemsSource = questions;
            cboQuestion.DisplayMemberPath = "Text";
            cboQuestion.SelectedValuePath = "Id";
        }

        private async void DrawScreen()
        {
            selectedQuestion = (Guid)cboQuestion.SelectedValue;

            ucAnswer ucAnswer = new ucAnswer(selectedQuestion);


            ucAnswer.Margin = new Thickness(0, -75, 0, 0);

            grdSurveyMaker.Children.Add(ucAnswer);

            ucAnswers[0] = ucAnswer;

        }

        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {
            new Maintain(ScreenMode.Answer, APIAddress).ShowDialog();
        }

        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            new Maintain(ScreenMode.Question, APIAddress).ShowDialog();
            LoadQuestions();
        }

        private void cboQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DrawScreen();
            
        }
    }
}
