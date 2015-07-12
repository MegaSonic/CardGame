using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Use this class to extend all MonoBehaviour scripts. Any functionality we want to add can be implemented from here.
/// </summary>
public class Extender : MonoBehaviour {
	

	public I GetInterfaceComponent<I>() where I : class
	{
		return GetComponent(typeof(I)) as I;
	}

	/// <summary>
	/// Finds all scripts of a certain interface
	/// </summary>
	public static List<I> FindObjectsOfInterface<I>() where I : class
	{
		MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>();
		List<I> list = new List<I>();
		
		foreach(MonoBehaviour behaviour in monoBehaviours)
		{
			I component = behaviour.GetComponent(typeof(I)) as I;
			
			if(component != null)
			{
				list.Add(component);
			}
		}
		
		return list;
	}

    public IEnumerator WaitAndSetActive(float time, GameObject g, bool active = false)
    {
        yield return new WaitForSeconds(time);
        g.SetActive(active);
        yield return null;
    }
}

