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
        public delegate void SkillImageHandler(Object sender, SkillImage e);
        public event SkillImageHandler SkillImageUpdate;

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
            PointerEntered += Game.instance.UpdateSkillWindowText;
            PointerExited += Game.instance.CloseSkillWindowText;
        }
        
    }
}
