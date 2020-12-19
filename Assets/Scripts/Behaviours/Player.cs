using UnityEngine;

using System;
using DG.Tweening;
using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Events;
using SecretSantaGameJam2020.Utils.CustomAttributes;
using SecretSantaGameJam2020.Utils.Events;

namespace SecretSantaGameJam2020.Behaviours {
	public class Player : BaseGameComponent, IDestructable {
		const string ThrustButtonName       = "Thrust";
		const string BreakButtonName        = "Break";
		const string SelfDestructButtonName = "SelfDestruct";
		const string FireButtonName         = "Fire1";

		const float MinAngularVelocity = 5f; 
		
		public float BreakAngularDrag = 1f;
		public float Speed            = 10f;
		public float TorquePower      = 1f;

		public float Hp = 3;
		
		[NotNull] public Rigidbody2D Rigidbody;
		[NotNull] public Transform   Sword;
		
		float _defaultAngularDrag;
		
		public event Action OnPlayerMoved;

		void Start() {
			_defaultAngularDrag = Rigidbody.angularDrag;
			Rigidbody.centerOfMass = Vector2.zero;
			Sword.gameObject.SetActive(true);
		}

		void OnDestroy() {
			EventManager.Fire(new PlayerDied());
		}

		void Update() {
			var moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
			Rigidbody.velocity = (Vector3) moveDirection * Speed;
			if (Input.GetButtonDown(ThrustButtonName)) {
				Rigidbody.AddTorque(TorquePower, ForceMode2D.Impulse);				
			}
			// if ( Input.GetButtonDown(FireButtonName) ) {
			// 	Sword.gameObject.SetActive(true);
			// }
			// if ( Input.GetButtonUp(FireButtonName) ) {
			// 	Sword.gameObject.SetActive(false);
			// }
			if (Input.GetButtonDown(BreakButtonName)) {
				Rigidbody.angularDrag = BreakAngularDrag;
			}
			if (Input.GetButtonDown(SelfDestructButtonName)) {
				Destroy(gameObject);
			}
			if (Input.GetButtonUp(BreakButtonName)) {
				Rigidbody.angularDrag = _defaultAngularDrag;
			}
			// Stop small rotation
			if (Rigidbody.angularVelocity < MinAngularVelocity) {
				Rigidbody.angularVelocity = 0f;
			}
			OnPlayerMoved?.Invoke();
		}

		public void GetDamage(float damage) {
			Hp -= damage;
			if (Hp <= 0f) {
				Debug.Log("Player is dead");
				Destroy(gameObject);
			}
		}
	}
}