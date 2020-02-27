using DllSky.Patterns;
using System.Collections.Generic;
using UnityEngine;

namespace DllSky.Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        #region Variables
        [Header("Settings")]
        public bool isMute;

        [Space()]
        [SerializeField]
        private List<string> musicIDs = new List<string>();
        [SerializeField]
        private List<AudioSource> sources = new List<AudioSource>();
        #endregion

        #region Unity methods
        private void Start()
        {
            musicIDs.Clear();
            sources.Clear();
        }

        private void OnEnable()
        {
            EventManager.AddEventListener("OnChangeMuteSetting", OnChangeMuteSettingsHandler);
        }

        private void OnDisable()
        {
            EventManager.RemoveEventListener("OnChangeMuteSetting", OnChangeMuteSettingsHandler);
        }
        #endregion

        #region Public methods
        public AudioSource PlaySound(string _path, string _name, bool _loop = false, float _volume = 1.0f)
        {
            var source = GetSource(_path, _name, true);
            Play(source, true, _loop, _volume);

            if (!_loop)
                Destroy(source, source.clip.length + 0.1f);     //FIX для очень коротких звуков (в нек.случ. компонент удалялся до начала звучания, если длительность ~0 сек)

            return source;
        }

        public AudioSource PlayMusic(string _path, string _name, bool _loop = false, float _volume = 1.0f)
        {
            var source = GetSource(_path, _name);
            Play(source, false, _loop, _volume);

            return source;
        }

        public void StopMusic(string _name)
        {
            foreach (var item in sources)
            {
                if (_name == item.clip.name)
                {
                    sources.Remove(item);
                    Destroy(item);
                    musicIDs.Remove(_name);
                    return;
                }
            }
        }

        public void StopAllMusic()
        {
            for (int i = sources.Count - 1; i >= 0; i--)
                if (sources[i] == null)
                    sources.RemoveAt(i);

            foreach (var item in musicIDs)
            {
                var source = sources.Find(x => x?.clip?.name == item);

                if (source == null)
                    continue;

                sources.Remove(source);
                Destroy(source);
            }

            musicIDs.Clear();
        }

        public void StopAll()
        {
            for (int i = sources.Count - 1; i >= 0; i--)
                if (sources[i] != null)
                    Destroy(sources[i]);

            musicIDs.Clear();
            sources.Clear();
        }

        public bool CheckPlaying(string _name)
        {
            bool result = sources.Exists(x => x.clip.name == _name);

            return result;
        }

        public void OnChangeMuteSettingsHandler(CustomEvent _event)
        {
            isMute = (bool)_event.EventData;

            foreach (var item in sources)
            {
                if (item == null)
                    continue;

                item.mute = isMute;
            }
        }
        #endregion

        #region Private methods
        private void Play(AudioSource _source, bool _isSound, bool _loop, float _volume)
        {
            if (_source == null)
                return;

            if (_isSound)                           //Если ЗВУК
            {
                _source.mute = isMute;
            }
            else                                    //Если МУЗЫКА
            {
                _source.mute = isMute;
                if (!musicIDs.Exists(x => x == _source.clip.name))
                    musicIDs.Add(_source.clip.name);
            }

            _source.loop = _loop;
            _source.volume = _volume;
            _source.Play();
        }

        private AudioSource GetSource(string _path, string _name, bool _sound = false)
        {
            AudioSource source;

            for (int i = sources.Count - 1; i >= 0; i--)
            {
                var item = sources[i];

                if (item == null)
                {
                    sources.Remove(item);
                }
                else if (!_sound)                       //Если "Звук" - не используем повторно компоненты
                {
                    if (_name == item.clip.name)
                    {
                        source = item;
                        return source;
                    }
                }
            }

            source = gameObject.AddComponent<AudioSource>();

            if (source)
            {
                sources.Add(source);

                var clip = Resources.Load<AudioClip>(string.Format(@"{0}{1}", _path, _name));

                source.clip = clip;
            }

            return source;
        }
        #endregion
    }
}