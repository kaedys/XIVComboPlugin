using Dalamud.Game.ClientState.JobGauge.Enums;
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
            PitOfDread = 34623,
            HuntersDen = 34624,
            SwiftskinsDen = 34625,

            SerpentsTail = 35920,
            DeathRattle = 34634,
            LastLash = 34635,
            Twinfang = 35921,
            Twinblood = 35922,
            TwinfangBite = 34636,
            TwinfangThresh = 34638,
            TwinbloodBite = 34637,
            TwinbloodThresh = 34639,

            UncoiledFury = 34633,
            UncoiledTwinfang = 34644,
            UncoiledTwinblood = 34645,

            SerpentsIre = 34647,
            Reawaken = 34626,
            FirstGeneration = 34627,
            SecondGeneration = 34628,
            ThirdGeneration = 34629,
            FourthGeneration = 34630,
            Ouroboros = 34631,
            FirstLegacy = 34640,
            SecondLegacy = 34641,
            ThirdLegacy = 34642,
            FourthLegacy = 34643,

            WrithingSnap = 34632,
            Slither = 34646;

    public static class Buffs
    {
        public const ushort
            FlankstungVenom = 3645,
            FlanksbaneVenom = 3646,
            HindstungVenom = 3647,
            HindsbaneVenom = 3648,
            GrimhuntersVenom = 3649,
            GrimskinsVenom = 3650,
            HuntersVenom = 3657,
            SwiftskinsVenom = 3658,
            FellhuntersVenom = 3659,
            FellskinsVenom = 3660,
            PoisedForTwinfang = 3665,
            PoisedForTwinblood = 3666,
            HuntersInstinct = 3668, // Double check, might also be 4120
            Swiftscaled = 3669,     // Might also be 4121
            Reawakened = 3670,
            ReadyToReawaken = 3671;
    }

    public static class Debuffs
    {
        public const ushort
            NoxiousGash = 3667;
    }

    public static class Levels
    {
        public const byte
            SteelFangs = 1,
            HuntersSting = 5,
            DreadFangs = 10,
            WrithingSnap = 15,
            SwiftskinsSting = 20,
            SteelMaw = 25,
            Single3rdCombo = 30, // Includes Flanksting, Flanksbane, Hindsting, and Hindsbane
            DreadMaw = 35,
            Slither = 40,
            HuntersBite = 40,
            SwiftskinsBike = 45,
            AoE3rdCombo = 50,    // Jagged Maw and Bloodied Maw
            DeathRattle = 55,
            LastLash = 60,
            Dreadwinder = 65,    // Also includes Hunter's Coil and Swiftskin's Coil
            PitOfDread = 70,     // Also includes Hunter's Den and Swiftskin's Den
            TwinsSingle = 75,    // Twinfang Bite and Twinblood Bite
            TwinsAoE = 80,       // Twinfang Thresh and Twinblood Thresh
            UncoiledFury = 82,
            UncoiledTwins = 92,  // Uncoiled Twinfang and Uncoiled Twinblood
            SerpentsIre = 86,
            EnhancedRattle = 88, // Third stack of Rattling Coil can be accumulated
            Reawaken = 90,       // Also includes First Generation through Fourth Generation
            Ouroboros = 96,
            Legacies = 100;      // First through Fourth Legacy
    }
}

internal class SteelTailFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperSteelTailFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.SteelFangs)
        {
            if (OriginalHook(VPR.SerpentsTail) == VPR.DeathRattle)
                return VPR.DeathRattle;
        }

        if (actionID == VPR.DreadFangs)
        {
            if (OriginalHook(VPR.SerpentsTail) == VPR.DeathRattle)
                return VPR.DeathRattle;
        }

        return actionID;
    }
}

internal class SteelTailAoEFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperSteelTailAoEFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.SteelMaw)
        {
            if (OriginalHook(VPR.SerpentsTail) == VPR.LastLash)
                return VPR.LastLash;
        }

        if (actionID == VPR.DreadMaw)
        {
            if (OriginalHook(VPR.SerpentsTail) == VPR.LastLash)
                return VPR.LastLash;
        }

        return actionID;
    }
}

internal class SteelCoilFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperSteelCoilFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        var gauge = GetJobGauge<VPRGauge>();
        if (actionID == VPR.SteelFangs)
        {
            if (gauge.AnguineTribute >= 5 || (level < VPR.Levels.Ouroboros && gauge.AnguineTribute >= 4))
                return VPR.FirstGeneration;

            if (gauge.AnguineTribute > 0)
                return VPR.ThirdGeneration;

            if (gauge.DreadCombo == DreadCombo.Dreadwinder || gauge.DreadCombo == DreadCombo.SwiftskinsCoil)
                return VPR.HuntersCoil;
        }

        if (actionID == VPR.DreadFangs)
        {
            if (gauge.AnguineTribute >= 3 || (level < VPR.Levels.Ouroboros && gauge.AnguineTribute >= 2))
                return VPR.SecondGeneration;

            if (gauge.AnguineTribute > 0)
                return VPR.FourthGeneration;

            if (gauge.DreadCombo == DreadCombo.Dreadwinder || gauge.DreadCombo == DreadCombo.HuntersCoil)
                    return VPR.SwiftskinsCoil;
        }

        return actionID;
    }
}


internal class SteelCoilAoEFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperSteelCoilAoEFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        var gauge = GetJobGauge<VPRGauge>();
        if (actionID == VPR.SteelMaw)
        {
            if (gauge.AnguineTribute >= 5 || (level < VPR.Levels.Ouroboros && gauge.AnguineTribute >= 4))
                return VPR.FirstGeneration;

            if (gauge.AnguineTribute > 0)
                return VPR.ThirdGeneration;

            if (gauge.DreadCombo == DreadCombo.PitOfDread || gauge.DreadCombo == DreadCombo.SwiftskinsDen)
                return VPR.HuntersDen;
        }

        if (actionID == VPR.DreadMaw)
        {
            if (gauge.AnguineTribute >= 3 || (level < VPR.Levels.Ouroboros && gauge.AnguineTribute >= 2))
                return VPR.SecondGeneration;

            if (gauge.AnguineTribute > 0)
                return VPR.FourthGeneration;

            if (gauge.DreadCombo == DreadCombo.PitOfDread || gauge.DreadCombo == DreadCombo.HuntersDen)
                return VPR.SwiftskinsDen;
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
            if (HasEffect(VPR.Buffs.HuntersVenom))
                return VPR.TwinfangBite;
            if (HasEffect(VPR.Buffs.SwiftskinsVenom))
                return VPR.TwinbloodBite;
            if (!IsOriginal(VPR.TwinfangBite))
                return OriginalHook(VPR.TwinfangBite);
            if (!IsOriginal(VPR.TwinbloodBite))
                return OriginalHook(VPR.TwinbloodBite);
        }

        if (actionID == VPR.SwiftskinsCoil)
        {
            if (HasEffect(VPR.Buffs.HuntersVenom))
                return VPR.TwinfangBite;
            if (HasEffect(VPR.Buffs.SwiftskinsVenom))
                return VPR.TwinbloodBite;
            if (!IsOriginal(VPR.Twinfang))
                return OriginalHook(VPR.Twinfang);
            if (!IsOriginal(VPR.Twinblood))
                return OriginalHook(VPR.Twinblood);
        }

        return actionID;
    }
}

internal class TwinDenFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperTwinDenFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.HuntersDen)
        {
            if (HasEffect(VPR.Buffs.FellhuntersVenom))
                return VPR.TwinfangThresh;
            if (HasEffect(VPR.Buffs.FellskinsVenom))
                return VPR.TwinbloodThresh;
            if (!IsOriginal(VPR.Twinfang))
                return OriginalHook(VPR.Twinfang);
            if (!IsOriginal(VPR.Twinblood))
                return OriginalHook(VPR.Twinblood);
        }

        if (actionID == VPR.SwiftskinsDen)
        {
            if (HasEffect(VPR.Buffs.FellhuntersVenom))
                return VPR.TwinfangThresh;
            if (HasEffect(VPR.Buffs.FellskinsVenom))
                return VPR.TwinbloodThresh;
            if (!IsOriginal(VPR.Twinfang))
                return OriginalHook(VPR.Twinfang);
            if (!IsOriginal(VPR.Twinblood))
                return OriginalHook(VPR.Twinblood);
        }

        return actionID;
    }
}

internal class GenerationLegacies : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperGenerationLegaciesFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.SteelFangs)
        {
            if (OriginalHook(VPR.SerpentsTail) == VPR.FirstLegacy)
                return VPR.FirstLegacy;
        }

        if (actionID == VPR.DreadFangs)
        {
            if (OriginalHook(VPR.SerpentsTail) == VPR.SecondLegacy)
                return VPR.SecondLegacy;
        }

        if (actionID == VPR.HuntersCoil)
        {
            if (OriginalHook(VPR.SerpentsTail) == VPR.ThirdLegacy)
                return VPR.ThirdLegacy;
        }

        if (actionID == VPR.SwiftskinsCoil)
        {
            if (OriginalHook(VPR.SerpentsTail) == VPR.FourthLegacy)
                return VPR.FourthLegacy;
        }

        return actionID;
    }
}

internal class GenerationLegaciesAoE : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperGenerationLegaciesAoEFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.SteelMaw)
        {
            if (OriginalHook(VPR.SerpentsTail) == VPR.FirstLegacy)
                return VPR.FirstLegacy;
        }

        if (actionID == VPR.DreadMaw)
        {
            if (OriginalHook(VPR.SerpentsTail) == VPR.SecondLegacy)
                return VPR.SecondLegacy;
        }

        if (actionID == VPR.HuntersDen)
        {
            if (OriginalHook(VPR.SerpentsTail) == VPR.ThirdLegacy)
                return VPR.ThirdLegacy;
        }

        if (actionID == VPR.SwiftskinsDen)
        {
            if (OriginalHook(VPR.SerpentsTail) == VPR.FourthLegacy)
                return VPR.FourthLegacy;
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
            if (OriginalHook(VPR.Twinfang) == VPR.UncoiledTwinfang && HasEffect(VPR.Buffs.PoisedForTwinfang))
                return VPR.UncoiledTwinfang;

            if (OriginalHook(VPR.Twinblood) == VPR.UncoiledTwinblood)
                return VPR.UncoiledTwinblood;
        }

        return actionID;
    }
}

internal class DreadSteelFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperDreadSteelFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.DreadFangs)
        {
            if (level >= VPR.Levels.DreadFangs && IsOriginal(VPR.DreadFangs))
            {
                var gash = FindTargetEffect(VPR.Debuffs.NoxiousGash);
                if (gash is null || gash.RemainingTime <= 20)
                    return VPR.DreadFangs;

                return VPR.SteelFangs;
            }
        }

        if (actionID == VPR.SteelFangs)
        {
            if (level >= VPR.Levels.DreadFangs && IsOriginal(VPR.SteelFangs))
            {
                var gash = FindTargetEffect(VPR.Debuffs.NoxiousGash);
                if (gash is null || gash.RemainingTime <= 20)
                    return VPR.DreadFangs;

                return VPR.SteelFangs;
            }
        }

        return actionID;
    }
}

internal class DreadSteelAoEFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperDreadSteelAoEFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.DreadMaw)
        {
            if (level >= VPR.Levels.DreadMaw && IsOriginal(VPR.DreadMaw))
            {
                var gash = FindTargetEffect(VPR.Debuffs.NoxiousGash);
                if (gash is null || gash.RemainingTime <= 20)
                    return VPR.DreadMaw;

                return VPR.SteelMaw;
            }
        }

        if (actionID == VPR.SteelMaw)
        {
            if (level >= VPR.Levels.DreadMaw && IsOriginal(VPR.SteelMaw))
            {
                var gash = FindTargetEffect(VPR.Debuffs.NoxiousGash);
                if (gash is null || gash.RemainingTime <= 20)
                    return VPR.DreadMaw;

                return VPR.SteelMaw;
            }
        }

        return actionID;
    }
}

internal class DreadfangsDreadwinderFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperDreadfangsDreadwinderFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.DreadFangs)
        {
            // I think in this case if we're not in a combo (and something else isn't replacing Dread Fangs), we can just replace if we have charges
            if (level >= VPR.Levels.Dreadwinder && IsOriginal(VPR.DreadFangs) && HasCharges(VPR.Dreadwinder) && IsOriginal(VPR.SerpentsTail)) // Add the check for Serpent's Tail to avoid stepping on other combo
                return VPR.Dreadwinder;
        }

        return actionID;
    }
}

internal class PitOfDreadFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperPitOfDreadFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.DreadMaw)
        {
            if (level >= VPR.Levels.PitOfDread && IsOriginal(VPR.DreadMaw) && HasCharges(VPR.PitOfDread) && IsOriginal(VPR.SerpentsTail)) // Add the check for Serpent's Tail to avoid stepping on other combo
                return VPR.PitOfDread;
        }

        return actionID;
    }
}

internal class MergeSerpentTwinsFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperMergeSerpentTwinsFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.SerpentsTail)
        {
            if (!IsOriginal(VPR.SerpentsTail))
                return OriginalHook(VPR.SerpentsTail);

            if (HasEffect(VPR.Buffs.PoisedForTwinfang) || HasEffect(VPR.Buffs.HuntersVenom) || HasEffect(VPR.Buffs.FellhuntersVenom))
                return OriginalHook(VPR.Twinfang);

            if (HasEffect(VPR.Buffs.PoisedForTwinblood) || HasEffect(VPR.Buffs.SwiftskinsVenom) || HasEffect(VPR.Buffs.FellskinsVenom))
                return OriginalHook(VPR.Twinblood);

            if (!IsOriginal(VPR.Twinfang))
                return OriginalHook(VPR.Twinfang);

            if (!IsOriginal(VPR.Twinblood))
                return OriginalHook(VPR.Twinblood);
        }

        return actionID;
    }
}

internal class MergeTwinsSerpentFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperMergeTwinsSerpentFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.Twinfang)
        {
            if (!IsOriginal(VPR.SerpentsTail))
                return OriginalHook(VPR.SerpentsTail);
            else
                return OriginalHook(VPR.Twinfang);
        }

        if (actionID == VPR.Twinblood)
        {
            if (!IsOriginal(VPR.SerpentsTail))
                return OriginalHook(VPR.SerpentsTail);
            else
                return OriginalHook(VPR.Twinblood);
        }

        return actionID;
    }
}

internal class FuryAndIreFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.ViperFuryAndIreFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == VPR.UncoiledFury && level >= VPR.Levels.UncoiledFury)
        {
            var gauge = GetJobGauge<VPRGauge>();
            if (gauge.RattlingCoilStacks == 0)
                return VPR.SerpentsIre;
        }

        return actionID;
    }
}