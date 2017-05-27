
using System;
using System.Threading;

namespace Quobject.EngineIoClientDotNet.Thread
{
	public class EasyTimer
	{
		private Timer m_timer = null;
		private bool m_stop = false;

		public EasyTimer(Action method, int delayInMilliseconds, bool once = true)
		{
			if (once)
			{
				ThreadPool.QueueUserWorkItem(arg =>
				{
					System.Threading.Thread.Sleep(delayInMilliseconds);
					if (! m_stop)
					{
						DoWork(method);
					}
				});
			}
			else
			{
				m_timer = new Timer(new TimerCallback(DoWork), method, 0, delayInMilliseconds);
			}
		}

		static void DoWork(object obj)
		{
			Action method = (Action)obj;
			if (method != null) method();
		}

		public static EasyTimer SetTimeout(Action method, int delayInMilliseconds)
		{
			// Returns a stop handle which can be used for stopping
			// the timer, if required
			return new EasyTimer(method, delayInMilliseconds, true);
		}

		public static EasyTimer SetInterval(Action method, int delayInMilliseconds)
		{
			return new EasyTimer(method, delayInMilliseconds, false);
		}

		public void Stop()
		{
			//var log = LogManager.GetLogger(Global.CallerName());
			//log.Info("EasyTimer stop");
			if (m_timer != null) m_timer.Dispose();
			m_stop = true;
		}

		public static void TaskRun(Action action)
		{
			var resetEvent = new ManualResetEvent(false);
			ThreadPool.QueueUserWorkItem(arg => 
			    {
			        DoWork( action );
					resetEvent.Set();
			    });
			resetEvent.WaitOne();
		}

		public static void TaskRunNoWait(Action action)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(DoWork), action);
		}
	}


}
