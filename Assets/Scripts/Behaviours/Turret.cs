using UnityEngine;

using SecretSantaGameJam2020.Utils;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
    public class Turret : BaseEnemy, IDestructable {
        const float ReloadBulletTime = 3f;
        const float BulletFireTime   = 1f;
        const float FireRate         = 10f;

        [NotNull] public ColliderTrigger StartFireRange;
        [NotNull] public ColliderTrigger EndFireRange;
        [NotNull] public GameObject      BulletPrefab;
        [NotNull] public Collider2D      Collider;

        public float RotationSpeed = 0.1f;
        public float Hp = 3;
        
        readonly Timer _bulletReloadTimer   = new Timer();
        readonly Timer _bulletFireTimer     = new Timer();
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
            else {
                TryReload();
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
            if ( _bulletFireTimer.Tick(Time.deltaTime) ) {
                _bulletReloadTimer.Init(ReloadBulletTime);
                _bulletFireTimer.Stop();
                _isFiring = false;
            }
            else {
                var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                var bulletComp = bullet.GetComponent<Bullet>();
                if ( !bulletComp ) {
                    Debug.LogError("can't find Bullet component in bullet object");
                }
                else {
                    bulletComp.Init(Collider, vectorToTarget.normalized);
                }
            }
        }

        void TryReload() {
            if ( _bulletReloadTimer.Tick(Time.deltaTime) ) {
                _isFiring = true;
                _bulletReloadTimer.Stop();
                _bulletFireTimer.Init(BulletFireTime);
            }
        }

        void OnEnterFireRange(Collider2D other) {
            var playerComp = other.GetComponent<Player>();
            if ( playerComp ) {
                _target = other.gameObject.transform;
                _isFiring = true;
                _bulletReloadTimer.Stop();
                _bulletFireTimer.Init(BulletFireTime);
            }
        }

        void OnLeaveFireRange(Collider2D other) {
            if ( _target && (other.gameObject == _target.gameObject) ) {
                _target = null;
                _isFiring = false;
                _bulletReloadTimer.Stop();
                _bulletFireTimer.Stop();
            }
        }

        public void GetDamage(float damage) {
            Hp = ComponentUtils.DefaultHpBehaviour(gameObject, Hp, damage);
        }
    }
}