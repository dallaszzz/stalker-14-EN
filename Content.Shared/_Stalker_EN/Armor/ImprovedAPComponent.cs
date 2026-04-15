using Robust.Shared.GameStates;

namespace Content.Shared._Stalker_EN.Armor;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class ImprovedAPComponent : Component
{
    [DataField, AutoNetworkedField]
    public float Penetration = 0;
}
