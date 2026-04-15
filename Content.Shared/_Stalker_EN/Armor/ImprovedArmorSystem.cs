using Content.Shared.Inventory;
using Content.Shared.Projectiles;

namespace Content.Shared._Stalker_EN.Armor;

public sealed partial class ImprovedArmorSystem : EntitySystem
{
    [Dependency] private readonly InventorySystem _inventory = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<ImprovedAPComponent, ProjectileHitEvent>(OnProjectileHit);
    }

    public void OnProjectileHit(EntityUid projectile, ImprovedAPComponent pierce, ref ProjectileHitEvent args)
    {

        if (!args.Damage.DamageDict.TryGetValue("Piercing", out var damage))
            return;

        string[] slots = {
            "mask",
            "head",
            "outerClothing",
            "cloak",
            "eyes",
            "ears",
            "jumpsuit",
            "neck",
            "back",
            "belt",
            "gloves",
            "shoes",
            "id",
            "legs",
            "torso"
        };

        foreach (var slot in slots)
        {
            if (!_inventory.TryGetSlotEntity(args.Target, slot, out var armorItem))
                continue;

            if (!TryComp<ImprovedArmorComponent>(armorItem, out var armor))
                continue;

            if (armor.Health == 0)
                continue;

            var penRatio = pierce.Penetration / armor.Hardness;
            if (pierce.Penetration >= armor.Hardness)
            {
                // penetrated
                armor.Health = Math.Clamp((float)(armor.Health - damage * penRatio), 0, armor.MaxHP);
            }
            else
            {
                // blocked
                armor.Health = Math.Clamp((float)(armor.Health - damage * penRatio), 0, armor.MaxHP);
                args.Damage.DamageDict["Piercing"] = (damage * Math.Pow(penRatio, 3)) * armor.Resistance;
            }
            armor.Health = (float)Math.Round(armor.Health);
            Dirty(armorItem.Value, armor);
        }
    }
}
