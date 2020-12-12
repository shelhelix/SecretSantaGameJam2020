using System;
using UnityEngine;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours.Rooms {
	public class Teleport : BaseGameComponent {
		const string TeleportKey = "E";
		
		[NotNull] public Transform DestinationPoint;
		
		bool       _isTeleportActive;
		GameObject _playerGO;

		public event Action<bool> OnTeleportStateChanged;

		bool IsTeleportActive {
			get => _isTeleportActive;
			set {
				_isTeleportActive = value;
				OnTeleportStateChanged?.Invoke(_isTeleportActive);
			}
		}
		
		public void Init(bool isDoorActive) {
			gameObject.SetActive(isDoorActive);
		}
		
		public void OnTriggerEnter2D(Collider2D other) {
			var playerComp = other.gameObject.GetComponent<Player>();
			if ( playerComp ) {
				IsTeleportActive = true;
				_playerGO = other.gameObject;
			}
			else {
				// Instant teleport ???
				TeleportObject(other.gameObject);
			}
		}

		public void OnTriggerExit2D(Collider2D other) {
			if ( other.gameObject == _playerGO ) {
				_playerGO = null;
				IsTeleportActive = false;
			}
		}

		void Update() {
			if ( IsTeleportActive && Input.GetButtonDown(TeleportKey) ) {
				TeleportObject(_playerGO);
			}
		}

		void TeleportObject(GameObject obj) {
			obj.transform.position = DestinationPoint.position;
		}
	}
}