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
using RPG_Noelf.Assets.Scripts.Interface;

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
        public static List<Ent> Entidades = new List<Ent>();

        public int Str;
        public int Spd;
        public int Dex;
        public int Con;
        public int Mnd;

        public double Mp;
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
        protected bool Ranged = false;
        protected bool CanAtk = true;
        protected bool Attacking = false;
        protected SkillGenerics UsingSkill = null;
        public ObjectPooling<HitSolid> HitPool { get; } = new ObjectPooling<HitSolid>();

        protected readonly string[] parts = { "eye", "hair", "head", "body", "arms", "legs" };
        protected readonly string[] sides = { "d", "e" };

        public delegate void AttackEventHandler(object sender, EntEvent args);
        public event AttackEventHandler Attacked;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private DispatcherTimer attackTime = new DispatcherTimer();

        private double RealTime = 0;
        private double AtkT = 0;

        public Ent()
        {
            Entidades.Add(this);
            AttackTimer();
        }
        
        private void AttackTimer()
        {
            attackTime.Tick += AtkTime;
            attackTime.Interval = new TimeSpan(0, 0, 1);
            attackTime.Start();
        }
        private void DispatcherSetup()
        {
            dispatcherTimer.Tick += Timer;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void AtkTime(object sender, object e)
        {
            if(AtkT > AtkSpd)
            {
                CanAtk = true;
                AtkT = 0;
            }

            if(!CanAtk) AtkT++;
        } 
        private void Timer(object sender, object e)
        {
            int count = 0;
            SkillGenerics[] removeStatus = new SkillGenerics[status.Count];
            foreach (SkillGenerics habilite in status)//para verificar se as skills ja acabaram seus tempos de CD
            {
                if (habilite.tipobuff != SkillTypeBuff.normal)
                {
                    if (habilite.CountBuffTime <= RealTime)
                    {
                        habilite.CountBuffTime = 0;
                        habilite.RevertSkill(this);
                        removeStatus[count] = habilite;
                        count++;
                    }
                }
            }
            foreach(SkillGenerics s in removeStatus)
            {
                status.Remove(s);
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
            Hp -= (damage / (1 + Con * 0.02 + Armor))/100;
            OnAttacked();
        }

        public void Update()
        {
            if(Attacking)
            {
                Attack(null);
                CanAtk = false;
            }

            if(UsingSkill != null)
            {
                AttackSkill(UsingSkill);
                UsingSkill = null;
            }
        }

        public void Attack(SkillGenerics skill)
        {
            if (!CanAtk) return;
            DynamicSolid Dsolid = (box as DynamicSolid);
            HitSolid hit = null;
            DynamicSolid tDynamic = null;
            double hitboxSize = 50;
            double speeed = Ranged == true ? 4 : 0;
            if(HitPool.PoolSize > 0)//verificar a pool
            {
                HitPool.GetFromPool(out hit);
                hit.speed = speeed;
                hit.Yi = box.Yi;
                hit.Visibility = Windows.UI.Xaml.Visibility.Visible;
                if (Dsolid.lastHorizontalDirection == 1)
                {
                    hit.Xi = box.Xf;
                } else if (Dsolid.lastHorizontalDirection == -1)
                {
                    hit.Xi = box.Xi - hitboxSize;
                }
                hit.Start(null, null); //.alive = true;
                hit.Who = box as DynamicSolid;
            } else
            {
                if (Dsolid.lastHorizontalDirection == 1)
                {
                    hit = new HitSolid(box.Xf + 10, box.Yi + 20, hitboxSize, box.Height / 2, box as DynamicSolid, speeed);
                    hit.Start(null, null);//hit.alive = true;
                }
                else if (Dsolid.lastHorizontalDirection == -1)
                {
                    hit = new HitSolid(box.Xi - hitboxSize - 10, box.Yi + 20, hitboxSize, box.Height/2, box as DynamicSolid, speeed);
                    hit.Start(null, null);//hit.alive = true;
                }
                InterfaceManager.instance.CanvasChunck01.Children.Add(hit);
            }
                
            if (hit == null) return;

            Solid s = hit.Interaction();
            if (!(tDynamic == null || tDynamic.MyEnt == null))
            {
                if(skill != null)
                {
                    tDynamic.MyEnt.BeHit(skill.UseSkill(this, tDynamic.MyEnt));
                } else 
                tDynamic.MyEnt.BeHit(Hit(0));
                //hit.speed = 0;
            }
            AtkT = 0;
            Attacking = false;
        }
        public void AttackSkill(SkillGenerics skill)
        {
            DynamicSolid Dsolid = (box as DynamicSolid);
            HitSolid hit = null;
            Solid tDynamic = null;
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
                InterfaceManager.instance.CanvasChunck01.Children.Add(hit);
            }

            if (hit == null) return;

            tDynamic = hit.Interaction();
            if (!(tDynamic == null || tDynamic.MyEnt == null))
            {
                if(skill.tipobuff == SkillTypeBuff.debuff)
                {
                    tDynamic.MyEnt.InsereStatus(skill);
                }
                double dano = skill.UseSkill(this, tDynamic.MyEnt);
                tDynamic.MyEnt.BeHit(dano);
            }
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
