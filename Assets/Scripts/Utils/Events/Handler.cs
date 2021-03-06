﻿using System;
using System.Collections.Generic;

namespace SecretSantaGameJam2020.Utils.Events {
    public class Handler<T> : BaseHandler {
        readonly List<Action<T>> _actions = new List<Action<T>>();

        public void Add(Action<T> action) {
            if ( _actions.Contains(action) ) {
                return;
            }
            _actions.Add(action);
        }

        public void Remove(Action<T> action) {
            _actions.Remove(action);
        }
        
        public void Invoke(T arg) {
            var actionsCopy = new List<Action<T>>(_actions);
            foreach (var action in actionsCopy) {
                SafeActionCaller.SafeCall(action, arg);
            }
        }
    }
}