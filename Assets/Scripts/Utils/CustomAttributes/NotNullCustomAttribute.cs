using System;

namespace SecretSantaGameJam2020.Utils.CustomAttributes {
	[AttributeUsage(AttributeTargets.Field)]
	public class NotNullCustomAttribute : BaseCustomAttribute{
		public NotNullCustomAttribute() : base(true) {}
		public NotNullCustomAttribute(bool checkInPrefab) : base(checkInPrefab) { }
	}
}