using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : BehaviorTreeNode, IParentBehaviourTreeNodes
{
	Func<float, BehaviourTreeStatus> function;

	public ActionNode(string name, Func<float, BehaviourTreeStatus> fn) : base(name)
	{
		function = fn;
	}

	public void AddChild(BehaviorTreeNode child)
	{
		ChildNodes = child;
	}


	public override BehaviourTreeStatus Tick(float Time)
	{
		return function(Time);
	}
}
