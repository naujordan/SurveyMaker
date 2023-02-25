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
        List<Answer> answers= new List<Answer>();
        Answer answer = new Answer();
        public ucAnswer(Guid questionId)
        {
            InitializeComponent();
            this.question.Id = questionId;
            Reload();
            
        }

        private async void Reload()
        {
            cboAnswer.ItemsSource = null;
            answers = (List<Answer>)await AnswerManager.Load(question.Id);
            cboAnswer.ItemsSource = answers;
            cboAnswer.DisplayMemberPath = "Text";
            cboAnswer.SelectedValuePath = "Id";
        }

        private void cboAnswer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            answer.Id = (Guid)cboAnswer.SelectedValue;
            if (answer.isCorrect == true)
                rdoCorrect.IsChecked = true;

        }
    }
}
