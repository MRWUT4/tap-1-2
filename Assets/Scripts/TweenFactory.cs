using System.Collections.Generic;
using UnityEngine;

public class TweenFactory
{
	public Tween AlphaScaleShowBounceInOut(Mutate mutate, float scale, float delay = 0, float alpha = 1)
	{
		return new DoTween().To( mutate, .6f, new
        { 
            delay = delay * .1,
            alpha = alpha,
            scaleX = scale,
            scaleY = scale

        }, Back.EaseOut );
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


    /** Progress Bar tween functions. */
    public Tween HeightQuadIn(Mutate mutate)
    {
        float height = RectTransformHeight( mutate );
        return new DoTween().From( mutate, .6f, new { y = mutate.y - height }, Quad.EaseOut );
    }

    public Tween HeightQuadOut(Mutate mutate)
    {
        float height = RectTransformHeight( mutate );
        return new DoTween().To( mutate, .6f, new { y = mutate.y - height }, Quad.EaseIn );
    }

    public float RectTransformHeight( Mutate mutate )
    {
        GameObject gameObject = mutate.gameObject;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        float height = rectTransform.rect.height;

        return height;
    }

    /** Circle tween functions. */
    public List<Tween> ScaleFillScreenBounceIn(Mutate mutate)
    {
        GameObject circle = mutate.gameObject;
        TextMesh textMesh = circle.GetComponentInChildren<TextMesh>();
        Color color = textMesh.color;

        Tween tweenColor = new DoTween().To( color, .6f, new
        {
            a = 0,
            gameObject = textMesh

        }, Quad.EaseOut );

        tweenColor.OnUpdate += tweenColorUpdateHandler;

        Tween tweenMutate = new DoTween().To( mutate, .6f, new
        {
            x = 0,  
            y = 0,
            scaleX = 2,
            scaleY = 2

        }, Back.EaseIn );

        List<Tween> list = new List<Tween>
        {
            tweenMutate,
            tweenColor
        };

        return list;
    }

    private void tweenColorUpdateHandler(Tween tween)
    {
        TextMesh textMesh = (TextMesh)( tween.Setup[ Tween.KEY_GAMEOBJECT ] );
        textMesh.color = (Color)tween.Target;
    }
}