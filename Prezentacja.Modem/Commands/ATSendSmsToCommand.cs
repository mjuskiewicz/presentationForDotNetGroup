using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prezentacja.Modem.Commands
{
    public class ATSendSmsToCommand : Request
    {
        public ATSendSmsToCommand(string phoneNumber, string message)
            : base(string.Format("AT+CMGS=\"{0}\"", phoneNumber))
        {
        }
    }
}
