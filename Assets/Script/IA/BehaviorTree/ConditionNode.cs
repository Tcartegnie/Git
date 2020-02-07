using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : BehaviorTreeNode, IParentBehaviourTreeNodes
{
	Stack<BehaviorTreeNode> StackBehavior;
	BehaviorTreeNode ValideNextNode;
	Func<float, bool> condition;

	public ConditionNode(string name, Func<float,bool> condition) : base(name)
	{
		this.condition = condition;
	}

	public override BehaviourTreeStatus Tick(float Time)
	{
		if (condition(Time))
		{
			return BehaviourTreeStatus.Succes;
		}
		else
		{
			return	BehaviourTreeStatus.Failed;
		}
	}


	public void AddChild(BehaviorTreeNode child)
	{
		ChildNodes = child;
	}
}
