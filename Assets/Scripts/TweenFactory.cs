using System.Collections.Generic;
using UnityEngine;

public class TweenFactory
{
	public Tween AlphaScaleShowBounceInOut(Mutate mutate, float scale, float delay = 0, float alpha = 1)
	{
		return new DoTween().To( mutate, 1.2f, new
        { 
            delay = delay * .1,
            alpha = alpha,
            scaleX = scale,
            scaleY = scale

        }, Back.EaseInOut );
	}

    public Tween AlphaScaleBackOut(Mutate mutate, int index )
    {
        return new DoTween().To( mutate, .6f, new
        {
            delay = index * .1,
            alpha = 0,
            scaleX = 0,
            scaleY = 0
            
        }, Back.EaseIn );
    }

    public Tween AlphaScaleHideBounceInOut(Mutate mutate)
    {
        return new DoTween().To( mutate, .6f, new
        {
            alpha = 0,
            scaleX = 0,
            scaleY = 0

        }, Back.EaseInOut );
    }

    public List<Tween> ScaleFillScreenBounceIn(Mutate mutate)
    {

        // GameObject circle = mutate.gameObject;
        // Debug.Log( circle.GetComponentInChildren<TextMesh>() );

        List<Tween> list = new List<Tween>
        {
            new DoTween().To( mutate, .6f, new
            {
                x = 0,  
                y = 0,
                scaleX = 2,
                scaleY = 2

            }, Back.EaseIn )
        };

        return list;
    }
}