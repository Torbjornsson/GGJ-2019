using UnityEngine;

public static class Easing
{

    /** Quadratic in. */
    public static float QuadIn(float t)
    {
        return t * t;
    }

    /** Quadratic out. */
    public static float QuadOut(float t)
    {
        return -t * (t - 2);
    }

    /** Quadratic in and out. */
    public static float QuadInOut(float t)
    {
        return t <= .5 ? t * t * 2 : 1 - (--t) * t * 2;
    }

    /** Cubic in. */
    public static float CubeIn(float t)
    {
        return t * t * t;
    }

    /** Cubic out. */
    public static float CubeOut(float t)
    {
        return 1 + (--t) * t * t;
    }

    /** Cubic in and out. */
    public static float CubeInOut(float t)
    {
        return t <= .5 ? t * t * t * 4 : 1 + (--t) * t * t * 4;
    }

    /** Quart in. */
    public static float QuartIn(float t)
    {
        return t * t * t * t;
    }

    /** Quart out. */
    public static float QuartOut(float t)
    {
        return 1 - (t -= 1) * t * t * t;
    }

    /** Quart in and out. */
    public static float QuartInOut(float t)
    {
        return t <= .5f ? t * t * t * t * 8 : (1 - (t = t * 2 - 2) * t * t * t) / 2 + .5f;
    }

    /** Quint in. */
    public static float QuintIn(float t)
    {
        return t * t * t * t * t;
    }

    /** Quint out. */
    public static float QuintOut(float t)
    {
        return (t = t - 1) * t * t * t * t + 1;
    }

    /** Quint in and out. */
    public static float QuintInOut(float t)
    {
        return ((t *= 2) < 1) ? (t * t * t * t * t) / 2 : ((t -= 2) * t * t * t * t + 2) / 2;
    }

    /** Sine in. */
    public static float SineIn(float t)
    {
        return -(float)Mathf.Cos(PI2 * t) + 1;
    }

    /** Sine out. */
    public static float SinOut(float t)
    {
        return Mathf.Sin(PI2 * t);
    }

    /** Sine in and out. */
    public static float SineInOUt(float t)
    {
        return -Mathf.Cos(Mathf.PI * t) / 2 + .5f;
    }

    /** Bounce in. */
    public static float BounceIn(float t)
    {
        t = 1 - t;
        if (t < B1) return 1 - 7.5625f * t * t;
        if (t < B2) return 1 - (7.5625f * (t - B3) * (t - B3) + .75f);
        if (t < B4) return 1 - (7.5625f * (t - B5) * (t - B5) + .9375f);
        return 1 - (7.5625f * (t - B6) * (t - B6) + .984375f);
    }

    /** Bounce out. */
    public static float BounceOut(float t)
    {
        if (t < B1) return 7.5625f * t * t;
        if (t < B2) return 7.5625f * (t - B3) * (t - B3) + .75f;
        if (t < B4) return 7.5625f * (t - B5) * (t - B5) + .9375f;
        return 7.5625f * (t - B6) * (t - B6) + .984375f;
    }

    /** Bounce in and out. */
    public static float BounceInOut(float t)
    {
        if (t < .5f)
        {
            t = 1 - t * 2;
            if (t < B1) return (1 - 7.5625f * t * t) / 2;
            if (t < B2) return (1 - (7.5625f * (t - B3) * (t - B3) + .75f)) / 2;
            if (t < B4) return (1 - (7.5625f * (t - B5) * (t - B5) + .9375f)) / 2;
            return (1 - (7.5625f * (t - B6) * (t - B6) + .984375f)) / 2;
        }
        t = t * 2 - 1;
        if (t < B1) return (7.5625f * t * t) / 2 + .5f;
        if (t < B2) return (7.5625f * (t - B3) * (t - B3) + .75f) / 2 + .5f;
        if (t < B4) return (7.5625f * (t - B5) * (t - B5) + .9375f) / 2 + .5f;
        return (7.5625f * (t - B6) * (t - B6) + .984375f) / 2 + .5f;
    }

    /** Circle in. */
    public static float CircleIn(float t)
    {
        return -(Mathf.Sqrt(1 - t * t) - 1);
    }

    /** Circle out. */
    public static float CircleOut(float t)
    {
        return Mathf.Sqrt(1 - (t - 1) * (t - 1));
    }

    /** Circle in and out. */
    public static float CircleInOut(float t)
    {
        return t <= .5 ? (Mathf.Sqrt(1 - t * t * 4) - 1) / -2 : (Mathf.Sqrt(1 - (t * 2 - 2) * (t * 2 - 2)) + 1) / 2;
    }

    /** Exponential in. */
    public static float ExponentialIn(float t)
    {
        return (float)Mathf.Pow(2, 10 * (t - 1));
    }

    /** Exponential out. */
    public static float ExponentialOut(float t)
    {
        return -(float)Mathf.Pow(2, -10 * t) + 1;
    }

    /** Exponential in and out. */
    public static float ExponentialInOut(float t)
    {
        return t < .5 ? Mathf.Pow(2, 10 * (t * 2 - 1)) / 2 : (-Mathf.Pow(2, -10 * (t * 2 - 1)) + 2) / 2;
    }

    /** Back in. */
    public static float BackIn(float t)
    {
        return t * t * (2.70158f * t - 1.70158f);
    }

    /** Back out. */
    public static float BackOut(float t)
    {
        return 1 - (--t) * (t) * (-2.70158f * t - 1.70158f);
    }

    /** Back in and out. */
    public static float BackInOut(float t)
    {
        t *= 2;
        if (t < 1) return t * t * (2.70158f * t - 1.70158f) / 2;
        t--;
        return (1 - (--t) * (t) * (-2.70158f * t - 1.70158f)) / 2 + .5f;
    }

    public static float ElasticIn(float t)
    {
        return (Mathf.Sin(13 * PIover2 * t) * Mathf.Pow(2, 10 * (t - 1)));
    }

    public static float ElasticOut(float t)
    {
        return (Mathf.Sin(-13 * PIover2 * (t + 1)) * Mathf.Pow(2, -10 * t) + 1);
    }

    public static float ElasticInOut(float t)
    {
        if (t < 0.5)
        {
            return (float)(0.5 * Mathf.Sin(13 * PIover2 * (2 * t)) * Mathf.Pow(2, 10 * ((2 * t) - 1)));
        }

        return (float)(0.5 * (Mathf.Sin(-13 * PIover2 * ((2 * t - 1) + 1)) * Mathf.Pow(2, -10 * (2 * t - 1)) + 2));
    }

    /** Bounce in and out. */
    private const float B1 = 1 / 2.75f;
    private const float B2 = 2 / 2.75f;
    private const float B3 = 1.5f / 2.75f;
    private const float B4 = 2.5f / 2.75f;
    private const float B5 = 2.25f / 2.75f;
    private const float B6 = 2.625f / 2.75f;
    private const float PI2 = Mathf.PI * 2;
    private const float PIover2 = Mathf.PI / 2;
}