using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prezentacja.Modem.Commands
{
    public class ATGetModelCommand : Request
    {
        private Action<string> onSuccessByUser;

        public ATGetModelCommand(Action<string> onSuccess)
            : base("AT+CGMM")
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
