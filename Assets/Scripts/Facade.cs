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
	private Dictionary<string, GameObject> states;


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
		states = proxy.states;
	}


	/** StateMachine functions. */
	private void initStateMachine()
	{
		stateMachine = new StateMachine();
		
		stateMachine.OnExit += stateMachineOnExitHandler;

		stateMachine.AddState( Names.GameState, new GameState( states[ "Game" ], proxy ) );
		stateMachine.SetState( Names.GameState );
	}

	private void stateMachineOnExitHandler(State state, string message)
	{
		switch( state.id )
		{
			case Names.GameState:
			
				switch( message )
				{
					case InteractionCircle.CONTINUE:
						stateMachine.SetState( Names.GameState );
						break;

					case InteractionCircle.GAMEOVER:
						break;
				}

				break;
		}
	}
}