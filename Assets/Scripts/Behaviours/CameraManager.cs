using UnityEngine;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
	public class CameraManager : BaseGameComponent {
		[NotNull] public Player Player;

		void Start() {
			Player.OnPlayerMoved += UpdateCamPos;
		}
		
		void UpdateCamPos() {
			var playerPos = Player.transform.position;
			transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
		}
	}
}