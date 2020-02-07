using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeSelector :BehaviorTreeNode, IParentBehaviourTreeNodes
{
	private List<BehaviorTreeNode> Nodes = new List<BehaviorTreeNode>();
	public BehaviourTreeSelector(string name) : base(name)
	{

	}

	public void AddChild(BehaviorTreeNode child)
	{
		Nodes.Add(child);
	}


	

	public override BehaviourTreeStatus Tick(float Time)
	{
		foreach (BehaviorTreeNode node in Nodes)
		{
			BehaviourTreeStatus BTS = node.Tick(Time);

			if (BTS != BehaviourTreeStatus.Failed)
			{
				return BTS;
			}
		}
		return BehaviourTreeStatus.Failed;
	}
}
