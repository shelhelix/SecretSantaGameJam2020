using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Behaviours.Rooms;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
	public class TestRoomStarter : BaseGameComponent {
		[NotNull] public Room TestRoom;
		[NotNull] public Player Player;
		void Start() {
			TestRoom.Init(Player.gameObject, false, false, false, false);
		}
	}
}