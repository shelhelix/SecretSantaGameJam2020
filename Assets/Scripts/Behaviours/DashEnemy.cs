using UnityEngine;

using SecretSantaGameJam2020.State;
using SecretSantaGameJam2020.Utils;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
    public class DashEnemy : BaseEnemy, IDestructable {
        public float DashPower = 4f;
        public float Damage    = 1f;
        public float Hp        = 3f;

        [NotNull] public Rigidbody2D RigidBody;

        readonly Timer _dashReloadTimer = new Timer();
        
        Transform _playerTrans;
        
        public void Init(GameObject player) {
            base.Init();
            _playerTrans = player.transform;
            _dashReloadTimer.Init(1f);
        }

        void OnCollisionEnter2D(Collision2D other) {
            var player = other.contacts[0].collider.gameObject.GetComponent<Player>();
            if ( player ) {
                player.GetDamage(Damage);
                Destroy(gameObject);
            }
        }

        void FixedUpdate() {
            if ( !BaseInited ) {
                return;
            }

            if ( _dashReloadTimer.Tick(Time.fixedDeltaTime) ) {
                var dashDirection = (_playerTrans.position - transform.position).normalized;
                RigidBody.velocity = Vector2.zero;
                RigidBody.AddForce(DashPower * dashDirection, ForceMode2D.Impulse);
            }
        }

        public void GetDamage(float damage) {
            Hp = ComponentUtils.DefaultGetDamage(gameObject, Hp, damage);
            if ( Hp <= 0 ) {
                GameState.Instance.Score += 200;
            }
        }
    }
}