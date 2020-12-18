using UnityEngine;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
    public class Sword : BaseGameComponent {
        [NotNull] public GameObject Holder;
        
        public float Damage = 1f;

        void OnCollisionEnter2D(Collision2D other) {
            if ( other.gameObject == Holder ) {
                return;
            }
            ComponentUtils.TryDealDamage(other.gameObject, Damage);
        }
    }
}