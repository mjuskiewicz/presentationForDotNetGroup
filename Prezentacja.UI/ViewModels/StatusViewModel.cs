using Prezentacja.Common;
using Prezentacja.Modem;
using Prezentacja.Modem.Commands;

namespace Prezentacja.UI.ViewModels
{
    public class StatusViewModel : NotificationObject
    {
        private IModemConnection _modemConnection;

        private string _model;
        private string _producer;

        public string Model
        {
            get { return !string.IsNullOrEmpty(_model) ? _model : "nieznany"; }
            private set
            {
                if (_model == value) return;
                _model = value;
                RaisePropertyChanged(() => Model);
            }
        }

        public string Producer
        {
            get { return !string.IsNullOrEmpty(_producer) ? _producer : "nieznany"; }
            private set
            {
                if (_producer == value) return;
                _producer = value;
                RaisePropertyChanged(() => Producer);
            }
        }

        public string Status
        {
            get { return IsConnected ? "Połączono" : "Nie połączono"; }
        }

        public bool IsConnected
        {
            get { return _modemConnection != null && _modemConnection.IsConnected; }
        }

        public StatusViewModel(IModemConnection modemConnection)
        {
            _modemConnection = modemConnection;
            _modemConnection.PropertyChanged += OnPropertyChanged;
        }

        void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                if (_modemConnection.IsConnected)
                {
                    _modemConnection.SendRequest(new ATGetProducerCommand((producer) =>
                    {
                        Producer = producer;
                    }));

                    _modemConnection.SendRequest(new ATGetModelCommand((model) =>
                    {
                        Model = model;
                    }));
                }
                else
                {
                    Producer = string.Empty;
                    Model = string.Empty;
                }

                RaisePropertyChanged(() => Status);
            }
        }

    }
}
