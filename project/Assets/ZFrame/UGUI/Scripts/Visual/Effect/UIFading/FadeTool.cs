using UnityEngine;
using System.Collections;

namespace ZFrame.UGUI
{
	using Tween;

    public static class FadeTool
    {
        public static ZTweener DOFade(GameObject go, FadeGroup fadeDir, bool reset)
        {
            ZTweener ret = null;
            if (go) {
                var list = ListPool<Component>.Get();
                go.GetComponents(typeof(FadeBase), list);
                float duration = 0;
                for (int i = 0; i < list.Count; ++i) {
                    var com = list[i];
					var fad = com as FadeBase;
                    if (fadeDir == fad.fadeGroup) {
                        if (fad.DOFade(reset)) {
                            if (fad.loops < 0) continue;
                            int nLoop = fad.loops == 0 ? 1 : fad.loops;
                            var fxDuration = fad.duration * nLoop + fad.delay;
                            if (duration < fxDuration) {
                                duration = fxDuration;
                                ret = fad.tweener;
                            }
                        }
                    }
                }
                ListPool<Component>.Release(list);
            }
            return ret;
        }

        public static ZTweener DOFade(GameObject go, bool reset)
        {
            ZTweener ret = null;
            if (go) {
                var list = ListPool<Component>.Get();
                go.GetComponents(typeof(FadeBase), list);
                float duration = 0;
                for (int i = 0; i < list.Count; ++i) {
                    var com = list[i];
                    var fad = com as FadeBase;
                    if (fad.DOFade(reset)) {
                        if (fad.loops < 0) continue;
                        int nLoop = fad.loops == 0 ? 1 : fad.loops;
                        var fxDuration = fad.duration * nLoop + fad.delay;
                        if (duration < fxDuration) {
                            duration = fxDuration;
                            ret = fad.tweener;
                        }
                    }
                }
                ListPool<Component>.Release(list);
            }
            return ret;
        }
    }
}
