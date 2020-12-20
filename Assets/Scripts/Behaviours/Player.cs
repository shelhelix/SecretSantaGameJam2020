using UnityEngine;

using System;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Behaviours.UI;
using SecretSantaGameJam2020.Events;
using SecretSantaGameJam2020.Utils;
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

		LevelUI _levelUI;
		
		float _defaultAngularDrag;
		
		public event Action OnPlayerMoved;

		public void Init(LevelUI screenTransitionController) {
			_levelUI = screenTransitionController;
			_defaultAngularDrag = Rigidbody.angularDrag;
			Rigidbody.centerOfMass = Vector2.zero;
			Sword.gameObject.SetActive(true);
		}

		void Update() {
			var moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
			ComponentUtils.MoveRigidbody(Rigidbody, moveDirection * Speed);
			ComponentUtils.LimitRigidbodySpeed(Rigidbody, Speed);
			
			if (Input.GetButtonDown(FireButtonName)) {
				Rigidbody.AddTorque(TorquePower, ForceMode2D.Impulse);				
			}
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
			if ( (Rigidbody.angularVelocity > -MinAngularVelocity) && (Rigidbody.angularVelocity < MinAngularVelocity) ) {
				Rigidbody.angularVelocity = 0f;
			}
			OnPlayerMoved?.Invoke();
		}

		public void GetDamage(float damage) {
			Hp = ComponentUtils.DefaultGetDamage(gameObject, Hp, damage);
			if ( Hp <= 0 ) {
				_levelUI.ShowPlayerDeadScreen();
				EventManager.Fire(new PlayerDied());
			}
		}
	}
}