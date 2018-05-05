using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Exception_Aware_Event_Raise
{
    //Define event argument you want to send while raising event.
    public class MyEventArgs : EventArgs
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
            //Initialize MyEventArgs object with some random value
            MyEventArgs eventArgs = new MyEventArgs(33);

            //Create list of exception
            List<Exception> exceptions = new List<Exception>();

            //Invoke OnChange Action by iterating on all subscribers event handlers
            foreach (Delegate handler in OnChange.GetInvocationList())
            {
                try
                {
                    //pass sender object and eventArgs while
                    handler.DynamicInvoke(this, eventArgs);
                }
                catch (Exception e)
                {
                    //Add exception in exception list if occured any
                    exceptions.Add(e);
                }
            }

            //Check if any exception occured while 
            //invoking the subscribers event handlers
            if(exceptions.Any())
            {
                //Throw aggregate exception of all exceptions 
                //occured while invoking subscribers event handlers
                throw new AggregateException(exceptions);
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
            p.OnChange += (sender, e) => Console.WriteLine("Subscriber 1! Value:" + e.Value );
            //register for OnChange event - Subscriber 2
            p.OnChange += (sender, e) => { throw new Exception(); };
            //register for OnChange event - Subscriber 3
            p.OnChange += (sender, e) => Console.WriteLine("Subscriber 3! Value:" + e.Value );

            //raise the event
            p.Raise();

            //After this Raise() method is called
            //all subscribers callback methods will get invoked

            Console.WriteLine("Press enter to terminate!");
            Console.ReadLine();
        }
    }
}
