using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
	public class GameStarter : BaseGameComponent {
		[NotNull] public LevelGenerator LevelGenerator;
		
		void Start() {
			LevelGenerator.GenerateLevel();
		}
	}
}