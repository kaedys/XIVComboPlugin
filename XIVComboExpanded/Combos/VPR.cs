using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedPlugin.Combos;

internal static class VPR
{
    public const byte JobID = 41;

    public const uint
        SteelFangs = 34606,
        DreadFangs = 34607,
        HuntersSting = 34608,
        SwiftskinsSting = 34609,
        FlankstingStrike = 34610,
        FlanksbaneFang = 34611,
        HindstingStrike = 34612,
        HindsbaneFang = 34613,
        SteelMaw = 34614,
        DreadMaw = 34615,
        HuntersBite = 34616,
        SwiftskinsBite = 34617,
        JaggedMaw = 34618,
        BloodiedMaw = 34619,
        Dreadwinder = 34620,
        HuntersCoil = 34621,
        SwiftskinsCoil = 34622,
        PitofDread = 34623,
        HuntersDen = 34624,
        SwiftskinsDen = 34625,
        Reawaken = 34626,
        FirstGeneration = 34627,
        SecondGeneration = 34628,
        ThirdGeneration = 34629,
        FourthGeneration = 34630,
        Ouroboros = 34631,
        WrithingSnap = 34632,
        UncoiledFury = 34633,
        DeathRattle = 34634,
        LastLash = 34635,
        TwinfangBite = 34636,
        TwinbloodBite = 34637,
        TwinfangThresh = 34638,
        TwinbloodThresh = 34639,
        FirstLegacy = 34640,
        SecondLegacy = 34641,
        ThirdLegacy = 34642,
        FourthLegacy = 34643,
        UncoiledTwinfang = 34644,
        UncoiledTwinblood = 34645,
        Slither = 34646,
        SerpentsIre = 34647,
        SerpentsTail = 35920,
        Twinfang = 35921,
        Twinblood = 35922;

    public static class Buffs
    {
        public const ushort
            Placeholder = 0;
    }

    public static class Debuffs
    {
        public const ushort
            Placeholder = 0;
    }

    public static class Levels
    {
        public const byte
            SerpentsTail = 55,
            TwinFangBlood = 75,
            Legacies = 100;
    }
}

internal class SteelTailFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperSteelTailFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.SteelFangs)
        {
            if (level >= VPR.Levels.SerpentsTail)
            {
                if (OriginalHook(VPR.SerpentsTail) == VPR.DeathRattle)
                    return VPR.DeathRattle;

                if (level >= VPR.Levels.Legacies && IsEnabled(CustomComboPreset.ViperSteelLegaciesFeature))
                    if(OriginalHook(VPR.SerpentsTail) == VPR.FirstLegacy)
                        return VPR.FirstLegacy;
            }
        }

        if (actionID == VPR.DreadFangs)
        {
            if (level >= VPR.Levels.Legacies && IsEnabled(CustomComboPreset.ViperSteelLegaciesFeature))
                if (OriginalHook(VPR.SerpentsTail) == VPR.SecondLegacy)
                    return VPR.SecondLegacy;
        }

        if (actionID == VPR.SteelMaw && IsEnabled(CustomComboPreset.ViperSteelTailAoEFeature))
        {
            if (level >= VPR.Levels.SerpentsTail)
            {
                if (OriginalHook(VPR.SerpentsTail) == VPR.LastLash)
                    return VPR.LastLash;

                if (level >= VPR.Levels.Legacies && IsEnabled(CustomComboPreset.ViperSteelLegaciesFeature))
                    if (OriginalHook(VPR.SerpentsTail) == VPR.FirstLegacy)
                        return VPR.FirstLegacy;
            }
        }

        if (actionID == VPR.DreadMaw)
        {
            if (level >= VPR.Levels.Legacies && IsEnabled(CustomComboPreset.ViperSteelLegaciesFeature))
                if (OriginalHook(VPR.SerpentsTail) == VPR.SecondLegacy)
                    return VPR.SecondLegacy;
        }

        return actionID;
    }
}

internal class TwinCoilFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperTwinCoilFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {

        if (actionID == VPR.HuntersCoil)
        {
            if (level >= VPR.Levels.TwinFangBlood && OriginalHook(VPR.Twinfang) == VPR.TwinfangBite)
                return VPR.TwinfangBite;

            if (level >= VPR.Levels.Legacies && IsEnabled(CustomComboPreset.ViperCoilLegaciesFeature))
                if (OriginalHook(VPR.SerpentsTail) == VPR.ThirdLegacy)
                    return VPR.ThirdLegacy;
        }

        if (actionID == VPR.SwiftskinsCoil)
        {
            if (level >= VPR.Levels.TwinFangBlood && OriginalHook(VPR.Twinblood) == VPR.TwinbloodBite)
                return VPR.TwinbloodBite;

            if (level >= VPR.Levels.Legacies && IsEnabled(CustomComboPreset.ViperCoilLegaciesFeature))
                if (OriginalHook(VPR.SerpentsTail) == VPR.FourthLegacy)
                    return VPR.FourthLegacy;
        }

        if (IsEnabled(CustomComboPreset.ViperTwinDenFeature))
        {
            if (actionID == VPR.HuntersDen)
            {
                if (level >= VPR.Levels.TwinFangBlood && OriginalHook(VPR.Twinfang) == VPR.TwinfangThresh)
                    return VPR.TwinfangThresh;

                if (level >= VPR.Levels.Legacies && IsEnabled(CustomComboPreset.ViperCoilLegaciesFeature))
                    if (OriginalHook(VPR.SerpentsTail) == VPR.ThirdLegacy)
                        return VPR.ThirdLegacy;
            }

            if (actionID == VPR.SwiftskinsDen)
            {
                if (level >= VPR.Levels.TwinFangBlood && OriginalHook(VPR.Twinblood) == VPR.TwinbloodThresh)
                    return VPR.TwinbloodThresh;

                if (level >= VPR.Levels.Legacies && IsEnabled(CustomComboPreset.ViperCoilLegaciesFeature))
                    if (OriginalHook(VPR.SerpentsTail) == VPR.FourthLegacy)
                        return VPR.FourthLegacy;
            }
        }

        return actionID;
    }
}

internal class UncoiledFollowupFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperUncoiledFollowupFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.UncoiledFury)
        {
            // If I'm reading this right, it will always want to go in this order
            if (OriginalHook(VPR.Twinfang) == VPR.UncoiledTwinfang)
                return VPR.UncoiledTwinfang;

            if (OriginalHook(VPR.Twinblood) == VPR.UncoiledTwinblood)
                return VPR.UncoiledTwinblood;
        }

        return actionID;
    }
}

// TODO: Once Gauge is implemented
// internal class FuryAndIreFeature : CustomCombo
// {
//     protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperFuryAndIreFeature;

//     protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
//     {
//         if (actionID == VPR.UncoiledFury)
//         {
//             var gauge = GetJobGauge<VPRGauge>();
//             if (gauge.RattlingCoil == 0)
//                 return VPR.SerpentsIre;
//         }
//         return actionID;
//     }
// }