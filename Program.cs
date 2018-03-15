using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stablegate stablegate = new Stablegate();
            Horses horses = new Horses();

            try
            {
                stablegate.OnGateOpening += horses.OnGateOpening;
                stablegate.GateOpen(horses);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally { stablegate.GateClose(); }
            Console.Read();
        }
    }
    class StablegateEventArgs : EventArgs
    {
        public int HorseCount { get; set; }
    }
    class Stablegate
    {
        public event EventHandler<StablegateEventArgs> OnGateOpening;

        protected virtual void OnGateOpened(Horses horses)
        {
            OnGateOpening?.Invoke(this, new StablegateEventArgs { HorseCount = horses.HorseList().Count });
        }
        public bool IsOpen = false;
        public bool GateOpen(Horses horses)
        {
            Console.WriteLine("Stable gate is Opening");
            Thread.Sleep(2000);
            Console.WriteLine("Stable gate is Opened");
            OnGateOpened(horses);

            return IsOpen = true;

        }
        
        public void GateClose()
        {
            Console.WriteLine("Stable gate is Closing");
            Thread.Sleep(2000);
            IsOpen = false;
            Console.WriteLine("Stable gate is Closed");
        }
    }
    class Horses
    {
        public String HorseName { get; set; }
        public int HorseID { get; set; }
        public string HorseBreed { get; set; }

        public List<Horses> HorseList()
        {
            List<Horses> horses = new List<Horses>()
        {
            new Horses{HorseID=101,HorseName="Seatle Slew", HorseBreed="ThoroughBred"},
            new Horses{HorseID=102,HorseName="Seabiscuit",HorseBreed="ThoroughBred"},
            new Horses{HorseID=103,HorseName="Trigger", HorseBreed="Grade Horse"},
            new Horses{HorseID=104,HorseName="Marengo", HorseBreed="Arabian - Equus caballus"},
            new Horses{HorseID=105,HorseName="Sea the Stars", HorseBreed="Irish ThoroughBred"},
            new Horses{HorseID=106,HorseName="Byerley Turk", HorseBreed="Arabian"},


        };
            return horses;
        }

        public void OnGateOpening(object sender, StablegateEventArgs e)
        {
            Console.WriteLine("There are {0} horses are in the Stable", e.HorseCount);
            Thread.Sleep(2000);
            Console.WriteLine("\n" + "Horses are leaving stable");


            List<Horses> exitlist = new List<Horses>();
            while (e.HorseCount > 0)
            {
                int a = new Random().Next(HorseList()[0].HorseID, HorseList()[5].HorseID + 1);
                int id = HorseList().FindIndex(s => s.HorseID == a);
                if (exitlist.Exists(d => d.HorseID == a)) continue;
                exitlist.Add(HorseList()[id]);
                Console.WriteLine("The Horse '{0}' bearing ID '{1}' is leaving the stable. The horse breed is {2}.", HorseList()[id].HorseName.ToString(), HorseList()[id].HorseID.ToString(),HorseList()[id].HorseBreed.ToString());
                e.HorseCount--;
                Console.WriteLine("Horses left in stable: {0}", e.HorseCount);
                Thread.Sleep(1500);
            }
            Console.WriteLine("\n" + "All Horses are outside");


        }
    }
}
