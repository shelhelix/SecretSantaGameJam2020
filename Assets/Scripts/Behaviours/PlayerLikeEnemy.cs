using UnityEngine;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Events;
using SecretSantaGameJam2020.Utils;
using SecretSantaGameJam2020.Utils.CustomAttributes;
using SecretSantaGameJam2020.Utils.Events;

namespace SecretSantaGameJam2020.Behaviours {
    public class PlayerLikeEnemy : BaseGameComponent, IDestructable {
        public float Speed        = 1f;
        public float SprintSpeed  = 2f;
        public float Hp           = 3;
        public float SpinDistance = 5f;
        public float TorqueSpeed  = 1f;
        public float BreakPower   = 15f;
        
        public float RotationSpeed = 2f;
        
        public float MaxSpeedMagnitude = 2f;

        
        [NotNull] public Rigidbody2D Rigidbody;
        
        Transform _playerTrans;

        bool _inited;
        
        public void Init(GameObject player) {
            _playerTrans = player.transform;
            _inited = true;
            EventManager.Subscribe<PlayerDied>(OnPlayerDied);
        }

        void OnDestroy() {
            EventManager.Unsubscribe<PlayerDied>(OnPlayerDied);
        }

        void FixedUpdate() {
            if ( !_inited ) {
                return;
            }
            var vectorToPlayer = (_playerTrans.position - transform.position);
            if ( SpinDistance > vectorToPlayer.magnitude ) {
                Rigidbody.AddTorque(TorqueSpeed);
                Rigidbody.angularDrag = 0.05f;
            }
            else {
                Rigidbody.angularDrag = BreakPower;
                var angleDiff   = -Vector2.SignedAngle(vectorToPlayer, transform.right);
                Rigidbody.rotation += LerpRotation(0, angleDiff, RotationSpeed);
            }
            var forcePowerDirection = (vectorToPlayer.magnitude < 2.5f) ? -vectorToPlayer.normalized : vectorToPlayer.normalized;
            ComponentUtils.MoveRigidbody(Rigidbody, forcePowerDirection * Speed);
            ComponentUtils.LimitRigidbodySpeed(Rigidbody, Speed);
        }

        float LerpRotation(float a, float b, float coeff) {
            return (b - a) * coeff + a;
        }

        void OnPlayerDied(PlayerDied e) {
            _inited = false;
        }

        public void GetDamage(float damage) {
            Hp -= damage;
            if (Hp <= 0f) {
                Debug.Log("Enemy is dead");
                Destroy(gameObject);
            }
        }
    }
}