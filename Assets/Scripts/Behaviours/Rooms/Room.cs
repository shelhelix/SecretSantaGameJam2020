using System.Collections.Generic;
using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours.Rooms {
	public class Room : BaseGameComponent {
		[NotNull] public Teleport UpperTeleport;
		[NotNull] public Teleport LeftTeleport;
		[NotNull] public Teleport RightTeleport;
		[NotNull] public Teleport BottomTeleport;

		[NotNull] public List<Enemy> Enemies;
		
		public virtual void Init(GameStarter starter, bool isLeftDoorOpened, bool isRightDoorOpened, bool isUpperDoorOpened, bool isBottomDoorOpened) {
			UpperTeleport.Init(isUpperDoorOpened);
			LeftTeleport.Init(isLeftDoorOpened);
			RightTeleport.Init(isRightDoorOpened);
			BottomTeleport.Init(isBottomDoorOpened);
			foreach (var enemy in Enemies) {
				enemy.Init(starter.Player.gameObject);
			}
		}
	}
}