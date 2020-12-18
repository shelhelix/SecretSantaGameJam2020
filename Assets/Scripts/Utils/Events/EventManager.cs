using System;
using System.Collections.Generic;

namespace SecretSantaGameJam2020.Utils.Events {
    public static class EventManager {
        static readonly Dictionary<Type, BaseHandler> _handlers = new Dictionary<Type, BaseHandler>();

        public static void Subscribe<T>(Action<T> callback) where T : struct {
            if ( callback == null ) {
                return;
            }
            var handler = GetOrCreateHandler<T>();
            handler.Add(callback);            
        }

        public static void Unsubscribe<T>(Action<T> callback) where T : struct {
            if ( callback == null ) {
                return;
            }
            var handler = GetOrCreateHandler<T>();
            handler.Remove(callback);      
        }
        
        public static void Fire<T>(T arg) where T : struct {
            var handler = GetOrCreateHandler<T>();
            handler.Invoke(arg);      
        }

        static Handler<T> GetOrCreateHandler<T>() where T : struct {
            var type = typeof(T);
            if ( _handlers.ContainsKey(type) ) {
                return _handlers[type] as Handler<T>;
            }
            var handler = new Handler<T>();
            _handlers.Add(type, handler);
            return handler;
        }
    }
}

