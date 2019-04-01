using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.Ents.Mobs;
using RPG_Noelf.Assets.Scripts.Interface;
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
        Stuck,
        Dead
    }

    public class CharacterMob : Character
    {
        public MobState MyState;
        public List<CharacterPlayer> players;
        public Thread UpdateThread;

        public double FirstX;
        public double FirstY;

        public const double MinDistance = 150;
        public const double MaxDistance = 700;
        public const double ChunckDistance = 1200;

        public Mob Mob;

        private double distInitialToMob;
        private double distPlayer;
        private string stats;

        public CharacterMob(Canvas characterCanvas, List<CharacterPlayer> players, Mob mob) : base(characterCanvas)
        {
            Mob = mob;
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
                        distInitialToMob = GetDistance(FirstX, FirstY, xCharacVal, yCharacVal);
                        distPlayer = GetDistance(target.xCharacVal + MainCamera.instance.CameraXOffSet *-1, target.yCharacVal + MainCamera.instance.CameraYOffSet * -1,
                                                xCharacVal + characT.Width/2, FirstY + characT.Height/2);
                        
                        if (distInitialToMob >= MaxDistance)
                        {
                            MyState = MobState.Alert;
                        }
                        else if(distPlayer < MinDistance && distInitialToMob < MinDistance / 2)
                        {
                            MyState = MobState.Following;
                        }
                        else if (distInitialToMob > ChunckDistance)
                        {
                            moveLeft = false;
                            moveRight = false;
                            MyState = MobState.Sleeping;
                        }
                    }

                    switch (MyState)
                    {
                        case MobState.Alert:
                            stats = "Alerta";
                            AlertState();
                            break;
                        case MobState.Following:
                            stats = "Seguindo";
                            FollowingState(target);
                            break;
                    }
                });
            }
        }

        public void AlertState()
        {
            if(xCharacVal - FirstX > 5)
            {
                MoveToLeft();
            } else if(xCharacVal - FirstX < -5)
            {
                MoveToRight();
            } else
            {
                Stop();
            }
        }

        private void MoveToLeft()
        {
            if (!freeLeft || !freeRight)
            {
                Jump();
            } else
            {
                moveRight = false;
                moveLeft = true;
            }
        }

        private void Stop()
        {
            moveRight = false;
            moveLeft = false;
        }

        private void MoveToRight()
        {
            if (!freeRight || !freeLeft)
            {
                Jump();
            }
            else
            {
                moveRight = true;
                moveLeft = false;
            }
        }

        public void FollowingState(Character characterFollowed)
        {
            double xcharac;
            double ycharac;

            if (characterFollowed is CharacterPlayer)
            {
                xcharac = GetCanvasLeft(characterFollowed.characT) + MainCamera.instance.CameraXOffSet *-1;
                ycharac = GetCanvasTop(characterFollowed.characT) + MainCamera.instance.CameraYOffSet *-1;
            } else
            {
                xcharac = GetCanvasLeft(characterFollowed.characT);
                ycharac = GetCanvasTop(characterFollowed.characT);
            }

            double xDist = xCharacVal - xcharac;
            if (xDist > characT.Width + 10)
            {
                MoveToLeft();
            }
            else if (xDist < -characT.Width - 10)
            {
                MoveToRight();
            } else
            {
                Stop();
            }
        }

        public CharacterPlayer GetNearestPlayer()
        {
            return
                (from player in players
                 orderby GetDistance(player.xCharacVal + MainCamera.instance.CameraXOffSet, player.yCharacVal + MainCamera.instance.CameraYOffSet,
                                    xCharacVal, yCharacVal)
                select player).ElementAt(0);

        }
    }
}
