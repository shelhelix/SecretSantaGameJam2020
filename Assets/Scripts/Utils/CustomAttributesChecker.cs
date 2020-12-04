using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
#endif

using System.Reflection;

using SecretSantaGameJam2020.Utils.CustomAttributes;

namespace SecretSantaGameJam2020.Utils {
	public static class CustomAttributesChecker {
		public static void CheckAttributes(object obj, Object context = null) {
			#if UNITY_EDITOR
			if ( !context ) {
				context = obj as Object;
			}
			var isPrefabMode  = false;
			if ( context is MonoBehaviour mb ) {
				isPrefabMode  = string.IsNullOrEmpty(mb.gameObject.scene.name) ||
				           (PrefabStageUtility.GetCurrentPrefabStage() != null);
			}

			foreach ( var fieldInfo in obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance) ) {
				foreach ( var attribute in fieldInfo.GetCustomAttributes(inherit: true) ) {
					if ( isPrefabMode && (attribute is BaseCustomAttribute baseCustomAttribute) && !baseCustomAttribute.CheckInPrefab ) {
						continue;
					}

					switch ( attribute ) {
						case NotNullAttribute _: {
							var value = fieldInfo.GetValue(obj);
							if ( value == null || ((value is Object unityObj) && !unityObj) ) {
								Debug.LogErrorFormat(context, "{0} is null", fieldInfo.Name);
							}
							break;
						}
						default: {
							break;
						}
					}
				}
			}
			#endif
		}
	}
}