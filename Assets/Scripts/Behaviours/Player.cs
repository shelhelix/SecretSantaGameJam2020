using UnityEngine;

using System;

using SecretSantaGameJam2020.Behaviours.Common;

namespace SecretSantaGameJam2020.Behaviours {
	[RequireComponent(typeof(Rigidbody2D))]
	public class Player : BaseGameComponent {
		const float Speed = 10f;

		public event Action OnPlayerMoved;

		Rigidbody2D _rigidbody;
		
		void Start() {
			_rigidbody = GetComponent<Rigidbody2D>();
		}
		
		void Update() {
			var moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
			_rigidbody.velocity = (Vector3) moveDirection * Speed;
			OnPlayerMoved?.Invoke();
		}
	}
}