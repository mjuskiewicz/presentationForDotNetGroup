using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prezentacja.Modem.Commands
{
    public class ATSetTextFormatCommand : Request
    {
        public ATSetTextFormatCommand()
            : base("AT+CMGF=1", null, null, null)
        {
        }
    }
}
