
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourTreeStatus
{
	Succes,
	Continue,
	Failed,
}

public abstract class BehaviorTreeNode
{
	protected BehaviorTreeNode ChildNodes;
	protected string Name;
	public abstract BehaviourTreeStatus Tick(float Time);

	public BehaviorTreeNode(string name)
	{
		Name = name;
	}

}
