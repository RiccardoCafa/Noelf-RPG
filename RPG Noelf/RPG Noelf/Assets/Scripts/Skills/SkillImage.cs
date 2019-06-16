﻿using RPG_Noelf.Assets.Scripts.Interface;
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

        public SkillImage(double widthSize, double heightSize, SkillGenerics skill)
        {
            image = new Image()
            {
                Width = widthSize,
                Height = heightSize
            };
            image.Source = new BitmapImage(new Uri("ms-appx://" + skill.pathImage));
            this.skill = skill;
            Children.Add(image);
            PointerEntered += InterfaceManager.instance.UpdateSkillWindowText;
            PointerExited += InterfaceManager.instance.CloseSkillWindowText;
        }

        public void UpdateImage()
        {
            image.Source = Encyclopedia.skillsImages[(uint) SkillManager.instance.SkillList.IndexOf(skill)];
        }
        
    }
}
