using UnityEngine;

namespace SecretSantaGameJam2020.Behaviours {
	public class RoomInfo {
		public Vector2Int Coords;
		public RoomType   RoomType = RoomType.SimpleRoom;
		
		public RoomInfo(int x, int y, RoomType roomType = RoomType.SimpleRoom) : this(new Vector2Int(x, y), roomType) { }
		
		public RoomInfo(Vector2Int coords, RoomType roomType = RoomType.SimpleRoom) {
			Coords = coords;
		}
	}
}