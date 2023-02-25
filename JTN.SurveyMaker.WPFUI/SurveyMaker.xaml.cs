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
    /// <summary>
    /// Interaction logic for SurveyMaker.xaml
    /// </summary>
    public partial class SurveyMaker : Window
    {
        ucAnswer[] ucAnswers = new ucAnswer[4];
        List<Question> questions;
        Guid selectedQuestion;
        public SurveyMaker()
        {
            InitializeComponent();
            LoadQuestions();
        }

        private async void LoadQuestions()
        {
            cboQuestion.ItemsSource = null;
            questions = (List<Question>)await QuestionManager.Load();
            cboQuestion.ItemsSource = questions;
            cboQuestion.DisplayMemberPath = "Text";
            cboQuestion.SelectedValuePath = "Id";
        }

        private async void DrawScreen()
        {
            selectedQuestion = (Guid)cboQuestion.SelectedValue;

            ucAnswer ucAnswer1 = new ucAnswer(selectedQuestion);
            ucAnswer ucAnswer2 = new ucAnswer(selectedQuestion);
            ucAnswer ucAnswer3 = new ucAnswer(selectedQuestion);
            ucAnswer ucAnswer4 = new ucAnswer(selectedQuestion);

            ucAnswer1.Margin = new Thickness(0, -75, 0, 0);
            ucAnswer2.Margin = new Thickness(0, 50, 0, 0);
            ucAnswer3.Margin = new Thickness(0, 175, 0, 0);
            ucAnswer4.Margin = new Thickness(0, 300, 0, 0);

            grdSurveyMaker.Children.Add(ucAnswer1);
            grdSurveyMaker.Children.Add(ucAnswer2);
            grdSurveyMaker.Children.Add(ucAnswer3);
            grdSurveyMaker.Children.Add(ucAnswer4);

            ucAnswers[0] = ucAnswer1;
            ucAnswers[1] = ucAnswer2;
            ucAnswers[2] = ucAnswer3;
            ucAnswers[3] = ucAnswer4;

        }

        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {
            new Maintain(ScreenMode.Answer).ShowDialog();
        }

        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            new Maintain(ScreenMode.Question).ShowDialog();
            LoadQuestions();
        }

        private void cboQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DrawScreen();
            
        }
    }
}
