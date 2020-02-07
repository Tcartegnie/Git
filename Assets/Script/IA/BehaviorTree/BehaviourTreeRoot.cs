using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRoot : BehaviorTreeNode
{
	BehaviorTreeNode NextNode;

	public BehaviourTreeRoot(string name,BehaviorTreeNode nextNode): base(name)
	{
		NextNode = nextNode;
	}


	public override BehaviourTreeStatus Tick(float Time)
	{
		return NextNode.Tick(Time);
	}
}
