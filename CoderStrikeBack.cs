using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;


//distance between d=sqrt((x2-x1)² + (y2-y1)²)
/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
 class Game
 {
       public List<CheckPoint> completedCheckpoints = new List<CheckPoint>();

       

       public double CalculateDistanceBetweenCheckpoint(CheckPoint first, CheckPoint second){

           return Math.Sqrt(Math.Pow(second.x - first.x, 2) + Math.Pow(second.y - first.y, 2));
       }

       

 }

 class CheckPoint
 {
    public int x {get; set;}
    public int y {get; set;}

    

   

    public override bool Equals(object obj)
        {
            return obj is CheckPoint checkPoint &&
                   base.Equals(obj) &&
                   x == checkPoint.x &&
                   y == checkPoint.y;
        }
    public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), x, y);
        }
    public override string ToString()
        {
            return base.ToString() + String.Format("CheckpX: {0} CheckpY: {1}", x, y);
        }

 }

class Player
{
        private int x;
        private int y;
        protected int nextCheckpointX;
        protected int nextCheckpointY;
        private int nextCheckpointDist;
        private int nextCheckpointAngle;
        private int opponentX;
        private int opponentY;
        
        public int lap = 1;
        private bool checkBoost = true;
        private int force;
        private string thrust;
        private double rad;

        public void UpdateInfo(){
            
        }

        public void Move()
        {
            Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " " + thrust);
        }

        public void Force(){
            
            force = 100;

            rad = nextCheckpointAngle * Math.PI / 180;
            
            if(nextCheckpointAngle < 90)
            {
                double perfectForce = nextCheckpointDist * Math.Cos(rad) * 0.15;
                if(perfectForce > 100)
                {
                    force = 100;
                }
                else if (perfectForce < 0)
                {
                    force = 0;
                }
                else
                {
                    force = (int)perfectForce;
                }
            }
            else
            {
                force = 0;
            }
                thrust = Convert.ToString(force);

                //BOOST using
                //if(lap > 1 && distanceBetween == distanceList.Max())
//                     {
//                         thrust = "BOOST";
//                         checkBoost = false;
//                      }
          

                if(checkBoost && nextCheckpointDist > 5000 && nextCheckpointAngle == 0)
                {
                    thrust = "BOOST";
                    checkBoost = false;
                }

                
            
            
        }
        public void Log()
                {
                    Console.Error.WriteLine("Debug messages: x = {0}, y = {1}", x, y);
                    Console.Error.WriteLine("Debug messages: angle = {0}", nextCheckpointAngle);
                    Console.Error.WriteLine("Debug messages: speed = {0}", thrust);
                    Console.Error.WriteLine("Debug messages: boostCheck = {0}", checkBoost);
                    Console.Error.WriteLine("Debug messages: radians = {0}", rad);
                    Console.Error.WriteLine("Next checkPoint in: {0}", nextCheckpointDist);
                    Console.Error.WriteLine("Lap = {0}", lap);
                    
                }

    static void Main(string[] args)
    {
        Game game = new Game();
        string[] inputs;
        Player player = new Player();

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            Console.Error.WriteLine("inputs[0] = {0}" , inputs[0]);
            Console.Error.WriteLine(inputs[0]);
            player.x = int.Parse(inputs[0]);
            player.y = int.Parse(inputs[1]);
            CheckPoint startCheckPoint = new CheckPoint(){
            x = player.x,
            y = player.y
            };
            if(game.completedCheckpoints.Any(x => x.x == startCheckPoint.x && x.y == startCheckPoint.y)){
            
            game.completedCheckpoints.Add(startCheckPoint);
            }

            player.nextCheckpointX = int.Parse(inputs[2]); // x position of the next check point
            player.nextCheckpointY = int.Parse(inputs[3]); // y position of the next check point
            player.nextCheckpointDist = int.Parse(inputs[4]); // distance to the next checkpoint
            player.nextCheckpointAngle = int.Parse(inputs[5]); // angle between your pod orientation and the direction of the next checkpoint
            inputs = Console.ReadLine().Split(' ');
            player.opponentX = int.Parse(inputs[0]);
            player.opponentY = int.Parse(inputs[1]);
            CheckPoint checkpoint = new CheckPoint(){
                x = player.nextCheckpointX,
                y = player.nextCheckpointY
            };

            if(!game.completedCheckpoints.Any(x => x.x == checkpoint.x && x.y == checkpoint.y))
            {
            game.completedCheckpoints.Add(checkpoint);
            }
            
            // if(game.completedCheckpoints.Last().x == player.nextCheckpointX && game.completedCheckpoints.Last().y == player.nextCheckpointY){
            //     player.lap++;
                
            // }

            foreach(var completedCheckpoints in game.completedCheckpoints)
            {
            Console.Error.WriteLine(completedCheckpoints);
            }

            List<double> distanceList = new List<double>();
            for(int i = 0; i < game.completedCheckpoints.Count-1; i++){

                //Console.Error.WriteLine("Dist{0} = {1} ",i+1,  game.CalculateDistanceBetweenCheckpoint(game.completedCheckpoints[i], game.completedCheckpoints[i+1]));
                distanceList.Add(game.CalculateDistanceBetweenCheckpoint(game.completedCheckpoints[i], game.completedCheckpoints[i+1]));
                
            }
            distanceList.Add(game.CalculateDistanceBetweenCheckpoint(game.completedCheckpoints[0], game.completedCheckpoints.Last()));
            foreach(var distance in distanceList){
                
                Console.Error.WriteLine("Dis bw check = {0}", distance);
                
            }
            Console.Error.WriteLine("MaxDist = {0}", distanceList.Max());

            Console.Error.WriteLine(checkpoint.x + " " + checkpoint.y);
            player.Log();
            player.Force();
            player.Move();
            
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // You have to output the target position
            // followed by the power (0 <= thrust <= 100)
            // i.e.: "x y thrust"
            //distance > boostRadius && boostAvailable && angle == 0 
        }
    }
}
