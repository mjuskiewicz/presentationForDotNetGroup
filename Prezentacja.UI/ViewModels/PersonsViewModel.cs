using System.Collections.Generic;
using System.Windows;
using Prezentacja.Common;
using Prezentacja.DTO;
using Prezentacja.Service;

namespace Prezentacja.UI.ViewModels
{
    public class PersonsViewModel : NotificationObject
    {
        private bool _isVisibleForm;
        private string _errorMessage;
        private string _successMessage;
        private DelegateCommand _addCommand;
        private DelegateCommand _cancelCommand;
        private DelegateCommand _deleteCommand;
        private DelegateCommand _editCommand;
        private DelegateCommand _saveCommand;
        private PersonsRepository _usersRepository;
        private Person _editedPerson;

        public PersonsViewModel(PersonsRepository usersRepository)
        {
            _usersRepository = usersRepository;
            AddCommand = new DelegateCommand(OnAdd, () => true);
            CancelCommand = new DelegateCommand(OnCancel, () => true);
            EditCommand = new DelegateCommand(OnEdit, () => EditedPerson != null);
            DeleteCommand = new DelegateCommand(OnDelete, () => EditedPerson != null);
            SaveCommand = new DelegateCommand(OnSave, () => true);
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            private set
            {
                if (_errorMessage == value) return;
                _errorMessage = value;
                RaisePropertyChanged(() => ErrorMessage);
            }
        }

        public string SuccessMessage
        {
            get
            {
                return _successMessage;
            }

            private set
            {
                if (_successMessage == value) return;
                _successMessage = value;
                RaisePropertyChanged(() => SuccessMessage);
            }
        }
        
        public DelegateCommand SaveCommand
        {
            get
            {
                return _saveCommand;
            }

            private set
            {
                if (_saveCommand == value) return;
                _saveCommand = value;
                RaisePropertyChanged(() => SaveCommand);
            }
        }
        
        public DelegateCommand CancelCommand
        {
            get
            {
                return _cancelCommand;
            }

            private set
            {
                if (_cancelCommand == value) return;
                _cancelCommand = value;
                RaisePropertyChanged(() => CancelCommand);
            }
        }
        
        public DelegateCommand DeleteCommand
        {
            get
            {
                return _deleteCommand;
            }

            private set
            {
                if (_deleteCommand == value) return;
                _deleteCommand = value;
                RaisePropertyChanged(() => DeleteCommand);
            }
        }
        
        public DelegateCommand EditCommand
        {
            get
            {
                return _editCommand;
            }

            private set
            {
                if (_editCommand == value) return;
                _editCommand = value;
                RaisePropertyChanged(() => EditCommand);
            }
        }
        
        public DelegateCommand AddCommand
        {
            get
            {
                return _addCommand;
            }

            private set
            {
                if (_addCommand == value) return;
                _addCommand = value;
                RaisePropertyChanged(() => AddCommand);
            }
        }
        
        public List<Person> Persons
        {
            get { return _usersRepository.GetAll(); }
        }
        
        public Person EditedPerson
        {
            get
            {
                return _editedPerson;
            }

            set
            {
                if (_editedPerson == value) return;
                _editedPerson = value;
                RaisePropertyChanged(() => EditedPerson);
                EditCommand.UpdateCanExecuteState();
                DeleteCommand.UpdateCanExecuteState();
            }
        }
        
        public bool IsVisibleForm
        {
            get
            {
                return _isVisibleForm;
            }

            private set
            {
                if (_isVisibleForm == value) return;
                _isVisibleForm = value;
                RaisePropertyChanged(() => IsVisibleForm);
            }
        }

        private void OnDelete()
        {
            _usersRepository.DeleteById(EditedPerson.Id);
            RaisePropertyChanged(() => Persons);
        }

        private void OnAdd()
        {
            EditedPerson = new Person();
            IsVisibleForm = true;
        }

        private void OnCancel()
        {
            RaisePropertyChanged(() => Persons);
            IsVisibleForm = false;
        }

        private void OnEdit()
        {
            IsVisibleForm = true;
        }

        private void OnSave()
        {
            if (EditedPerson.Id == 0)
                _usersRepository.Add(EditedPerson);
            else
                _usersRepository.Update(EditedPerson);

            ShowSuccessMessage("Element został zaktualizowany.");

            RaisePropertyChanged(() => Persons);
        }

        private void ShowSuccessMessage(string text)
        {
            MessageBox.Show(text, "Komunikat", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}