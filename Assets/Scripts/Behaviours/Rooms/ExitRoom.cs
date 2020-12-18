using UnityEngine;

using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours.Rooms {
	public class ExitRoom : Room {
		[NotNull] public ActionPoint ExitPoint;
		
		public override void Init(GameStarter starter, bool isLeftDoorOpened, bool isRightDoorOpened, bool isUpperDoorOpened, bool isBottomDoorOpened) {
			base.Init(starter, isLeftDoorOpened, isRightDoorOpened, isUpperDoorOpened, isBottomDoorOpened);
			ExitPoint.Init(() => Debug.Log("you win"));
		}
	}
}