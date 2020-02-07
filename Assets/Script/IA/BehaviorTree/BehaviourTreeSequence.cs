using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeSequence : BehaviorTreeNode , IParentBehaviourTreeNodes
{
	private List<BehaviorTreeNode> Nodes = new List<BehaviorTreeNode>();
	public BehaviourTreeSequence(string name) : base(name)
	{

	}



	public override BehaviourTreeStatus Tick(float Time)
	{
		foreach(BehaviorTreeNode node in Nodes)
		{
			BehaviourTreeStatus BTS = node.Tick(Time);
			
			if(BTS != BehaviourTreeStatus.Succes)
			{
				return BTS;
			}
		}
		return BehaviourTreeStatus.Succes;
	}


	public void AddChild(BehaviorTreeNode child)
	{
		Nodes.Add(child);
	}
}
