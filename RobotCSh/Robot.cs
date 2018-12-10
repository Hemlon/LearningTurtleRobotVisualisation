using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Drawing;

namespace WindowsFormsApplication1
{
   public class Robot
    {
       enum shape { rect = 1, circle = 2};
       public double Direction; //face to angle
       public double Angle = 45 * Math.PI/180.0d;
       public SizeF Size;
       public PointF Centre = new PointF(20.0f,30.0f);
       public double Speed = 2;
       public List<Command> Command = new List<Command> { };
       public double FinalValue = 1;
       public double CurrentValue;
       private double cAngle = Math.PI/4;
       private int Turn = 1;
       public int CommandNumber = 0;
      public bool isFinished = true;
      private bool isRepeat = false;
      private int StartRepeat = 0;
      private int EndRepeat = 0;
      private int RepeatCounter = 0;
      public Color BackColor = Color.Black;
      private Random r = new Random();
       private bool isRand;
       private double RandNum = 0;
       private double RandMin = 0;
       private double RandMax = 0;
       private bool isPenDown = true;
       private shape Shape = shape.rect; //RECT;

      Stack<int> RobotStack = new Stack<int> { };
        class Line { public PointF s; public PointF e; public Color Color; public Line(PointF start, PointF end, Color color) { s = start; e = end; Color = color; } }
        List<Line> lines = new List<Line>();
        public Robot (Size size) :base()
        {
            this.Direction = 0;
            this.Size = size;
            this.Centre = new Point(0, 0);
        }
        public void clearLines ()
        {
            lines.Clear();
        }

       public void getFinalValue()
        {
                 if (this.Command[CommandNumber].Action == "rt" || this.Command[CommandNumber].Action =="lt")
               {
                  this.FinalValue = this.Command[CommandNumber].Value;
               }

                else if (this.Command[CommandNumber].Action == "fd" || this.Command[CommandNumber].Action == "bw")
                {
                    this.FinalValue = this.Command[CommandNumber].Value / this.Speed;
                }
                 else if (this.Command[CommandNumber].Action == "repeat" || this.Command[CommandNumber].Action == "x" || this.Command[CommandNumber].Action == "y" || this.Command[CommandNumber].Action == "pos" || this.Command[CommandNumber].Action == "size" || this.Command[CommandNumber].Action == "color" || this.Command[CommandNumber].Action == "shape" || this.Command[CommandNumber].Action == "rand")
                {
                    this.FinalValue = 1;
                }
                 
        }

       public void loadCommand()
       {
           double val1 = Command[CommandNumber].Value;
           double val2 = Command[CommandNumber].Value2;
           RandNum = r.NextDouble() * (RandMax - RandMin) + RandMin;

                   if (Command[CommandNumber].Action == "rt" || Command[CommandNumber].Action == "lt")
                   {
                       CurrentValue = (Direction - cAngle);

                       if (Command[CommandNumber].Value == -1.0d)
                       {
                           val1 = RandNum;
                       }
              
                       FinalValue = val1 + (Direction - cAngle);
                   }
                   else if (Command[CommandNumber].Action == "fd" || Command[CommandNumber].Action == "bw")
                   {
                       CurrentValue = 0;
                         lines.Add(new Line(Centre, Centre, BackColor));
                        if (isPenDown == false) lines[lines.Count - 1].Color = Color.White;
                 
                       Angle = Direction - cAngle;

                       if (Command[CommandNumber].Value == -1.0d)
                       {
                           val1 = RandNum;
                       }

                       FinalValue = val1 / Speed;
                   }
                   else if (Command[CommandNumber].Action == "sp")
                   {
                       Speed = Command[CommandNumber].Value;
                       EndAction();
                   }
                   else if (Command[CommandNumber].Action == "repeat")
                   {
                       if (isRepeat == false)
                       {
                      //first time
                       }
                       else if(isRepeat == true) //second repeat
                       {
                           RobotStack.Push(RepeatCounter);
                           RobotStack.Push(StartRepeat);
                           RobotStack.Push(EndRepeat);
                           RepeatCounter = 0;

                       }
                       ContActionOnce();
                   }
                   else if (Command[CommandNumber].Action == "rend")
                   {
                      if (isRepeat == true)
                      {

                          ContActionOnce();
                      }
                      else if (isRepeat == false)
                       {
                           EndAction();

                          if (RobotStack.Count > 0) 
                          {
                            EndRepeat = RobotStack.Pop();
                            StartRepeat = RobotStack.Pop();
                            RepeatCounter = RobotStack.Pop();
                            isRepeat = true;
                            ContActionOnce();
                          }
                       }
                   }
                   else if (Command[CommandNumber].Action == "x")
                   {
                       Centre.X = Convert.ToSingle(Command[CommandNumber].Value);
                       EndAction();
                   }
                   else if (Command[CommandNumber].Action == "y")
                   {
                       Centre.Y = Convert.ToSingle(Command[CommandNumber].Value);
                       EndAction();
                   }
                   else if (Command[CommandNumber].Action == "pos")
                   {

                       if (Command[CommandNumber].Value == -1.0d)
                       {
                           val1 = RandNum;
                       }

                       if (Command[CommandNumber].Value2 == -1.0d)
                       {
                           val2 = r.NextDouble() * (RandMax - RandMin) + RandMin;
                       }
                       
                       
                       Centre.X = Convert.ToSingle(val1);
                       Centre.Y = Convert.ToSingle(val2);
                       EndAction();
                   }
                   else if (Command[CommandNumber].Action == "size")
                   {
                       if (Command[CommandNumber].Value == -1.0d)
                       {
                           val1 = RandNum;
                       }

                       if (Command[CommandNumber].Value2 == -1.0d)
                       {
                           val2 = r.NextDouble() * (RandMax - RandMin) + RandMin;
                       }
                       
                       Size.Width = Convert.ToSingle(val1);
                       Size.Height = Convert.ToSingle(val2);
                       EndAction();
                   }
                   else if (Command[CommandNumber].Action == "color")
                   {

                       if (Command[CommandNumber].Value == -1.0d)
                       {
                           BackColor = Color.FromArgb(r.Next(1, 255), r.Next(1, 255), r.Next(1, 255));
                       }
                       else
                       {
                         BackColor = Color.FromArgb(Convert.ToInt32(val1));
                       }

                
                       EndAction();
                   }
                   else if (Command[CommandNumber].Action == "shape")
                   {
                       Shape = (shape)Command[CommandNumber].Value;
                       EndAction();
                   }
                   else if (Command[CommandNumber].Action == "rand")
                   {
                       RandMin = Command[CommandNumber].Value;
                       RandMax = Command[CommandNumber].Value2;
                       RandNum = r.NextDouble() * (RandMax - RandMin) + RandMin;
                       EndAction();
                   }
                   else if (Command[CommandNumber].Action == "pen")
            {
                 if (Command[CommandNumber].Value == 0)
                { isPenDown = false; }

                if (Command[CommandNumber].Value == 1)
                { isPenDown = true; }


            }




       }

       public void Update()
       {
           float increm = 0;
           float distx = Convert.ToSingle(Speed * Math.Sin(Angle * Math.PI / 180.0d));
           float disty = Convert.ToSingle(Speed * Math.Cos(Angle * Math.PI / 180.0d));

           if (isRand == true)
           {
              
           }

           if (CurrentValue < FinalValue) //handles any animations.
           {
               switch (Command[CommandNumber].Action)
               {
                   case "fd": { this.Centre.X += distx; this.Centre.Y += disty; increm = 1; CurrentValue += increm; lines[lines.Count - 1].e = Centre; break; }
                   case "rt": { increm = Turn;  CurrentValue += increm; Direction -= increm;  break; }
                   case "lt": { increm = Turn;  CurrentValue += increm; Direction += increm; break; }
                   case "bw": { this.Centre.X -= distx; this.Centre.Y -= disty; increm = 1; CurrentValue += increm; lines[lines.Count - 1].e = Centre; break; }
                 
               //    case "rand": { isRand = true; break; }//regen randnum }
                   case "repeat": {

                       if (Command[CommandNumber].Value > 0)
                       {
                            if (RepeatCounter < Command[CommandNumber].Value)
                                {
                                StartRepeat = CommandNumber;
                                // CommandNumber += 1;
                                // CurrentValue = FinalValue;
                                EndAction();
                                 isRepeat = true;
                               }
                             else if (RepeatCounter >= Command[CommandNumber].Value && isRepeat == true)
                              {
                                CommandNumber = EndRepeat;
                               // CommandNumber += 1;
                                RepeatCounter = 0;
                                isRepeat = false;
                              }
                       }

                   break;}
                     
                   case "rend": {

                       if (isRepeat == true) //at the end
                       {
                           EndRepeat = CommandNumber;
                           CommandNumber = StartRepeat; //jump to start repeat
                           RepeatCounter += 1;
                       }  
                       else
                       {
                           CurrentValue = FinalValue;
                       }

                       break; }

                       default: {

                       EndAction();
                       break;}
               }

     
           }

               else
           {
               if (Command[CommandNumber].Action != "end") //handles next command
               {
                   CommandNumber += 1;
                   loadCommand();
               }
               else //finished all commands
               {
                   isFinished = true;
               }
           }


       }

     public void EndAction()
       {
          CurrentValue =  FinalValue;
       }

       public void ContActionOnce()
       {
           CurrentValue = 0;
           FinalValue = 1;
       }

       public void Start(string CommandList)
       {
          // int tempValue;
           if (this.isFinished == true)
           {
               string[] temp = CommandList.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
               this.Command.Clear();

               for (int counter = 0; counter < temp.Length; counter += 1)
               {
                   String[] CommandLine = temp[counter].Split(' ');

                   if (CommandLine[0] == "color")
                   {
                     //  if (int.TryParse(CommandLine[1], out tempValue) != true)
                       if (CommandLine[1] == "rand")
                       {
                           CommandLine[1] = "-1";
                       }
                       else
                       {
                       CommandLine[1] = (Color.FromName(CommandLine[1]).ToArgb()).ToString();
                       }
                    

                   }
                   else if (CommandLine[0] == "shape")
                   {
                      
                          shape a = (shape)Enum.Parse(typeof(shape), CommandLine[1]);            
                           int aa   = (int)(a);
                           CommandLine[1] = aa.ToString();
                   }

          

                   if (CommandLine.Length == 2)// && int.TryParse (CommandLine[1], out tempValue) == true)
                   {
                       if (CommandLine[1] == "rand")
                       {
                           CommandLine[1] = "-1";
                       }

                        if (CommandLine[0] == "pen")
                        {
                            if (CommandLine[1] == "down")
                            {
                                CommandLine[1] = "0";
                            }
                            else
                            {
                                CommandLine[1] = "1";
                            }
                        }

                       this.Command.Add(new Command(CommandLine[0].ToString(), double.Parse(CommandLine[1]), 0f));
                    }
                   else if (CommandLine.Length == 3)
                   {
                       if (CommandLine[1] == "rand")
                       {
                           CommandLine[1] = "-1";
                       }
                       if (CommandLine[2] == "rand")
                       {
                           CommandLine[2] = "-1";
                       }

                       this.Command.Add(new Command(CommandLine[0].ToString(), double.Parse(CommandLine[1]), double.Parse(CommandLine[2])));
                   }
                   else if (CommandLine.Length == 1)
                   {
                       this.Command.Add(new Command(CommandLine[0].ToString(), 0f, 0f));
                   }
               
               }

               this.Command.Add(new Command("end", 0.0f, 0.0f));
               this.CommandNumber = 0;
               this.CurrentValue = 0;
               this.RepeatCounter = 0;
               this.isFinished = false;
               this.isRepeat = false;
               this.isRand = false;
               this.RandNum = 0;
              // this.getFinalValue();
               this.loadCommand();
           }

       }

       public void Reset(PointF centre)
       {
          // Radius = 0.5 * Math.Sqrt(Size.Width, 2)
        //  this.Centre = new Point(20, 30);
          this.CommandNumber = 0;
          this.Centre = centre;
          cAngle = 31;// (90 - (2 * Math.Atan(Size.Width/ Size.Height) * 180 / Math.PI / 2));
          Direction = cAngle;
          RepeatCounter = 0;
          this.CurrentValue = 0;
          this.FinalValue = 1;
          Angle = 0;
          this.isFinished = true;
         

       }

       public void Draw(Graphics g, Color c)
       {
            foreach (Line item in lines) g.DrawLine(new Pen(new SolidBrush(item.Color), 3), item.s, item.e);

            Brush b = new SolidBrush(this.BackColor);
           RectangleF r =  new RectangleF(0f, 0f, this.Size.Height, this.Size.Width);
           string temp;
                  if (Command.Count > 0)
           {
                      if (Command[CommandNumber].Value == -1)
                      {
                          temp = Math.Round(RandNum, 2).ToString();
                      }
                      else
                      {
                          temp = Command[CommandNumber].Value.ToString();
                      }

               //     temp =  Color.Blue.ToArgb().ToString();

     g.DrawString(Command[CommandNumber].Action + " " + temp + " " + Command[CommandNumber].Value2.ToString(),new Font("Arial", 13), b, new PointF(10,10));
           }

           g.TranslateTransform(this.Centre.X, this.Centre.Y);
           g.RotateTransform(-(float.Parse(this.Direction.ToString()) - 31.0f));
           g.TranslateTransform(-this.Size.Height / 2, -this.Size.Width / 2);

          
      
          // this.BackColor = b;
           if (Shape == shape.rect)
           {
               g.FillRectangle(b, r);
           }
           else if (Shape == shape.circle)
           {
               g.FillEllipse(b, r);
           }


      
       }

    }
}
