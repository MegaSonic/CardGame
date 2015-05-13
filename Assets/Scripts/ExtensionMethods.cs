using UnityEngine;
using System.Collections;

/// <summary>
/// This class is for extension methods.
/// </summary>
public static class ExtensionMethods {

	/// <summary>
	/// Like GetComponent, but logs an error if it can't find the component.
	/// </summary>
	public static T GetSafeComponent<T>(this GameObject obj) where T : MonoBehaviour
	{
		T component = obj.GetComponent<T>();
		
		if(component == null)
		{
			Debug.LogError("Expected to find component of type " 
			               + typeof(T) + " but found none", obj);
		}
		
		return component;
	}
}