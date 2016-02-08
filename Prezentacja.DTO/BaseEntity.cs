using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prezentacja.Common
{
    public abstract class BaseEntity : NotificationObject, IEntity
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set 
            {
                if (_id == value) return;
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }
        
    }
}
