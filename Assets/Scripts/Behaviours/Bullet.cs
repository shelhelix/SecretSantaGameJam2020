using UnityEngine;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
    public class Bullet : BaseGameComponent {
        [NotNull] public Rigidbody2D Rigidbody;
        [NotNull] public Collider2D  Collider;

        public float Damage = 0.1f;
        public float Speed  = 5f;
        
        public void Init(Collider2D ownerCollider, Vector2 direction) {
            Rigidbody.rotation = -Vector2.SignedAngle(direction, Vector2.up);
            Rigidbody.AddForce(direction * Speed, ForceMode2D.Impulse);
            Physics2D.IgnoreCollision(ownerCollider, Collider);
        }

        void OnCollisionEnter2D(Collision2D other) {
            var contact = other.contacts[0]; 
            ComponentUtils.DefaultDealDamage(contact.collider.gameObject, Damage);
            Destroy(gameObject);
        }
    }
}