using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Behaviours.UI;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
	public class GameStarter : BaseGameComponent {
		[NotNull] public LevelGenerator LevelGenerator;
		[NotNull] public Player         Player;
		[NotNull] public LevelUI        LevelUI;
		
		void Start() {
			Player.Init(LevelUI);
			// Generate new state 
			var map = LevelGenerator.GenerateMap();
			// Generate map object based on state 
			LevelGenerator.GenerateLevelObjects(this, map);
			LevelUI.Init();
		}
	}
}