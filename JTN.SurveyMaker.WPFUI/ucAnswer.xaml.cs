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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JTN.SurveyMaker.WPFUI
{
    /// <summary>
    /// Interaction logic for ucAnswer.xaml
    /// </summary>
    public partial class ucAnswer : UserControl
    {
        Question question = new Question();
        
        public ucAnswer(Guid questionId)
        {
            InitializeComponent();
            this.question.Id = questionId;
            Reload();
            
        }

        private async void Reload()
        {
            cboAnswer.ItemsSource = null;
            question = await QuestionManager.LoadById(question.Id);
            cboAnswer.ItemsSource = question.Answers;
            cboAnswer.DisplayMemberPath = "Text";
            cboAnswer.SelectedValuePath = "Id";
        }

        private void cboAnswer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboAnswer.SelectedIndex > -1) 
            {
                if (question.Answers[cboAnswer.SelectedIndex].isCorrect == true)
                    rdoCorrect.IsChecked = true;
                else
                    rdoCorrect.IsChecked = false;
            }    
        }

        private async void imgDelete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            await QuestionAnswerManager.Delete(question.Id, (Guid)cboAnswer.SelectedValue);
            Reload();
            cboAnswer.SelectedIndex = -1;

        }
    }
}
