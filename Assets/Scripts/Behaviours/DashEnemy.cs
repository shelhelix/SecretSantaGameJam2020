using UnityEngine;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
    public class DashEnemy : BaseGameComponent {
        public float DashPower = 4f;
        public float Damage    = 1f;

        [NotNull] public Rigidbody2D RigidBody;

        Transform _playerTrans;

        Timer _dashReloadTimer = new Timer();

        bool _inited;
        
        public void Init(GameObject player) {
            _playerTrans = player.transform;
            _inited = true;
            _dashReloadTimer.Init(1f);
        }

        void OnCollisionEnter2D(Collision2D other) {
            var destructable = other.gameObject.GetComponent<IDestructable>();
            if ( destructable != null ) {
                destructable.GetDamage(Damage);
            }
            Destroy(gameObject);
        }

        void FixedUpdate() {
            if ( !_inited ) {
                return;
            }

            if ( _dashReloadTimer.Tick(Time.fixedDeltaTime) ) {
                var dashDirection = (_playerTrans.position - transform.position).normalized;
                RigidBody.velocity = Vector2.zero;
                RigidBody.AddForce(DashPower * dashDirection, ForceMode2D.Impulse);
            }
        }
    }
}