using UnityEngine;

using System.Collections.Generic;
using SecretSantaGameJam2020.Utils;

namespace SecretSantaGameJam2020.Behaviours.Rooms {
    public class RoomRandomObjectsInitializer {
        readonly GameObject _player;
        
        public RoomRandomObjectsInitializer(GameObject player) {
            _player = player;
        }
        
        public void InitRoomObjects(Room room) {
            if ( !room ) { 
                Debug.LogError("Can't initialize objects in the room. Room is null");
            }
            var spawns = room.EnemySpawns;
            foreach ( var spawn in spawns ) {
                if ( spawn.Probabilities.Count != spawn.PossibleEnemyPrefab.Count ) {
                    Debug.LogError("Spawner is unavailable. Probabilities and prefabs counts are different.");
                    continue;
                }

                var selectedEnemyPrefab = RandomUtils.GetRandomPrefab(spawn.Probabilities, spawn.PossibleEnemyPrefab);
                if ( !selectedEnemyPrefab ) {
                    continue;
                }
                var go = GameObject.Instantiate(selectedEnemyPrefab, spawn.transform.position, Quaternion.identity);
                TryInitEnemy(go);
            }
        }

        void TryInitEnemy(GameObject enemyObj) {
            var enemyComp = enemyObj.GetComponent<PlayerLikeEnemy>();
            if ( enemyComp ) {
                enemyComp.Init(_player);
                return;
            }
            var enemyComp2 = enemyObj.GetComponent<DashEnemy>();
            if ( enemyComp2 ) {
                enemyComp2.Init(_player);
                return;
            }
            var enemyComp3 = enemyObj.GetComponent<Turret>();
            if ( enemyComp3 ) {
                enemyComp3.Init();
            }
        }
    }
}