using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;

public class Facade : MonoBehaviour
{
	public Proxy proxy = new Proxy();

	private StateMachine stateMachine;
	private Names names;


	/**
	 * Public interface.
	 */

	void Start() 
	{
		initVariables();
		initStateMachine();
	}

	void FixedUpdate() 
	{
		stateMachine.FixedUpdate();
	}


	/** Variables. */
	private void initVariables()
	{
		
	}


	/** StateMachine functions. */
	private void initStateMachine()
	{
		stateMachine = new StateMachine();
		
		stateMachine.AddState( Names.GameState, new GameState( new GameObject(), proxy ) );
		stateMachine.SetState( Names.GameState );
	}
}