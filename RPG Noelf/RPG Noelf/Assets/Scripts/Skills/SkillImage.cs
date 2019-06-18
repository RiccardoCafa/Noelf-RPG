using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Interface;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    class SkillImage : Canvas
    {
        public delegate void SkillImageHandler(object sender, SkillImage e);

        public Image image;
        public SkillGenerics skill;
        public uint position;
        public bool OnBar = false;
        public Canvas CooldownImage;
        public SkillImage(double widthSize, double heightSize, SkillGenerics skill, bool OnBar)
        {
            this.OnBar = OnBar;
            image = new Image()
            {
                Width = widthSize,
                Height = heightSize
            };
            Children.Add(image);
            if(OnBar)
            {
                CooldownImage = new Canvas()
                {
                    Width = widthSize,
                    Height = heightSize
                };
                Children.Add(CooldownImage);
            }
            this.skill = skill;
            if(skill!=null) image.Source = new BitmapImage(new Uri("ms-appx://" + skill.pathImage));
            if(skill!=null)position = (uint) SkillManager.instance.SkillList.IndexOf(skill);
            PointerEntered += InterfaceManager.instance.UpdateSkillWindowText;
            PointerExited += InterfaceManager.instance.CloseSkillWindowText;
            if(!OnBar)
            {
                PointerPressed += InterfaceManager.instance.SkillTreePointerEvent;
            }
            UpdateImage();
        }

        public void UpdateImage()
        {
            if(skill != null)
            {
                position = (uint)SkillManager.instance.SkillList.IndexOf(skill);
                image.Source = Encyclopedia.skillsImages[position];
                if(OnBar)
                {
                    if (skill.locked)
                    {
                         CooldownImage.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(0, 182, 182, 182));
                    }
                    else
                    {
                        CooldownImage.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(200, 182, 182, 182));
                    }
                }
            } else
            {
                image.Source = null;
            }
            
        }

        public void UpSkill()
        {
            GameManager.instance.player._SkillManager.UpSkill(skill);
        }

        public void ChangeSkill()
        {
            if (skill.Unlocked == false) return;
            GameManager.instance.player._SkillManager.ChangeSkill(skill);
        }
        
    }
}
