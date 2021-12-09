using System;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
    public class Mirror : WeaponAbility
    {
        public Mirror()
        {
        }

        public override int BaseMana
        {
            get
            {
                return 0;
            }
        }
        public override void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            if (!this.Validate(attacker) || !this.CheckMana(attacker, true))
                return;

            TimeSpan duration = TimeSpan.FromMinutes(3.0);
            Point3D loc = new Point3D(attacker);
            loc.X = loc.X + Utility.Random(3) - 1;
            loc.Y = loc.Y + Utility.Random(3) - 1;

            BaseCreature cAttacker = attacker as BaseCreature;
            BaseCreature c = (BaseCreature)Activator.CreateInstance(cAttacker.GetType());

            c.Name = cAttacker.Name;
            c.Hue = cAttacker.Hue;

            c.SetStr(cAttacker.RawStr);
            c.SetDex(cAttacker.RawDex);
            c.SetInt(cAttacker.RawInt);

            c.FightMode = FightMode.None;
            c.ControlSlots = 0;

            BaseCreature.Summon(c, true, attacker, loc, 0x28, duration);

            c.HitsMaxSeed = cAttacker.HitsMax;
            c.Hits = c.HitsMaxSeed;

            c.ControlOrder = OrderType.Friend;
            c.ControlTarget = cAttacker.ControlMaster;
            
            c.ControlOrder = OrderType.Guard;
            c.ControlTarget = cAttacker.ControlMaster;
        }
    }
}