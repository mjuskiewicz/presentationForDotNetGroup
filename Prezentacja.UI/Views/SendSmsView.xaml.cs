using Prezentacja.Service;
using Prezentacja.UI.ViewModels;
using System.Windows.Controls;

namespace Prezentacja.UI.Views
{
    /// <summary>
    /// Interaction logic for SendSmsView.xaml
    /// </summary>
    public partial class SendSmsView : UserControl
    {
        public SendSmsView()
        {
            InitializeComponent();
            this.DataContext = new SendSmsViewModel(new PersonsRepository(), Modem.ModemConnection.Instance);
        }
    }
}
