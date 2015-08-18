using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;


/**
 * Names
 */

public class Names
{
	public const string Game = "Game";
	public const string Result = "Result";

	public const string ButtonPlay = "ButtonPlay";
}


/**
 * LevelVO
 */

public class LevelVO
{
	public float minScale = .2f;
	public float maxScale = .4f;
	public float force = .2f;

	public List<CircleVO> circleVOList = new List<CircleVO>();
}


/**
 * StateVO
 */

[Serializable]
public struct StateVO 
{
	public string name;
}


/**
 * Proxy
 */

[Serializable]
public class Proxy
{
	public GameObject circlePrefab;
	public StateMachine stateMachine;
	public LevelVO levelVO;
	
	[NonSerialized]
	public int level = 0;
	
	public int numCircles = 3;
	public int numLevels = 10;

	private TweenFactory _tweenFactory;
	private CircleVOFactory _circleVOFactory;


	public Proxy()
	{
		level = 0;
		numCircles = 3;
	}


	/**
	 * System
	 */

	public TweenFactory tweenFactory
	{
		get 
	    { 
	        _tweenFactory = _tweenFactory != null ? _tweenFactory : new TweenFactory();
	        return _tweenFactory; 
	    }
	}
	
	public CircleVOFactory circleVOFactory
	{
		get 
	    { 
	        _circleVOFactory = _circleVOFactory != null ? _circleVOFactory : new CircleVOFactory();
	        return _circleVOFactory; 
	    }
	}


	/**
	 * Game
	 */

	public LevelVO GetLevelVO()
	{
	   	LevelVO vo = new LevelVO();
    	vo.circleVOList = levelCircleVOList;

    	return vo;
	}
	
	public List<CircleVO> levelCircleVOList
	{
		get 
	    { 
	    	circleVOFactory.level = level;
	    	circleVOFactory.numCircles = numCircles;
	    	circleVOFactory.circlePrefab = circlePrefab;
	    	circleVOFactory.numLevels = numLevels;

	    	List<CircleVO> list = circleVOFactory.getList();

	        return list;
	    }
	}

}