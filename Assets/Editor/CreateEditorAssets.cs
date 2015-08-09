using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateEditorAssets
{
	[MenuItem("Assets/Tap-1-2/Facade")]
	public static void CreateFacade()
	{
		Facade facade = ScriptableObject.CreateInstance<Facade>();

		AssetDatabase.CreateAsset( facade, "Assets/ScriptableObjects/Facade.asset" );
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = facade;
	}

	// [MenuItem("Assets/Tap-1-2/Proxy")]
	// public static void CreateProxy()
	// {
	// 	Proxy proxy = ScriptableObject.CreateInstance<Proxy>();

	// 	AssetDatabase.CreateAsset( proxy, "Assets/ScriptableObjects/Proxy.asset" );
	// 	AssetDatabase.SaveAssets();

	// 	EditorUtility.FocusProjectWindow();

	// 	Selection.activeObject = proxy;
	// }
}