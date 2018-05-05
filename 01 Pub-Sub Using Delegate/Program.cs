using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Pub_Sub_Using_Delegate
{
    
    //Define publisher class as Pub
    public class Pub
    {
        //OnChange property containing all the 
        //list of subscribers callback methods
        public Action OnChange { get; set; }

        public void Raise()
        {
            //Check if OnChange Action is set before invoking it
            if(OnChange!=null)
            {
                //Invoke OnChange Action
                OnChange();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Initialize pub class object
            Pub p = new Pub();

            //register for OnChange event - Subscriber 1
            p.OnChange += () => Console.WriteLine("Subscriber 1!");
            //register for OnChange event - Subscriber 2
            p.OnChange += () => Console.WriteLine("Subscriber 2!");

            //raise the event
            p.Raise();

            //After this Raise() method is called
            //all subscribers callback methods will get invoked

            Console.WriteLine("Press enter to terminate!");
            Console.ReadLine();
        }
    }
}
