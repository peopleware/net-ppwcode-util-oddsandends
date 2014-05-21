using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace PPWCode.Util.OddsAndEnds.II.Threading
{
    public class ProducerConsumerQueue : IDisposable
    {
        private readonly object m_Locker = new object();
        private readonly CancellationTokenSource m_QueueCancellationTokenSource = new CancellationTokenSource();

        private readonly BlockingCollection<WorkItem> m_WorkItems =
            new BlockingCollection<WorkItem>();

        private readonly Task[] m_Workers;
        private bool m_Disposed;

        public ProducerConsumerQueue(int workerCount, TaskCreationOptions taskCreationOptions = TaskCreationOptions.None)
        {
            workerCount = Math.Max(1, workerCount);

            m_Workers = new Task[workerCount];
            for (int i = 0; i < workerCount; i++)
            {
                m_Workers[i] = Task.Factory.StartNew(Consume, taskCreationOptions);
            }
        }

        public CancellationTokenSource QueueCancellationTokenSource
        {
            get { return m_QueueCancellationTokenSource; }
        }

        public Task Enqueue(Action action, CancellationTokenSource cancellationTokenSource = null)
        {
            if (m_Disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            lock (m_Locker)
            {
                TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
                m_WorkItems.Add(new WorkItem(tcs, action, cancellationTokenSource));
                return tcs.Task;
            }
        }

        private void Consume()
        {
            foreach (WorkItem workItem in m_WorkItems.GetConsumingEnumerable())
            {
                // bail out
                if (workItem.Action == null || QueueCancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }

                if (workItem.CancellationTokenSource != null
                    && workItem.CancellationTokenSource.IsCancellationRequested)
                {
                    workItem.TaskSource.SetCanceled();
                }
                else
                {
                    // do action an catch exception
                    try
                    {
                        workItem.Action();
                        workItem.TaskSource.SetResult(null);
                    }
                    catch (Exception e)
                    {
                        workItem.TaskSource.SetException(e);
                    }
                }
            }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            lock (m_Locker)
            {
                if (!m_Disposed)
                {
                    Dispose(TimeSpan.FromMilliseconds(Timeout.Infinite));
                    m_Disposed = true;
                }
            }
        }

        public void Dispose(TimeSpan? waitOnWorkersTimeout)
        {
            // ReSharper disable UnusedVariable
            foreach (Task t in m_Workers)
            {
                Enqueue(null);
            }
            // ReSharper restore UnusedVariable

            m_WorkItems.CompleteAdding();

            if (waitOnWorkersTimeout.HasValue)
            {
                Task.WaitAll(m_Workers, waitOnWorkersTimeout.Value);
            }
            QueueCancellationTokenSource.Cancel();
        }

        #endregion

        private class WorkItem
        {
            public readonly Action Action;
            public readonly CancellationTokenSource CancellationTokenSource;
            public readonly TaskCompletionSource<object> TaskSource;

            public WorkItem(TaskCompletionSource<object> taskSource, Action action, CancellationTokenSource cancellationTokenSource)
            {
                TaskSource = taskSource;
                Action = action;
                CancellationTokenSource = cancellationTokenSource;
            }
        }
    }
}