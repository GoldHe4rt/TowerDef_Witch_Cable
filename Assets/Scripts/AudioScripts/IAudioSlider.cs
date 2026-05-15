
namespace AudioScripts
{
    public interface IAudioSlider
    {
        float MasterVolume { get; }
        float MusicVolume { get; }
        float SFXVolume { get; }
        float UIVolume { get; }
    }
}
