using Prezentacja.UI.ViewModels;
using System.Windows.Controls;

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
            this.DataContext = new StatusViewModel(Modem.ModemConnection.Instance);
        }
    }
}
