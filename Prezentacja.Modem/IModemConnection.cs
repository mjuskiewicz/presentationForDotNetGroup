using Prezentacja.Modem.Commands;
using System.ComponentModel;
using System.IO.Ports;

namespace Prezentacja.Modem
{
    public interface IModemConnection : INotifyPropertyChanged
    {
        bool OpenConnection(string port, int baudRate, int dataBits, int writeBudderSize);
        void CloseConnection();
        bool IsConnected { get; }
        void SendRequest(Request userRequest);
        void SendCommand(string command);
    }
}
