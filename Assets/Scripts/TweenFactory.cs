public class TweenFactory
{
	public Tween AlphaScaleShowBounceInOut(Mutate mutate, float scale, float delay = 0, float alpha = 1)
	{
		return new DoTween().To( mutate, 1.2f, new
        { 
            delay = delay * .1,
            alpha = alpha,
            scaleX = scale,
            scaleY = scale,
            ease = "Back.EaseInOut"
        });
	}

    public Tween AlphaScaleHideBounceInOut(Mutate mutate)
    {
        return new DoTween().To( mutate, .6f, new
        { 
            alpha = 0,
            scaleX = 0,
            scaleY = 0,
            ease = "Back.EaseInOut"
        });
    }
}