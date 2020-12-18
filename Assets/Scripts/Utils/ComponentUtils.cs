using SecretSantaGameJam2020.Behaviours;
using UnityEngine;

namespace SecretSantaGameJam2020.Utils {
    public static class ComponentUtils {
        public static void TryDealDamage(GameObject target, float damage) {
            var destructable = target.GetComponent<IDestructable>();
            destructable?.GetDamage(damage);
        }
    }
}