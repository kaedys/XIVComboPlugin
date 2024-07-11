using System.Runtime.InteropServices;

using FFXIVClientStructs.FFXIV.Client.Game;

namespace XIVComboExpandedPlugin;

/// <summary>
/// Internal cooldown data.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
internal struct CooldownData
{
    [FieldOffset(0x0)]
    private readonly bool isCooldown;

    [FieldOffset(0x4)]
    private readonly uint actionID;

    [FieldOffset(0x8)]
    private readonly float cooldownElapsed;

    [FieldOffset(0xC)]
    private readonly float cooldownTotal;

    /// <summary>
    /// Gets the base cooldown time in seconds.
    /// </summary>
    public float BaseCooldown => ActionManager.GetAdjustedRecastTime(ActionType.Action, this.ActionID) / 1000f;

    /// <summary>
    /// Gets the total cooldown calculated from AdjustedRecastTime in seconds.
    /// </summary>
    public float TotalBaseCooldown
    {
        get
        {
            var (cur, max) = Service.ComboCache.GetMaxCharges(this.ActionID);

            // Rebase to the current charge count
            var total = this.BaseCooldown / max * cur;

            return total * cur;
        }
    }

    /// <summary>
    /// Gets the total cooldown time.
    /// </summary>
    public float CooldownTotal
    {
        get
        {
            if (this.cooldownTotal == 0)
                return 0;

            var (cur, max) = Service.ComboCache.GetMaxCharges(this.ActionID);
            if (cur == max)
                return this.cooldownTotal;

            // Rebase to the current charge count
            var total = this.cooldownTotal / max * cur;

            if (this.cooldownElapsed > total)
                return 0;

            return total;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the action is on cooldown.
    /// </summary>
    public bool IsCooldown
    {
        get
        {
            return this.cooldownElapsed < this.BaseCooldown;
        }
    }

    /// <summary>
    /// Gets a value indicating whether all charges are capped.
    /// </summary>
    public bool IsCapped
    {
        get
        {
            return this.cooldownElapsed == 0;
        }
    }

    /// <summary>
    /// Gets the action ID on cooldown.
    /// </summary>
    public uint ActionID => this.actionID;

    /// <summary>
    /// Gets the elapsed cooldown time limited to an active charge (0 if a charge is available).
    /// </summary>
    public float CooldownElapsed
    {
        get
        {
            if (this.cooldownElapsed > this.BaseCooldown)
                return 0;

            return this.cooldownElapsed;
        }
    }

    /// <summary>
    /// Gets the elapsed cooldown time across the total cooldown (total cooldown time - total cooldown already regained).
    /// </summary>
    public float TotalCooldownElapsed => this.cooldownElapsed;

    /// <summary>
    /// Gets the cooldown time remaining until all charges are replenished.
    /// </summary>
    public float TotalCooldownRemaining => this.TotalBaseCooldown - this.TotalCooldownElapsed;

    /// <summary>
    /// Gets the cooldown time remaining until the current cooldown has recovered.
    /// </summary>
    public float CooldownRemaining
    {
        get
        {
            var (cur, _) = Service.ComboCache.GetMaxCharges(this.ActionID);

            return this.TotalCooldownRemaining % (this.TotalBaseCooldown / cur);
        }
    }

    /// <summary>
    /// Gets the maximum number of charges for an action at the current level.
    /// </summary>
    /// <returns>Number of charges.</returns>
    public ushort MaxCharges => Service.ComboCache.GetMaxCharges(this.ActionID).Current;

    /// <summary>
    /// Gets a value indicating whether the action has charges, not charges available.
    /// </summary>
    public bool HasCharges => this.MaxCharges > 1;

    /// <summary>
    /// Gets the remaining number of charges for an action.
    /// </summary>
    public ushort RemainingCharges
    {
        get
        {
            var (cur, _) = Service.ComboCache.GetMaxCharges(this.ActionID);

            if (this.TotalCooldownElapsed == 0)
            {
                return this.MaxCharges;
            }

            return (ushort)(this.TotalCooldownElapsed / (this.TotalBaseCooldown / this.MaxCharges));
        }
    }

    /// <summary>
    /// Gets a value indicating whether gets value indicating whether this action has at least one charge out of however many it has total, even if it can only have one "charge".
    /// </summary>
    public bool Available => this.CooldownRemaining == 0 || this.RemainingCharges > 0;

    /// <summary>
    /// Gets the time since the cooldown was spent in seconds (only fuctional if actionID is not charge based).
    /// </summary>
    public float CooldownDuration => this.BaseCooldown - this.CooldownRemaining;

    /// <summary>
    /// Gets the cooldown time remaining until the next charge.
    /// </summary>
    public float ChargeCooldownRemaining
    {
        get
        {
            var (cur, _) = Service.ComboCache.GetMaxCharges(this.ActionID);

            return this.TotalCooldownRemaining % (this.TotalBaseCooldown / cur);
        }
    }

    /// <summary>
    /// Gets the recovery time in seconds if action is used when cooldown is off.
    /// </summary>
    public float RecoveryTime => this.CooldownRemaining + this.BaseCooldown;

    /// <summary>
    /// Gets the time until another charge is available after using the currently refreshing charge.
    /// </summary>
    public float ChargeRecoveryTime
    {
        get
        {
            if ((this.RemainingCharges - 1) >= 0)
            {
                return this.ChargeCooldownRemaining;
            }

            return this.ChargeCooldownRemaining + this.BaseCooldown;
        }
    }

    /// <summary>
    /// Gets the cooldown time remaining until all charges of ability are replenished.
    /// </summary>
    public float TotalChargeCooldownRemaining => this.MaxCharges - this.RemainingCharges > 0
            ? this.MaxCharges - this.RemainingCharges == 1
                ? this.ChargeCooldownRemaining
                : (this.BaseCooldown * ((this.MaxCharges - this.RemainingCharges) - 1)) + this.ChargeCooldownRemaining
            : 0;
}