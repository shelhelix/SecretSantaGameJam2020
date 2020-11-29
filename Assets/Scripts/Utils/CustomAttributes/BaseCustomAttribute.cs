using System;

namespace SecretSantaGameJam2020.Utils.CustomAttributes {
	public class BaseCustomAttribute : Attribute {
		public bool CheckInPrefab;

		protected BaseCustomAttribute(bool checkInPrefab) {
			CheckInPrefab = checkInPrefab;
		}
	}
}