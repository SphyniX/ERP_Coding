using UnityEngine;
using System.Collections;

namespace ZFrame.Tween
{
    public interface ITweenable
    {
        ZTweener Tween(object from, object to, float duration);
    }
}
