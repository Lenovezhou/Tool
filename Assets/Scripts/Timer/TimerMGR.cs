using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMGR :SIngleton<TimerMGR> 
{
	private List<Timer> _Timers;

	protected override void Awake ()
	{
		base.Awake ();
		if (_Timers == null) {
			_Timers = new List<Timer>();
		}
	}


	public Timer CreateTimer(TimeHandler callback,float frequency,int reapeats)
	{
		return Create(callback,null,frequency,reapeats);
	}

	public Timer CreateTimer(TimeArgHandler argcallback,float frequency,int repeate,params System.Object[] args)
	{
		return Create(null,argcallback,frequency,repeate,args);
	}

	public Timer Create(TimeHandler callback,TimeArgHandler argcallback,float frequency,int repeate,params System.Object[] args)
	{
		Timer ti = new Timer(callback,argcallback,frequency,repeate,args);
		_Timers.Add(ti);
		return ti;
	}


	void DestroyTimer(Timer timer)
	{
		if (timer != null)
		{
			_Timers.Remove(timer);
			timer.CleanUp();
		}

	}

	void FixedUpdate () 
	{
		if (_Timers.Count != 0) 
		{
			for (int i = 0; i < _Timers.Count; i++) 
			{
				float currenttime = Time.time;
				if (_Timers[i].Frequency + _Timers[i].LastTickTime > currenttime)
				{
					continue;
				}
				_Timers[i].LastTickTime = currenttime;
				if (_Timers[i].Repeate -- == 0) 
				{
					DestroyTimer(_Timers[i]);
				}else{
					_Timers[i].Notify();
				}
			}
		}	
	}
}
