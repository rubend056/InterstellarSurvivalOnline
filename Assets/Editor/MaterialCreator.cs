using UnityEngine;
using UnityEditor;

public class MaterialCreator : MonoBehaviour
{
	[MenuItem("GameObject/Create Material")]
	public static void CreateMaterial(string name)
	{
		// Create a simple material asset

		Material material = new Material( Shader.Find( "Standard" ) );
		AssetDatabase.CreateAsset( material, ("Assets/Resources/" + name + ".mat"));
		material.SetFloat ("Smoothness", 0f);

		// Print the path of the created asset
		Debug.Log( AssetDatabase.GetAssetPath( material ) );
	}
}