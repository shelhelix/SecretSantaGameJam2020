using System.Collections.Generic;
using UnityEngine;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Behaviours.Rooms;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
	public class LevelGenerator : BaseGameComponent {
		public int GridSizeX = 10;
		public int GridSizeY = 15;
		public int CellsCount = 20;
		
		public Vector2Int RoomSize;

		[NotNull] public GameObject RoomPrefab;
		[NotNull] public Transform  LevelRoot;

		
		readonly List<Vector2Int> _directions = new List<Vector2Int> {
			new Vector2Int(-1,  0),
			new Vector2Int( 1,  0),
			new Vector2Int( 0, -1),
			new Vector2Int( 0,  1),
		};
		
		Vector2Int InvalidIndex => new Vector2Int(-1, -1);
		
		public LevelMap GenerateLevel() {
			var map = GenerateMap();
		
			for ( var x = 0; x < GridSizeX; x++ ) {
				for ( var y = 0; y < GridSizeY; y++ ) {
					if ( map[x, y] != null ) {
						var pos = RoomSize * new Vector2(x - GridSizeX/2 , y - GridSizeY/2);
						var go = Instantiate(RoomPrefab, pos, Quaternion.identity, LevelRoot);
						var roomComp = go.GetComponent<Room>();
						// Init room doors
						var index = new Vector2Int(x, y);
						var isLeftOpened   = IsDoorOpened(map, index + Vector2Int.left);
						var isRightOpened  = IsDoorOpened(map, index + Vector2Int.right);
						var isUpperOpened  = IsDoorOpened(map, index + Vector2Int.up);
						var isBottomOpened = IsDoorOpened(map, index + Vector2Int.down);
						roomComp.Init(isLeftOpened, isRightOpened, isUpperOpened, isBottomOpened);
					}
				}
			}
			
			return new LevelMap(map);
		}

		RoomInfo[,] GenerateMap() {
			var map       = new RoomInfo[GridSizeX, GridSizeY];
			var startRoom = new RoomInfo(GridSizeX/2, GridSizeY/2);
			PlaceRoomOnMap(map, startRoom);
			var availableRooms = new List<RoomInfo>{startRoom};
			for ( var cellIndex = 0; cellIndex < CellsCount; cellIndex++ ) {
				var room = GetRandomRoom(availableRooms);
				var emptyCellPos = GetRandomNeighbourEmptyCell(map, room);
				if ( emptyCellPos == InvalidIndex ) {
					availableRooms.Remove(room);
					cellIndex--;
					continue;
				}
				var newRoom = new RoomInfo(emptyCellPos);
				PlaceRoomOnMap(map, newRoom);
				if ( HasEmptyCellsAround(map, newRoom) ) {
					availableRooms.Add(newRoom);
				}
				if ( !HasEmptyCellsAround(map, room) ) {
					availableRooms.Remove(room);
				}
			}
			return map;
		}

		bool IsDoorOpened(RoomInfo[,] map, Vector2Int pos) {
			return IsPointOnMap(map, pos) && !IsCellEmpty(map, pos);
		}
		
		bool IsCellEmpty(RoomInfo[,] map, Vector2Int pos) {
			return IsPointOnMap(map, pos) && (map[pos.x, pos.y] == null);
		}
		
		void PlaceRoomOnMap(RoomInfo[,] map, RoomInfo room) {
			map[room.Coords.x, room.Coords.y] = room;
		}
		
		bool HasEmptyCellsAround(RoomInfo[,] map, RoomInfo room) {
			var found = false;
			foreach ( var direction in _directions ) {
				var newPos = room.Coords + direction;
				if ( IsPointOnMap(map, newPos) && (map[newPos.x, newPos.y] == null) ) {
					found = true;
					break;
				}
			}
			return found;
		}
		
		Vector2Int GetRandomNeighbourEmptyCell(RoomInfo[,] map, RoomInfo room) {
			var availableCells = new List<Vector2Int>();
			foreach ( var direction in _directions ) {
				var newPos = room.Coords + direction;
				if ( IsCellEmpty(map, newPos) ) {
					availableCells.Add(newPos);
				}
			}

			if ( availableCells.Count == 0 ) {
				return InvalidIndex;
			}
			var index = Random.Range(0, availableCells.Count);
			return availableCells[index];
		}
		
		RoomInfo GetRandomRoom(List<RoomInfo> roomInfos) {
			var index = Random.Range(0, roomInfos.Count);
			return roomInfos[index];
		}

		bool IsPointOnMap(RoomInfo[,] map, Vector2Int point) {
			return (point.x >= 0) && (point.y >= 0) && (point.x < map.GetLength(0)) && (point.y < map.GetLength(1));
		}
	}
}