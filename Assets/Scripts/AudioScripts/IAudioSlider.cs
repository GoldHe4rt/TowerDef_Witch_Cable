
namespace AudioScripts
{
    public interface IAudioSlider
    {
        float MasterVolume { get; }
        float SFXVolume { get; }
        float MusicVolume { get; }
        float UIVolume { get; }
    }
}
