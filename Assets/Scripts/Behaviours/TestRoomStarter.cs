using System.Collections.Generic;
using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Behaviours.Rooms;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
	public class TestRoomStarter : BaseGameComponent {
		[NotNull] public Room   TestRoom;
		[NotNull] public Player Player;

		[NotNull] public List<PlayerLikeEnemy> Enemies;
		[NotNull] public List<DashEnemy>       DashEnemies;
		[NotNull] public List<Turret>          Turrets;
		void Start() {
			TestRoom.Init(false, false, false, false);
			foreach (var enemy in Enemies) {
				enemy.Init(Player.gameObject);
			}
			foreach (var enemy in DashEnemies) {
				enemy.Init(Player.gameObject);
			}
			foreach (var enemy in Turrets) {
				enemy.Init();
			}
		}
	}
}