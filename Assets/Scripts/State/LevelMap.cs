using UnityEngine;

using SecretSantaGameJam2020.Behaviours;

namespace SecretSantaGameJam2020.State {
	public class LevelMap {
		RoomInfo[,] Map = new RoomInfo[0,0];

		public int SizeX => Map.GetLength(0);
		public int SizeY => Map.GetLength(1);
		
		public LevelMap(int x, int y) {
			if ( (x <= 0) || (y <= 0) ) {
				Debug.LogErrorFormat("Can't create map with sizes {0}x{1}. Sizes must be positive.", x, y);
			}
			Map = new RoomInfo[x, y];
		}

		public RoomInfo GetRoom(int x, int y) {
			return GetRoom(new Vector2Int(x, y));
		}
		
		public RoomInfo GetRoom(Vector2Int coords) {
			if ( !IsCellOnMap(coords) ) {
				Debug.LogErrorFormat("Can't get room from map at coords {0}. Coords are out of bounds", coords);
				return null;
			}
			return Map[coords.x, coords.y];
		}

		public bool HasRoom(int x, int y) {
			return HasRoom(new Vector2Int(x, y));
		}
		
		public bool HasRoom(Vector2Int coords) {
			return IsCellOnMap(coords) && (GetRoom(coords) != null);
		}
		
		public void SetRoom(RoomInfo room) {
			if ( !IsCellOnMap(room.Coords) ) {
				Debug.LogErrorFormat("Can't place room on map at coords {0}. Coords are out of bounds", room.Coords);
				return;
			}
			Map[room.Coords.x, room.Coords.y] = room;
		}

		public bool IsCellOnMap(Vector2Int coords) {
			return	(coords.x >= 0) && (coords.x < SizeX) && (coords.y >= 0) && (coords.y < SizeY);
		}
	}
}