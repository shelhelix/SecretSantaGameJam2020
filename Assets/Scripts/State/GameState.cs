using SecretSantaGameJam2020.Utils;

namespace SecretSantaGameJam2020.State {
	public class GameState : Singleton<GameState> {
		public int CompletedLevels;
		public int Score;
		public int PrevLevelScore;

		public void Reset() {
			CompletedLevels = 0;
			Score = 0;
			PrevLevelScore = 0;
		}
	}
}