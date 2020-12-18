using System.Collections.Generic;

using SecretSantaGameJam2020.Behaviours.Common;

namespace SecretSantaGameJam2020.Behaviours {
    public abstract class LevelComponent : BaseGameComponent {
        static readonly HashSet<LevelComponent> Instances = new HashSet<LevelComponent>();
        
        void OnEnable() => Instances.Add(this);
        void OnDisable() => Instances.Remove(this);
        void OnDestroy() => Instances.Remove(this);

        protected virtual void Init(GameStarter starter) {}
    }
}