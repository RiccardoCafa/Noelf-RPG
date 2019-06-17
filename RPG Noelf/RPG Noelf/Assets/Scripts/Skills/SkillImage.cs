using RPG_Noelf.Assets.Scripts.General;
using RPG_Noelf.Assets.Scripts.Interface;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
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

        public SkillImage(double widthSize, double heightSize, SkillGenerics skill, bool OnBar)
        {
            this.OnBar = OnBar;
            image = new Image()
            {
                Width = widthSize,
                Height = heightSize
            };
            this.skill = skill;
            if(skill!=null) image.Source = new BitmapImage(new Uri("ms-appx://" + skill.pathImage));
            position = (uint) SkillManager.instance.SkillList.IndexOf(skill);
            Children.Add(image);
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
                image.Source = Encyclopedia.skillsImages[position];
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
