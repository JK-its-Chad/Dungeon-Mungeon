using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour {
    
    public AudioSource track;
    public bool Musicstatus;
    public GameObject player;
    public GameObject target;
    public float trackdistance;
    public float Modifysound;
   

    public Sound[] sounds;
   

    public static AudioManager instance;
	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            instance = this;
      
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
		
	}

   
    void Start()
    {
        play("start");
    }

    
    void Update()
    {
        if(Musicstatus)
        {
            track.volume = DistancefromTarget(trackdistance) - Modifysound;
        }
    }
    

    
    public void play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " no findy");
            return;
        }
        s.source.Play();
    }

   
    float DistancefromTarget(float Maxdistance)
    {
        float d = Vector3.Distance(player.transform.position, target.transform.position);
        float distanceClamped = Mathf.Clamp(d, 0, Maxdistance);
        float audiodistance = distanceClamped / Maxdistance;
        audiodistance = 1 - audiodistance;
        return audiodistance;
    }
    
}
