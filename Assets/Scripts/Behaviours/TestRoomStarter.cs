using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Behaviours.Rooms;
using SecretSantaGameJam2020.Behaviours.UI;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
	public class TestRoomStarter : BaseGameComponent {
		[NotNull] public Room    TestRoom;
		[NotNull] public Player  Player;
		[NotNull] public LevelUI LevelUI;

		void Start() {
			Player.Init(LevelUI);
			TestRoom.Init(false, false, false, false);
			var objectInitializer = new RoomRandomObjectsInitializer(Player.gameObject);
			objectInitializer.InitRoomObjects(TestRoom);
			LevelUI.Init();
		}
	}
}