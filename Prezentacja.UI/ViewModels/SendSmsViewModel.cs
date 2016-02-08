using Prezentacja.Common;
using Prezentacja.DTO;
using Prezentacja.Modem.Commands;
using Prezentacja.Service;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prezentacja.UI.ViewModels
{
    public class SendSmsViewModel : NotificationObject
    {
        #region properties

        private Modem.IModemConnection _modemConnection;
        private IRepository<Person> _usersRepository;

        private DelegateCommand _sendCommand;
        public DelegateCommand SendCommand
        {
            get { return _sendCommand; }
            set { _sendCommand = value; }
        }

        private DelegateCommand _clearCommand;
        public DelegateCommand ClearCommand
        {
            get { return _clearCommand; }
            set { _clearCommand = value; }
        }

        public DelegateCommand RehreshCommand { get; set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message == value) return;
                _message = value;
                RaisePropertyChanged(() => Message);
            }
        }

        private string _smsText;
        public string SmsText
        {
            get { return _smsText; }
            set 
            {
                if (_smsText == value) return;
                _smsText = value;
                RaisePropertyChanged(() => SmsText);
                SendCommand.UpdateCanExecuteState();
            }
        }

        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                if (_selectedPerson == value) return;
                _selectedPerson = value;
                RaisePropertyChanged(() => SelectedPerson);
            }
        }

        public IEnumerable<Person> Persons
        {
            get
            {
                return _usersRepository.GetAll();
            }
        }
        
        void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                SendCommand.UpdateCanExecuteState();
            }
        }

        #endregion

        public SendSmsViewModel(IRepository<Person> usersRepository, Modem.IModemConnection modemConnection)
        {
            _usersRepository = usersRepository;
            _modemConnection = modemConnection;
            _modemConnection.PropertyChanged += OnPropertyChanged;
            
            SendCommand = new DelegateCommand(OnSend, CanSend);
            ClearCommand = new DelegateCommand(OnClear);
            RehreshCommand = new DelegateCommand(() => RaisePropertyChanged(() => Persons));
            
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string foo = sp.ReadExisting();

        }

        private void OnClear()
        {
            SelectedPerson = null;
            SmsText = string.Empty;
        }

        private void OnSend()
        {
            string sendCommand = string.Format("AT+CMGS=\"{0}\"", SelectedPerson.PhoneNumber);
            
            _modemConnection.SendRequest(new ATSetTextFormatCommand());
            _modemConnection.SendRequest(new Request(sendCommand, (s) =>
            {
                _modemConnection.SendCommand(SmsText);
                _modemConnection.SendCommand("\u001A");
                Message = string.Format("{0}:{1}", DateTime.Now, "SMS został wysłany.");
            }, () =>
            {
                Message = string.Format("{0}:{1}", DateTime.Now, "Wystąpił bład podczas wysyłania.");
            }));
        }

        private bool CanSend()
        {
            return !string.IsNullOrEmpty(SmsText)
                && SelectedPerson != null
                && _modemConnection.IsConnected;
        }

    }
}
