using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParentBehaviourTreeNodes : IBehaviourTreeNode
{
	void AddChild(BehaviorTreeNode child);
}
