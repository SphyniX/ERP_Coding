using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

namespace ZFrame
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public static AudioManager Inst { get { return Instance; } }

        [SerializeField]
		private GameObject[] sources;

        private List<AudioSource> m_UniqueSrouces = new List<AudioSource>();

        private void OnClipLoaded(Object o, object p)
        {
            var clip = o as AudioClip;
            var src = p as AudioSource;
            if (clip) {
                src.clip = clip;
                src.Play();
            }
        }

        public GameObject GetTemplate(string template)
        {
            for (int i = 0; i < sources.Length; ++i) {
                var src = sources[i];
				if (src && src.name == template) {
					return src;
                }
            }

            return null;
        }

        public AudioSource GetSource(string template)
        {
            var prefab = GetTemplate(template);
            Assert.IsNotNull(prefab,
                string.Format("<AudioSource> with name '{0}' not exist!", template));

            var go = ObjectPoolManager.AddChild(gameObject, prefab);
            go.SetActive(true);
            return go.GetComponent<AudioSource>();
        }
        
        public AudioSource FindSource(string template)
        {
            for (int i = 0; i < m_UniqueSrouces.Count; ++i) {
                if (m_UniqueSrouces[i].name == template) {
                    return m_UniqueSrouces[i];
                }
            }

            var src = GetSource(template);
            m_UniqueSrouces.Add(src);
            return src;
        }

        public void Play(string clipName, string template)
        {
            var src = GetSource(template);
            AssetsMgr.A.LoadAsync(typeof(AudioClip), clipName, true, OnClipLoaded, src);
        }

        public void Replay(string clipName, string template)
        {
            var src = FindSource(template);
            AssetsMgr.A.LoadAsync(typeof(AudioClip), clipName, true, OnClipLoaded, src);
        }

        public void Stop(string template)
        {
            var src = FindSource(template);
            src.Stop();
        }
    }
}
