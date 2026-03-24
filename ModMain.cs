using HarmonyLib;
using ICities;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace RailwayProfitTuner
{
    public class ModMain : IUserMod
    {
        public string Name => "Railway Profit Tuner";
        public string Description => "按载货量和距离计算货运铁路收入。";

        public void OnEnabled()
        {
            try
            {
                // 使用唯一 ID 实例化
                var harmony = new HarmonyLib.Harmony("com.user.railwayprofit");
                harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
                UnityEngine.Debug.Log("[RailwayProfitTuner] Harmony Patches successfully applied.");
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError("[RailwayProfitTuner] Harmony Patching failed: " + e.Message);
            }
        }

        public void OnDisabled()
        {
            var harmony = new Harmony("com.user.railwayprofit");
            // 只卸载此 ID 的补丁
            harmony.UnpatchSelf();
            // 或者用静态方法：Harmony.UnpatchID("com.user.railwayprofit");
            UnityEngine.Debug.Log("[RailwayProfitTuner] Harmony Patches removed.");
        }

        // 定义默认单价系数（rate）
        public static float multiplier = 0.01f;

        // 保存配置实例
        private static ModConfig config = ModConfig.Load();

        // 初始化时从配置文件读取数值
        static ModMain()
        {
            multiplier = config.Multiplier;
        }

        // 生成设置界面
        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group = helper.AddGroup("铁路暴利设置");

            // 控制倍率
            group.AddSlider("收益总倍率", 0f, 5f, 0.1f, multiplier, (value) =>
            {
                multiplier = value;
                config.Multiplier = value;
                config.Save(); // 保存到文件
            });

            // 控制起步价
            group.AddSlider("单程起步价", 0f, 10000f, 500f, config.BaseFee, (value) =>
            {
                config.BaseFee = value;
                config.Save(); // 保存到文件
            });
        }
    }

    public class ModConfig
    {
        public float Multiplier = 0.1f;
        public float BaseFee = 3000f; // 起步价配置

        private static readonly string FileName = "RailwayProfitTunerConfig.xml";

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(ModConfig));
            using (var writer = new StreamWriter(FileName))
            {
                serializer.Serialize(writer, this);
            }
        }

        public static ModConfig Load()
        {
            if (!File.Exists(FileName)) return new ModConfig();
            var serializer = new XmlSerializer(typeof(ModConfig));
            using (var reader = new StreamReader(FileName))
            {
                return (ModConfig)serializer.Deserialize(reader);
            }
        }
    }
}