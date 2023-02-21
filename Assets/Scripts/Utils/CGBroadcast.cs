using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool CGQueryDelegate<T>(T queriedObject);
public delegate void CGDelegate<T>(T broadcaster);
public class CGBroadcast<T> : IDisposable
{
	private event CGDelegate<T> m_Event;
	private List<CGDelegate<T>> m_SingleSubscribers = new List<CGDelegate<T>>();

	~CGBroadcast()
	{
		Dispose();
	}

	public void Dispose()
	{
		Clear();
		System.GC.SuppressFinalize(this);
	}

	public void Clear()
	{
		ClearSingleSubscribers();
		m_Event = delegate { };
	}

	public void ClearSingleSubscribers()
	{
		m_SingleSubscribers.Clear();
	}

	public bool IsBound()
    {
		return m_SingleSubscribers.Count > 0 || m_Event.GetInvocationList().Length > 0;
    }

	// add a one shot to this delegate that is removed after first broadcast
	public void SubscribeOnce(CGDelegate<T> del)
	{
		if (del != null && !HasSubscription(del))
		{
			m_Event += del;
			m_SingleSubscribers.Add(del);
		}
	}

	private bool HasSubscription(CGDelegate<T> del)
	{
		if (m_Event == null)
			return false;

		for (int i = 0; i < m_Event.GetInvocationList().Length; ++i)
		{
			var invocation = m_Event.GetInvocationList()[i];
			if (invocation.Target == del.Target)
				return true;
		}
		return false;
	}

	// add a recurring delegate that gets called each time
	public void Subscribe(CGDelegate<T> del)
	{
		if (del != null && !HasSubscription(del))
			m_Event += del;
	}

	public void Unsubscribe(CGDelegate<T> del)
	{
		m_Event -= del;
	}

	public void Broadcast(T broadcaster)
	{
		m_Event?.Invoke(broadcaster);
		for (int i = 0; i < m_SingleSubscribers.Count; ++i)
		{
			Unsubscribe(m_SingleSubscribers[i]);
		}
		m_SingleSubscribers.Clear();
	}
}