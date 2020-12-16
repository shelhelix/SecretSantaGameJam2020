using UnityEngine;

using System;

using SecretSantaGameJam2020.Behaviours.Common;

namespace SecretSantaGameJam2020.Behaviours.Rooms {
	public class ActionPoint : BaseGameComponent {
		const string ActionKey = "E";

		Action _action;
		
		bool       _isActionPointActive;
		GameObject _playerGO;

		public event Action<bool> OnTeleportStateChanged;

		bool IsActionPointActive {
			get => _isActionPointActive;
			set {
				_isActionPointActive = value;
				OnTeleportStateChanged?.Invoke(_isActionPointActive);
			}
		}
		
		public void OnTriggerEnter2D(Collider2D other) {
			var playerComp = other.gameObject.GetComponent<Player>();
			if ( playerComp ) {
				IsActionPointActive = true;
				_playerGO = other.gameObject;
			}
		}

		public void OnTriggerExit2D(Collider2D other) {
			if ( other.gameObject == _playerGO ) {
				_playerGO = null;
				IsActionPointActive = false;
			}
		}

		public void Init(Action onClick) {
			_action = onClick;
		}

		void Update() {
			if ( IsActionPointActive && Input.GetButtonDown(ActionKey) ) {
				_action?.Invoke();
			}
		}
	}
}