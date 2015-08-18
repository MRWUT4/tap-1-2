using System.Collections.Generic;
using UnityEngine;


/**
 * CircleVO
 */

public class CircleVO
{
	public GameObject gameObject;
	public NotationVO notationVO;
	public int index;
	public int level;
	public bool active = true;
}


public class CircleVOFactory
{
	public delegate NotationVO NotationDelegate(int level, int index, float seed);
	public int level = 0;

	public CircleVOFactory(){}


	/**
	 * Public interface.
	 */

	public List<CircleVO> getList(int level, int numCircles, GameObject prefab)
	{
		this.level = level;
    	List<CircleVO> list = new List<CircleVO>();

    	for( int i = 0; i < numCircles; ++i )
    	{
    		CircleVO circleVO = new CircleVO();

    		NotationVO notationVO = null;

    		do
    		{
				float seed = UnityEngine.Random.value;
      			notationVO = levelNotationDelegate( level, i, seed );  		
      		}
    		while( notationValueIsInList( notationVO, list ) );

    		circleVO.level = level;
    		circleVO.gameObject = Assist.GetGameObjectClone( prefab );
    		circleVO.notationVO = notationVO;

    	    list.Add( circleVO );
    	}

    	return list;
	}


	public NotationDelegate levelNotationDelegate
	{
		get 
	    { 
	    	float numLevels = 10;
	    	float currentLevel = Mathf.Min( level, numLevels );

	    	float count = (float)NotationDelegateList.Count;
	    	float range = Linear.EaseNone( currentLevel, 0, count, numLevels);
	    	float random = UnityEngine.Random.value;
	    
	    	int index = (int)Mathf.Floor( random * range );
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
	        	Notation.positive,
	        	Notation.negative
	        }; 
	    }
	}


	/**
	 * Private interface.
	 */

	private bool notationValueIsInList(NotationVO notationVO, List<CircleVO> list)
	{
		if( notationVO != null )
		{
			for( int i = 0; i < list.Count; ++i )
			{
			    CircleVO circleVO = list[ i ];
				NotationVO compareVO = circleVO.notationVO;

				if( compareVO.text == notationVO.text )
					return true; 
			}
		}	

		return false;
	}
}