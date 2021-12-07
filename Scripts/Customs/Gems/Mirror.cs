using System;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
    /// <summary>
    /// This special move allows the skilled warrior to bypass his target's physical resistance, for one shot only.
    /// The Armor Ignore shot does slightly less damage than normal.
    /// Against a heavily armored opponent, this ability is a big win, but when used against a very lightly armored foe, it might be better to use a standard strike!
    /// </summary>
    public class Mirror : WeaponAbility
    {
        public Mirror()
        {
        }

        public override int BaseMana
        {
            get
            {
                return 3;
            }
        }
        public override void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            if (!this.Validate(attacker) || !this.CheckMana(attacker, true))
                return;

            TimeSpan duration = TimeSpan.FromSeconds(30.0);
            Point3D loc = new Point3D(attacker);
            loc.X = loc.X + Utility.Random(3) - 1;
            loc.Y = loc.Y + Utility.Random(3) - 1;

            BaseCreature bc_Attacker = ((BaseCreature)attacker);

            BaseCreature c = new BaseCreature(bc_Attacker);
            // BaseCreature c = new ((BaseCreature)attacker)();
            // c.ChangeAIType(AIType.AI_Mage);
            c.FightMode = FightMode.None;

            BaseCreature.Summon(c, true, attacker, loc, 0x28, duration);
            
            c.ControlOrder = OrderType.Friend;
            c.ControlTarget = bc_Attacker.ControlMaster;
            
            c.ControlOrder = OrderType.Guard;
            c.ControlTarget = bc_Attacker.ControlMaster;
        }
    }
}