using System;

namespace SecretSantaGameJam2020.Utils.CustomAttributes {
	[AttributeUsage(AttributeTargets.Field)]
	public class NotNullAttribute : BaseCustomAttribute{
		public NotNullAttribute() : base(true) {}
		public NotNullAttribute(bool checkInPrefab) : base(checkInPrefab) { }
	}
}