using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_EventHandler
{
    //Define event argument you want to send while raising event.
    public class MyEventArgs:EventArgs
    {
        public int Value { get; set; }

        public MyEventArgs(int value)
        {
            Value = value;
        }
    }


    //Define publisher class as Pub
    public class Pub
    {
        //OnChange property containing all the 
        //list of subscribers callback methods.

        //This is generic EventHandler delegate where 
        //we define the type of argument want to send 
        //while raising our event, MyEventArgs in our case.
        public event EventHandler<MyEventArgs> OnChange = delegate { };

        public void Raise()
        {
            //Invoke OnChange Action
            //Lets pass MyEventArgs object with some random value
            OnChange(this,new MyEventArgs(33));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Initialize pub class object
            Pub p = new Pub();

            //register for OnChange event - Subscriber 1
            p.OnChange += (sender,e) => Console.WriteLine("Subscriber 1! Value:" + e.Value);
            //register for OnChange event - Subscriber 2
            p.OnChange += (sender,e) => Console.WriteLine("Subscriber 2! Value:" + e.Value);

            //raise the event
            p.Raise();

            //After this Raise() method is called
            //all subscribers callback methods will get invoked

            Console.WriteLine("Press enter to terminate!");
            Console.ReadLine();
        }
    }
}
