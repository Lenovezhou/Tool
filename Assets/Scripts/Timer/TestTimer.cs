using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimer : MonoBehaviour {


	void Start () 
	{
		TimerMGR.GetInstance.CreateTimer(DoThing,3,2);
		TimerMGR.GetInstance.CreateTimer(DoArgThing,1,5,"veryyoung");
	}


	void DoThing()
	{
		print("DDDDDDDD");
	}

	void DoArgThing(System.Object[] args)
	{
		for (int i = 0; i < args.Length; i++) {
			print(args[i]);
		}
		print ("done!!!!!!");
	}
	void Update () {
		
	}
}
