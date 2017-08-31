using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIngleton<T> : MonoBehaviour where T:SIngleton<T>
{

	private static T instance;
	public static T GetInstance
	{
		get{return instance;}
	}

	protected virtual void Awake()
	{
		if (instance != null) 
		{
			Debug.Log("begine to instance twice class{0}"+GetType().ToString());
		}else
		{
			instance = (T)this;
		}
	}

	protected virtual void OnDestroy()
	{
		if (instance == this) 
		{
			instance = null;
		}
	}
}
