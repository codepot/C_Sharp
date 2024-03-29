﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Talent.DataAccess.Ado;
using Talent.Domain;
using Ucla.Common.Interfaces;

namespace Talent.WpfClient
{
    public partial class PeopleView : UserControl
    {
        IRepository<Person> _personRepository;
        ObservableCollection<Person> _people = new
            ObservableCollection<Person>();

        public PeopleView()
        {
            InitializeComponent();
            _personRepository = new PersonRepository();
            this.DataContext = _people;

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Find,
                Find, CanFind));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New,
                New, CanNew));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete,
                Delete, CanDelete));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save,
                Save, CanSave));
            CommandBindings.Add(new CommandBinding(CustomCommands.Cancel,
                Cancel, CanCancel));
        }

        private void CanFind(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResultsListBox.SelectedItem == null
                || !((Person)ResultsListBox.SelectedItem).IsDirty;
        }

        private void Find(object sender, ExecutedRoutedEventArgs e)
        {
            Search();
        }

        private void CanNew(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResultsListBox.SelectedItem == null
                || !((Person)ResultsListBox.SelectedItem).IsDirty;
        }

        private void New(object sender, ExecutedRoutedEventArgs e)
        {
            var newItem = new Person();
            _people.Add(newItem);
            ResultsListBox.SelectedItem = newItem;
        }

        private void CanDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResultsListBox.SelectedItem != null;
        }

        private void Delete(object sender, ExecutedRoutedEventArgs e)
        {
            var item = (Person)ResultsListBox.SelectedItem;
            if (item == null) return;
            var msg = String.Format("Are you sure you want to delete {0}?",
                item.FirstLastName);
            if (MessageBox.Show(msg, "Confirm Delete?",
                MessageBoxButton.YesNo, MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {
                item.IsMarkedForDeletion = true;
                _personRepository.Persist(item);
                _people.Remove(item);
                ResultsListBox.SelectedItem = null;
                Search();
            }
        }

        private void CanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResultsListBox.SelectedItem != null
                && ((Person)ResultsListBox.SelectedItem).IsGraphDirty
                && ((Person)ResultsListBox.SelectedItem).Error == null;
        }

        private void Save(object sender, ExecutedRoutedEventArgs e)
        {
            var item = (Person)ResultsListBox.SelectedItem;
            if (item == null) return;
            _personRepository.Persist(item);
            Search();
        }

        private void CanCancel(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResultsListBox.SelectedItem != null
                && ((Person)ResultsListBox.SelectedItem).IsGraphDirty;
        }

        private void Cancel(object sender, ExecutedRoutedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            var previouslySelectedItem = (Person)ResultsListBox.SelectedItem;
            _people.Clear();
            PersonCriteria crit = new PersonCriteria
            {
                Name = NameCriterion.Text
            };
            foreach (var item in _personRepository.Fetch(crit))
            {
                _people.Add(item);
            }
            Person selectedPerson = null;
            if (previouslySelectedItem != null)
            {
                selectedPerson = _people
                .Where(o => o.PersonId == previouslySelectedItem.PersonId)
                .FirstOrDefault();
            }
            if (selectedPerson != null)
            {
                ResultsListBox.SelectedItem = selectedPerson;
            }
            else
            {
                ResultsListBox.SelectedItem = null;
            }
        }

    }
}

