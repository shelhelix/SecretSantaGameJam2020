using UnityEngine;

using System;

namespace SecretSantaGameJam2020.Utils {
    public static class SafeActionCaller {
       public static void SafeCall(Action action) {
            try {
                action();
            }
            catch (Exception e) {
                Debug.LogException(e);
            }
       }
       
       public static void SafeCall<T>(Action<T> action, T arg) {
           try {
               action(arg);
           }
           catch (Exception e) {
               Debug.LogException(e);
           }
       }
    }
}