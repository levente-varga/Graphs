using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs_Framework
{
    public class Atomic<T>
    {
        bool LOG = false;

        private T item;
        private bool readWriteInProgress = false;

        public Atomic(T value) {
            item = value;
        }

        private class Result<K>
        {
            public K result;
            public bool success;

            public Result(K result, bool success)
            {
                this.result = result;
                this.success = success;
            }
        }

        public T Data
        {
            get { return Get(); }
            set { Set(value); }
        }

        public T Get()
        {
            if (LOG) Console.WriteLine("Atomic ___GET start");
            Result<T> result = new Result<T>(default(T), false);
            while (!result.success)
            {
                result = TryToGet();
            }
            return result.result;
        }

        public void Set(T newItem)
        {
            if (LOG) Console.WriteLine("Atomic SET___ start");
            bool success = false;
            while (!success)
            {
                success = TryToSet(newItem);
            }
        }

        private Result<T> TryToGet()
        {
            lock (this)
            {
                if (LOG) Console.WriteLine("Atomic ___GET in progress...");
                if (readWriteInProgress) return new Result<T>(default(T), false);
                readWriteInProgress = true;
            }

            Result<T> result = new Result<T>(item, true);

            lock (this)
            {
                if (LOG) Console.WriteLine("Atomic ___GET end");
                readWriteInProgress = false;
            }

            return result;
        }

        private bool TryToSet(T newItem)
        {
            lock (this)
            {
                if (LOG) Console.WriteLine("Atomic SET___ in progress...");
                if (readWriteInProgress) return false;
                readWriteInProgress = true;
            }

            item = newItem;

            lock (this)
            {
                if (LOG) Console.WriteLine("Atomic SET___ end");
                readWriteInProgress = false;
            }

            return true;
        }
    }
}
