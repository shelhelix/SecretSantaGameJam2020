using UnityEngine;

using System.Collections.Generic;

namespace SecretSantaGameJam2020.Utils {
	public static class RandomUtils {
		public static T GetRandomElement<T>(List<T> container) where T : class {
			if ( container.Count == 0 ) {
				Debug.LogWarning("There is no items in container");
				return null;
			}
			var randomIndex = Random.Range(0, container.Count);
			return container[randomIndex];
		}


		public static GameObject GetRandomPrefab(List<int> probabilities, List<GameObject> prefabs) {
			if ( probabilities.Count != prefabs.Count ) {
				Debug.LogError("Can't get random prefab. probabilites count and items probabilites count are different");
				return null;
			}
			var totalProbability = GetTotalProbability(probabilities);
			var randomValue = Random.Range(0, totalProbability);
			for ( var i = 0; i < prefabs.Count; i++ ) {
				if ( randomValue < probabilities[i] ) {
					return prefabs[i];
				}
				randomValue -= probabilities[i];
			}
			return null;
		}

		static int GetTotalProbability(List<int> probabilities) {
			var res = 0;
			foreach ( var probability in probabilities ) {
				res += probability;
			}
			return res;
		}
	}
}