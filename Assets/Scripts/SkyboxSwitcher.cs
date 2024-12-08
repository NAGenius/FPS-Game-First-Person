using UnityEngine;

public class SkyboxAndSunController : MonoBehaviour
{
    public Light sunLight; // 引用到太阳光源
    public Material daySkybox; // 白天天空盒材质
    public Material nightSkybox; // 夜晚天空盒材质
    public float dayDuration = 240f; // 游戏内一天的持续时间
    public Color morningColor = new Color(0.6f, 0.6f, 0.6f); // 早上8点的颜色
    public Color noonColor = Color.white; // 中午12点的颜色
    public Color eveningColor = new Color(0.4f, 0.4f, 0.4f); // 傍晚18点的颜色
    public Color nightColor = new Color(0f, 0.1f, 0.3f); // 夜晚的颜色

    private float currentTime; // 当前游戏内时间

    void Start()
    {
        // 游戏从清晨开始
        currentTime = dayDuration * 0.25f; // 清晨对应时间

        // 初始化清晨的环境
        RenderSettings.skybox = daySkybox; // 白天天空盒
        sunLight.color = morningColor; // 清晨光源颜色
    }

    void Update()
    {
        // 时间循环
        currentTime = (currentTime + Time.deltaTime) % dayDuration;
        UpdateSkyboxAndSun();
    }

    void UpdateSkyboxAndSun()
    {
        float progress = currentTime / dayDuration;

        if (progress >= 0f && progress < 0.25f) // 夜晚到清晨
        {
            float t = progress / 0.25f;
            sunLight.color = Color.Lerp(nightColor, morningColor, t);
            RenderSettings.skybox = nightSkybox;
        }
        else if (progress >= 0.25f && progress < 0.5f) // 清晨到中午
        {
            float t = (progress - 0.25f) / 0.25f;
            sunLight.color = Color.Lerp(morningColor, noonColor, t);
            RenderSettings.skybox = daySkybox;
        }
        else if (progress >= 0.5f && progress < 0.75f) // 中午到傍晚
        {
            float t = (progress - 0.5f) / 0.25f;
            sunLight.color = Color.Lerp(noonColor, eveningColor, t);
            RenderSettings.skybox = daySkybox;
        }
        else if (progress >= 0.75f && progress < 1f) // 傍晚到夜晚
        {
            float t = (progress - 0.75f) / 0.25f;
            sunLight.color = Color.Lerp(eveningColor, nightColor, t);
            RenderSettings.skybox = nightSkybox;
        }
    }
}
