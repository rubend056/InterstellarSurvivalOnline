using UnityEngine;
using System.Collections;
using UnityEditor;

//[CustomEditor(typeof(PesantAI)), CanEditMultipleObjects]
public class PesantAIEditor : Editor {

	/*public SerializedProperty
		pesantType_Prop,
		full_Prop,
		checkDistance_Prop,
		suckAmmount_Prop,
		moveForce_Prop,
		yOffset_Prop,
		maxSpeed_Prop,
		anim_Prop,
		ownRigidbody_Prop,
		ownTransform_Prop,
		torsoTransform_Prop,
		planet_Prop;

	void OnEnable(){
		pesantType_Prop = serializedObject.FindProperty ("pesantType");
		full_Prop = serializedObject.FindProperty ("full");
		checkDistance_Prop = serializedObject.FindProperty ("checkDistance");
		suckAmmount_Prop = serializedObject.FindProperty ("suckAmmount");
		moveForce_Prop = serializedObject.FindProperty ("moveForce");
		yOffset_Prop = serializedObject.FindProperty ("yOffset");
		maxSpeed_Prop = serializedObject.FindProperty ("maxSpeed");
		anim_Prop = serializedObject.FindProperty ("anim");
		ownRigidbody_Prop = serializedObject.FindProperty ("ownRigidbody");
		ownTransform_Prop = serializedObject.FindProperty ("ownTransform");
		torsoTransform_Prop = serializedObject.FindProperty ("torsoTransform");
		planet_Prop = serializedObject.FindProperty ("planet");
	}

	public override void OnInspectorGUI(){
		serializedObject.Update ();

		EditorGUILayout.PropertyField( pesantType_Prop );


		 P st = (PropertyAttribute.PesantType)pesantType_Prop.enumValueIndex;

		switch( st ) {
		case PropertyHolder.Status.A:            
			EditorGUILayout.PropertyField( controllable_Prop, new GUIContent("controllable") );            
			EditorGUILayout.IntSlider ( valForA_Prop, 0, 10, new GUIContent("valForA") );
			EditorGUILayout.IntSlider ( valForAB_Prop, 0, 100, new GUIContent("valForAB") );
			break;

		case PropertyHolder.Status.B:            
			EditorGUILayout.PropertyField( controllable_Prop, new GUIContent("controllable") );    
			EditorGUILayout.IntSlider ( valForAB_Prop, 0, 100, new GUIContent("valForAB") );
			break;

		case PropertyHolder.Status.C:            
			EditorGUILayout.PropertyField( controllable_Prop, new GUIContent("controllable") );    
			EditorGUILayout.IntSlider ( valForC_Prop, 0, 100, new GUIContent("valForC") );
			break;

		}


		serializedObject.ApplyModifiedProperties ();




	}*/
}
