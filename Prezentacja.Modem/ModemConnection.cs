using Prezentacja.Modem.Commands;
using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace Prezentacja.Modem
{
    public class ModemConnection : IModemConnection
    {
        #region properties 
        private static readonly object padLock = new object();

        private static ModemConnection instance;
        private SerialPort sp;
        private AutoResetEvent callInProgress = new AutoResetEvent(false);
        private Request _processedRequest;
        
        public static ModemConnection Instance
        {
            get
            {
                lock (padLock)
                {
                    if (instance == null)
                    {
                        instance = new ModemConnection();
                    }
                    return instance;
                }
            }
        }
        #endregion

        private ModemConnection() { }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            var answer = sp.ReadExisting();

            var isResultLine = answer.Contains("OK") || answer.Contains(">") || answer.Contains("ERROR");

            if (isResultLine)
            {
                InvokeAction(answer.Contains("OK") || answer.Contains(">"), answer);
                callInProgress.Set();
            }
        }

        public bool OpenConnection(string port, int baudRate, int dataBits, int writeBudderSize)
        {
            sp = new SerialPort() { NewLine = System.Environment.NewLine, PortName = port, BaudRate = baudRate, DataBits = dataBits, WriteBufferSize = writeBudderSize };

            try
            {
                if (!sp.IsOpen)
                {
                    sp.Open();
                    sp.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);
                    RaisePropertyChanged("IsConnected");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        public void CloseConnection()
        {
            if (sp.IsOpen)
            {
                sp.Close();
                RaisePropertyChanged("IsConnected");
            }
        }

        public bool IsConnected
        {
            get
            {
                return sp != null && sp.IsOpen;
            }
        }

        public void SendCommand(string command)
        {
            sp.WriteLine(command);
        }

        public void SendRequest(Request userRequest)
        {
            _processedRequest = userRequest;

            sp.WriteLine(userRequest.Query);

            var signal = callInProgress.WaitOne();

            if (!signal)
                InvokeAction(null, string.Empty);
        }

        private void InvokeAction(bool? isSuccess, string answer)
        {
            var requestToInvoke = _processedRequest;
            _processedRequest = null;

            if (requestToInvoke != null)
                requestToInvoke.InvokeAction(isSuccess, answer);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
