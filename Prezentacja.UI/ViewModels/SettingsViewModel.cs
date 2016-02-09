using System.Collections.Generic;
using Prezentacja.Common;
using Prezentacja.Common.Helpers;
using Prezentacja.Modem;

namespace Prezentacja.UI.ViewModels
{
    public class SettingsViewModel : NotificationObject
    {
        private string _comPort;
        private int? _dataBit = 8;
        private int? _transferSpeed = 9600;
        private int? _bufforSize = 1024;

        private DelegateCommand _connectCommand;
        private DelegateCommand _disconnectCommand;
        private ModemConnection _modemConnection;

        public SettingsViewModel()
        {
            ConnectCommand = new DelegateCommand(OnConnect, CanConnect);
            DisconnectCommand = new DelegateCommand(OnDisconnect, CanDisconnect);
            _modemConnection = ModemConnection.Instance;
        }

        public List<string> AvailableComPorts
        {
            get
            {
                return General.GetAvailableComPorts();
            }
        }

        public string ComPort
        {
            get
            {
                return _comPort;
            }

            private set
            {
                if (_comPort == value) return;
                _comPort = value;
                RaisePropertyChanged(() => ComPort);
                ConnectCommand.UpdateCanExecuteState();
            }
        }

        public int? DataBit
        {
            get
            {
                return _dataBit;
            }

            private set
            {
                if (_dataBit == value) return;
                _dataBit = value;
                RaisePropertyChanged(() => DataBit);
                ConnectCommand.UpdateCanExecuteState();
            }
        }

        public int? TransferSpeed
        {
            get
            {
                return _transferSpeed;
            }

            private set
            {
                if (_transferSpeed == value) return;
                _transferSpeed = value;
                RaisePropertyChanged(() => TransferSpeed);
                ConnectCommand.UpdateCanExecuteState();
            }
        }

        public int? BufforSize
        {
            get
            {
                return _bufforSize;
            }

            private set
            {
                if (_bufforSize == value) return;
                _bufforSize = value;
                RaisePropertyChanged(() => BufforSize);
                ConnectCommand.UpdateCanExecuteState();
            }
        }

        public DelegateCommand ConnectCommand
        {
            get { return _connectCommand; }
            set { _connectCommand = value; }
        }

        public DelegateCommand DisconnectCommand
        {
            get { return _disconnectCommand; }
            set { _disconnectCommand = value; }
        }

        private void OnConnect()
        {
            if (_modemConnection.OpenConnection(ComPort, TransferSpeed.Value, DataBit.Value, BufforSize.Value))
            {
                ConnectCommand.UpdateCanExecuteState();
                DisconnectCommand.UpdateCanExecuteState();
            }
        }

        private bool CanConnect()
        {
            return !string.IsNullOrEmpty(ComPort)
                && DataBit.HasValue
                && TransferSpeed.HasValue
                && BufforSize.HasValue
                && _modemConnection != null
                && !_modemConnection.IsConnected;
        }

        private void OnDisconnect()
        {
            _modemConnection.CloseConnection();
            ConnectCommand.UpdateCanExecuteState();
            DisconnectCommand.UpdateCanExecuteState();
        }

        private bool CanDisconnect()
        {
            return _modemConnection != null
                && _modemConnection.IsConnected;
        }
    }
}
