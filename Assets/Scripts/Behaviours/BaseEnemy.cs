using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Events;
using SecretSantaGameJam2020.Utils.Events;

namespace SecretSantaGameJam2020.Behaviours {
    public class BaseEnemy : BaseGameComponent {
        protected bool BaseInited;
        
        public virtual void Init() {
            EventManager.Subscribe<PlayerDied>(OnPlayerDied);
            BaseInited = true;
        }

        protected virtual void OnDestroy() {
            EventManager.Unsubscribe<PlayerDied>(OnPlayerDied);
        }
        
        void OnPlayerDied(PlayerDied e) {
            EventManager.Unsubscribe<PlayerDied>(OnPlayerDied);
            BaseInited = false;
        }
    }
}