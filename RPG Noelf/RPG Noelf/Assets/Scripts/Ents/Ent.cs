using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using RPG_Noelf.Assets.Scripts.General;
using System.Collections.Generic;
using RPG_Noelf.Assets.Scripts.Skills;
using Windows.UI.Xaml;

namespace RPG_Noelf.Assets.Scripts.Ents
{
    public class EntEvent : EventArgs
    {
        public DynamicSolid EntBox;

        public EntEvent(DynamicSolid boxs)
        {
            EntBox = boxs;
        }
    }

    public abstract class Ent
    {
        public DynamicSolid box;

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

        private List<SkillGenerics> status = new List<SkillGenerics>();
        private bool ranged = false;
        public ObjectPooling<HitSolid> HitPool { get; } = new ObjectPooling<HitSolid>();

        protected readonly string[] parts = { "eye", "hair", "head", "body", "arms", "legs" };
        protected readonly string[] sides = { "d", "e" };

        public delegate void AttackEventHandler(object sender, EntEvent args);
        public event AttackEventHandler Attacked;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private double RealTime = 0;
        
        private void DispatcherSetup()
        {
            dispatcherTimer.Tick += Timer;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void Timer(object sender, object e)
        {

            foreach (SkillGenerics habilite in status)//para verificar se as skills ja acabaram seus tempos de CD
            {
                if (habilite.CountTime >= RealTime)
                {
                    habilite.CountTime = 0;
                    habilite.RevertSkill(this);
                    status.Remove(habilite);
                }
                if (habilite.tipobuff != SkillTypeBuff.normal)
                {
                    if (habilite.CountBuffTime >= RealTime)
                    {
                        habilite.CountBuffTime = 0;
                    }
                }
            }
            if(status.Count == 0)
            {
                dispatcherTimer.Stop();
            }
            RealTime++;
        }
        public void InsereStatus(SkillGenerics stats)
        {
            bool a = status.Count == 0;
            status.Add(stats);
            stats.CountTime = RealTime + stats.cooldown;
            stats.CountBuffTime = RealTime + stats.timer;
            if(a) DispatcherSetup();
        }

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

        public async void Attack()
        {
            DynamicSolid Dsolid = (box as DynamicSolid);
            HitSolid hit = null;
            DynamicSolid tDynamic = null;
            double hitboxSize = 50;
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if(HitPool.PoolSize > 0)
                {
                    HitPool.GetFromPool(out hit);
                    hit.speed = 0;
                    hit.Yi = box.Yi;
                    hit.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    if (Dsolid.lastHorizontalDirection == 1)
                    {
                        hit.Xi = box.Xf;
                    } else if (Dsolid.lastHorizontalDirection == -1)
                    {
                        hit.Xi = box.Xi - hitboxSize;
                    }
                    hit.Who = box as DynamicSolid;
                } else
                {
                    if (Dsolid.lastHorizontalDirection == 1)
                    {
                        hit = new HitSolid(box.Xf, box.Yi + 20, hitboxSize, box.Height/2, box as DynamicSolid,0);
                    }
                    else if (Dsolid.lastHorizontalDirection == -1)
                    {
                        hit = new HitSolid(box.Xi - hitboxSize, box.Yi + 20, hitboxSize, box.Height/2,box as DynamicSolid,0);
                    }
                    Game.TheScene.Children.Add(hit);
                }
                
                if (hit == null) return;

                tDynamic = hit.Interaction();
                if (!(tDynamic == null || tDynamic.MyEnt == null))
                {
                    tDynamic.MyEnt.BeHit(Hit(0));
                    tDynamic.MyEnt.OnAttacked();
                }
            });
        }
        public async void AttackSkill(SkillGenerics skill)
        {
            DynamicSolid Dsolid = (box as DynamicSolid);
            HitSolid hit = null;
            DynamicSolid tDynamic = null;
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (HitPool.PoolSize > 0)
                {
                    HitPool.GetFromPool(out hit);
                    skill.UpdateThrow(hit, this);
                    hit.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    if(skill.Active)
                    {
                        hit = skill.Throw(this);
                    }
                    Game.TheScene.Children.Add(hit);
                }

                if (hit == null) return;

                tDynamic = hit.Interaction();
                if (!(tDynamic == null || tDynamic.MyEnt == null))
                {
                    if(skill.tipobuff == SkillTypeBuff.debuff)
                    {
                        tDynamic.MyEnt.InsereStatus(skill);
                    }
                    tDynamic.MyEnt.BeHit(skill.UseSkill(this,tDynamic.MyEnt));
                    tDynamic.MyEnt.OnAttacked();
                }
            });
        }
        public abstract void Die();

        public void OnAttacked()
        {
            Attacked?.Invoke(this, new EntEvent(box));
            if(Hp <= 0)
            {
                Die();
            }
        }
    }
}
