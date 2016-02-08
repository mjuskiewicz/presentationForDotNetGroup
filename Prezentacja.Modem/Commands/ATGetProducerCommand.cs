using System;

namespace Prezentacja.Modem.Commands
{
    public class ATGetProducerCommand : Request
    {
        private Action<string> OnSuccessByUser;

        public ATGetProducerCommand(Action<string> onSuccess)
            : base("AT+CGMI")
        {
            OnSuccess = GetFirstRow;
            OnSuccessByUser = onSuccess;
        }

        private void GetFirstRow(string result)
        {
            var value = result.Replace("\r", string.Empty).Split('\n')[1];
            OnSuccessByUser(value);
        }
    }
}
