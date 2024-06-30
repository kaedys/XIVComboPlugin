using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedPlugin.Combos;

internal static class PCT
{
    public const byte JobID = 42; // Verify

    public const uint
        Fire = 34650,
        Aero = 34651,
        Water = 34652,
        Fire2 = 34656,
        Aero2 = 34651,
        Water2 = 34652,
        Blizzard = 34653,
        Stone = 34654,
        Thunder = 34655,
        Blizzard2 = 34659,
        Stone2 = 34660,
        Thunder2 = 34661,
        Holy = 34662,
        Comet = 34663,
        MogAges = 34676,
        RetributionMadeen = 34677,
        HammerStamp = 34678,
        HammerBrush = 34679,
        PolishingHammer = 34680,
        StarPrism = 34681,
        SubtractivePalette = 34683,
        RainbowDrip = 34688,

        PomMotif = 34664,
        WingMotif = 34665,
        ClawMotif = 34666,
        MawMotif = 34667,
        HammerMotif = 34668,
        StarrySkyMotif = 34669,
        CreatureMotif = 34689,
        WeaponMotif = 34690,
        LandscapeMotif = 34691,

        PomMuse = 34670,
        WingedMuse = 34671,
        ClawedMuse = 34672,
        FangedMuse = 34673,
        LivingMuse = 35347,
        SteelMuse = 35348,
        StrikingMuse = 34674,
        StarryMuse = 34675,
        ScenicMuse = 35349;

    public static class Buffs
    {
        public const ushort
            Swiftcast = 167,
            Aetherhues1 = 3675, // Confirm, could also be 4100
            Aetherhues2 = 3676, // Confirm, could also be 4101
            SubtractivePalette = 3674, // Confirm, could also be 4102
            Hyperphantasia = 3688,
            HammerTime = 3680,
            MonochromeTones = 3691,
            RainbowBright = 3679,
            Starstruck = 3681; // Confirm, could also be 4118
    }

    public static class Debuffs
    {
        public const ushort
            Placeholder = 0;
    }

    public static class Levels
    {
        public const byte
            Fire2 = 25,
            CreatureMotif = 30, // Also includes Pom of the Ages
            WeaponMotif = 50,
            HammerStamp = 50,
            SubtractivePalette = 60, // Also when the Palette gauge unlocks
            LandscapeMotif = 70,
            Holy = 80, // Also when the White Paint gauge unlocks
            Hyperphantasia = 82,
            HammerCombo = 86, // Hammer Brush and Polishing Hammer
            Comet = 90,
            RainbowDrip = 92,
            CreatureMotif2 = 96, // Claw and Maw Motifs, and Retribution of the Madeen
            StarPrism = 100;
    }
}

internal class PictomancerHoly : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == PCT.Holy)
        {
            if (IsEnabled(CustomComboPreset.PictomancerHolyCometCombo))
            {
                if (HasEffect(PCT.Buffs.MonochromeTones))
                    return PCT.Comet;
            }
        }

        return actionID;
    }
}

internal class PictomancerSubtractivePalette : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == PCT.SubtractivePalette)
        {
            // FIXME: Waiting for implementation
            // var gauge = GetJobGauge<PCTGauge>();

            if (IsEnabled(CustomComboPreset.PictomancerSubtractiveCometCombo))
            {
                // FIXME: Adjust for how the gauge object is actually implemented
                // if (HasEffect(PCT.Buffs.MonochromeTones) && gauge.Paint > 0)
                //     return PCT.Comet;
            }
        }

        return actionID;
    }
}

internal class PictomancerPrimaryCombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == PCT.Fire || actionID == PCT.Aero || actionID == PCT.Water)
        {
            // FIXME: Waiting for implementation
            // var gauge = GetJobGauge<PCTGauge>();

            if (IsEnabled(CustomComboPreset.PictomancerHolyAutoCombo))
            {
                // FIXME: Adjust for how the gauge object is actually implemented
                // if (gauge.Paint > 4)
                // {
                //     if (HasEffect(PCT.Buffs.MonochromeTones))
                //         return PCT.Comet;

                //     return PCT.Holy;
                // }
            }

            if (IsEnabled(CustomComboPreset.PictomancerSubtractiveSTCombo))
            {
                if (HasEffect(PCT.Buffs.SubtractivePalette))
                {
                    return OriginalHook(PCT.Blizzard);
                }
            }

            if (IsEnabled(CustomComboPreset.PictomancerSubtractiveAutoCombo))
            {
                // FIXME: Adjust for how the gauge object is actually implemented
                // if (guage.Palette > 75)
                //     return PCT.SubtractivePalette;
            }
        }

        return actionID;
    }
}

internal class PictomancerPaletteCombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == PCT.Fire || actionID == PCT.Aero || actionID == PCT.Water)
        {
            // FIXME: Waiting for implementation
            // var gauge = GetJobGauge<PCTGauge>();

            if (IsEnabled(CustomComboPreset.PictomancerHolyAutoCombo))
            {
                // FIXME: Adjust for how the gauge object is actually implemented
                // if (gauge.Paint > 4)
                // {
                //     if (HasEffect(PCT.Buffs.MonochromeTones))
                //         return PCT.Comet;

                //     return PCT.Holy;
                // }
            }
            
            if (IsEnabled(CustomComboPreset.PictomancerBlizzardSubtractiveCombo))
            {
                if (!HasEffect(PCT.Buffs.SubtractivePalette))
                {
                    if (IsEnabled(CustomComboPreset.PictomancerHolyAutoCombo))
                    {
                        // Avoid overwriting Monochrome Tones if it's up
                        if (HasEffect(PCT.Buffs.MonochromeTones))
                            return PCT.Comet;
                    }
                    return PCT.SubtractivePalette;
                }
            }
        }

        return actionID;
    }
}

internal class PictomancerPrimaryAoECombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == PCT.Fire2 || actionID == PCT.Aero2 || actionID == PCT.Water2)
        {
            // FIXME: Waiting for implementation
            // var gauge = GetJobGauge<PCTGauge>();

            if (IsEnabled(CustomComboPreset.PictomancerHolyAutoCombo))
            {
                // FIXME: Adjust for how the gauge object is actually implemented
                // if (gauge.Paint > 4)
                // {
                //     if (HasEffect(PCT.Buffs.MonochromeTones))
                //         return PCT.Comet;

                //     return PCT.Holy;
                // }
            }

            if (IsEnabled(CustomComboPreset.PictomancerSubtractiveAoECombo))
            {
                if (HasEffect(PCT.Buffs.SubtractivePalette))
                {
                    return OriginalHook(PCT.Blizzard2);
                }
            }

            if (IsEnabled(CustomComboPreset.PictomancerSubtractiveAutoCombo))
            {
                // FIXME: Adjust for how the gauge object is actually implemented
                // if (guage.Palette > 75)
                //     return PCT.SubtractivePalette;
            }
        }

        return actionID;
    }
}

internal class PictomancerPaletteAoECombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == PCT.Blizzard2 || actionID == PCT.Stone2 || actionID == PCT.Thunder2)
        {
            // FIXME: Waiting for implementation
            // var gauge = GetJobGauge<PCTGauge>();

            if (IsEnabled(CustomComboPreset.PictomancerHolyAutoCombo))
            {
                // FIXME: Adjust for how the gauge object is actually implemented
                // if (gauge.Paint > 4)
                // {
                //     if (HasEffect(PCT.Buffs.MonochromeTones))
                //         return PCT.Comet;

                //     return PCT.Holy;
                // }
            }
            
            if (IsEnabled(CustomComboPreset.PictomancerBlizzardSubtractiveCombo))
            {
                if (!HasEffect(PCT.Buffs.SubtractivePalette))
                {
                    if (IsEnabled(CustomComboPreset.PictomancerHolyAutoCombo))
                    {
                        // Avoid overwriting Monochrome Tones if it's up
                        if (HasEffect(PCT.Buffs.MonochromeTones))
                            return PCT.Comet;
                    }
                    return PCT.SubtractivePalette;
                }
            }
        }

        return actionID;
    }
}

internal class PictomancerCreatureMotifCombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.PctAny;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        // Note sure which action IDs on these ones will come in as.
        if (actionID == PCT.CreatureMotif || actionID == PCT.PomMotif || actionID == PCT.WingMotif || actionID == PCT.ClawMotif || actionID == PCT.MawMotif)
        {
            // FIXME: Waiting for implementation
            // var gauge = GetJobGauge<PCTGauge>();

            if (IsEnabled(CustomComboPreset.PictomancerCreatureMogCombo))
            {
                // FIXME: Adjust for how the gauge object is actually implemented.  I'm assuming for now that 
                // 0 == no creature portrait available, and enums defining which one is available if so.
                // However it ends up implemented, this needs to detect if one of the two creature portraits (mog or
                // madeen) are active, which occurs after Wing and Maw Motif are used.
                // if (gauge.Portrait > 0)
                //     return OriginalHook(PCT.LivingMuse);
            }
            
            if (IsEnabled(CustomComboPreset.PictomancerCreatureMotifCombo))
            {
                // FIXME: Adjust for how the gauge object is actually implemented.  I'm assuming for now that 
                // 0 == unpainted, and then what's actually painted on it then defined by enums.
                // However it ends up implemented, this needs to detect if the canvas has been already painted.
                // if (gauge.CreatureCanvas > 0)
                //     return OriginalHook(PCT.LivingMuse);
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
        // Note sure which action IDs on these ones will come in as.
        if (actionID == PCT.WeaponMotif || actionID == PCT.HammerMotif)
        {
            // FIXME: Waiting for implementation
            // var gauge = GetJobGauge<PCTGauge>();

            if (IsEnabled(CustomComboPreset.PictomancerWeaponHammerCombo))
            {
                if (HasEffect(PCT.Buffs.HammerTime))
                    return OriginalHook(PCT.HammerStamp);
            }
            
            if (IsEnabled(CustomComboPreset.PictomancerWeaponMotifCombo))
            {
                // FIXME: Adjust for how the gauge object is actually implemented.  I'm assuming for now that 
                // 0 == unpainted, and then what's actually painted on it then defined by enums.
                // However it ends up implemented, this needs to detect if the canvas has been already painted.
                // if (gauge.WeaponCanvas > 0)
                //     return OriginalHook(PCT.StrikingMuse);
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
        // Note sure which action IDs on these ones will come in as.
        if (actionID == PCT.LandscapeMotif || actionID == PCT.StarrySkyMotif)
        {
            // FIXME: Waiting for implementation
            // var gauge = GetJobGauge<PCTGauge>();

            if (IsEnabled(CustomComboPreset.PictomancerLandscapePrismCombo))
            {
                if (HasEffect(PCT.Buffs.Starstruck))
                    return OriginalHook(PCT.StarPrism);
            }
            
            if (IsEnabled(CustomComboPreset.PictomancerLandscapeMotifCombo))
            {
                // FIXME: Adjust for how the gauge object is actually implemented.  I'm assuming for now that 
                // 0 == unpainted, and then what's actually painted on it then defined by enums.
                // However it ends up implemented, this needs to detect if the canvas has been already painted.
                // if (gauge.LandscapeCanvas > 0)
                //     return OriginalHook(PCT.ScenicMuse);
            }
        }

        return actionID;
    }
}