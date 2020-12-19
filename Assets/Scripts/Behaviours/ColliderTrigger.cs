using UnityEngine;

using System;

using SecretSantaGameJam2020.Behaviours.Common;

namespace SecretSantaGameJam2020.Behaviours {
    public class ColliderTrigger : BaseGameComponent {
        public event Action<Collider2D> OnTriggerEnter;
        public event Action<Collider2D> OnTriggerExit;
        
        void OnTriggerEnter2D(Collider2D other) {
            OnTriggerEnter?.Invoke(other);
        }

        void OnTriggerExit2D(Collider2D other) {
            OnTriggerExit?.Invoke(other);
        }
    }
}