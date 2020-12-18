using UnityEngine;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Events;
using SecretSantaGameJam2020.Utils;
using SecretSantaGameJam2020.Utils.CustomAttributes;
using SecretSantaGameJam2020.Utils.Events;

namespace SecretSantaGameJam2020.Behaviours {
    public class Enemy : BaseGameComponent, IDestructable {
        public float Speed  = 11;
        public float Hp     = 3;
        public float Damage = 1;
        
        
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


        void OnTriggerEnter2D(Collider2D other) {
            ComponentUtils.TryDealDamage(other.gameObject, Damage);
        }

        void Update() {
            if ( !_inited ) {
                return;
            }
            var directionToPlayer = (_playerTrans.position - transform.position).normalized;
            Rigidbody.velocity = directionToPlayer * Speed;
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