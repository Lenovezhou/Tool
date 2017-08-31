using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TimeHandler();
public delegate void TimeArgHandler(System.Object[] args);

public class Timer 
{
	public TimeHandler Handler;
	public TimeArgHandler ArgHandler;
	public bool Iscomplete = true;
	public float Frequency;
	public int Repeate;
	public System.Object[] Args;
	public float LastTickTime;


	public Timer(TimeHandler handler,TimeArgHandler arghandler,float frequency,int reapeate,System.Object[] args)
	{
		this.Handler = handler;
		this.ArgHandler = arghandler;
		this.Frequency = frequency;
		this.Repeate = reapeate;
		this.Args = args;
		LastTickTime = Time.time;
	}



	public void Notify()
	{
		if (Handler != null)
		{
			Handler();
		}
		if (ArgHandler != null) {
			ArgHandler(Args);
		}
	}


	public void CleanUp()
	{
		Handler = null;
		ArgHandler = null;
		Iscomplete = true;
		Repeate = 1;
		Frequency = 0;
	}

}
