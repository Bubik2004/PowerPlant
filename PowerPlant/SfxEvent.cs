//using System;
//using System.Collections.Generic;
//using System.Text;
//using ChaiFoxes.FMODAudio;
//public class SfxEvent
//{
//    public FMOD.Studio.EventDescription desc;
//    public FMOD.Studio.EventInstance instance;
//    public bool isContiunous; //if set to true, the sound will not be released immediatly after start
//}

//public class AudioEngine
//{
//    FMOD.System _fmodLowLevelSystem;
//    FMOD.Studio.System _fmodSystem;

//    //Music and Ambiance
//    private string _currentMusicName = "";
//    private FMOD.Studio.EventDescription _musicDescription;
//    private FMOD.Studio.EventInstance _musicInstance;

//    private string _currentAmbianceName = "";
//    private FMOD.Studio.EventDescription _ambianceDescription;
//    private FMOD.Studio.EventInstance _ambianceInstance;

//    //Sounds Bank
//    private Dictionary<string, SfxEvent> _sfxDict;

//    //Buses
//    private Dictionary<string, FMOD.Studio.Bus> _busDict;

//    public string currentMusicName { get { return _currentMusicName; } }
//    public string currentAmbianceName { get { return _currentAmbianceName; } }

//    FMOD.Studio.EventInstance _snapshotInstance;
//    FMOD.Studio.EventDescription _snapshotDesc;

//    public void Initialize()
//    {
//        ErrorCheck(FMOD.Studio.System.create(out _fmodSystem));
//        ErrorCheck(_fmodSystem.initialize(512, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.PREFER_DOLBY_DOWNMIX, (IntPtr)0));
//        ErrorCheck(_fmodSystem.getCoreSystem(out _fmodLowLevelSystem));

//        //Load Default Sound Bank - change the name to whatever your bank is named, without extensions
//        LoadBank("common");

//        _sfxDict = new Dictionary<string, SfxEvent>();
//        _busDict = new Dictionary<string, FMOD.Studio.Bus>();
//    }

//    public void LoadBus(string busName)
//    {
//        FMOD.Studio.Bus bus;
//        _fmodSystem.getBus("bus:/" + busName, out bus);
//        _busDict.Add(busName, bus);
//    }

//    public void SetBusVolume(string busName, float volume)
//    {
//        if (_busDict.ContainsKey(busName))
//        {
//            _busDict[busName].setVolume(volume);
//        }
//    }

//    public float GetBusVolume(string busName)
//    {
//        float vol = -1f;
//        float finalVol;

//        if (_busDict.ContainsKey(busName))
//        {
//            _busDict[busName].getVolume(out vol, out finalVol);
//        }

//        return vol;
//    }

//    public void LoadBank(string bankFileName)
//    {
//        FMOD.Studio.Bank masterBank;
//        ErrorCheck(_fmodSystem.loadBankFile("Content/Desktop/" + bankFileName + ".bank", FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out masterBank));

//        FMOD.Studio.Bank stringsBank;
//        ErrorCheck(_fmodSystem.loadBankFile("Content/Desktop/" + bankFileName + ".strings.bank", FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out stringsBank));
//    }

//    public void LoadAndApplySnapshot(string snapshotName)
//    {
//        _snapshotInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
//        ErrorCheck(_fmodSystem.getEvent("snapshot:/" + snapshotName, out _snapshotDesc));
//        _snapshotDesc.createInstance(out _snapshotInstance);
//        _snapshotInstance.start();
//    }

//    public void LoadSound(string sndName, bool isLooping = false)
//    {
//        SfxEvent sfx = new SfxEvent();
//        sfx.isContiunous = isLooping;
//        ErrorCheck(_fmodSystem.getEvent("event:/" + sndName, out sfx.desc));

//        // Start loading sample data and keep it in memory
//        ErrorCheck(sfx.desc.loadSampleData());

//        _sfxDict.Add(sndName, sfx);
//    }

//    public void PlaySound(string sndName, float volume = -1000.0f, float pitch = -1000.0f)
//    {
//        if (_sfxDict.ContainsKey(sndName))
//        {
//            FMOD.Studio.EventInstance instance;
//            ErrorCheck(_sfxDict[sndName].desc.createInstance(out instance));

//            if (pitch != -1000.0f) //if not specified, leave fmod studio default value and don't apply any pitch
//                instance.setPitch(pitch);

//            if (volume != -1000.0f) //if not specified, leave fmod studio default value and don't apply any volume
//                instance.setVolume(volume);

//            instance.start();

//            if (!_sfxDict[sndName].isContiunous)
//                instance.release();

//            _sfxDict[sndName].instance = instance;
//        }
//    }

//    public void PlayMusic(string trackName)
//    {
//        if (_currentMusicName != trackName)
//        {
//            ErrorCheck(_fmodSystem.getEvent("event:/MUSIC/" + trackName, out _musicDescription));
//            ErrorCheck(_musicDescription.createInstance(out _musicInstance));
//            _currentMusicName = trackName;
//        }
//        ErrorCheck(_musicInstance.start());
//    }

//    public void PlayAmbiance(string ambianceName)
//    {
//        if (_currentAmbianceName != ambianceName)
//        {
//            ErrorCheck(_fmodSystem.getEvent("event:/AMBIANCE/" + ambianceName, out _ambianceDescription));
//            ErrorCheck(_ambianceDescription.createInstance(out _ambianceInstance));
//            _currentAmbianceName = ambianceName;
//        }
//        ErrorCheck(_ambianceInstance.start());
//    }

//    public void StopMusic(bool fadeOut = false)
//    {
//        if (_currentMusicName != "")
//        {
//            _musicInstance.stop(fadeOut ? FMOD.Studio.STOP_MODE.ALLOWFADEOUT : FMOD.Studio.STOP_MODE.IMMEDIATE);
//            _musicInstance.release();
//            _currentMusicName = "";
//        }
//    }

//    public void StopAmbiance(bool fadeOut = false)
//    {
//        if (_currentAmbianceName != "")
//        {
//            ErrorCheck(_ambianceInstance.stop(fadeOut ? FMOD.Studio.STOP_MODE.ALLOWFADEOUT : FMOD.Studio.STOP_MODE.IMMEDIATE));
//            //ErrorCheck(ambianceInstance.release());
//            _currentAmbianceName = "";
//        }
//    }

//    public void PauseMusic(bool pause)
//    {
//        if (_currentMusicName != "")
//        {
//            ErrorCheck(_musicInstance.setPaused(pause));
//        }
//    }

//    public void PauseAmbiance(bool pause)
//    {
//        if (_currentAmbianceName != "")
//        {
//            ErrorCheck(_ambianceInstance.setPaused(pause));
//        }
//    }

//    public void Update()
//    {
//        ErrorCheck(_fmodSystem.update());
//    }

//    public int ErrorCheck(FMOD.RESULT result)
//    {
//        if (result != FMOD.RESULT.OK)
//        {
//            Console.WriteLine(result.ToString() + " | Stack Trace : " + Environment.StackTrace);
//            return 1;
//        }
//        return 0;
//    }
//}
