using UnityEngine;

namespace SecretSantaGameJam2020.Behaviours {
	public class RoomInfo {
		public Vector2Int Coords;
		public RoomType   RoomType;
		
		public RoomInfo(Vector2Int coords, RoomType roomType = RoomType.SimpleRoom) {
			Coords   = coords;
			RoomType = roomType;
		}
	}
}