using UnityEngine;

using System.Collections.Generic;
using System.Diagnostics;
using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Behaviours.Rooms;
using SecretSantaGameJam2020.State;
using SecretSantaGameJam2020.Utils;
using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Behaviours {
	public class LevelGenerator : BaseGameComponent {
		public int GridSizeX  = 10;
		public int GridSizeY  = 15;
		public int CellsCount = 20;
		
		public Vector2Int RoomSize;

		[NotNull] public GameObject RoomPrefab;
		[NotNull] public GameObject ExitRoomPrefab;
		[NotNull] public Transform  LevelRoot;

		
		readonly List<Vector2Int> _directions = new List<Vector2Int> {
			new Vector2Int(-1,  0),
			new Vector2Int( 1,  0),
			new Vector2Int( 0, -1),
			new Vector2Int( 0,  1),
		};
		
		Vector2Int InvalidIndex => new Vector2Int(-1, -1);
		
		public LevelMap GenerateMap() {
			var map = new LevelMap(GridSizeX, GridSizeY);
			GenerateRoomsOnMap(map);
			// Select one of the furthest rooms as final room;
			SetFinalRoomOnMap(map);
			return map;
		}

		public void GenerateLevelObjects(GameStarter starter, LevelMap map) {
			for ( var x = 0; x < GridSizeX; x++ ) {
				for ( var y = 0; y < GridSizeY; y++ ) {
					if ( map.HasRoom(x, y) ) {
						var pos      = RoomSize * new Vector2(x - GridSizeX/2 , y - GridSizeY/2);
						var roomInfo = map.GetRoom(new Vector2Int(x, y));
						var prefab   = GetPrefab(roomInfo.RoomType); 
						var go       = Instantiate(prefab, pos, Quaternion.identity, LevelRoot);
						// Init room doors
						var index = new Vector2Int(x, y);
						var isLeftOpened   = IsDoorOpened(map, index + Vector2Int.left);
						var isRightOpened  = IsDoorOpened(map, index + Vector2Int.right);
						var isUpperOpened  = IsDoorOpened(map, index + Vector2Int.up);
						var isBottomOpened = IsDoorOpened(map, index + Vector2Int.down);
						switch (roomInfo.RoomType) {
							case RoomType.SimpleRoom: {
								var comp = go.GetComponent<Room>();
								comp.Init(isLeftOpened, isRightOpened, isUpperOpened, isBottomOpened);
								break;
							}
							case RoomType.RoomWithExit: {
								var comp = go.GetComponent<ExitRoom>();
								comp.Init(isLeftOpened, isRightOpened, isUpperOpened, isBottomOpened);
								break;
							}
						}
					}
				}
			}
		}
		
		GameObject GetPrefab(RoomType roomType) {
			return (roomType == RoomType.SimpleRoom) ? RoomPrefab : ExitRoomPrefab;
		}

		bool IsDoorOpened(LevelMap map, Vector2Int pos) {
			return map.IsCellOnMap(pos) && map.HasRoom(pos);
		}
		
		bool HasEmptyCellsAround(LevelMap map, RoomInfo room) {
			var found = false;
			foreach ( var direction in _directions ) {
				var newPos = room.Coords + direction;
				if ( map.IsCellOnMap(newPos) && !map.HasRoom(newPos) ) {
					found = true;
					break;
				}
			}
			return found;
		}
		
		Vector2Int GetRandomNeighbourEmptyCell(LevelMap map, RoomInfo room) {
			var availableCells = new List<Vector2Int>();
			foreach ( var direction in _directions ) {
				var newPos = room.Coords + direction;
				if ( map.IsCellOnMap(newPos) && !map.HasRoom(newPos) ) {
					availableCells.Add(newPos);
				}
			}
			if ( availableCells.Count == 0 ) {
				return InvalidIndex;
			}
			var index = Random.Range(0, availableCells.Count);
			return availableCells[index];
		}

		void GenerateRoomsOnMap(LevelMap map) {
			var centerCoords = new Vector2Int(GridSizeX/2, GridSizeY/2);
			var startRoom    = new RoomInfo(centerCoords);
			map.SetRoom(startRoom);
			var availableRooms = new List<RoomInfo>{startRoom};
			for ( var cellIndex = 0; cellIndex < CellsCount; cellIndex++ ) {
				var room = RandomUtils.GetRandomElement(availableRooms);
				if ( room == null ) {
					break;
				}
				var emptyCellPos = GetRandomNeighbourEmptyCell(map, room);
				if ( emptyCellPos == InvalidIndex ) {
					availableRooms.Remove(room);
					cellIndex--;
					continue;
				}
				var newRoom = new RoomInfo(emptyCellPos);
				map.SetRoom(newRoom);
				if ( HasEmptyCellsAround(map, newRoom) ) {
					availableRooms.Add(newRoom);
				}
				if ( !HasEmptyCellsAround(map, room) ) {
					availableRooms.Remove(room);
				}
			}
		}

		void SetFinalRoomOnMap(LevelMap map) {
			var startCellCoords  = new Vector2Int(GridSizeX/2, GridSizeY/2);
			var startRoom        = map.GetRoom(startCellCoords);
			var maxDistancedRoom = startRoom;
			for (var y = 0; y < map.SizeY; y++) {
				for (var x = 0; x < map.SizeX; x++) {
					if (!map.HasRoom(x, y)) {
						continue;
					}
					var room = map.GetRoom(x, y);
					if (GetDistanceBetweenRooms(startRoom, room) >
					    GetDistanceBetweenRooms(startRoom, maxDistancedRoom)) {
						maxDistancedRoom = room;
					}
				}
			}
			maxDistancedRoom.RoomType = RoomType.RoomWithExit;
		}

		float GetDistanceBetweenRooms(RoomInfo one, RoomInfo other) {
			return ((one == null) || (other == null)) ? float.NaN : (one.Coords - other.Coords).magnitude;
		} 
	}
}