using System;

namespace Prezentacja.Modem.Commands
{
    public class ATGetProducerCommand : Request
    {
        private Action<string> onSuccessByUser;

        public ATGetProducerCommand(Action<string> onSuccess)
            : base("AT+CGMI")
        {
            OnSuccess = GetFirstRow;
            onSuccessByUser = onSuccess;
        }

        private void GetFirstRow(string result)
        {
            var value = result.Replace("\r", string.Empty).Split('\n')[1];
            onSuccessByUser(value);
        }
    }
}
