using SecretSantaGameJam2020.Behaviours.UI;

using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours.Rooms {
	public class ExitRoom : Room {
		[NotNull] public ActionPoint ExitPoint;
		
		public void Init(LevelUI levelUI, bool isLeftDoorOpened, bool isRightDoorOpened, bool isUpperDoorOpened, bool isBottomDoorOpened) {
			base.Init(isLeftDoorOpened, isRightDoorOpened, isUpperDoorOpened, isBottomDoorOpened);
			ExitPoint.Init(levelUI.ShowLevelFinishedScreen);
		}
	}
}