using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours.Rooms {
	public class Room : BaseGameComponent {
		[NotNull] public RoomWall LeftWall;
		[NotNull] public RoomWall RightWall;
		[NotNull] public RoomWall UpperWall;
		[NotNull] public RoomWall BottomWall;
		
		public void Init(bool isLeftDoorOpened, bool isRightDoorOpened, bool isUpperDoorOpened, bool isBottomDoorOpened) {
			LeftWall.Init(isLeftDoorOpened);
			RightWall.Init(isRightDoorOpened);
			UpperWall.Init(isUpperDoorOpened);
			BottomWall.Init(isBottomDoorOpened);
		}
	}
}