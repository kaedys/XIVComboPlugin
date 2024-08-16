using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedPlugin.Combos;

internal static class PCT
{
    public const byte JobID = 42;

    public const uint
        FireRed = 34650,
        AeroGreen = 34651,
        WaterBlue = 34652,
        BlizzardCyan = 34653,
        EarthYellow = 34654,
        ThunderMagenta = 34655,
        FireRedAoE = 34656,
        AeroGreenAoE = 34657,
        WaterBlueAoE = 34658,
        BlizzardCyanAoE = 34659,
        EarthYellowAoE = 34660,
        ThunderMagentaAoE = 34661,
        HolyWhite = 34662,
        CometBlack = 34663,
        PomMotif = 34664,
        WingMotif = 34665,
        ClawMotif = 34666,
        MawMotif = 34667,
        HammerMotif = 34668,
        StarrySkyMotif = 34669,
        PomMuse = 34670,
        WingedMuse = 34671,
        ClawedMuse = 34672,
        FangedMuse = 34673,
        StrikingMuse = 34674,
        StarryMuse = 34675,
        MogOftheAges = 34676,
        Retribution = 34677,
        HammerStamp = 34678,
        HammerBrush = 34679,
        PolishingHammer = 34680,
        StarPrism1 = 34681,
        StarPrism2 = 34682,
        SubstractivePalette = 34683,
        Smudge = 34684,
        TemperaCoat = 34685,
        TemperaGrassa = 34686,
        RainbowDrip = 34688,
        CreatureMotif = 34689,
        WeaponMotif = 34690,
        LandscapeMotif = 34691,
        CreatureMotifDrawn = 35347,
        WeaponMotifDrawn = 35348,
        LandscapeMotifDrawn = 35349;

    public static class Buffs
    {
        public const ushort
            SubstractivePalette = 3674,
            Aetherhues1 = 3675,
            Aetherhues2 = 3676,
            RainbowReady = 3679,
            HammerReady = 3680,
            StarPrismReady = 3681,
            Hyperphantasia = 3688,
            Inspiration = 3689,
            SubstractiveReady = 3690,
            MonochromeTones = 3691;
    }

    public static class Debuffs
    {
        public const ushort
            Placeholder = 0;
    }

    public static class Levels
    {
        public const byte
            FireRed = 1,
            AeroGreen = 5,
            TemperaCoat = 10,
            WaterBlue = 15,
            Smudge = 20,
            FireRedAoE = 25,
            MoogleMotifs = 30,
            AeroGreenAoE = 35,
            WaterBlueAoE = 45,
            HammerMotif = 50,
            SubstractivePalette = 60,
            StarrySkyMotif = 70,
            HolyWhite = 80,
            HammerExtended = 86,
            PolishingHammer = 86,
            TemperaGrassa = 88,
            CometBlack = 90,
            RainbowDrip = 92,
            MadeenMotifs = 96,
            Retribution = 96,
            StarPrism = 100;
    }
}

internal class PictomancerSTCombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        var gauge = GetJobGauge<PCTGauge>();

        if (actionID == PCT.FireRed || actionID == PCT.BlizzardCyan)
        {
            if (IsEnabled(CustomComboPreset.PictomancerRainbowStarterCombo))
            {
                if (level >= PCT.Levels.RainbowDrip && !InCombat())
                    return PCT.RainbowDrip;
            }

            if (IsEnabled(CustomComboPreset.PictomancerStarPrismAutoCombo))
            {
                if (HasEffect(PCT.Buffs.StarPrismReady) &&
                    (!IsEnabled(CustomComboPreset.PictomancerStarPrismAfterSubtractiveFeature) ||
                    (!HasEffect(PCT.Buffs.SubstractivePalette) && !HasEffect(PCT.Buffs.SubstractiveReady))))
                    return PCT.StarPrism1;
            }

            if (IsEnabled(CustomComboPreset.PictomancerRainbowAutoCombo))
            {
                if (HasEffect(PCT.Buffs.RainbowReady) &&
                    (gauge.Paint < 5 || !IsEnabled(CustomComboPreset.PictomancerHolyAutoCombo)))
                    return PCT.RainbowDrip;
            }

            if (IsEnabled(CustomComboPreset.PictomancerAutoMogCombo))
            {
                if ((gauge.MooglePortraitReady || gauge.MadeenPortraitReady) && IsCooldownUsable(PCT.MogOftheAges))
                    return OriginalHook(PCT.MogOftheAges);
            }

            if (IsEnabled(CustomComboPreset.PictomancerAutoCometFeature))
            {
                if (HasEffect(PCT.Buffs.MonochromeTones) &&
                    (!IsEnabled(CustomComboPreset.PictomancerCometAfterSubtractive) ||
                    !HasEffect(PCT.Buffs.SubstractivePalette)) &&
                    (!IsEnabled(CustomComboPreset.PictomancerCometStarryOnly) ||
                    HasEffect(PCT.Buffs.Inspiration)))
                    return PCT.CometBlack;
            }

            if (IsEnabled(CustomComboPreset.PictomancerSubtractiveAutoCombo) &&
                !HasEffect(PCT.Buffs.SubstractivePalette))
            {
                if (HasEffect(PCT.Buffs.SubstractiveReady))
                    return PCT.SubstractivePalette;

                if (gauge.PalleteGauge >= 50 && !IsEnabled(CustomComboPreset.PictomancerSubtractiveOvercap))
                    return PCT.SubstractivePalette;

                if (gauge.PalleteGauge == 100 && HasEffect(PCT.Buffs.Aetherhues2))
                    return PCT.SubstractivePalette;
            }

            if (IsEnabled(CustomComboPreset.PictomancerHolyAutoCombo))
            {
                if (gauge.Paint == 5 && HasEffect(PCT.Buffs.Aetherhues2))
                    return HasEffect(PCT.Buffs.MonochromeTones) ? PCT.CometBlack : PCT.HolyWhite;
            }

            if (IsEnabled(CustomComboPreset.PictomancerSubtractiveSTCombo) &&
                !HasEffect(PCT.Buffs.SubstractivePalette))
                return OriginalHook(PCT.FireRed);
        }

        return actionID;
    }
}

internal class PictomancerAoECombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        var gauge = GetJobGauge<PCTGauge>();

        if (actionID == PCT.FireRedAoE || actionID == PCT.BlizzardCyanAoE)
        {
            if (IsEnabled(CustomComboPreset.PictomancerRainbowStarterCombo))
            {
                if (level >= PCT.Levels.RainbowDrip && !InCombat())
                    return PCT.RainbowDrip;
            }

            if (IsEnabled(CustomComboPreset.PictomancerStarPrismAutoCombo))
            {
                if (HasEffect(PCT.Buffs.StarPrismReady) &&
                    (!IsEnabled(CustomComboPreset.PictomancerStarPrismAfterSubtractiveFeature) ||
                    (!HasEffect(PCT.Buffs.SubstractivePalette) && !HasEffect(PCT.Buffs.SubstractiveReady))))
                    return PCT.StarPrism1;
            }

            if (IsEnabled(CustomComboPreset.PictomancerRainbowAutoCombo))
            {
                if (HasEffect(PCT.Buffs.RainbowReady) &&
                    (gauge.Paint < 5 || !IsEnabled(CustomComboPreset.PictomancerHolyAutoCombo)))
                    return PCT.RainbowDrip;
            }

            if (IsEnabled(CustomComboPreset.PictomancerAutoMogCombo))
            {
                if ((gauge.MooglePortraitReady || gauge.MadeenPortraitReady) && IsCooldownUsable(PCT.MogOftheAges))
                    return OriginalHook(PCT.MogOftheAges);
            }

            if (IsEnabled(CustomComboPreset.PictomancerAutoCometFeature))
            {
                if (HasEffect(PCT.Buffs.MonochromeTones) &&
                    (!IsEnabled(CustomComboPreset.PictomancerCometAfterSubtractive) ||
                    !HasEffect(PCT.Buffs.SubstractivePalette)) &&
                    (!IsEnabled(CustomComboPreset.PictomancerCometStarryOnly) ||
                    HasEffect(PCT.Buffs.Inspiration)))
                    return PCT.CometBlack;
            }

            if (IsEnabled(CustomComboPreset.PictomancerSubtractiveAutoCombo) &&
                !HasEffect(PCT.Buffs.SubstractivePalette))
            {
                if (HasEffect(PCT.Buffs.SubstractiveReady))
                    return PCT.SubstractivePalette;

                if (gauge.PalleteGauge >= 50 && !IsEnabled(CustomComboPreset.PictomancerSubtractiveOvercap))
                    return PCT.SubstractivePalette;

                if (gauge.PalleteGauge == 100 && HasEffect(PCT.Buffs.Aetherhues2))
                    return PCT.SubstractivePalette;
            }

            if (IsEnabled(CustomComboPreset.PictomancerHolyAutoCombo))
            {
                if (gauge.Paint == 5 && HasEffect(PCT.Buffs.Aetherhues2))
                    return HasEffect(PCT.Buffs.MonochromeTones) ? PCT.CometBlack : PCT.HolyWhite;
            }

            if (IsEnabled(CustomComboPreset.PictomancerSubtractiveAoECombo) && !HasEffect(PCT.Buffs.SubstractivePalette))
                return OriginalHook(PCT.FireRedAoE);
        }

        return actionID;
    }
}

internal class PictomancerHolyCometCombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == PCT.HolyWhite)
        {
            var gauge = GetJobGauge<PCTGauge>();

            if (IsEnabled(CustomComboPreset.PictomancerRainbowStarterHoly))
            {
                if (level >= PCT.Levels.RainbowDrip && !InCombat())
                    return PCT.RainbowDrip;
            }

            if (IsEnabled(CustomComboPreset.PictomancerRainbowHolyCombo) && HasEffect(PCT.Buffs.RainbowReady) &&
                (gauge.Paint < 5 || !IsEnabled(CustomComboPreset.PictomancerHolyAutoCombo)))
                return PCT.RainbowDrip;

            if (IsEnabled(CustomComboPreset.PictomancerHolyCometCombo) && HasEffect(PCT.Buffs.MonochromeTones))
                return PCT.CometBlack;
        }

        return actionID;
    }
}

internal class PictomancerCreatureMotifCombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        var gauge = GetJobGauge<PCTGauge>();

        if (actionID == PCT.CreatureMotif)
        {
            if (IsEnabled(CustomComboPreset.PictomancerCreatureMogCombo))
            {
                if (IsEnabled(CustomComboPreset.PictomancerCreatureMogOvercapCombo))
                {
                    var moogleNext = (gauge.CreatureFlags & CreatureFlags.Pom) != 0 &&
                        (gauge.CreatureFlags & CreatureFlags.Wings) == 0;
                    var madeenNext = (gauge.CreatureFlags & CreatureFlags.Claw) != 0;

                    if ((gauge.MooglePortraitReady || gauge.MadeenPortraitReady) && gauge.CreatureMotifDrawn &&
                        (level < PCT.Levels.MadeenMotifs || moogleNext || madeenNext))
                        return OriginalHook(PCT.MogOftheAges);
                }
                else
                {
                    if ((gauge.MooglePortraitReady || gauge.MadeenPortraitReady) &&
                        IsCooldownUsable(PCT.MogOftheAges))
                            return OriginalHook(PCT.MogOftheAges);
                }
            }

            if (IsEnabled(CustomComboPreset.PictomancerCreatureMotifCombo))
            {
                if (actionID == PCT.CreatureMotif)
                {
                    if (OriginalHook(PCT.CreatureMotifDrawn) != PCT.CreatureMotifDrawn)
                        return OriginalHook(PCT.CreatureMotifDrawn);
                }
            }
        }

        return actionID;
    }
}

internal class PictomancerWeaponMotifCombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        var gauge = GetJobGauge<PCTGauge>();

        if (actionID == PCT.WeaponMotif)
        {
            if (IsEnabled(CustomComboPreset.PictomancerWeaponMotifCombo))
            {
                if (gauge.WeaponMotifDrawn)
                    return PCT.StrikingMuse;
            }

            if (IsEnabled(CustomComboPreset.PictomancerWeaponHammerCombo))
            {
                if (HasEffect(PCT.Buffs.HammerReady))
                {
                    return OriginalHook(PCT.HammerStamp);
                }
            }
        }

        return actionID;
    }
}

internal class PictomancerLandscapeMotifCombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        var gauge = GetJobGauge<PCTGauge>();

        if (actionID == PCT.LandscapeMotif)
        {
            if (IsEnabled(CustomComboPreset.PictomancerLandscapeMotifCombo))
            {
                if (gauge.LandscapeMotifDrawn)
                    return PCT.StarryMuse;
            }

            if (IsEnabled(CustomComboPreset.PictomancerLandscapePrismCombo))
            {
                if (HasEffect(PCT.Buffs.StarPrismReady))
                {
                    return OriginalHook(PCT.StarPrism1);
                }
            }
        }

        return actionID;
    }
}
