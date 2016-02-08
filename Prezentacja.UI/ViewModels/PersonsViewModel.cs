using Prezentacja.Common;
using Prezentacja.DTO;
using Prezentacja.Service;
using System.Collections.Generic;
using System.Windows;

namespace Prezentacja.UI.ViewModels
{
    public class PersonsViewModel : NotificationObject
    {
        #region Properties

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            private set
            {
                if (_errorMessage == value) return;
                _errorMessage = value;
                RaisePropertyChanged(() => ErrorMessage);
            }
        }

        private string _successMessage;
        public string SuccessMessage
        {
            get { return _successMessage; }
            private set 
            {
                if (_successMessage == value) return;
                _successMessage = value;
                RaisePropertyChanged(() => SuccessMessage);
            }
        }
        
        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand
        {
            get { return _saveCommand; }
            private set 
            {
                if (_saveCommand == value) return;
                _saveCommand = value;
                RaisePropertyChanged(() => SaveCommand);
            }
        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand
        {
            get { return _cancelCommand; }
            private set
            {
                if (_cancelCommand == value) return;
                _cancelCommand = value;
                RaisePropertyChanged(() => CancelCommand);
            }
        }

        private DelegateCommand _deleteCommand;
        public DelegateCommand DeleteCommand
        {
            get { return _deleteCommand; }
            private set
            {
                if (_deleteCommand == value) return;
                _deleteCommand = value;
                RaisePropertyChanged(() => DeleteCommand);
            }
        }

        private DelegateCommand _editCommand;

        public DelegateCommand EditCommand
        {
            get { return _editCommand; }
            private set
            {
                if (_editCommand == value) return;
                _editCommand = value;
                RaisePropertyChanged(() => EditCommand);
            }
        }

        private DelegateCommand _addCommand;

        public DelegateCommand AddCommand
        {
            get { return _addCommand; }
            private set
            {
                if (_addCommand == value) return;
                _addCommand = value;
                RaisePropertyChanged(() => AddCommand);
            }
        }

        private PersonsRepository _usersRepository;

        public List<Person> Persons
        {
            get { return _usersRepository.GetAll(); }
        }

        private Person _editedPerson;

        public Person EditedPerson
        {
            get { return _editedPerson; }
            set 
            {
                if (_editedPerson == value) return;
                _editedPerson = value;
                RaisePropertyChanged(() => EditedPerson);
                EditCommand.UpdateCanExecuteState();
                DeleteCommand.UpdateCanExecuteState();
            }
        }

        private bool _isVisibleForm;

        public bool IsVisibleForm
        {
            get { return _isVisibleForm; }
            private set 
            {
                if (_isVisibleForm == value) return;
                _isVisibleForm = value;
                RaisePropertyChanged(() => IsVisibleForm);
            }
        }
        
        #endregion
        
        public PersonsViewModel(PersonsRepository usersRepository)
        {
            _usersRepository = usersRepository;
            AddCommand = new DelegateCommand(OnAdd, () => true);
            CancelCommand = new DelegateCommand(OnCancel, () => true);
            EditCommand = new DelegateCommand(OnEdit, () => EditedPerson != null);
            DeleteCommand = new DelegateCommand(OnDelete, () => EditedPerson != null);
            SaveCommand = new DelegateCommand(OnSave, () => true);
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