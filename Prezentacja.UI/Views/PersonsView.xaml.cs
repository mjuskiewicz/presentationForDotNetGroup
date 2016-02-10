using System.Windows.Controls;
using Prezentacja.Service;
using Prezentacja.UI.ViewModels;

namespace Prezentacja.UI.Views
{
    /// <summary>
    /// Interaction logic for UsersControls.xaml
    /// </summary>
    public partial class UsersControls : UserControl
    {
        public UsersControls()
        {
            InitializeComponent();
            this.DataContext = new PersonsViewModel(new PersonsRepository());
        }
    }
}
