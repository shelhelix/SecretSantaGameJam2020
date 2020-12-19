using UnityEngine;

using SecretSantaGameJam2020.Utils;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
    public class Turret : BaseEnemy, IDestructable {
        const float ReloadBulletTime = 3f;
        const float BulletFireTime   = 1f;
        
        [NotNull] public ColliderTrigger StartFireRange;
        [NotNull] public ColliderTrigger EndFireRange;
        [NotNull] public GameObject      BulletPrefab;
        
        public float Hp = 3;
        
        Transform _target;

        Timer _bulletReloadTimer;
        Timer _bulletFireTimer;

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
            var vectorToTarget = _target.position - transform.position;

            if ( _isFiring ) {
                if ( _bulletFireTimer.Tick(Time.deltaTime) ) {
                    _bulletReloadTimer.Init(ReloadBulletTime);
                    _bulletFireTimer.Stop();
                    _isFiring = false;
                }
                else {
                    var bullet = Instantiate(BulletPrefab);
                    var bulletComp = bullet.GetComponent<Bullet>();
                    if ( !bulletComp ) {
                        Debug.LogError("can't find Bullet component in bullet object");
                    }
                    else {
                        //TODO: Init Bullet
                    }
                }
            }
            else {
                //TODO: reload turret
            }
        }

        void OnEnterFireRange(Collider2D other) {
            var playerComp = other.GetComponent<Player>();
            if ( playerComp ) {
                _target = other.gameObject.transform;
            }
        }

        void OnLeaveFireRange(Collider2D other) {
            if ( other.gameObject == _target.gameObject ) {
                _target = null;
            }
        }

        public void GetDamage(float damage) {
            Hp = ComponentUtils.DefaultHpBehaviour(gameObject, Hp, damage);
        }
    }
}