using Robust.Shared.GameStates;

namespace Content.Shared._Stalker_EN.Armor;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class ImprovedArmorComponent : Component
{
    [DataField, AutoNetworkedField]
    public float MaxHP = 0;

    [DataField, AutoNetworkedField]
    public float Health = 0;

    [DataField, AutoNetworkedField]
    public float Hardness = 0;

    /// <summary>
    /// The coeffecient applied to damage that doesnt penetrate
    /// </summary>
    [DataField, AutoNetworkedField]
    public float Resistance = 0.85f;
}
