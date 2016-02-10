using System.Windows.Controls;
using Prezentacja.UI.ViewModels;

namespace Prezentacja.UI.Views
{
    /// <summary>
    /// Interaction logic for StatusView.xaml
    /// </summary>
    public partial class StatusView : UserControl
    {
        public StatusView()
        {
            InitializeComponent();
            DataContext = new StatusViewModel(Modem.ModemConnection.Instance);
        }
    }
}
