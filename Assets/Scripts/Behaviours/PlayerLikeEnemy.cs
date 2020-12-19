﻿using UnityEngine;

using SecretSantaGameJam2020.Utils;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
    public class PlayerLikeEnemy : BaseEnemy, IDestructable {
        public float Speed        = 1f;
        public float Hp           = 3;
        public float SpinDistance = 5f;
        public float TorqueSpeed  = 1f;
        public float BreakPower   = 15f;
        
        public float RotationSpeed = 2f;

        [NotNull] public Rigidbody2D Rigidbody;
        
        Transform _playerTrans;
        
        public void Init(GameObject player) {
            base.Init();
            _playerTrans = player.transform;
        }

        void FixedUpdate() {
            if ( !BaseInited ) {
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
        
        public void GetDamage(float damage) {
            Hp = ComponentUtils.DefaultHpBehaviour(gameObject, Hp, damage);
        }
    }
}