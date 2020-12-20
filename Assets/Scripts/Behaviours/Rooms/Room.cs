using UnityEngine;

using System.Collections.Generic;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours.Rooms {
	public class Room : BaseGameComponent {
		[NotNull] public Teleport UpperTeleport;
		[NotNull] public Teleport LeftTeleport;
		[NotNull] public Teleport RightTeleport;
		[NotNull] public Teleport BottomTeleport;

		[NotNull] public List<GameObject>          PossibleDecorations;
		[NotNull] public List<EnemySpawnPointInfo> EnemySpawns;  
		
		public virtual void Init(bool isLeftDoorOpened, bool isRightDoorOpened, bool isUpperDoorOpened, bool isBottomDoorOpened) {
			UpperTeleport.Init(isUpperDoorOpened);
			LeftTeleport.Init(isLeftDoorOpened);
			RightTeleport.Init(isRightDoorOpened);
			BottomTeleport.Init(isBottomDoorOpened);
			InitRandomDecorations();
		}

		void InitRandomDecorations() {
			if ( PossibleDecorations.Count == 0 ) {
				return;
			}
			var decorationsToEnable = Random.Range(0, PossibleDecorations.Count);
			var decorationsCopy = new List<GameObject>(PossibleDecorations);
			for ( var i = 0; i < decorationsToEnable; i++ ) {
				var randomDecorationIndex = Random.Range(0, decorationsCopy.Count);
				decorationsCopy[randomDecorationIndex].SetActive(true);
				decorationsCopy.RemoveAt(randomDecorationIndex);
			}

			foreach ( var decoration in decorationsCopy ) {
				decoration.SetActive(false);
			}
		}
	}
}