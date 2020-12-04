using UnityEngine;

using System.Collections.Generic;

namespace SecretSantaGameJam2020.Behaviours {
	public class RoomInfo {
		public Vector2Int       Coords;
		
		public RoomInfo(int x, int y) {
			Coords = new Vector2Int(x, y);
		}
		public RoomInfo(Vector2Int coords) {
			Coords = coords;
		}
	}
}