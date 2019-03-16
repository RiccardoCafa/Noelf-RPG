using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts.Mobs
{
    public enum MobState
    {
        Following,
        Alert,
        Sleeping,
        Dead
    }

    class CharacterMob : Character
    {
        public MobState MyState;
        public List<CharacterPlayer> players;
        public Thread UpdateThread;

        public double FirstX;
        public double FirstY;

        public const double MinDistance = 250;
        public const double ChunckDistance = 1200;

        public CharacterMob(Canvas characterCanvas, List<CharacterPlayer> players) : base(characterCanvas)
        {
            this.players = players;
            UpdateThread = new Thread(Update);

            FirstX = xCharacVal;
            FirstY = yCharacVal;

            UpdateThread.Start();
        }

        private async void Update()
        {
            while (alive && MyState != MobState.Sleeping)
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    CharacterPlayer target = GetNearestPlayer();
                    if (target == null)
                    {
                        MyState = MobState.Alert;
                    }
                    else
                    {
                        double distInitialToMob = GetDistance(FirstX, FirstY, xCharacVal, yCharacVal);
                        double distPlayer = GetDistance(target.xCharacVal, target.yCharacVal, FirstX, FirstY);
                        
                        if (distInitialToMob > ChunckDistance)
                        {
                            moveLeft = false;
                            moveRight = false;
                            MyState = MobState.Sleeping;
                        }
                        else if (distInitialToMob >= MinDistance)
                        {
                            MyState = MobState.Alert;
                        }
                        else if(distPlayer < MinDistance && distInitialToMob < MinDistance / 2)
                        {
                            MyState = MobState.Following;
                        }
                    }

                    switch (MyState)
                    {
                        case MobState.Alert:
                            AlertState();
                            break;
                        case MobState.Following:
                            FollowingState(target);
                            break;
                    }
                });
            }
        }

        public void AlertState()
        {
            if(xCharacVal - FirstX > 0)
            {
                //Precisa ir para a esquerda
                moveRight = false;
                moveLeft = true;

                if(blocoLeftx != null)
                {
                    jumping = true;
                    Jump();
                }
            } else
            {
                moveRight = true;
                moveLeft = false;

                if(blocoRightx != null)
                {
                    jumping = true;
                    Jump();
                }
            }
        }

        public void FollowingState(Character characterFollowed)
        {
            double xcharac = GetCanvasLeft(characterFollowed.characT);
            double ycharac = GetCanvasTop(characterFollowed.characT);
            
            double xDist = xCharacVal - xcharac;
            if (xDist > characT.Width + 10)
            {
                moveRight = false;
                moveLeft = true;

                if (!freeLeft)
                {
                    jumping = true;
                    Jump();
                }
            }
            else if (xDist < -characT.Width - 10)
            {
                moveLeft = false;
                moveRight = true;

                if (!freeRight)
                {
                    jumping = true;
                    Jump();
                }
            } else
            {
                moveLeft = false;
                moveRight = false;
            }
        }

        public CharacterPlayer GetNearestPlayer()
        {
            return
                (from player in players
                 orderby GetDistance(player.xCharacVal, player.yCharacVal, xCharacVal, yCharacVal)
                select player).ElementAt(0);

        }
    }
}
