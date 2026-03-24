using HarmonyLib;
using ICities;
using UnityEngine;
using ColossalFramework;

[HarmonyPatch(typeof(CargoTrainAI), "ArriveAtTarget")]
public class CargoTrainPatch
{
    // Postfix 代表在原方法执行完后再运行代码
    static void Postfix(ushort vehicleID, ref Vehicle data)
    {
        if (data.m_targetBuilding == 0) return;

        // 1. 直接获取原始载货量（绝对值）
        float cargo = data.m_transferSize;
        if (cargo <= 0) return;

        // 2. 计算距离
        var bManager = Singleton<BuildingManager>.instance;
        Vector3 startPos = bManager.m_buildings.m_buffer[data.m_sourceBuilding].m_position;
        Vector3 endPos = bManager.m_buildings.m_buffer[data.m_targetBuilding].m_position;
        float distance = Vector3.Distance(startPos, endPos);

        // 3. 暴力调薪公式
        // 基础固定费：5000
        // 距离加成：将距离乘以一个很大的系数，比如 50
        // 载货加成：cargo 即使只有 1，乘以 10 也有保底
        float baseFee = 5000f;
        float distanceBonus = distance * 50f;
        float cargoBonus = cargo * 20f;

        // 最终总和再乘以你的滑动条系数 Multiplier (默认设为 1.0)
        int totalIncome = (int)((baseFee + distanceBonus + cargoBonus) * RailwayProfitTuner.ModMain.multiplier);

        // 4. 结算
        Singleton<EconomyManager>.instance.AddResource(
            EconomyManager.Resource.PublicIncome,
            totalIncome,
            data.Info.m_class
        );

        // 强制弹窗调试
        //string message = $"[Mod生效] 列车ID:{vehicleID} | 距离:{distance:F0} | 载货:{data.m_transferSize} | 收入:{totalIncome}";
        //Debug.Log(message); // 记录到日志文件
     }
}