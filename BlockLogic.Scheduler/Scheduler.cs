using System;
using System.Collections.Generic;
using System.Text;
using BlockLogic.TrickleSystem;
using System.Threading.Tasks;
using System.Linq;
using System.Timers;

namespace BlockLogic.Scheduler
{
    public class Scheduler
    {
        public TrickleTasks trickle;

        public Scheduler()
        {
            trickle = new TrickleTasks();
        }

        public void BeginScheduler()
        {
            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 10000; // 10 sec
            aTimer.Enabled = true;

            Console.WriteLine("Press \'q\' to stop the scheduler");
            while (Console.Read() != 'q');
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            trickle.DailyTasks();

        }
    }
}
