using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Prezentacja.Common.Helpers
{
    public class General
    {
        static public List<string> GetAvailableComPorts()
        {
            var result = new List<string>();

            foreach (string comName in System.IO.Ports.SerialPort.GetPortNames())
            {
                result.Add(comName);
            }

            return result;
        }

        static public string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            var memberExpression = propertyExpression.Body as MemberExpression;
            return memberExpression.Member.Name;
        }
    }
}
