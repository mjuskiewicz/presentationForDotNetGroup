using System.Windows.Controls;
using Prezentacja.Service;
using Prezentacja.UI.ViewModels;

namespace Prezentacja.UI.Views
{
    /// <summary>
    /// Interaction logic for SendSMSView.xaml
    /// </summary>
    public partial class SendSmsView : UserControl
    {
        public SendSmsView()
        {
            InitializeComponent();
            DataContext = new SendSmsViewModel(new PersonsRepository(), Modem.ModemConnection.Instance);
        }
    }
}
