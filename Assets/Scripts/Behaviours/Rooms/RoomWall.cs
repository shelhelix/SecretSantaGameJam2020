using JetBrains.Annotations;
using SecretSantaGameJam2020.Behaviours.Common;
using UnityEngine;

namespace SecretSantaGameJam2020.Behaviours.Rooms {
	public class RoomWall : BaseGameComponent {
		[NotNull] public GameObject ClosedWall;
		[NotNull] public GameObject OpenedWall;
		
		public void Init(bool isOpened) {
			ClosedWall.SetActive(!isOpened);
			OpenedWall.SetActive(isOpened);
		}
	}
}