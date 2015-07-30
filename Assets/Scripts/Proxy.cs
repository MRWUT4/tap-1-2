using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;


public class Names
{
	public const string GameState = "GameState";
}

public class LevelVO
{
	public float minScale = .2f;
	public float maxScale = .4f;
	public float force = .2f;

	public List<CircleVO> circleVOList = new List<CircleVO>();
}

public class CircleVO
{
	public GameObject gameObject;
	public NotationVO notationVO;
	public int index;
	public int level;
	public bool active = true;
}


[Serializable]
public class Proxy : GameObjectProxy
{
	public GameObject circlePrefab;
	public delegate NotationVO NotationDelegate(int level, int index, float seed);
	
	public int level = 0;
	public int numCircles = 3;
	
	private LevelVO _levelVO;
	private List<GameObject> _levelCircleVOList;


	/**
	 * System
	 */

	private TweenFactory _tweenFactory;
	
	public TweenFactory tweenFactory
	{
		get 
	    { 
	        _tweenFactory = _tweenFactory != null ? _tweenFactory : new TweenFactory();
	        return _tweenFactory; 
	    }
	}


	/**
	 * Game
	 */

	public LevelVO levelVO
	{
		get 
	    { 
	        if( _levelVO == null )
	        {
	        	_levelVO = new LevelVO();
	        	_levelVO.circleVOList = levelCircleVOList;
	        }

	        return _levelVO;
	    }
	}

	
	public List<CircleVO> levelCircleVOList
	{
		get 
	    { 
    		float seed = UnityEngine.Random.value;

	    	List<CircleVO> list = new List<CircleVO>();

	    	for( int i = 0; i < numCircles; ++i )
	    	{
	    		CircleVO circleVO = new CircleVO();
	    		
	    		circleVO.level = level;
	    		circleVO.gameObject = Assist.GetGameObjectClone( circlePrefab );
	    		circleVO.notationVO = levelNotationDelegate( level, i, seed );

	    	    list.Add( circleVO );
	    	}

	        return list;
	    }
	}


	public NotationDelegate levelNotationDelegate
	{
		get 
	    { 
	    	int index = (int)Mathf.Floor( UnityEngine.Random.value * (float)NotationDelegateList.Count );
	        NotationDelegate notationDelegate = NotationDelegateList[ index ];

	        return notationDelegate; 
	    }
	}


	public List<NotationDelegate> NotationDelegateList
	{	
		get 
	    { 	
	        return new List<NotationDelegate>
	        {
	        	Notation.addition
	        }; 
	    }
	}
}