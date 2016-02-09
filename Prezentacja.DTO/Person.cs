using Prezentacja.Common;

namespace Prezentacja.DTO
{
    public class Person : BaseEntity
    {
        private int _age;
        private string _name;
        private string _surname;
        private string _phoneNumber;

        public int Age
        {
            get
            {
                return _age;
            }

            set
            {
                if (_age == value) return;
                _age = value;
                RaisePropertyChanged(() => Age);
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value) return;
                _name = value;
                RaisePropertyChanged(() => Name);
                RaisePropertyChanged(() => FullName);
            }
        }
        
        public string Surname
        {
            get
            {
                return _surname;
            }

            set
            {
                if (_surname == value) return;
                _surname = value;
                RaisePropertyChanged(() => Surname);
                RaisePropertyChanged(() => FullName);
            }
        }
        
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }

            set
            {
                if (_phoneNumber == value) return;
                _phoneNumber = value;
                RaisePropertyChanged(() => PhoneNumber);
            }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", Name, Surname);
            }
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
