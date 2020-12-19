using UnityEngine;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
    public class Sword : BaseGameComponent {
        [NotNull] public Rigidbody2D HolderRigidbody;

        public float Damage = 1f;
        public float SlowDownCoeff = 0.5f;
        
        
        void OnCollisionEnter2D(Collision2D other) {
            if ( other.gameObject == HolderRigidbody.gameObject ) {
                return;
            }
            var contact = other.contacts[0]; 
            var destructable = contact.collider.gameObject.GetComponent<IDestructable>();
            if ( destructable != null ) {
                destructable.GetDamage(Damage);
                print("Deal damage to " + contact.collider.gameObject);
            }
        }
    }
}