using UnityEngine;

using System.Collections.Generic;
using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
    public class EnemySpawnPointInfo : BaseGameComponent {
        [NotNull] public List<int>        Probabilities;
        [NotNull] public List<GameObject> PossibleEnemyPrefab;
    }
}