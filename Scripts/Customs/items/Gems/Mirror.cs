using System;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
    public class MirrorGem : Gem
    {
        [Constructable]
        public MirrorGem()
            : base("Mirror Gem", 1283)
        {
        }

        public MirrorGem(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.RevealingAction();

            from.SendMessage("Which pet do you want to apply?");

            from.Target = new InternalTarget(this);
        }
        
        private class InternalTarget : Target
        {
            private readonly MirrorGem m_Item;
            public InternalTarget(MirrorGem item)
                : base(2, false, TargetFlags.None)
            {
                this.m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted)
                {
                    return;
                }

                if (targeted is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)targeted;
                    if (bc.ControlMaster == from)
                    {
                        WeaponAbility wa = WeaponAbility.Mirror;
                        if (bc.HasAbility(wa))
                        {
                            from.SendMessage("Already!");
                        }
                        else
                        {
                            bc.FixedEffect(0x9F89, 1, 360);
                            bc.PlaySound(0x2A1);

                            bc.SetWeaponAbility(wa);
                            m_Item.Consume();
                        }
                    }
                    else
                    {
                        from.SendMessage("Not yours!");
                    }
                }
                else
                {
                    from.SendMessage("Nothing!");
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch( version )
            {
                case 0:
                    {
                        break;
                    }
            }
        }
    }
}
