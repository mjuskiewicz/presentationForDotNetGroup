using Prezentacja.Common;

namespace Prezentacja.DTO
{
    public class Message : BaseEntity
    {
        private int _status;
        private string _phoneNumber;
        private string _text;

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }

            set
            {
                if (_phoneNumber == null) return;
                _phoneNumber = value;
                RaisePropertyChanged(() => PhoneNumber);
            }
        }
        
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (_text == value) return;
                _text = value;
                RaisePropertyChanged(() => Text);
            }
        }
        
        public int Status
        {
            get
            {
                return _status;
            }

            set
            {
                if (_status == value) return;
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }
    }
}
