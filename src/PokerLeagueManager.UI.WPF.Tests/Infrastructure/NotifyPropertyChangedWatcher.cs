﻿using System.Collections.Generic;
using System.ComponentModel;

namespace PokerLeagueManager.UI.WPF.Tests.Infrastructure
{
    public class NotifyPropertyChangedWatcher
    {
        private Dictionary<string, int> _propertyChangedEvents;

        public NotifyPropertyChangedWatcher(INotifyPropertyChanged target)
        {
            _propertyChangedEvents = new Dictionary<string, int>();
            target.PropertyChanged += OnPropertyChanged;
        }

        public bool HasPropertyChanged(string propertyName)
        {
            return _propertyChangedEvents.ContainsKey(propertyName);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_propertyChangedEvents.ContainsKey(e.PropertyName))
            {
                _propertyChangedEvents.Add(e.PropertyName, 0);
            }

            _propertyChangedEvents[e.PropertyName]++;
        }
    }
}
