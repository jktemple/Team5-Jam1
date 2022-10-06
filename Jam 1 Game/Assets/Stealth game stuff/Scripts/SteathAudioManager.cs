using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteathAudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public int ID;
        public AudioClip clip;
        public float volume;
        public float pitch;
        public bool looping;
    }
    public List<Sound> sounds = new List<Sound>();

    public static SteathAudioManager instance;
    public AudioSource music;
    public AudioSource global;

    private void Awake() {
        instance = this;
    }

    private Sound getSoundFromID(int soundID) {
        for (int i = 0; i < sounds.Count; i++) {
            if (sounds[i].ID == soundID) {
                return sounds[i];
            }
        }
        Debug.LogError("Tried to get sound with invalid ID. ID: " + soundID);
        return null;
    }

    public void SetMusicVolume(float vol, bool playIfPaused = true) {
        music.volume = vol;
        if (playIfPaused && !music.isPlaying) {
            music.Play();
        }
    }

    public void PlayGlobal(int soundID, float volume = -1) {
        PlayHere(soundID, global, volume);
    }

    public void PlayHere(int soundID, AudioSource source, float volume = -1) {
        Sound toPlay = getSoundFromID(soundID);
        source.volume = volume == -1 ? toPlay.volume : volume;
        source.pitch = toPlay.pitch;
        source.loop = toPlay.looping;
        source.clip = toPlay.clip;
        source.Play();
    }
}
