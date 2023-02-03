using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;


    public static AudioManager instance;

    void Awake(){
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound _sound in sounds){
            _sound.source = gameObject.AddComponent<AudioSource>();
            _sound.source.clip = _sound.clip;
            _sound.source.volume = _sound.volume;
            _sound.source.pitch = _sound.pitch;
            _sound.source.loop = _sound.loop;
        }
    }

    public void PlaySound(string name){
        Sound _sound = Array.Find(sounds, sound => sound.name == name);
        if(_sound == null) return;
        _sound.source.Play();
    }

    public void StopSound(string name){
        Sound _sound = Array.Find(sounds, sound => sound.name == name);
        if(_sound == null) return;
        _sound.source.Stop();
    }

    public void StopAllSounds(){
        foreach(Sound s in sounds){
            s.source.Stop();
        }
    }

    public void PauseSound(string name){
        Sound _sound = Array.Find(sounds, sound => sound.name == name);
        if(_sound == null) return;
        _sound.source.Pause();
    }

    public void ResumeSound(string name){
        Sound _sound = Array.Find(sounds, sound => sound.name == name);
        if(_sound == null) return;
        _sound.source.UnPause();
    }

    public void DeathPlay(){
        StopAllSounds();
    }

    public Sound getSound(string name){
        Sound _sound = Array.Find(sounds, sound => sound.name == name);
        return _sound;
    }
}
