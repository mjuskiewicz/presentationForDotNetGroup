using System.ComponentModel;
using Prezentacja.Modem.Commands;

namespace Prezentacja.Modem
{
    public interface IModemConnection : INotifyPropertyChanged
    {
        bool IsConnected { get; }

        bool OpenConnection(string port, int baudRate, int dataBits, int writeBudderSize);

        void CloseConnection();

        void SendRequest(Request userRequest);

        void SendCommand(string command);
    }
}
