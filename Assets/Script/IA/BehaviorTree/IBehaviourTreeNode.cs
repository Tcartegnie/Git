using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourTreeNode 
{
	BehaviourTreeStatus Tick(float deltatime);
}
