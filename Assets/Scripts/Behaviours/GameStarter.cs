using System.Diagnostics;
using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
	public class GameStarter : BaseGameComponent {
		[NotNull] public LevelGenerator LevelGenerator;
		[NotNull] public Player         Player;
		
		void Start() {
			// Generate new state 
			var map = LevelGenerator.GenerateMap();
			// Generate map object based on state 
			var stopwatch = Stopwatch.StartNew();
			LevelGenerator.GenerateLevelObjects(this, map);
			print(stopwatch.Elapsed.TotalSeconds);
		}
	}
}