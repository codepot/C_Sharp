﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ucla.Common.BaseClasses
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Fields

        private bool _isDirty;
        private bool _isMarkedForDeletion;

        #endregion

        #region Properties

        /// <summary>
        /// Property indicating whether entity has changed since it
        /// was retrieved from database.
        /// </summary>
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (_isDirty == value) return;
                _isDirty = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Property that can be set to cause entity to be deleted
        /// from database when persisted.
        /// </summary>
        public bool IsMarkedForDeletion
        {
            get { return _isMarkedForDeletion; }
            set
            {
                if (_isMarkedForDeletion == value) return;
                _isMarkedForDeletion = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(
            [CallerMemberNameAttribute] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            if (propertyName != "IsDirty"
                && propertyName != "IsMarkedForDeletion")
            {
                IsDirty = true;
            }
        }

        #endregion


    }
}
