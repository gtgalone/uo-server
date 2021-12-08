using System;
using System.Collections.Generic;

namespace Server.Items
{
    public abstract class Gem : Item
    {
        public Gem(string name, int hue)
            : base(0x2DB2)
        {
            this.Name = name;
            this.Hue = hue;
            this.Stackable = true;
            this.Weight = 1.0;
        }

        public Gem(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!this.Movable)
                return;

            if (!from.BeginAction(this.GetType()))
            {
                from.SendLocalizedMessage(500119); // You must wait to perform another action.
                return;
            }

            Timer.DelayCall(TimeSpan.FromMilliseconds(500), () => from.EndAction(this.GetType()));

            if (from.InRange(this.GetWorldLocation(), 1))
            {
            }
            else
            {
                from.SendLocalizedMessage(502138); // That is too far away for you to use
            }
        }

        public static Gem GetGem(WeaponAbility a)
        {
            Gem gem = null;
            
            switch (a.GetType().Name)
            {
                case "Mirror":
                    gem = new MirrorGem();
                    break;
                default:
                    break;
            }

            return gem;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch ( version )
            {
                case 1:
                case 0:
                    {
                        break;
                    }
            }
        }
    }
}
