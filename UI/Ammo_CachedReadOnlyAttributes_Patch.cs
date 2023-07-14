﻿using EFT.InventoryLogic;
using SIT.Core.Misc;
using SIT.Tarkov.Core;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SIT.Core.Other.UI
{
    internal class Ammo_CachedReadOnlyAttributes_Patch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return ReflectionHelpers.GetMethodForType(typeof(AmmoTemplate), "GetCachedReadonlyQualities");
        }

        [PatchPostfix]
        private static void Postfix(ref AmmoTemplate __instance, ref List<ItemAttribute0> __result)
        {
            if (!__result.Any((ItemAttribute0 a) => (Attributes.ENewMaximumDurabilityId)a.Id == Attributes.ENewMaximumDurabilityId.Damage))
            {
                AddNewAttributes(ref __result, __instance);
            }
        }

        public static void AddNewAttributes(ref List<ItemAttribute0> attributes, AmmoTemplate template)
        {
            if (template == null)
                return;

            // Damage
            if (template.Damage > 0)
            {
                attributes.Add(
                    new ItemAttribute0(Attributes.ENewMaximumDurabilityId.Damage)
                    {
                        Name = Attributes.ENewMaximumDurabilityId.Damage.GetName(),
                        Base = (() => template.Damage),
                        StringValue = (() => template.Damage.ToString()),
                        DisplayType = (() => EItemAttributeDisplayType.Compact)
                    }
                );
            }

            // Armor Damage
            if (template.ArmorDamage > 0)
            {
                attributes.Add(
                    new ItemAttribute0(Attributes.ENewMaximumDurabilityId.ArmorDamage)
                    {
                        Name = Attributes.ENewMaximumDurabilityId.ArmorDamage.GetName(),
                        Base = (() => template.ArmorDamage),
                        StringValue = (() => template.ArmorDamage.ToString()),
                        DisplayType = (() => EItemAttributeDisplayType.Compact)
                    }
                );
            }

            // Penetration
            if (template.PenetrationPower > 0)
            {
                attributes.Add(
                    new ItemAttribute0(Attributes.ENewMaximumDurabilityId.Penetration)
                    {
                        Name = Attributes.ENewMaximumDurabilityId.Penetration.GetName(),
                        Base = (() => template.PenetrationPower),
                        StringValue = (() => template.PenetrationPower.ToString()),
                        DisplayType = (() => EItemAttributeDisplayType.Compact)
                    }
                );
            }
        }
    }

    public static class Attributes
    {
        public static string GetName(this Attributes.ENewMaximumDurabilityId id)
        {
            switch (id)
            {
                case Attributes.ENewMaximumDurabilityId.Damage:
                    return "DAMAGE";
                case Attributes.ENewMaximumDurabilityId.ArmorDamage:
                    return "ARMOR DAMAGE";
                case Attributes.ENewMaximumDurabilityId.Penetration:
                    return "PENETRATION";
                case Attributes.ENewMaximumDurabilityId.FragmentationChance:
                    return "FRAGMENTATION CHANCE";
                case Attributes.ENewMaximumDurabilityId.RicochetChance:
                    return "RICOCHET CHANCE";
                default:
                    return id.ToString();
            }
        }

        public enum ENewMaximumDurabilityId
        {
            Damage,
            ArmorDamage,
            Penetration,
            FragmentationChance,
            RicochetChance
        }
    }
}
