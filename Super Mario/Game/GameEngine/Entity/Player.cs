using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using MyLibrary;
using Microsoft.Xna.Framework.Input;

namespace Super_Mario
{
    internal enum PowerStateType { Small,Big, White ,Lous }
    internal enum TransformationPower { SmallToBig,BigToSmall,BigToWhite,WhiteToBig, Lous }
    internal class Player : Entity
    {        
        private delegate void RunPlayer(GameTime gameTime,ref Vector2 Velocity, List<BoundingBox> BoundingBoxList);
        private delegate void CrouchPlayer(GameTime gameTime, ref Vector2 Velocity);
        private delegate void Update_TimeBetoineHurt(GameTime gameTime);
     
        #region Fields
      
        private RunPlayer Run;
        private CrouchPlayer Crouch;
        private Update_TimeBetoineHurt updateTimeBetoineHurt;
        private float Speed, /*JumpSpeed, */Gravity /*,fallSpeed, startY*/;//vitesse de chute        
        private Dictionary<PowerStateType, Dictionary<EntityState, Sprite>> animationsShift;
        private Dictionary<TransformationPower, AnimationTransformation> TransformationAnimation;
        internal PowerStateType powerStateType;
        private Color color;
        internal bool isFalling, isJumping ,RunToRight,IsGameOver;
        private bool JumpOnTheEnemy;
        internal short /*health,*/RunStopAnim,Time_Betoine_hurt,RunCrouchSpeed,Coin, Score;
        readonly short SpeedRun, SpeedWalk, Time_Max_Betoine_hurt, JumpingSpeed,GravityMin,GravityMax;
        private List<Bullet> bullets;
        private List<BoundingBox> BoundinBoxList;
        internal byte Life;
        #endregion

        #region Constructor

        internal Player(Point Position, List<Bullet> bullets, List<BoundingBox> boundinBoxList)
       : base(GetSpIdel(), Position.X, Position.Y)
        {
            this.Life = 3;
            this.updateTimeBetoineHurt = NotUpdateTimeBetoineHurt;
            this.updateEntity = _Update;
            this.powerStateType = PowerStateType.Small;
            this.JumpingSpeed = -850;
            this.SpeedRun = 400;
            this.SpeedWalk = 200;
            this.Run = RunEco;
            this.Crouch = this.CrouchEco;
            this.RunCrouchSpeed = 0;
            this.BoundinBoxList = boundinBoxList;
            this.RunStopAnim = 0;
            this.GravityMin = 500;
            this.Gravity = this.GravityMin;
            this.GravityMax = 800;
            this.Score = 0;
            this.Coin = 0;
            this.IsGameOver = false;
            this.Time_Max_Betoine_hurt = 2000;
            this.Time_Betoine_hurt = this.Time_Max_Betoine_hurt;
            this.Effects = SpriteEffects.None;
            this.Speed = this.SpeedWalk;
            this.bullets = bullets;
            this.isJumping = false;
            this.JumpOnTheEnemy = false;
            this.State = EntityState.Idel;
            AddAnimations();
            Game1.game.camera.InitialazeTarget(this.Hitbox);
            this.color = Color.White;
        }

        #endregion

        #region Methods
        internal void Push(Vector2 velocity)
        {
            this.Position += velocity;
        }
        internal void IsHurt(GameTime gameTime)
        {
            if (this.Time_Betoine_hurt == this.Time_Max_Betoine_hurt)
            {
                //this.health--;
                // coment
                if (this.powerStateType == PowerStateType.Small)
                {
                    this.powerStateType = PowerStateType.Lous;
                    base.Destroy();
                    this.CurentAnimation = this.animationsShift[powerStateType][this.State];
                    this.CurentAnimation.Update(gameTime);
                    //this.health = 0;
                }
                else if (this.powerStateType == PowerStateType.Big)
                    this.powerStateType = PowerStateType.Small;
                else this.powerStateType = PowerStateType.Big;

                this.updateTimeBetoineHurt = UpdateTimeBetoineHurt;
            }

        }
        //internal void NotHurt()
        //{
        //    this.color = Color.White;
        //    this.Time_Betoine_hurt = this.Time_Max_Betoine_hurt;
        //}
        
        internal void Transformation(PowerStateType NewState)
        {            
            TransformationPower Transformation;
            if (this.powerStateType == PowerStateType.Small && NewState == PowerStateType.Big)
                Transformation = TransformationPower.SmallToBig;
            else if (this.powerStateType == PowerStateType.Big && NewState == PowerStateType.Small)
                Transformation = TransformationPower.BigToSmall;
            else if (this.powerStateType == PowerStateType.Big && NewState == PowerStateType.White)
                Transformation = TransformationPower.BigToWhite;
            else if (this.powerStateType == PowerStateType.White && NewState == PowerStateType.Big)
                Transformation = TransformationPower.WhiteToBig;
            else if (NewState == PowerStateType.Lous)
                Transformation = TransformationPower.Lous;
            else if (this.powerStateType == NewState) return;
            //else if (this.powerStateType == PowerStateType.White && this.powerStateType == PowerStateType.Big) return;
            else throw new Exception("Not foud");

            this.powerStateType = NewState;
            this.CurentAnimation = this.TransformationAnimation[Transformation];
            this.updateEntity = UpdateTransformation;

        }
        void AddAnimations()
        {
            string strPath = "Images\\Entitys\\Mario\\";
            this.animationsShift = new Dictionary<PowerStateType, Dictionary<EntityState, Sprite>>();
            this.animationsShift.Add(PowerStateType.Small,AddAnimationsSmall(strPath));
            this.animationsShift.Add(PowerStateType.Big, AddAnimationsBig(strPath));
            this.animationsShift.Add(PowerStateType.White, AddAnimationsWhite(strPath));
            this.animationsShift.Add(PowerStateType.Lous, AddAnimationsLous(strPath));
            //AddAnimationsLoss
            this.TransformationAnimation = AddAnimationsTransformation(strPath);
            
            this.sprite = this.animationsShift[this.powerStateType][this.State];// this.animations[this.State];
        }
        Dictionary<EntityState, Sprite> AddAnimationsLous(string strPath)
        {           
            AnimationLoss Loss = new AnimationLoss(Game1.game.Content.Load<Texture2D>(strPath + "Mario Crouche pm 2"), 1, 100);
            //"Mario Crouche pm 5"
            //"Images\\Entitys\\Mario\\Mario Idel Pm"
            Dictionary<EntityState, Sprite> Animations = new Dictionary<EntityState, Sprite>();

            Animations.Add(EntityState.Lous, Loss);
            //Animations.Add(EntityState.Idel, PlayerLous);
            //Animations.Add(EntityState.Walk, PlayerLous);
            //Animations.Add(EntityState.Jumping, PlayerLous);
            //Animations.Add(EntityState.Falling, PlayerLous);
            //Animations.Add(EntityState.Run, PlayerLous);
            //Animations.Add(EntityState.StopRun, PlayerLous);
            //Animations.Add(EntityState.Throw, PlayerLous);
            //Animations.Add(EntityState.Crouch, PlayerLous);
            return Animations;
        }
        Dictionary<EntityState, Sprite> AddAnimationsSmall(string strPath)
        {          
            Texture2D PlayerWalk = Game1.game.Content.Load<Texture2D>(strPath + "Mario Walk pm");
            Sprite PlayerIdel = GetSpIdel();
            
            Texture2D PlayerJump = Game1.game.Content.Load<Texture2D>(strPath + "Mario Jump pm");
            Texture2D PlayerFall = Game1.game.Content.Load<Texture2D>(strPath + "Mario Fall pm");
            Texture2D PlayerRun = Game1.game.Content.Load<Texture2D>(strPath + "Mario Run pm");
            Texture2D PlayerStopRun = Game1.game.Content.Load<Texture2D>(strPath + "Mario Stop Runing pm");
            Texture2D[] PlayerCrouch =
            {
              Game1.game.Content.Load<Texture2D>(strPath + "Mario Crouche pm 1"),
              Game1.game.Content.Load<Texture2D>(strPath + "Mario Crouche pm 2"),
              Game1.game.Content.Load<Texture2D>(strPath + "Mario Crouche pm 3"),
              Game1.game.Content.Load<Texture2D>(strPath + "Mario Crouche pm 4"),
              Game1.game.Content.Load<Texture2D>(strPath + "Mario Crouche pm 5")
            };
            Dictionary<EntityState, Sprite> Animations = new Dictionary<EntityState, Sprite>();

            Animations.Add(EntityState.Idel, PlayerIdel);
            Animations.Add(EntityState.Walk, new Animation(PlayerWalk, 11,false, 80));
            Animations.Add(EntityState.Jumping, new Animation(PlayerJump, 4,true, 180));
            Animations.Add(EntityState.Falling, new Animation(PlayerFall, 1, false, 180));
            Animations.Add(EntityState.Run, new Animation(PlayerRun, 9, false, 40));
            Animations.Add(EntityState.StopRun, new Animation(PlayerStopRun, 1, false, 100));
            Animations.Add(EntityState.Crouch, new AnimationCrouching(PlayerCrouch, 45));

            return Animations;
        }
        Dictionary<EntityState, Sprite> AddAnimationsBig(string strPath)
        {
            Texture2D PlayerWalk = Game1.game.Content.Load<Texture2D>(strPath + "mario walk gm");
            Texture2D PlayerIdel = Game1.game.Content.Load<Texture2D>(strPath + "mario Idel gm");
            Texture2D PlayerJump = Game1.game.Content.Load<Texture2D>(strPath + "jump gm");
            Texture2D PlayerFall = Game1.game.Content.Load<Texture2D>(strPath + "Fall gm");
            Texture2D PlayerRun = Game1.game.Content.Load<Texture2D>(strPath + "Run Gm");
            Texture2D PlayerStopRun = Game1.game.Content.Load<Texture2D>(strPath + "Stop Runing gm");
            Texture2D[] PlayerCrouch =
           {              
              Game1.game.Content.Load<Texture2D>(strPath + "Mario Crouche pm 1"),
              Game1.game.Content.Load<Texture2D>(strPath + "Mario Crouche pm 2"),              
              Game1.game.Content.Load<Texture2D>(strPath + "Crouche gm")
            };
            Dictionary<EntityState, Sprite> Animations = new Dictionary<EntityState, Sprite>();

            Animations.Add(EntityState.Idel, new Animation(PlayerIdel, 4, 100, 1000));
            Animations.Add(EntityState.Walk, new Animation(PlayerWalk, 10, false, 80));
            Animations.Add(EntityState.Jumping, new Animation(PlayerJump, 5,true, 180));
            Animations.Add(EntityState.Falling, new Animation(PlayerFall, 1, false, 180));
            Animations.Add(EntityState.Run, new Animation(PlayerRun, 8, false, 40));
            Animations.Add(EntityState.StopRun, new Animation(PlayerStopRun, 1, false, 100));
            Animations.Add(EntityState.Crouch, new AnimationCrouching(PlayerCrouch, 75));
            return Animations;
        }
        Dictionary<EntityState, Sprite> AddAnimationsWhite(string strPath)
        {
            Texture2D PlayerWalk = Game1.game.Content.Load<Texture2D>(strPath + "white walck");
            Texture2D PlayerIdel = Game1.game.Content.Load<Texture2D>(strPath + "White Idel");
            Texture2D PlayerJump = Game1.game.Content.Load<Texture2D>(strPath + "White Jump");
            Texture2D PlayerFall = Game1.game.Content.Load<Texture2D>(strPath + "White Fall");
            Texture2D PlayerRun = Game1.game.Content.Load<Texture2D>(strPath + "White Run");
            Texture2D PlayerStopRun = Game1.game.Content.Load<Texture2D>(strPath + "White Stop Runing");
            Texture2D PlayerFier = Game1.game.Content.Load<Texture2D>(strPath + "White Fier");
            Texture2D[] PlayerCrouch =
            {
             Game1.game.Content.Load<Texture2D>(strPath + "White Crouching 1"),
             Game1.game.Content.Load<Texture2D>(strPath + "White Crouching 2"),
             Game1.game.Content.Load<Texture2D>(strPath + "White Crouching 3"),
             Game1.game.Content.Load<Texture2D>(strPath + "White Crouching 4"),
             Game1.game.Content.Load<Texture2D>(strPath + "White Crouching 5")
            };
            Dictionary<EntityState, Sprite> Animations = new Dictionary<EntityState, Sprite>();

            Animations.Add(EntityState.Idel, new Animation(PlayerIdel, 3, 100, 1000));
            Animations.Add(EntityState.Walk, new Animation(PlayerWalk, 12, false, 80));
            Animations.Add(EntityState.Jumping, new Animation(PlayerJump, 3, true, 180));
            Animations.Add(EntityState.Falling, new Animation(PlayerFall, 1, false, 180));
            Animations.Add(EntityState.Run, new Animation(PlayerRun, 18, false, 40));
            Animations.Add(EntityState.StopRun, new Animation(PlayerStopRun, 1, false, 100));
            Animations.Add(EntityState.Throw, new Animation(PlayerFier, 6, true, 80));
            Animations.Add(EntityState.Crouch, new AnimationCrouching(PlayerCrouch, 45));
            return Animations;
        }
        Dictionary<TransformationPower, AnimationTransformation> AddAnimationsTransformation(string strPath)
        {
            Dictionary<TransformationPower, AnimationTransformation> List = new Dictionary<TransformationPower, AnimationTransformation>();
            //this.TransformationAnimation = new Dictionary<PowerStateType[], Animation>();
            //PowerStateType[] Curent = { PowerStateType.Curent, PowerStateType.Curent };
           
            //PowerStateType[] FromBigToSmall  = { PowerStateType.Big, PowerStateType.Small };
            //PowerStateType[] FromBigToWhite  = { PowerStateType.Big, PowerStateType.White };
            //PowerStateType[] FromWhiteToBig  = { PowerStateType.White, PowerStateType.Big };
            //PowerStateType[] FromSmallToLous = { PowerStateType.Small, PowerStateType.Lous };

            Texture2D Small = Game1.game.Content.Load<Texture2D>(strPath + "Mario Idel Pm");
            Texture2D Big = Game1.game.Content.Load<Texture2D>(strPath + "mario Idel gm");
            Texture2D White = Game1.game.Content.Load<Texture2D>(strPath + "White Idel");

            AnimationTransformation animationSmallToBig = new AnimationTransformation(Small, 3,1,0,0,Big,4,1,0,0);
            AnimationTransformation animationBigToSmall = new AnimationTransformation( Big, 4, 1, 0, 0, Small, 3, 1, 0, 0);
            AnimationTransformation animationBigToWhite = new AnimationTransformation(Big, 4, 1, 0, 0,White,3,1,0,0);
            AnimationTransformation animationWhiteToBig = new AnimationTransformation( White, 3, 1, 0, 0, Big, 4, 1, 0, 0);
            //List.Add(Curent, animationSmallToBig);
            List.Add(TransformationPower.SmallToBig, animationSmallToBig);
            List.Add(TransformationPower.BigToWhite, animationBigToWhite);
            return List;
        }
        Rectangle IsExpectedHitbox(Vector2 Velocity)
        {
            return new Rectangle(
                this.Hitbox.X + ((int)Velocity.X),
                this.Hitbox.Y + ((int)Velocity.Y),
                this.Hitbox.Width, this.Hitbox.Height);
        }
        private bool CheckColisionFromTheRight(ref Vector2 Velocity, List<BoundingBox> BoundingBoxList)
        {
            foreach (BoundingBox Box in BoundingBoxList)
            {
                if (Box.IsTouchingLeft(this.IsExpectedHitbox(Velocity)))
                {
                    Velocity.X = 0;
                    this.Position = Box.IsAttachedToTheRight(this.Position, this.Hitbox);
                    return true;
                }
            }
            return false;
        }
        private bool CheckColisionFromTheLeft(ref Vector2 Velocity, List<BoundingBox> BoundingBoxList)
        {
            foreach (BoundingBox Box in BoundingBoxList)
            {
                if (Box.IsTouchingRight(this.IsExpectedHitbox(Velocity)))
                {
                    //Stop = true;
                    Velocity.X = 0;
                    this.Position = Box.IsAttachedToTheLeft(this.Position, this.Hitbox);
                    return true;
                }
            }
            return false;
        }
        private void Mouve(GameTime gameTime, ref Vector2 Velocity, List<BoundingBox> BoundingBoxList)
        {
            if (Input.IskeyDown(Settings.KeyRight))
            {
                //this.walkSpeed = SpeedWalk;
                Velocity.X = this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (!CheckColisionFromTheRight(ref Velocity, BoundingBoxList))
                {
                    this.State = EntityState.Walk;
                    this.Effects = SpriteEffects.None;
                }

            }
            else if (Input.IskeyDown(Settings.KeyLeft)) //arreter ici sur le run crouche walck speed
            {
                //this.walkSpeed = SpeedWalk;
                Velocity.X = -(this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                if (!CheckColisionFromTheLeft(ref Velocity, BoundingBoxList))
                {
                    this.State = EntityState.Walk;
                    this.Effects = SpriteEffects.FlipHorizontally;
                }
            }
        }
        void RunFull(GameTime gameTime, ref Vector2 Velocity, List<BoundingBox> BoundingBoxList)
        {
            if (Input.IskeyDown(Settings.KeySpeed) && (Input.IskeyDown(Settings.KeyRight) || Input.IskeyDown(Settings.KeyLeft)))
            {
                this.Speed = this.SpeedRun;
                if (this.RunStopAnim < 500) this.RunStopAnim += 10;
                this.State = EntityState.Run;
                this.RunToRight = Input.IskeyDown(Settings.KeyRight);
            }
            else if (this.RunStopAnim > 0)
            {
                this.Speed = this.SpeedWalk;
                this.RunStopAnim -= (short)(gameTime.ElapsedGameTime.Milliseconds);
                this.State = EntityState.StopRun;

                if (this.RunToRight)
                {
                    if (!Input.IskeyDown(Settings.KeyRight))
                    {
                        if (!CheckColisionFromTheRight(ref Velocity, BoundingBoxList))
                            Velocity.X += this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (Input.IskeyDown(Settings.KeyLeft))
                        {
                            Velocity.X += this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            this.Effects = SpriteEffects.None;
                        }
                    }
                    else this.RunStopAnim = 0;
                }
                else
                {
                    if (!Input.IskeyDown(Settings.KeyLeft))
                    {
                        if (!CheckColisionFromTheLeft(ref Velocity, BoundingBoxList))
                            Velocity.X -= (this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (Input.IskeyDown(Settings.KeyRight))
                        {
                            Velocity.X -= (this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                            this.Effects = SpriteEffects.FlipHorizontally;
                        }
                    }
                    else this.RunStopAnim = 0;
                }

                if (this.RunStopAnim <= 0)
                {
                    this.RunStopAnim = 0;
                    this.State = EntityState.Idel;
                    this.Run = RunEco;
                }

            }
            else this.Run = RunEco;

        }
        void RunEco(GameTime gameTime, ref Vector2 Velocity, List<BoundingBox> BoundingBoxList)
        {
            if (Input.IskeyDown(Settings.KeySpeed))
            {
                this.Run = RunFull;
            }
        }
        private void CrouchFull(GameTime gameTime, ref Vector2 Velocity)
        {
            if (Input.IskeyDown(Settings.KeyDown)) 
            {
                this.State = EntityState.Crouch;
                if (Input.IskeyDown(Settings.KeySpeed))
                {
                    this.RunStopAnim = 0;
                    //this.RunCrouchSpeed -= (short)(gameTime.ElapsedGameTime.Milliseconds);

                    if (RunCrouchSpeed > 0) this.RunCrouchSpeed -= 10;
                    else this.RunCrouchSpeed = 0;
                    this.Speed = this.RunCrouchSpeed;   //= this.SpeedWalk;

                    //else Velocity.X = 0;
                    return;
                }

                Velocity.X = 0;
            }
            else
            {
                this.RunCrouchSpeed = 500;
                this.Speed = SpeedWalk;
                this.Crouch = CrouchEco;
                ((AnimationCrouching)this.animationsShift[this.powerStateType][EntityState.Crouch]).InitialazeAnimation();
                //this.animations[EntityState.Crouch]).InitialazeAnimation();                    
            }
        }
        void CrouchEco(GameTime gameTime, ref Vector2 Velocity)
        {
            if (Input.IskeyDown(Settings.KeyDown))
            {
                this.Crouch = this.CrouchFull;
            }
        }

        void Falling(GameTime gameTime, ref Vector2 Velocity, List<BoundingBox> BoundingBoxList)
        {
            if (!isJumping) isFalling = true;

            if (this.isFalling)
            {
                Velocity.Y += this.Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                //Velocity.Y += this.JumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                foreach (BoundingBox box in BoundingBoxList)
                {
                    if (box.IsTouchingTop(this.IsExpectedHitbox(Velocity)))
                    {
                        Velocity.Y = 0;
                        this.Position = box.IsAttachedToTheBottom(this.Position, this.Hitbox);
                        this.isFalling = false;
                        return;
                    }
                }
                //this.JumpSpeed += 1300 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (this.Gravity < this.GravityMax) this.Gravity += (short)(500 * gameTime.ElapsedGameTime.TotalSeconds);
               
                this.State = EntityState.Falling;
            }
            else this.Gravity = this.GravityMin;                    
            
        }
        void Jump(GameTime gameTime, ref Vector2 Velocity, List<BoundingBox> boindingBoxList)
        {
            if (isJumping)
            {
                if (!Input.IskeyDown(Settings.KeyUP) && this.JumpSpeed < 0 && !this.JumpOnTheEnemy) this.JumpSpeed = 0;
                //Mouve(gameTime, ref Velocity, boindingBoxList);
                if (this.JumpSpeed >= 0)
                {
                    this.isJumping = false;
                    this.JumpOnTheEnemy = false;
                    ((Animation)this.animationsShift[powerStateType][EntityState.Jumping]).initialazeForJumping();
                    return;
                }
                Velocity.Y += this.JumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                foreach (BoundingBox box in boindingBoxList)
                {
                    if (box.IsTouchingBottom(this.IsExpectedHitbox(Velocity),powerStateType))
                    {
                        this.Position = box.IsAttachedToTheTop(this.Position, this.Hitbox);
                        this.isJumping = false;
                        this.JumpOnTheEnemy=false;
                        ((Animation) this.animationsShift[powerStateType][EntityState.Jumping]).initialazeForJumping();
                        return;
                        //break;
                    }
                }

                this.State = EntityState.Jumping;
                //this.CurentAnimation = this.animations[this.State];
                this.JumpSpeed += 1300 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (Input.IskeyDown(Settings.KeyUP) && !this.isFalling)
            {
                this.isJumping = true;
                this.JumpSpeed = this.JumpingSpeed;//Give it upward thrust
            }

        }
        /// <summary>
        /// 0 Equals Defult Value
        /// </summary>
        /// <param name="jumpSpeed"></param>
        internal void ToJump(/*float jumpSpeed=0,bool JumpOnTheEnem=true*/)
        {
            this.isJumping = true;
            this.JumpOnTheEnemy = true;
            //if (jumpSpeed == 0)
                this.JumpSpeed = this.JumpingSpeed;//Give it upward thrust
            //else this.JumpSpeed = jumpSpeed;
            this.isFalling = false;
        }
        void Throw()
        {
            if (Input.IsKey(Settings.bullet) && this.powerStateType == PowerStateType.White && this.State != EntityState.Throw && this.bullets.Count < 2)
            {
                float speed;
                if (this.Effects == SpriteEffects.None) speed = 7;
                else speed = -7;

                this.bullets.Add(new Bullet(speed, this.Hitbox));
                this.State = EntityState.Throw;
                this.updateEntity = UpdateThrowFire;
            }
        }
        #endregion

        #region Static Methods
        static Animation GetSpIdel()
        {
            Texture2D PlayerIdel = Game1.game.Content.Load<Texture2D>("Images\\Entitys\\Mario\\Mario Idel Pm");
            return new Animation(PlayerIdel,3,100,1000);           
        }
        #endregion

        #region Propertis  



        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            this.updateEntity(gameTime);
            this.updateTimeBetoineHurt(gameTime);
        }
        protected override void UpdateLoss(GameTime gameTime)
        {
            ((AnimationLoss)this.CurentAnimation).Update(gameTime);
            if (((AnimationLoss)this.CurentAnimation).TheEnd())
            {
                this.IsGameOver = true;
            }
                

            this.Position = new Vector2(this.Position.X, this.Position.Y + (this.JumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds));
            this.JumpSpeed += 1300 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void UpdateTimeBetoineHurt(GameTime gameTime)
        {
            this.Time_Betoine_hurt -= ((short)gameTime.ElapsedGameTime.Milliseconds);
            if (this.Time_Betoine_hurt % 2 == 0)
            {
                if (this.color == Color.White) this.color = Color.Black * 0f;
                else this.color = Color.White;
            }
            if (this.Time_Betoine_hurt <= 100)
            {
                this.color = Color.White;
                this.Time_Betoine_hurt = this.Time_Max_Betoine_hurt;
                this.updateTimeBetoineHurt = NotUpdateTimeBetoineHurt;
            }
        }
        private void NotUpdateTimeBetoineHurt(GameTime gameTime)
        {

        }
        private void UpdateThrowFire(GameTime gameTime)
        {            
            this.CurentAnimation.Update(gameTime);
            if (((Animation)this.CurentAnimation).TheEnd()) this.updateEntity = _Update;
        }
        private void UpdateTransformation(GameTime gameTime)
        {
            this.CurentAnimation.Update(gameTime);
            
            if (((AnimationTransformation)this.CurentAnimation).TheEnd()) this.updateEntity = _Update;
        }
        private void _Update(GameTime gameTime)
        {
            Vector2 Velocity = Vector2.Zero;
            this.State = EntityState.Idel;

            Mouve(gameTime, ref Velocity, this.BoundinBoxList);
            Run(gameTime, ref Velocity, this.BoundinBoxList);
            Falling(gameTime, ref Velocity, this.BoundinBoxList);
            Jump(gameTime, ref Velocity, this.BoundinBoxList);
            Throw();
            Crouch(gameTime, ref Velocity);

            this.Position += Velocity;
           
            this.CurentAnimation = this.animationsShift[powerStateType][this.State];

                       //this.animations[this.State];

            this.CurentAnimation.Update(gameTime);
        }
        public override void Draw(SpriteBatch SpriteBatch, GameTime gameTime)
        {
            this.CurentAnimation.Draw(SpriteBatch, this.Position, this.color, this.Effects);
        }
        #endregion

    }
}

