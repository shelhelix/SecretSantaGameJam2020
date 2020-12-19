using SecretSantaGameJam2020.Behaviours;
using UnityEngine;

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
    }
}