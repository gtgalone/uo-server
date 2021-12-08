using System;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using System.Reflection;

namespace Server.Gumps
{
    public class GemGump : Gump
    {
        private readonly BaseCreature m_Pet;
        private const int LabelColor = 0xFFFFFF;

        public static string FormatSkill(BaseCreature c, SkillName name)
        {
            Skill skill = c.Skills[name];

            if (skill.Base < 10.0)
                return "<div align=right color=white>---</div>";

            return String.Format("<div align=right color=white>{0:F1}</div>", skill.Value);
        }

        public GemGump(Mobile from, BaseCreature c)
            : base(50, 40)
        {
            m_Pet = c;
            int baseY = 10;
            int baseHeight = 24;

            AddImageTiled(0, 0, 210, 210, 0xA40);
            AddAlphaRegion(0, 0, 210, 210);
            
            AddHtmlLocalized(0, baseY, 210, baseHeight, 1114513, "Gems", LabelColor, false, false); // Gems

            for (int i = 0; i < c.AbilityProfile.WeaponAbilities.Length; ++i)
            {
                WeaponAbility a = c.AbilityProfile.WeaponAbilities[i];

                baseY = baseY + baseHeight;
                var loc = PetTrainingHelper.GetLocalization(a);

                if (loc[0] != null)
                {
                    AddHtmlLocalized(10, baseY, 160, baseHeight, loc[0], LabelColor, false, false);
                }
                else
                {
                    AddHtmlLocalized(10, baseY, 160, baseHeight, 1042971, a.GetType().Name, LabelColor, false, false);
                }

                AddButton(170, baseY, 4005, 4007, i + 1, GumpButtonType.Reply, 0);
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
			{
				case 0: // Closed or Cancel
					return;
                default:
                    int idxAbility = info.ButtonID - 1;
                    WeaponAbility a = m_Pet.AbilityProfile.WeaponAbilities[idxAbility];

                    Gem gem = Gem.GetGem(a);

                    if (gem != null)
                    {
                        from.AddToBackpack(gem);
                        m_Pet.RemoveWeaponAbility(a);
                    }
                    else
                    {
                        from.SendMessage("You cannot extract this gem.");
                    }

					break;
			}
        }
    }
}
