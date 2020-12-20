using System.Collections.Generic;
using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours.UI {
    public class LevelUI : BaseGameComponent {
        [NotNull] public PlayerDeadScreen    PlayerDeadScreen;
        [NotNull] public LevelCompleteScreen LevelCompleteScreen;

        readonly List<IScreen> _allScreens = new List<IScreen>();
        
        public void Init() {
            _allScreens.Add(PlayerDeadScreen);
            _allScreens.Add(LevelCompleteScreen);
            foreach ( var screen in _allScreens ) {
                screen.Hide();
            }
        }
        
        public void ShowLevelFinishedScreen() {
            LevelCompleteScreen.Show();
            HideAllScreensExcept(LevelCompleteScreen);
        }
        
        public void ShowPlayerDeadScreen() {
            PlayerDeadScreen.Show();
            HideAllScreensExcept(PlayerDeadScreen);
        }

        void HideAllScreensExcept(IScreen screenToShow) {
            foreach ( var screen in _allScreens ) {
                if ( screen == screenToShow ) {
                    continue;
                }
                screen.Hide();
            }
        }
    }
}