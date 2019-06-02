using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using RPG_Noelf.Assets.Scripts.General;

namespace RPG_Noelf.Assets.Scripts.Ents
{
    public abstract class Ent
    {
        public Solid box;

        public int Str;
        public int Spd;
        public int Dex;
        public int Con;
        public int Mnd;

        public double Hp;
        public int HpMax;
        public double AtkSpd;
        public double Run;
        public double TimeMgcDmg;
        public double Damage;
        public double BonusChanceCrit = 1;
        public double Armor;
        public Level level;

        public double DamageBuff;
        public double ArmorBuff;
        public double ArmorEquip;
        public double AtkSpeedBuff;

        public ObjectPooling<HitSolid> HitPool { get; } = new ObjectPooling<HitSolid>();

        protected readonly string[] parts = { "eye", "hair", "head", "body", "arms", "legs" };
        protected readonly string[] sides = { "d", "e" };

        public void ApplyDerivedAttributes()
        {
            HpMax = Con * 6 + level.actuallevel * 2;
            Hp = HpMax;
            AtkSpd = 2 - (1.25 * Dex + 1.5 * Spd) / 100;
            Run = Math.Pow(Spd, 0.333) * 4 / 5 + 3;
            TimeMgcDmg = 0.45 * Mnd;
            Damage = Str;
        }

        public double Hit(double bonusDamage)//golpeia
        {
            Random random = new Random();
            double dmg100 = random.NextDouble() * 100;
            if (dmg100 < 1 / Dex * 0.05) return 0;//errou
            else if (dmg100 < Dex * BonusChanceCrit * 0.1) return bonusDamage + Damage * dmg100;//acertou
            else return bonusDamage + Damage * dmg100 * 2;//critico
        }

        public void BeHit(double damage)//tratamento do dano levado
        {
            Hp -= damage / (1 + Con * 0.02 + Armor);
        }

        public async void Attack(byte dmg)
        {
            DynamicSolid Dsolid = (box as DynamicSolid);
            HitSolid hit = null;
            DynamicSolid tDynamic = null;
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if(HitPool.PoolSize > 0)
                {
                    HitPool.GetFromPool(out hit);
                    hit.Yi = box.Yi;
                    hit.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    if (Dsolid.lastHorizontalDirection == 1)
                    {
                        hit.Xi = box.Xf;
                    } else if (Dsolid.lastHorizontalDirection == -1)
                    {
                        hit.Xi = box.Xi - 100;
                    }
                    hit.Who = box as DynamicSolid;
                } else
                {
                    if (Dsolid.lastHorizontalDirection == 1)
                    {
                        hit = new HitSolid(box.Xf, box.Yi, 100, box.Height, dmg, box as DynamicSolid);
                    }
                    else if (Dsolid.lastHorizontalDirection == -1)
                    {
                        double val = 0;
                        val = Math.Clamp(val, 0.0, box.Xi - 100.0);
                        hit = new HitSolid(box.Xi - 100, box.Yi, 100, box.Height, dmg, box as DynamicSolid);
                    }
                    Game.TheScene.Children.Add(hit);
                }
                
                if (hit == null) return;

                tDynamic = hit.Interaction();
            });
            if (!(tDynamic == null || tDynamic.MyEnt == null))
            {
                tDynamic.MyEnt.BeHit(dmg);
                Debug.WriteLine("Mob hitado hp: " + tDynamic.MyEnt.Hp);
            }
        }
    }
}
