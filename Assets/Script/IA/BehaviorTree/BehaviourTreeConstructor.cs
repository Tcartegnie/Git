using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 
 * WARNING
 * 
 * All the credit from the Behaviour three code comme to Ashley Davis
 * and his Git : https://github.com/ashleydavis
 * i use to learn and apply Behaviour tree.
 * 
 * WARNING
*/


public class BehaviourTreeConstructor
{
	Stack<IParentBehaviourTreeNodes> Nodes = new Stack<IParentBehaviourTreeNodes>();

	private IBehaviourTreeNode currentNode = null;




	public BehaviourTreeConstructor Sequence(string name)
	{
		BehaviourTreeSequence node = new BehaviourTreeSequence(name);

		if(Nodes.Count > 0)
		{
			Nodes.Peek().AddChild(node);
		}

		Nodes.Push(node);

		return this;
	}


	public BehaviourTreeConstructor Selector(string name)
	{

		BehaviourTreeSelector node = new BehaviourTreeSelector(name);

		if (Nodes.Count > 0)
		{
			Nodes.Peek().AddChild(node);
		}

		Nodes.Push(node);

		return this;
	}

	public BehaviourTreeConstructor Do(string name, Func<float,BehaviourTreeStatus> function)
	{
		if(Nodes.Count <= 0)
		{
			throw new ApplicationException("Can't create an unnested ActionNode, it must be a leaf node.");
		}

		//Debug.Log("Add the action : " + name);

		ActionNode node = new ActionNode(name, function);
		Nodes.Peek().AddChild(node);
		return this;
	}

	public BehaviourTreeConstructor Condition(string name, Func<float, bool> function)
	{
		//Debug.Log("Add the condition : " + name);
		BehaviourTreeConstructor Result = Do(name, t => function(t) ? BehaviourTreeStatus.Succes : BehaviourTreeStatus.Failed);
		//Debug.Log("The Action : " + name + " return : " + Result);
		return Result;
	
	}

	public BehaviourTreeConstructor InvertedCondition(string name, Func<float, bool> function)
	{
		//Debug.Log("Add the condition : " + name);
		BehaviourTreeConstructor Result = Do(name, t => function(t) ? BehaviourTreeStatus.Failed : BehaviourTreeStatus.Succes);
		//Debug.Log("The Action : " + name + " return : " + Result);
		return Result;

	}


	public BehaviourTreeConstructor Splice(IBehaviourTreeNode subTree)
	{
		if(subTree == null)
		{
			throw new Exception("Subtree is null");
		}

		if(Nodes.Count <= 0)
		{
			throw new ApplicationException("Can't splice an unnested sub-tree, there must be a parent-tree.");
		}

		Nodes.Peek().AddChild((BehaviorTreeNode)subTree);
		return this;
	}

	public IBehaviourTreeNode Build()
	{
		if(currentNode == null)
		{
			throw new ApplicationException("There is no node in the three");
		}
		return currentNode;
	}

	public BehaviourTreeConstructor End()
	{
		currentNode = Nodes.Pop();
		return this;
	}

}
