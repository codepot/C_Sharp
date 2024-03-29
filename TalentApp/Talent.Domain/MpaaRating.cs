﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucla.Common.BaseClasses;

namespace Talent.Domain
{
    public class MpaaRating : AggregateRootBase
    {
        #region Constructor

        #endregion

        #region Fields

        private int _mpaaRatingId;
        private string _name = String.Empty;
        private string _code = String.Empty;
        private string _description = String.Empty;

        private bool _isInactive;
        private int _displayOrder = 10;

        #endregion

        #region Properties

        public int MpaaRatingId
        {
            get { return _mpaaRatingId; }
            set 
            {
                if (_mpaaRatingId == value) return;
                _mpaaRatingId = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                var val = value ?? String.Empty;
                if (_name == val) return;
                _name = val;
                OnPropertyChanged();
            }
        }

        public string Code
        {
            get { return _code; }
            set
            {
                var val = value ?? String.Empty;
                if (_code == val) return;
                _code = val;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                var val = value ?? String.Empty;
                if (_description == val) return;
                _description = val;
                OnPropertyChanged();
            }
        }

        public bool IsInactive
        {
            get { return _isInactive; }
            set
            {
                if (_isInactive == value) return;
                _isInactive = value;
                OnPropertyChanged();
            }
        }

        public int DisplayOrder
        {
            get { return _displayOrder; }
            set
            {
                if (_displayOrder == value) return;
                _displayOrder = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return Name;
        }

        public override string Validate(string propertyName = null)
        {
            var errors = new List<string>();
            string err;
            switch (propertyName)
            {
                case "Name":
                    if (String.IsNullOrWhiteSpace(Name))
                        errors.Add("Name is required.");
                    break;
                case "Code":
                    if (String.IsNullOrWhiteSpace(Code))
                        errors.Add("Code is required.");
                    break;

                case null:
                    err = Validate("Name");
                    if (err != null) errors.Add(err);

                    err = Validate("Code");
                    if (err != null) errors.Add(err);

                    break;
                default:
                    return null;
            }
            return errors.Count == 0 ? null : String.Join("\r\n", errors);
        }

        #endregion
    }
}
