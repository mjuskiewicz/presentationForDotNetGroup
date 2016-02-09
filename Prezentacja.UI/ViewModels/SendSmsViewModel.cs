using System;
using System.Collections.Generic;
using System.IO.Ports;
using Prezentacja.Common;
using Prezentacja.DTO;
using Prezentacja.Modem.Commands;

namespace Prezentacja.UI.ViewModels
{
    public class SendSmsViewModel : NotificationObject
    {
        private string _message;
        private string _smsText;
        private Modem.IModemConnection _modemConnection;
        private IRepository<Person> _usersRepository;
        private Person _selectedPerson;
        private DelegateCommand _clearCommand;
        private DelegateCommand _sendCommand;

        public SendSmsViewModel(IRepository<Person> usersRepository, Modem.IModemConnection modemConnection)
        {
            _usersRepository = usersRepository;
            _modemConnection = modemConnection;
            _modemConnection.PropertyChanged += OnPropertyChanged;

            SendCommand = new DelegateCommand(OnSend, CanSend);
            ClearCommand = new DelegateCommand(OnClear);
            RehreshCommand = new DelegateCommand(() => RaisePropertyChanged(() => Persons));
        }

        public DelegateCommand SendCommand
        {
            get { return _sendCommand; }
            set { _sendCommand = value; }
        }

        public DelegateCommand ClearCommand
        {
            get { return _clearCommand; }
            set { _clearCommand = value; }
        }

        public DelegateCommand RehreshCommand { get; set; }
        
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                if (_message == value) return;
                _message = value;
                RaisePropertyChanged(() => Message);
            }
        }
        
        public string SmsText
        {
            get
            {
                return _smsText;
            }

            set
            {
                if (_smsText == value) return;
                _smsText = value;
                RaisePropertyChanged(() => SmsText);
                SendCommand.UpdateCanExecuteState();
            }
        }

        public Person SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }

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

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                SendCommand.UpdateCanExecuteState();
            }
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
            _modemConnection.SendRequest(new Request(sendCommand, OnSendSuccess(), OnSendError()));
        }

        private Action<string> OnSendSuccess()
        {
            return (s) =>
            {
                _modemConnection.SendCommand(SmsText);
                _modemConnection.SendCommand("\u001A");
                Message = string.Format("{0}:{1}", DateTime.Now, "SMS został wysłany.");
            };
        }

        private Action OnSendError()
        {
            return () =>
            {
                Message = string.Format("{0}:{1}", DateTime.Now, "Wystąpił bład podczas wysyłania.");
            };
        }

        private bool CanSend()
        {
            return !string.IsNullOrEmpty(SmsText)
                && SelectedPerson != null
                && _modemConnection.IsConnected;
        }
    }
}
