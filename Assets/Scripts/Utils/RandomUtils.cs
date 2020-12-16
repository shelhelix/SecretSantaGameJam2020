using UnityEngine;

using System.Collections.Generic;

namespace SecretSantaGameJam2020.Utils {
	public class RandomUtils {
		public static T GetRandomElement<T>(List<T> container) where T : class {
			if ( container.Count == 0 ) {
				Debug.LogWarning("There is no items in container");
				return null;
			}
			var randomIndex = Random.Range(0, container.Count);
			return container[randomIndex];
		}
	}
}