using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts.Skills
{
    class SkillImage : Canvas
    {
        public delegate void SkillImageHandler(Object sender, SkillImage e);
        public event SkillImageHandler SkillImageUpdate;

        public Image image;
        public SkillGenerics skill;
    }
}
