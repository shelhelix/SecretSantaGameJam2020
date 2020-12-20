using UnityEngine;

using SecretSantaGameJam2020.Utils;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
    public class Turret : BaseEnemy, IDestructable {

        [NotNull] public ColliderTrigger StartFireRange;
        [NotNull] public ColliderTrigger EndFireRange;
        [NotNull] public GameObject      BulletPrefab;
        [NotNull] public Collider2D      Collider;

        public float BulletFireDelay = 0.5f;
        public float RotationSpeed   = 0.1f;
        public float Hp = 3;
        
        readonly Timer _bulletFireRateTimer = new Timer();
        
        Transform _target;


        bool _isFiring;

        public override void Init() {
            StartFireRange.OnTriggerEnter += OnEnterFireRange;
            EndFireRange.OnTriggerExit    += OnLeaveFireRange;
            base.Init();   
        }

        void Update() {
            if ( !_target ) {
                return;
            }

            if ( _isFiring ) {
                TryFire();
            }

            LookToTarget();
        }

        void LookToTarget() {
            if ( !_target ) {
                Debug.LogError("Can't look to target. Target not found.");
                return;
            }
            var vectorToPlayer = _target.position - transform.position;
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + ComponentUtils.SmoothRotate(transform.up, vectorToPlayer, RotationSpeed));
        }
        
        void TryFire() {
            var vectorToTarget = _target.position - transform.position;
            if ( !_bulletFireRateTimer.Tick(Time.deltaTime) ) {
                return;
            }
            var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            var bulletComp = bullet.GetComponent<Bullet>();
            if ( !bulletComp ) {
                Debug.LogError("can't find Bullet component in bullet object");
                return;
            }
            bulletComp.Init(Collider, vectorToTarget.normalized);
        }

        void OnEnterFireRange(Collider2D other) {
            var playerComp = other.GetComponent<Player>();
            if ( !playerComp ) {
                return;
            }
            _target = other.gameObject.transform;
            _isFiring = true;
            _bulletFireRateTimer.Init(BulletFireDelay);
        }

        void OnLeaveFireRange(Collider2D other) {
            if ( _target && (other.gameObject == _target.gameObject) ) {
                _target = null;
                _isFiring = false;
                _bulletFireRateTimer.Stop();
            }
        }

        public void GetDamage(float damage) {
            Hp = ComponentUtils.DefaultGetDamage(gameObject, Hp, damage);
        }
    }
}