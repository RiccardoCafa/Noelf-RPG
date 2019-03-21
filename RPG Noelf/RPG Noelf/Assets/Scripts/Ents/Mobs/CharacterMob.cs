using System;
using System.Collections.Generic;
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
                    //ShowBlocks();

                    CharacterPlayer target = GetNearestPlayer();
                    if (target == null)
                    {
                        MyState = MobState.Alert;
                    }
                    else
                    {
                        distInitialToMob = GetDistance(FirstX, FirstY, xCharacVal, yCharacVal);
                        distPlayer = GetDistance(target.xCharacVal + MainCamera.instance.CameraXOffSet *-1, target.yCharacVal + MainCamera.instance.CameraYOffSet * -1,
                                                FirstX, FirstY);
                        
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

        public void ShowCoordinates()
        {
            TextBlock text = MainPage.instance.mobStatus;

            text.Text = "Initial Position: x " + FirstX + " y " + FirstY + "\n";
            text.Text += "My position: x " + xCharacVal + " y " + yCharacVal + "\n";
            text.Text += "Distance Mob to Player " + distPlayer + "\n";
            text.Text += "Distance Mob to Init " + distInitialToMob + "\n";
        }

        public void ShowBlocks()
        {
            TextBlock text = MainPage.instance.mobStatus;

            text.Text = "";
            if(blocoLeftx != null) { text.Text = "Bloco Leftx existe"; } else { text.Text = "Bloco Leftx não existe"; }
            text.Text += "\n";
            if(blocoRightx != null) { text.Text += "Bloco Rightx existe"; } else { text.Text += "Bloco Rightx não existe"; }
            text.Text += "\n";
            if(blocoBottomx != null) { text.Text += "Bloco Bottomx existe"; } else { text.Text += "Bloco Bottomx não existe"; }
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
            //Precisa ir para a esquerda
            if (!freeLeft || !freeRight)
            {
                Jump();
            }
            if(!freeRight)
            {
                moveRight = true;
                moveLeft = false;
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
            if (!freeLeft)
            {
                moveRight = false;
                moveLeft = true;
            }
            else
            {
                moveLeft = false;
                moveRight = true;
            }
        }

        public void FollowingState(Character characterFollowed)
        {
            double xcharac;
            double ycharac;
            if(characterFollowed is CharacterPlayer)
            {
                xcharac = GetCanvasLeft(characterFollowed.characT) + MainCamera.instance.CameraXOffSet *-1;
                ycharac = GetCanvasTop(characterFollowed.characT) + MainCamera.instance.CameraYOffSet * -1;
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
