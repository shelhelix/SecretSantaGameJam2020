using UnityEngine;

using SecretSantaGameJam2020.Behaviours;

namespace SecretSantaGameJam2020.Utils {
    public static class ComponentUtils {
        public static void MoveRigidbody(Rigidbody2D rigidbody2D, Vector2 power) {
            if ( power == Vector2.zero ) {
                rigidbody2D.drag = 5f;
            }
            else {
                rigidbody2D.drag = 0f;
            }
            rigidbody2D.AddForce(power, ForceMode2D.Impulse);
        }

        public static void LimitRigidbodySpeed(Rigidbody2D rigidbody2D, float maxSpeed) {
            if ( rigidbody2D.velocity.magnitude > maxSpeed ) {
                rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxSpeed;
            }
        }

        public static float DefaultHpBehaviour(GameObject obj, float hp, float damage) {
            var newHp = hp - damage;
            if ( newHp <= 0 ) {
                MonoBehaviour.print($"{obj} destroyed.");
                GameObject.Destroy(obj);
            }
            MonoBehaviour.print($"Dealed damage {damage} to {obj}.\nLeft hp {newHp}");
            return newHp;
        }

        public static void DefaultDealDamage(GameObject target, float damage) {
            var destructable = target.GetComponent<IDestructable>();
            destructable?.GetDamage(damage);
        }

        public static float SmoothRotate(Vector2 right, Vector2 viewVector, float rotationSpeed) {
            var angleDiff = -Vector2.SignedAngle(viewVector, right);
            return LerpFloat(0, angleDiff, rotationSpeed);
        }
        
        public static float LerpFloat(float a, float b, float coeff) {
            coeff = Mathf.Clamp01(coeff);
            return (b - a) * coeff + a;
        }
    }
}